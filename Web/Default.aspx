<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Default" EnableEventValidation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:20px">
            客戶代號從<asp:TextBox ID="txtCustomerIdStart" runat="server" />
            到<asp:TextBox ID="txtCustomerIdEnd" runat="server" />
            <asp:Button ID="btnSearch" runat="server" Text="查詢" style="margin-left:20px;" OnClick="btnSearch_Click" />
            <asp:Button ID="btnInsert" runat="server" Text="新增" style="margin-left:20px;" OnClick="btnInsert_Click" />
        </div>
        <div id="divInsert" runat="server" visible="false" style="margin:20px;border:1px solid;padding:10px;">
            客戶代號:<asp:TextBox ID="txtCustomerId" runat="server" style="margin-right:20px;"/>  
            客戶名稱:<asp:TextBox ID="txtCustomerName" runat="server" style="margin-right:20px;"/>
            <asp:Button ID="btnInsertSubmit" runat="server" Text="確定" style="margin-right:20px;" OnClick="btnInsertSubmit_Click" ValidationGroup="Insert"/>
            <asp:Button ID="btnInsertCancel" runat="server" Text="取消" OnClick="btnInsertCancel_Click"/>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerId" Display="Dynamic" Text="客戶代碼不得空白" ValidationGroup="Insert" ForeColor="Red" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerName" Display="Dynamic" Text="客戶名稱不得空白" ValidationGroup="Insert" ForeColor="Red"/>
            <asp:Label id="lblInsertMessage" runat="server" ForeColor="Red" />
        </div>
        <div>
            <asp:GridView ID="gvCustomer" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="客戶代號" />
                    <asp:BoundField DataField="Name" HeaderText="客戶名稱" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
