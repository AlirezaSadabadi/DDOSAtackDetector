<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="DDoSAttackDetector.Approve" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="contentBackground">        
        <h3>تایید و یا تکذیب تشخیص حملات</h3>
        <hr />
        <div class="row">
            <asp:GridView ID="GridView1" runat="server" Width="90%" PageSize="15" HorizontalAlign="Center" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>

                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" CancelText="انصراف" DeleteText="حذف" EditText="ویرایش" SelectText="انتخاب" UpdateText="تایید" />
                    <asp:CheckBoxField DataField="ApprovalFlag" HeaderText="پرچم تایید" SortExpression="ApprovalFlag"></asp:CheckBoxField>
                    <asp:CheckBoxField DataField="LearningFlag" HeaderText="پرچم آموزش" SortExpression="LearningFlag"></asp:CheckBoxField>
                    <asp:BoundField DataField="Id" HeaderText="شناسه لاگ" SortExpression="Id" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="SymptomState" HeaderText="وضعیت علایم" SortExpression="SymptomState" />
                    <asp:BoundField DataField="PosteriorProb" HeaderText="احتمال" SortExpression="PosteriorProb" />
                    <asp:BoundField DataField="DateTimeOccurred" HeaderText="تاریخ و زمان" SortExpression="DateTimeOccurred" />
                    <asp:BoundField DataField="SymptomAndAttack_Id" HeaderText="شناسه حمله" SortExpression="SymptomAndAttack_Id"></asp:BoundField>
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DADConnectionString %>" DeleteCommand="DELETE FROM [AttackLogs] WHERE [Id] = @Id" InsertCommand="INSERT INTO [AttackLogs] ([SymptomState], [PosteriorProb], [ApprovalFlag], [DateTimeOccurred], [ts], [SymptomAndAttack_Id]) VALUES (@SymptomState, @PosteriorProb, @ApprovalFlag, @DateTimeOccurred, @ts, @SymptomAndAttack_Id)" SelectCommand="SELECT * FROM [AttackLogs] ORDER BY [DateTimeOccurred] DESC" UpdateCommand="UPDATE [AttackLogs] SET [SymptomState] = @SymptomState, [PosteriorProb] = @PosteriorProb, [ApprovalFlag] = @ApprovalFlag,[LearningFlag]=@LearningFlag, [DateTimeOccurred] = @DateTimeOccurred, [SymptomAndAttack_Id] = @SymptomAndAttack_Id WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="SymptomState" Type="String" />
                    <asp:Parameter Name="PosteriorProb" Type="Double" />
                    <asp:Parameter Name="ApprovalFlag" Type="Boolean" />
                    <asp:Parameter Name="DateTimeOccurred" Type="DateTime" />
                    <asp:Parameter Name="ts" Type="Object" />
                    <asp:Parameter Name="SymptomAndAttack_Id" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SymptomState" Type="String" />
                    <asp:Parameter Name="PosteriorProb" Type="Double" />
                    <asp:Parameter Name="ApprovalFlag" Type="Boolean" />
                    <asp:Parameter Name="LearningFlag" Type="Boolean" />
                    <asp:Parameter Name="DateTimeOccurred" Type="DateTime" />
                    <asp:Parameter Name="SymptomAndAttack_Id" Type="Int32" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            </div>
          </div>
</asp:Content>
