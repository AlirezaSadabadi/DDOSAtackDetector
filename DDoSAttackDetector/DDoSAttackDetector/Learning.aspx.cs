using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Transactions;

namespace DDoSAttackDetector
{
    public partial class Learning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLearn_Click(object sender, ImageClickEventArgs e)
        {
            DatabaseContext db = null;
            int approveCount = 0,
                revokeCount = 0,
                i = 0;
            double newProb = 0,
                priorProbLearningCoefficient = 0,
                cptProbLearningCoefficeint=0;
            gvAfterLearning.DataSource = gvBeforeLearning.DataSource = null;
            gvBeforeLearning.DataBind();
            gvAfterLearning.DataBind();
            gvCPTAfterLearning.DataSource = gvCPTBeforeLearning.DataSource = null;
            gvCPTBeforeLearning.DataBind();
            gvCPTAfterLearning.DataBind();
            try
            {
                //تمامی مراحل آموزش سیستم بایستی به صورت اتمیک
                //انجام پذیرد یعنی یا تمام مراحل آموزش اعم از جدول
                //احتمالات پیشین و جدول احتمالات شرطی باهم آموزش
                //می بینند یا در صورت بروز خطا هیچکدام آموزش نمیبینند
                using (TransactionScope scope1 = new TransactionScope())
                {
                    db = new DatabaseContext();
                    var oAL = db.AttackLogs.Where(z => z.SymptomAndAttack == db.SymptomAndAttacks.Where(x => x.Title == "FloodingAttack").FirstOrDefault() && z.LearningFlag == false).AsQueryable();
                    if (oAL.FirstOrDefault() != null)
                    {
                        panelReport.Visible = true;
                        //لیست از طریق ستون سورتر به ترتیب مطلوب خوانده می شود
                        //اندیس آی هم از صفر شروع کرده و تک تک عناصر موجود در
                        //وضعیت علامت ها در لاگ را به ترتیب بررسی می کند
                        priorProbLearningCoefficient = Convert.ToDouble(db.Facts.Where(z => z.Title == "priorProbLearningCoefficient").Select(z => z.Value).FirstOrDefault());
                        //آموزش و به روزرسانی جدول احتمالات پیشین
                        var oSymptomWithUI_true = db.SymptomAndAttacks.Where(z => z.UI == true).OrderBy(z => z.Sorter).ToList();
                        gvBeforeLearning.DataSource = oSymptomWithUI_true;
                        gvBeforeLearning.DataMember = "SymptomAndAttacks";
                        gvBeforeLearning.DataBind();
                        foreach (var item in oSymptomWithUI_true)
                        {
                            //فقط باید در صورتی که علامت حمله مشخص باشد آن را برای آموزش استفاده کرد
                            if (oAL.Where(z => z.SymptomState.Substring(i, 1) != "2").Any())
                            {
                                var oAL0 = oAL.Where(z => z.SymptomState.Substring(i, 1) == "0");
                                var oAL1 = oAL.Where(z => z.SymptomState.Substring(i, 1) == "1");
                                if (oAL0.FirstOrDefault() != null)
                                {
                                    approveCount = oAL0.Where(z => z.ApprovalFlag == true).Count();
                                    revokeCount = oAL0.Where(z => z.ApprovalFlag == false).Count();
                                }
                                if (oAL1.FirstOrDefault() != null)
                                {
                                    approveCount += oAL1.Where(z => z.ApprovalFlag == false).Count();
                                    revokeCount += oAL1.Where(z => z.ApprovalFlag == true).Count();
                                }
                                newProb = (approveCount - revokeCount) * priorProbLearningCoefficient + Convert.ToDouble(item.PriorProb);
                                if (newProb > 1)
                                {
                                    newProb = 1;
                                }
                                else if (newProb < 0)
                                {
                                    newProb = 0;
                                }
                                item.PriorProb = newProb;

                                //آماده سازی برای بررسی علامت بعدی
                                approveCount = revokeCount = 0;
                                newProb = 0;
                            }
                            i++;
                        }
                        gvAfterLearning.DataSource = oSymptomWithUI_true;
                        gvAfterLearning.DataMember = "SymptomAndAttacks";
                        gvAfterLearning.DataBind();

                        //آموزش و به روزرسانی جدول احتمالات شرطی
                        var oCPT = db.CPTs.Where(z => z.SymptomAndAttack == db.SymptomAndAttacks.Where(x => x.Title == "BotNet").FirstOrDefault()).OrderByDescending(z => z.State).ToList();
                        cptProbLearningCoefficeint = Convert.ToDouble(db.Facts.Where(z => z.Title == "cptProbLearningCoefficeint").Select(z => z.Value).FirstOrDefault());
                        gvCPTBeforeLearning.DataSource = oCPT;
                        gvCPTBeforeLearning.DataMember = "CPTs";
                        gvCPTBeforeLearning.DataBind();
                        foreach (var item in oCPT)
                        {
                            approveCount = revokeCount = 0;
                            newProb = 0;
                            string stateBinaryFormat = Convert.ToString(item.State, 2);
                            //درصورتی که کد باینری تولید شده دارای کمتر از پنج رقم
                            //باشد مانند عدد هشت که می شود 1000 باید حتما پنج رقمی 
                            //شود یعنی به این شکل در آید 01000
                            while (stateBinaryFormat.Length < 5)
                            {
                                stateBinaryFormat = "0" + stateBinaryFormat;
                            }
                            //منطق دراپ داون های رابط کاربری با منطق جدول احتمالات شرطی
                            //فرق می کند بنابراین بایستی سازگاری صورت پذیرد
                            stateBinaryFormat = stateBinaryFormat.Replace("1", "2");
                            stateBinaryFormat = stateBinaryFormat.Replace("0", "1");
                            stateBinaryFormat = stateBinaryFormat.Replace("2", "0");

                            var oALSpecifiecState = oAL.Where(z => z.SymptomState.Substring(0, 5) == stateBinaryFormat);
                            if (oALSpecifiecState.Any())
                            {

                                approveCount = oALSpecifiecState.Where(z => z.ApprovalFlag == true).Count();
                                revokeCount = oALSpecifiecState.Where(z => z.ApprovalFlag == false).Count();

                                newProb = (approveCount - revokeCount) * cptProbLearningCoefficeint + Convert.ToDouble(item.Prob);
                                if (newProb > 1)
                                {
                                    newProb = 1;
                                }
                                else if (newProb < 0)
                                {
                                    newProb = 0;
                                }
                                item.Prob = newProb;
                            }
                        }

                        gvCPTAfterLearning.DataSource = oCPT;
                        gvCPTAfterLearning.DataMember = "CPTs";
                        gvCPTAfterLearning.DataBind();

                        string msg = "آموزش سیستم با موفقیت به پایان رسید.";
                        string script = "bootbox.alert('" + msg + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "infoMessage", script, true);
                    }
                    else
                    {
                        string msg = "رکوردی جهت آموزش سیستم موجود نمی باشد ویا رکوردهای موجود به سیستم آموزش داده شده اند";
                        string script = "bootbox.alert('" + msg + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "infoMessage", script, true);
                    }
                    //لاگ هایی که در آموزش دادن سیستم سهیم شده اند
                    // نباید مجددا برای آموزش استفاده شوند
                    foreach (var item in oAL)
                    {
                        item.LearningFlag = true;
                    }

                    db.SaveChanges();
                    scope1.Complete();
                }
            }
            catch (Exception ex)
            {
                string script = "bootbox.alert('خطایی در آموزش سیستم رخ داده است');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "infoMessage", script, true);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
    }
}