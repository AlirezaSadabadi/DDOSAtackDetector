<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CPT.aspx.cs" Inherits="DDoSAttackDetector.CPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentBackground">
        <h3>ویرایش جدول احتمالات شرطی</h3>
        <hr />
        <div class="row">
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" Width="60%" HorizontalAlign="Center" PageSize="20">
                <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>

                <Columns>
                    <asp:CommandField ShowEditButton="True" CancelText="انصراف" DeleteText="حذف" EditText="ویرایش" UpdateText="به روزرسانی"></asp:CommandField>
                    <asp:BoundField DataField="SymptomAndAttack_Id" HeaderText="شناسه علامت و یا حمله" SortExpression="SymptomAndAttack_Id"></asp:BoundField>
                    <asp:BoundField DataField="State" HeaderText="وضعیت" SortExpression="State"></asp:BoundField>
                    <asp:BoundField DataField="Prob" HeaderText="احتمال" SortExpression="Prob"></asp:BoundField>
                    <asp:BoundField DataField="Id" HeaderText="شناسه" SortExpression="Id" InsertVisible="False" ReadOnly="True"></asp:BoundField>
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
            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DADConnectionString %>' DeleteCommand="DELETE FROM [CPTs] WHERE [Id] = @Id" InsertCommand="INSERT INTO [CPTs] ([State], [Prob], [ts], [SymptomAndAttack_Id]) VALUES (@State, @Prob, @ts, @SymptomAndAttack_Id)" SelectCommand="SELECT * FROM [CPTs] ORDER BY [SymptomAndAttack_Id], [State]" UpdateCommand="UPDATE [CPTs] SET [State] = @State, [Prob] = @Prob, [SymptomAndAttack_Id] = @SymptomAndAttack_Id WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="State" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="Prob" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="ts" Type="Object"></asp:Parameter>
                    <asp:Parameter Name="SymptomAndAttack_Id" Type="Int32"></asp:Parameter>
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="State" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="Prob" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="SymptomAndAttack_Id" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
