<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fact.aspx.cs" Inherits="DDoSAttackDetector.Fact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentBackground">
        <h3>ویرایش حقایق</h3>
        <hr />
        <div class="row">
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" Width="80%" HorizontalAlign="Center" PageSize="20" AllowPaging="True" AllowSorting="True">
        <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>

        <Columns>
            <asp:CommandField ShowEditButton="True" CancelText="انصراف" EditText="ویرایش" UpdateText="به روزرسانی"></asp:CommandField>
            <asp:BoundField DataField="Id" HeaderText="شناسه" ReadOnly="True" InsertVisible="False" SortExpression="Id"></asp:BoundField>
            <asp:BoundField DataField="Title" HeaderText="عنوان" SortExpression="Title"></asp:BoundField>
            <asp:BoundField DataField="Property" HeaderText="صفت" SortExpression="Property"></asp:BoundField>
            <asp:BoundField DataField="Value" HeaderText="مقدار" SortExpression="Value"></asp:BoundField>
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
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DADConnectionString %>' DeleteCommand="DELETE FROM [Facts] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Facts] ([Title], [Property], [Value]) VALUES (@Title, @Property, @Value)" SelectCommand="SELECT [Id], [Title], [Property], [Value] FROM [Facts] ORDER BY [Title], [Property]" UpdateCommand="UPDATE [Facts] SET [Title] = @Title, [Property] = @Property, [Value] = @Value WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String"></asp:Parameter>
            <asp:Parameter Name="Property" Type="String"></asp:Parameter>
            <asp:Parameter Name="Value" Type="String"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Title" Type="String"></asp:Parameter>
            <asp:Parameter Name="Property" Type="String"></asp:Parameter>
            <asp:Parameter Name="Value" Type="String"></asp:Parameter>
            <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
            </div>
        </div>
</asp:Content>
