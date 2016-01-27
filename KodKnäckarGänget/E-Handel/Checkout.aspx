<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="E_Handel.Checkout" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/Checkout.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <asp:Table class="checkout_table" ID="checkout_product_table" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>Title</asp:TableCell>
                    <asp:TableCell>Price</asp:TableCell>
                    <asp:TableCell>Quantity</asp:TableCell>
                    <asp:TableCell>Stock status</asp:TableCell>
                    <asp:TableCell>VAT</asp:TableCell>
                    <asp:TableCell>Total</asp:TableCell>
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
        <v class="span12">
           <p class="ParagraphChekout"> Choose shipping method:</p>
            <asp:DropDownList ID="shipping_dropdown" runat="server" AutoPostBack="True">
                <asp:ListItem Value="1">Pickup at store</asp:ListItem>
                <asp:ListItem Value="2">Standard Mail</asp:ListItem>
                <asp:ListItem Value="3">DHL</asp:ListItem>
            </asp:DropDownList>

           <p class="ParagraphChekout"> Choose payment method:</p>
            <asp:DropDownList ID="payment_dropdown" runat="server" AutoPostBack="True">
                <asp:ListItem Value="1">Cash at pickup</asp:ListItem>
                <asp:ListItem Value="2">Invoice</asp:ListItem>
                <asp:ListItem Value="3">Debit card</asp:ListItem>
            </asp:DropDownList>
            <hr />
            <div>
            <div id="NameTxtBox">
                <%--<asp:Label ID="customer_name_label"  AssociatedControlID="customer_name" Text="Name" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_name" placeholder="Name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_name" Display="Dynamic" EnableClientScript="False" ErrorMessage="Name is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_name" Display="Dynamic" EnableClientScript="False" ErrorMessage="Name must be 2 to 50 characters." ValidationExpression="^\S[a-zA-ZöäåÖÄÅ\-.\s]{2,50}" />

                <%--<asp:Label ID="customer_surname_label" AssociatedControlID="customer_surname" Text="Surname" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_surname" placeholder="Surname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_surname" Display="Dynamic" EnableClientScript="False" ErrorMessage="Surname is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_surname" Display="Dynamic" EnableClientScript="False" ErrorMessage="Surname must be 2 to 50 characters." ValidationExpression="^\S[a-zA-ZöäåÖÄÅ\-.\s]{2,50}" />
            </div>
            <div id="Address">
                <%--<asp:Label ID="customer_address_label" AssociatedControlID="customer_address" Text="Address" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_address" placeholder="Address" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_address" Display="Dynamic" EnableClientScript="False" ErrorMessage="Address is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_address" Display="Dynamic" EnableClientScript="False" ErrorMessage="Address must be 2 to 255 characters." ValidationExpression="^\S[a-zA-ZöäåÖÄÅ1-9\s]{2,50}" />
            </div>
            <div id="PostCity">
                <%--<asp:Label ID="customer_postalcode_label" AssociatedControlID="customer_postalcode" Text="Postal Code" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_postalcode" placeholder="PostalCode" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_postalcode" Display="Dynamic" EnableClientScript="False" ErrorMessage="Postal Code is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_postalcode" Display="Dynamic" EnableClientScript="False" ErrorMessage="Postal Code must be [placeholder]." ValidationExpression="^\S[1-9\s]{2,50}" />

                <%-- <asp:Label ID="customer_city_label" AssociatedControlID="customer_city" Text="City" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_city" placeholder="City" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_city" Display="Dynamic" EnableClientScript="False" ErrorMessage="City is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_city" Display="Dynamic" EnableClientScript="False" ErrorMessage="City must be 2 to 50 characters." ValidationExpression="^\S[a-zA-ZöäåÖÄÅ\s]{2,50}" />
            </div>

            <div id="OtherLabel">
                <%--<asp:Label ID="customer_country_label" AssociatedControlID="customer_country" Text="Country" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_country" placeholder="Country" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_country" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_country" Display="Dynamic" EnableClientScript="False" ErrorMessage="Country must be 2 to 50 characters." ValidationExpression="^\S[a-zA-ZöäåÖÄÅ\s]{2,50}" />

                <%--<asp:Label ID="customer_phone_label" AssociatedControlID="customer_phone" Text="Phone" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_phone" placeholder="Phone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_phone" Display="Dynamic" EnableClientScript="False" ErrorMessage="Phone is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_phone" Display="Dynamic" EnableClientScript="False" ErrorMessage="Phone must be [placeholder]." ValidationExpression="^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$" />

                <%--<asp:Label ID="customer_email_label" AssociatedControlID="customer_email" Text="Email" runat="server"></asp:Label>--%>
                <asp:TextBox CssClass="Checkout_TextBox" ID="customer_email" placeholder="E-mail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_email" Display="Dynamic" EnableClientScript="False" ErrorMessage="Email is a required field." />
                <asp:RegularExpressionValidator runat="server" CssClass="checkout_validator" ControlToValidate="customer_email" Display="Dynamic" EnableClientScript="False" ErrorMessage="Email must be [placeholder]." ValidationExpression="^\S[a-zA-Z0-9!#$%&'*+=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+=?^_`{|}~-]+)*@(?:[a-zA-Z-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-z0-9-]*[a-z0-9])?" />
            </div>
            <asp:Button ID="customer_submit_order" Style="margin: 20px" Text="Submit order" class="btn btn-primary" runat="server" OnClick="SubmitOrder_Click" />
        <v>
    </div>
</asp:Content>
