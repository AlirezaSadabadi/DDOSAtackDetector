<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PPT.aspx.cs" Inherits="DDoSAttackDetector.PPT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="contentBackground">
        <h3>ویرایش جدول احتمالات پیشین</h3>
        <hr />
        <div class="row">
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Width="60%" HorizontalAlign="Center" PageSize="20" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
                <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>

                <Columns>
                    <asp:CommandField ShowEditButton="True" CancelText="انصراف" EditText="ویرایش" UpdateText="به روزرسانی"></asp:CommandField>
                    <asp:BoundField DataField="Id" HeaderText="شناسه" ReadOnly="True" InsertVisible="False" SortExpression="Id"></asp:BoundField>
                    <asp:BoundField DataField="Title" HeaderText="عنوان" SortExpression="Title"></asp:BoundField>
                    <asp:BoundField DataField="PriorProb" HeaderText="احتمال" SortExpression="PriorProb"></asp:BoundField>
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
            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DADConnectionString %>' DeleteCommand="DELETE FROM [SymptomAndAttacks] WHERE [Id] = @Id" InsertCommand="INSERT INTO [SymptomAndAttacks] ([Title], [PriorProb], [SorA], [UI], [Sorter], [ts]) VALUES (@Title, @PriorProb, @SorA, @UI, @Sorter, @ts)" SelectCommand="SELECT * FROM [SymptomAndAttacks] WHERE ([UI] = @UI) ORDER BY [Sorter]" UpdateCommand="UPDATE [SymptomAndAttacks] SET [Title] = @Title, [PriorProb] = @PriorProb WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Title" Type="String"></asp:Parameter>
                    <asp:Parameter Name="PriorProb" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="SorA" Type="String"></asp:Parameter>
                    <asp:Parameter Name="UI" Type="Boolean"></asp:Parameter>
                    <asp:Parameter Name="Sorter" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="ts" Type="Object"></asp:Parameter>
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter DefaultValue="True" Name="UI" Type="Boolean"></asp:Parameter>
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Title" Type="String"></asp:Parameter>
                    <asp:Parameter Name="PriorProb" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
