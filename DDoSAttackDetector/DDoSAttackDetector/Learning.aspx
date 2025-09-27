<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Learning.aspx.cs" Inherits="DDoSAttackDetector.Learning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="contentBackground">        
        <div class="buttonDiv">
            <asp:ImageButton ID="btnLearn" runat="server" ImageUrl="~/Icons/Learn.png" Height="48" Width="48" ImageAlign="Middle" OnClick="btnLearn_Click" ToolTip="آموزش سیستم" />
        </div>
         <h3>آموزش سیستم</h3>
        <hr />
         <asp:Panel ID="panelReport" runat="server" Visible="false">
         <h4>جدول احتمالات پیشین</h4>
        <div class="row">
            <div class="col-md-6">

                <asp:GridView ID="gvBeforeLearning" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" HorizontalAlign="Center" Width="60%" Caption="قبل از آموزش">
                    <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="عنوان"></asp:BoundField>
                        <asp:BoundField DataField="PriorProb" HeaderText="احتمال"></asp:BoundField>
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
            <div class="col-md-6">
                            <asp:GridView ID="gvAfterLearning" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" HorizontalAlign="Center" Width="60%" Caption="بعد از آموزش">
                    <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="عنوان"></asp:BoundField>
                        <asp:BoundField DataField="PriorProb" HeaderText="احتمال"></asp:BoundField>
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
         <hr />
         <h4>جدول احتمالات شرطی</h4>
         <div class="row">
            <div class="col-md-6">

                <asp:GridView ID="gvCPTBeforeLearning" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" HorizontalAlign="Center" Width="60%" Caption="قبل از آموزش">
                    <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="SymptomAndAttack.Title" HeaderText="شناسه علامت"></asp:BoundField>
                        <asp:BoundField DataField="State" HeaderText="حالت"></asp:BoundField>
                        <asp:BoundField DataField="Prob" HeaderText="احتمال"></asp:BoundField>
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
            <div class="col-md-6">
                          <asp:GridView ID="gvCPTAfterLearning" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" HorizontalAlign="Center" Width="60%" Caption="بعد از آموزش">
                    <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="SymptomAndAttack.Title" HeaderText="شناسه علامت"></asp:BoundField>
                        <asp:BoundField DataField="State" HeaderText="حالت"></asp:BoundField>
                        <asp:BoundField DataField="Prob" HeaderText="احتمال"></asp:BoundField>
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
             </asp:Panel>
</asp:Content>
