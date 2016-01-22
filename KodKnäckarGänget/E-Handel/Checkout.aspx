<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="E_Handel.Checkout" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/Checkout.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <asp:Table class="checkout_table" ID="checkout_product_table" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableCell ></asp:TableCell>
                    <asp:TableCell >Title</asp:TableCell>
                    <asp:TableCell >Price per item</asp:TableCell>
                    <asp:TableCell >Quantity</asp:TableCell>
                    <asp:TableCell >Total amount</asp:TableCell>
                    <asp:TableCell >Stock status</asp:TableCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <div class="span12">
            <table class="checkout_table">
                <tr>
                    <td>Shipping:</td>
                    <td><span id="tableShippingPrice" runat="server"></span></td>
                </tr>
                <tr>
                    <td>Total Price inc shipping:</td>
                    <td><span id="tableTotalPrice" runat="server"></span></td>
                </tr>
            </table>
        </div>
    </div>

    <div class="row-fluid">
        <div class="span12">
            Choose shipping method:&nbsp;&nbsp;
            <asp:DropDownList ID="shipping_dropdown" runat="server">
                <asp:ListItem Value="1">Pickup at store</asp:ListItem>
                <asp:ListItem Value="2">Stvatandard Mail</asp:ListItem>
                <asp:ListItem Value="3">DHL</asp:ListItem>
            </asp:DropDownList>
            
            Choose payment method:&nbsp;&nbsp;
            <asp:DropDownList ID="payment_dropdown" runat="server">
                <asp:ListItem Value="1">Cash at pickup</asp:ListItem>
                <asp:ListItem Value="2">Invoice</asp:ListItem>
                <asp:ListItem Value="3">Debit card</asp:ListItem>
            </asp:DropDownList>

            <asp:Label ID="customer_name_label" CssClass="checkout_label" AssociatedControlID="customer_name" Text="Name" runat="server"></asp:Label>
            <asp:TextBox ID="customer_name" runat="server"></asp:TextBox>
            <asp:Label ID="customer_surname_label" AssociatedControlID="customer_surname" Text="Surname" runat="server"></asp:Label>
            <asp:TextBox ID="customer_surname" runat="server"></asp:TextBox>
            <asp:Label ID="customer_email_label" AssociatedControlID="customer_email" Text="Email" runat="server"></asp:Label>
            <asp:TextBox ID="customer_email" runat="server"></asp:TextBox>
            <asp:Label ID="customer_phone_label" AssociatedControlID="customer_phone" Text="Phone" runat="server"></asp:Label>
            <asp:TextBox ID="customer_phone" runat="server"></asp:TextBox>
            <asp:Label ID="customer_address_label" AssociatedControlID="customer_address" Text="Address" runat="server"></asp:Label>
            <asp:TextBox ID="customer_address" runat="server"></asp:TextBox>
            <asp:Label ID="customer_postalcode_label" AssociatedControlID="customer_postalcode" Text="Zip Code" runat="server"></asp:Label>
            <asp:TextBox ID="customer_postalcode" runat="server"></asp:TextBox>
            <asp:Label ID="customer_city_label" AssociatedControlID="customer_city" Text="City" runat="server"></asp:Label>
            <asp:TextBox ID="customer_city" runat="server"></asp:TextBox>
            <asp:Label ID="customer_country_label" AssociatedControlID="customer_country" Text="Country" runat="server"></asp:Label>
            <asp:TextBox ID="customer_country" runat="server"></asp:TextBox>
            <asp:Button ID="customer_submit_order" Text="Submit order" runat="server" OnClick="SubmitOrder_Click" />
        </div>
    </div>
</asp:Content>
