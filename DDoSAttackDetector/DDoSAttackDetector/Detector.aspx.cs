using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MicrosoftResearch.Infer.Models;
using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Distributions;
using Models;

namespace DDoSAttackDetector
{
    public partial class Detector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInference_Click(object sender, ImageClickEventArgs e)
        {
            string symptomState = string.Empty;
            double posteriorProbForOldFloodingAttack = 0,
                posteriorProb = 0,
                coefficient=0;
            int approveCount = 0,
                revokeCount = 0;
            DatabaseContext db = null;
            lblMaxProb2.Text = lblMinProb2.Text = "ندارد";
            try
            {
                db = new DatabaseContext();

                coefficient = Convert.ToDouble(db.Facts.Where(z => z.Title == "oldAttackLearningCoefficient").Select(z => z.Value).FirstOrDefault());
                symptomState = ddOnSign.SelectedValue +
                               ddIPFastFluxing.SelectedValue + ddDNSFastFluxing.SelectedValue +
                               ddPartoRule.SelectedValue + ddZipfDistribution.SelectedValue +
                               ddEntropyVerify.SelectedValue + ddCorrelation.SelectedValue;

                var oSAndA = db.SymptomAndAttacks.ToList();

                //احتمال رخ دادن هر علامت حمله (علایم اولیه) در صورت نامشخص بودن پنجاه درصد می باشد           
                //که البته قابل به روز رسانی می باشد
                Variable<bool> IPFFN = Variable.Bernoulli(oSAndA.Where(z => z.Title == "IPFFN").Select(z => z.PriorProb).FirstOrDefault());
                Variable<bool> DNSFFN = Variable.Bernoulli(oSAndA.Where(z => z.Title == "DNSFFN").Select(z => z.PriorProb).FirstOrDefault());
                Variable<bool> onSign = Variable.Bernoulli(oSAndA.Where(z => z.Title == "OnSign").Select(z => z.PriorProb).FirstOrDefault());
                Variable<bool> noParto = Variable.Bernoulli(oSAndA.Where(z => z.Title == "NoParto").Select(z => z.PriorProb).FirstOrDefault());
                Variable<bool> noZipf = Variable.Bernoulli(oSAndA.Where(z => z.Title == "NoZipf").Select(z => z.PriorProb).FirstOrDefault());
                Variable<bool> flowEntropy = Variable.Bernoulli(oSAndA.Where(z => z.Title == "FlowEntropy").Select(z => z.PriorProb).FirstOrDefault());


                //احتمال حملات قبلی بایستی از روی لاگ حمله ها محاسبه گردد
                var oCaseAL = db.AttackLogs.Where(z => z.SymptomAndAttack == db.SymptomAndAttacks.Where(x => x.Title == "FloodingAttack").FirstOrDefault() && z.SymptomState == symptomState).AsQueryable();

                //درصورتی که بخواهیم نتایج را با نتیکا مقایسه کنیم بایستی از اینجا تا الس را کامنت کنیم
                if (oCaseAL != null)
                {
                    approveCount = oCaseAL.Where(z => z.ApprovalFlag == true).Count();
                    revokeCount = oCaseAL.Where(z => z.ApprovalFlag == false).Count();
                    //احتمال آخرین مقدار تایید شده از جانب متخصص در صورت وجود در نظر گرفته می شود
                    //اگر مقدار تایید شده ای برای یک حمله با علایم خاص وجود نداشته باشد، مقدار پیش فرض
                    //در نظر گرفته می شود
                    posteriorProbForOldFloodingAttack = approveCount > 0 ? oCaseAL.Where(z => z.ApprovalFlag == true).OrderByDescending(z => z.Id).Select(z => z.PosteriorProb).FirstOrDefault() : Convert.ToDouble(oSAndA.Where(z => z.Title == "OldAttack").Select(z => z.PriorProb).First());
                    //تعداد حملات تایید شده از تعداد حملات تایید نشده کسر شده و در یک درصد ضرب می شود
                    //سپس حاصل به یا از مقدار قبلی اضافه یا کم می گردد
                    posteriorProbForOldFloodingAttack = (approveCount - revokeCount) * coefficient + posteriorProbForOldFloodingAttack;
                    //در صورتی که احتمال حملات قبلی اینقدر تایید شده باشد که به صددرصد برسد
                    //دیگر نباید بیشتر از صد در صد شود
                    posteriorProbForOldFloodingAttack = posteriorProbForOldFloodingAttack > 1 ? 1 : posteriorProbForOldFloodingAttack;
                    //در صورتی که احتمال حملات قبلی اینقدر تکذیب شده باشد که 
                    //احتمال آن منفی شده باشد بایستی احتمال آن صفر در نظر گرفته شود
                    //چون احتمال منفی نداریم
                    posteriorProbForOldFloodingAttack = posteriorProbForOldFloodingAttack < 0 ? 0 : posteriorProbForOldFloodingAttack;
                }
                else
                    posteriorProbForOldFloodingAttack = Convert.ToDouble(oSAndA.Where(z => z.Title == "OldAttack").Select(z => z.PriorProb).First());

                Variable<bool> oldFloodingAttack = Variable.Bernoulli(posteriorProbForOldFloodingAttack);

                //علایم وابسته یا علایم میانی
                Variable<bool> correlationCoefficient = Variable.New<bool>();
                Variable<bool> botNet = Variable.New<bool>();

                //مگر اینکه آن علامت واقعا رخ دهد که در آن صورت
                //مقدار آن را صد درصد در نظر می گیریم و یا مطمئن باشیم
                //که رخ نداده است که در آن صورت مقدار آن را صفر درصد
                //درنظر می گیریم
                if (ddIPFastFluxing.SelectedValue == "0")
                {
                    //فقط یکی از علامت های زیر می توانند در آن واحد صحیح باشند
                    IPFFN.ObservedValue = true;
                    DNSFFN.ObservedValue = false;
                }
                else if (ddIPFastFluxing.SelectedValue == "1")
                {
                    IPFFN.ObservedValue = false;
                }
                if (ddDNSFastFluxing.SelectedValue == "0")
                {
                    //فقط یکی از علامت های زیر می توانند در آن واحد صحیح باشند
                    IPFFN.ObservedValue = false;
                    DNSFFN.ObservedValue = true;
                }
                else if (ddDNSFastFluxing.SelectedValue == "1")
                {
                    DNSFFN.ObservedValue = false;
                }
                if (ddOnSign.SelectedValue == "0")
                {
                    onSign.ObservedValue = true;
                }
                else if (ddOnSign.SelectedValue == "1")
                {
                    onSign.ObservedValue = false;
                }
                if (ddPartoRule.SelectedValue == "0")
                {
                    noParto.ObservedValue = true;
                }
                else if (ddPartoRule.SelectedValue == "1")
                {
                    noParto.ObservedValue = false;
                }
                if (ddZipfDistribution.SelectedValue == "0")
                {
                    noZipf.ObservedValue = true;
                }
                else if (ddZipfDistribution.SelectedValue == "1")
                {
                    noZipf.ObservedValue = false;
                }
                if (ddEntropyVerify.SelectedValue == "0")
                {
                    flowEntropy.ObservedValue = true;
                }
                else if (ddEntropyVerify.SelectedValue == "1")
                {
                    flowEntropy.ObservedValue = false;
                    ddCorrelation.SelectedValue = "1";
                }
                else if (ddEntropyVerify.SelectedValue == "2")
                {
                    ddCorrelation.SelectedValue = "2";
                }
                //در صورتی که مقدار این دراپ داون دو باشد باید جدول توزیع احتمال
                //تشکیل گردد که جلوتر آمده است        
                if (ddCorrelation.SelectedValue == "0")
                {
                    correlationCoefficient.ObservedValue = true;
                }
                else if (ddCorrelation.SelectedValue == "1")
                {
                    correlationCoefficient.ObservedValue = false;
                }

                //تعریف متغیر برای احتمال وقوع حملات
                Variable<bool> floodingAttack = Variable.New<bool>();


                //ایجاد جدول احتمالات شرطی از روی ارتباط بین علایم
                //و همچنین ارتباط بین علایم و حملات
                var oCPT = db.CPTs.AsQueryable();
                //Correlation Coefficient CPT
                //در صورتی که مقدار ضریب همبستگی از طریق دراپ داون نامعتبر و یا معتبر شد
                //دیگر جدول احتمال برای ضریب همبستگی معنا ندارد
                //تنها در صورتی جدول احتمال معنا پیدا می کند که ضریب همبستگی نامشخص باشد
                if (ddCorrelation.SelectedValue == "2")
                {
                    var oCorrelationCoefficeintState = oCPT.Where(z => z.SymptomAndAttack == db.SymptomAndAttacks.Where(x => x.Title == "CorrelationCoefficient").FirstOrDefault()).OrderBy(z => z.State).Select(z => z.Prob).ToList();
                    using (Variable.If(flowEntropy)) correlationCoefficient.SetTo(Variable.Bernoulli(oCorrelationCoefficeintState[1]));
                    using (Variable.IfNot(flowEntropy)) correlationCoefficient.SetTo(Variable.Bernoulli(oCorrelationCoefficeintState[0]));
                }

                //BotNet CPT   
                var oBotNetState = oCPT.Where(z => z.SymptomAndAttack == db.SymptomAndAttacks.Where(x => x.Title == "BotNet").FirstOrDefault()).OrderBy(z => z.State).Select(z => z.Prob).ToList();
                using (Variable.If(onSign))
                {
                    using (Variable.If(IPFFN))
                    {
                        using (Variable.If(DNSFFN))
                        {
                            //این قسمت با توجه به شرط ابتدایی هیج وقت اتفاق نمی افتد
                            //اما باید حتما بگذاریم چون کامپایلر اینفردات نت ایراد میگیرد
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[31]));//1
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[30]));//0.85
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[29]));//0.95
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[28]));//0.8
                                }
                            }
                        }
                        using (Variable.IfNot(DNSFFN))
                        {
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[27]));//1
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[26]));//0.85
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[25]));//0.95
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[24]));//0.8
                                }
                            }
                        }
                    }
                    using (Variable.IfNot(IPFFN))
                    {
                        using (Variable.If(DNSFFN))
                        {
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[23]));//1
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[22]));//0.85
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[21]));//0.9
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[20]));//0.8
                                }
                            }
                        }
                        using (Variable.IfNot(DNSFFN))
                        {
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[19]));//0.6
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[18]));//0.45
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[17]));//0.5
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[16]));//0.4
                                }
                            }
                        }
                    }
                }
                using (Variable.IfNot(onSign))
                {
                    using (Variable.If(IPFFN))
                    {
                        //این قسمت با توجه به شرط ابتدایی هیج وقت اتفاق نمی افتد
                        //اما باید حتما بگذاریم چون کامپایلر اینفردات نت ایراد میگیرد
                        using (Variable.If(DNSFFN))
                        {
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[15]));//0.6
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[14]));//0.45
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[13]));//0.5
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[12]));//0.4
                                }
                            }
                        }
                        using (Variable.IfNot(DNSFFN))
                        {
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[11]));//0.6
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[10]));//0.45
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[9]));//0.5
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[8]));//0.4
                                }
                            }
                        }
                    }
                    using (Variable.IfNot(IPFFN))
                    {
                        using (Variable.If(DNSFFN))
                        {
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[7]));//0.6
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[6]));//0.45
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[5]));//0.55
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[4]));//0.4
                                }
                            }
                        }
                        using (Variable.IfNot(DNSFFN))
                        {
                            using (Variable.If(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[3]));//0.2
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[2]));//0.05
                                }
                            }
                            using (Variable.IfNot(noParto))
                            {
                                using (Variable.If(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[1]));//0.15
                                }
                                using (Variable.IfNot(noZipf))
                                {
                                    botNet.SetTo(Variable.Bernoulli(oBotNetState[0]));//0
                                }
                            }
                        }
                    }
                }

                //Flooding Attack CPT
                var oFloodingAttackState = oCPT.Where(z => z.SymptomAndAttack == db.SymptomAndAttacks.Where(x => x.Title == "FloodingAttack").FirstOrDefault()).OrderBy(z => z.State).Select(z => z.Prob).ToList();
                using (Variable.If(flowEntropy))
                {
                    using (Variable.If(correlationCoefficient))
                    {
                        using (Variable.If(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[15]));//1
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[14]));//0.95
                            }
                        }
                        using (Variable.IfNot(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[13]));//0.9
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[12]));//0.85
                            }
                        }
                    }
                    using (Variable.IfNot(correlationCoefficient))
                    {
                        using (Variable.If(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[11]));//0.9
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[10]));//0.8
                            }
                        }
                        using (Variable.IfNot(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[9]));//0.6
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[8]));//0.5
                            }
                        }
                    }
                }
                using (Variable.IfNot(flowEntropy))
                {
                    using (Variable.If(correlationCoefficient))
                    {
                        //این قسمت با توجه به شرط ابتدایی هیج وقت اتفاق نمی افتد
                        //اما باید حتما بگذاریم چون کامپایلر اینفردات نت ایراد میگیرد
                        using (Variable.If(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[7]));//0.6
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[6]));//0.5
                            }
                        }
                        using (Variable.IfNot(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[5]));//0.1
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[4]));//0
                            }
                        }
                    }
                    using (Variable.IfNot(correlationCoefficient))
                    {
                        using (Variable.If(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[3]));//0.6
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[2]));//0.5
                            }
                        }
                        using (Variable.IfNot(oldFloodingAttack))
                        {
                            using (Variable.If(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[1]));//0.1
                            }
                            using (Variable.IfNot(botNet))
                            {
                                floodingAttack.SetTo(Variable.Bernoulli(oFloodingAttackState[0]));//0
                            }
                        }
                    }
                }

                //استنتاج و تولید احتمال پسین
                InferenceEngine iE = new InferenceEngine(new VariationalMessagePassing());
                string result = iE.Infer(floodingAttack).ToString();
                //lblResult.Text = string.Format("Flooding Attack : {0} - Correlation Coefficient : {1} - BotNet : {2}", iE.Infer(floodingAttack).ToString(), iE.Infer(correlationCoefficient).ToString(), iE.Infer(IPFFN).ToString());
                //استخراج مقدار عددی احتمال از متغیر برنولی            
                posteriorProb = Convert.ToDouble(result.Substring(result.IndexOf('(') + 1, result.IndexOf(')') - 10));

                //دریافت تاریخچه حمله با علایم مشابه
                panelReport.Visible = true;
                var historyOfAttack = oCaseAL.OrderByDescending(z => z.DateTimeOccurred);

                //محاسبه آمار حمله
                lblResult2.Text = posteriorProb.ToString();
                lblAttackCount2.Text = (approveCount + revokeCount).ToString();
                lblApproveCount2.Text = approveCount.ToString();
                lblRevokeCount2.Text = revokeCount.ToString();
                var oApprovedDetect = oCaseAL.Where(z => z.ApprovalFlag == true);
                if (oApprovedDetect.FirstOrDefault() != null)
                {
                    lblMaxProb2.Text = oApprovedDetect.Max(z => z.PosteriorProb).ToString();
                    lblMinProb2.Text = oApprovedDetect.Min(z => z.PosteriorProb).ToString();
                }

                gvReport.DataSource = historyOfAttack.ToList();
                gvReport.DataMember = "AttackLogs";
                gvReport.DataBind();
                //ثبت حمله
                AttackLog oAL = new AttackLog()
                {
                    SymptomAndAttack = db.SymptomAndAttacks.Where(z => z.Title == "FloodingAttack").FirstOrDefault(),
                    SymptomState = symptomState,
                    PosteriorProb = posteriorProb,
                    ApprovalFlag = false,
                    LearningFlag = false
                };
                db.AttackLogs.Add(oAL);
                db.SaveChanges();

                string msg = "احتمال وقوع حمله با موفقیت محاسبه گردید";
                string script = "bootbox.alert('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "infoMessage", script, true);
            }
            catch(Exception ex)
            {
                string script = "bootbox.alert('خطایی در تشخیص حمله رخ داده است');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "infoMessage", script, true);
            }
            finally
            {
                if (db!=null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
    }
}