<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detector.aspx.cs" Inherits="DDoSAttackDetector.Detector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //نمایش ضریب همبستگی در صورت تجاوز از حد انتروپی جریان
            $("#ddEntropyVerify").change(function () {
                if ($("#ddEntropyVerify option:selected").text() == 'تجاوز از حد') {
                    $("#divCorrelation").fadeIn();
                    $("#ddCorrelation").val("2");
                }
                else if ($("#ddEntropyVerify option:selected").text() == 'معتبر') {
                    $("#divCorrelation").fadeOut();
                    $("#ddCorrelation").val("1");
                }
                else {
                    $("#divCorrelation").fadeOut();
                    $("#ddCorrelation").val("2");
                }
            });           

            if ($("#ddEntropyVerify option:selected").text() == 'تجاوز از حد') {
                $("#divCorrelation").fadeIn();
                $("#ddCorrelation").val("2");
            }
            else if ($("#ddEntropyVerify option:selected").text() == 'معتبر') {
                $("#divCorrelation").fadeOut();
                $("#ddCorrelation").val("1");
            }
            else {
                $("#divCorrelation").fadeOut();
                $("#ddCorrelation").val("2");
            }

            $("#ddIPFastFluxing").change(function () {
                if ($("#ddIPFastFluxing option:selected").text() == 'نامعتبر') {
                    $("#ddDNSFastFluxing").val("1");
                }
            });
            $("#ddDNSFastFluxing").change(function () {
                if ($("#ddDNSFastFluxing option:selected").text() == 'نامعتبر') {
                    $("#ddIPFastFluxing").val("1");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentBackground">
        <div class="buttonDiv">
            <asp:ImageButton ID="btnInference" runat="server" ImageUrl="~/Icons/report.png" Height="48" Width="48" ImageAlign="Middle" OnClick="btnInference_Click" ToolTip="استنتاج و ارایه گزارش" />
        </div>
        <h3>علامت ها</h3>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Label11" runat="server" Text="راهکارهای مبتنی برامضای باتنت ها :" AssociatedControlID="ddOnSign"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddOnSign" runat="server">
                    <asp:ListItem Text="معتبر" Value="1" />
                    <asp:ListItem Text="نامعتبر" Value="0" />
                    <asp:ListItem Text="نامشخص" Value="2" Selected="True" />
                </asp:DropDownList>
            </div>


        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Label7" runat="server" Text="شبکه های تغییر پی در پی آی پی :" AssociatedControlID="ddIPFastFluxing"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddIPFastFluxing" runat="server">
                    <asp:ListItem Text="معتبر" Value="1" />
                    <asp:ListItem Text="نامعتبر" Value="0" />
                    <asp:ListItem Text="نامشخص" Value="2" Selected="True" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Label8" runat="server" Text="شبکه های تغییر پی در پی دامنه :" AssociatedControlID="ddDNSFastFluxing"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddDNSFastFluxing" runat="server">
                    <asp:ListItem Text="معتبر" Value="1" />
                    <asp:ListItem Text="نامعتبر" Value="0" />
                    <asp:ListItem Text="نامشخص" Value="2" Selected="True" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Label9" runat="server" Text="پیروی کردن از قانون پرتو :" AssociatedControlID="ddPartoRule"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddPartoRule" runat="server">
                    <asp:ListItem Text="بله" Value="1" />
                    <asp:ListItem Text="خیر" Value="0" />
                    <asp:ListItem Text="نامشخص" Value="2" Selected="True" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Label10" runat="server" Text="پیروی کردن از توزیع زیپف :" AssociatedControlID="ddZipfDistribution"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddZipfDistribution" runat="server">
                    <asp:ListItem Text="بله" Value="1" />
                    <asp:ListItem Text="خیر" Value="0" />
                    <asp:ListItem Text="نامشخص" Value="2" Selected="True" />
                </asp:DropDownList>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Label5" runat="server" Text="بررسی انتروپی جریان :" AssociatedControlID="ddEntropyVerify"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddEntropyVerify" runat="server">
                    <asp:ListItem Text="معتبر" Value="1" />
                    <asp:ListItem Text="تجاوز از حد" Value="0" />
                    <asp:ListItem Text="نامشخص" Value="2" Selected="True" />
                </asp:DropDownList>
            </div>
            <div id="divCorrelation">
                <div class="col-md-3">
                    <asp:Label ID="Label6" runat="server" Text="ضریب همبستگی جریان :" AssociatedControlID="ddCorrelation"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddCorrelation" runat="server">
                        <asp:ListItem Text="معتبر" Value="1" />
                        <asp:ListItem Text="نامعتبر" Value="0" />
                        <asp:ListItem Text="نامشخص" Value="2" Selected="True" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="panelReport" runat="server" Visible="false">
        <div class="contentBackground">
            <h3>گزارش حمله</h3>
            <hr />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblResult1" runat="server" Text="احتمال حمله :" ForeColor="Red" Font-Size="X-Large"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblResult2" runat="server" Text="" ForeColor="Red" Font-Size="X-Large"></asp:Label>
                </div>
            </div>
            <hr />
            <h3><span style="color: mediumpurple;">تاریخچه تشخیص حمله با علایم مشابه</span></h3>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblAttackCount1" runat="server" Text="تعداد حمله تشخیص داده شده :"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAttackCount2" runat="server" Text=""></asp:Label>
                </div>
                </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblApproveCount1" runat="server" Text="تعداد تشخیص صحیح :"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblApproveCount2" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblRevokeCount1" runat="server" Text="تعداد تشخیص اشتباه :"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblRevokeCount2" runat="server" Text=""></asp:Label>
                </div>
                </div>
            <div class="row">
                 <div class="col-md-3">
                    <asp:Label ID="lblMaxProb1" runat="server" Text="بیشینه احتمال درست تشخیص داده شده :"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblMaxProb2" runat="server" Text="ندارد"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblMinProb1" runat="server" Text="کمینه احتمال درست تشخیص داده شده :"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblMinProb2" runat="server" Text="ندارد"></asp:Label>
                </div>
                </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvReport" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" GridLines="Horizontal" AutoGenerateColumns="False" PageSize="100" EmptyDataText="برای اولین بار است که این حمله با این علایم رخ می دهد" Font-Size="Larger" HorizontalAlign="Center" CellPadding="100" Width="80%">
                        <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>

                        <Columns>
                            <asp:BoundField DataField="DateTimeOccurred" HeaderText="تاریخ و زمان"></asp:BoundField>
                            <asp:BoundField DataField="PosteriorProb" HeaderText="احتمال حمله"></asp:BoundField>
                            <asp:CheckBoxField DataField="ApprovalFlag" HeaderText="پرچم تایید"></asp:CheckBoxField>

                        </Columns>

                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C"></FooterStyle>

                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7"></HeaderStyle>

                        <PagerStyle HorizontalAlign="Right" BackColor="#E7E7FF" ForeColor="#4A3C8C"></PagerStyle>

                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C"></RowStyle>

                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"></SelectedRowStyle>

                        <SortedAscendingCellStyle BackColor="#F4F4FD"></SortedAscendingCellStyle>

                        <SortedAscendingHeaderStyle BackColor="#5A4C9D"></SortedAscendingHeaderStyle>

                        <SortedDescendingCellStyle BackColor="#D8D8F0"></SortedDescendingCellStyle>

                        <SortedDescendingHeaderStyle BackColor="#3E3277"></SortedDescendingHeaderStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
