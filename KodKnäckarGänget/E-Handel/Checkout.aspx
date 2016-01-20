<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="E_Handel.Checkout" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <style>
        .checkout_table {
            border: 1px solid #cccccc;
            padding: 10px;
            width: 800px;
        }

        td, th {
            padding: 10px;
            width: 90px;
            text-align: left;
        }

        .checkout_product_data {
            vertical-align: top;
        }

        .checkout_table_img_width {
            width: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">

    <div class="row-fluid">
        <div class="span12">
            <table class="checkout_table">
                <tr>
                    <th class="checkout_table_img_width"></th>
                    <th>Title</th>
                    <th>Quantity</th>
                    <th>Price per item</th>
                    <th>Total amount</th>
                    <th>Stock status</th>
                    <th></th>
                </tr>
            </table>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <table class="checkout_table">
                    <tr>
                        <td class="checkout_product_data checkout_table_img_width">
                            <div class="image_thumbnail" runat="server">
                                <img src="http://www.dev15akwkar.se/titanic.jpg" alt="" style="width: 50px;" runat="server" id="productThumbnail" />
                            </div>
                        </td>
                        <!--<td class="checkout_product_data" id="tableProductId" runat="server"></td>-->
                        <td class="checkout_product_data" id="tableProductName">Titanic</td>
                        <td class="checkout_product_data" id="tableProductQuantity">99</td>
                        <td class="checkout_product_data" id="tableProductPrice">£7</td>
                        <td class="checkout_product_data" id="tableProductSum">2</td>
                        <td class="checkout_product_data" id="tableProductStock">24</td>
                        <td class="checkout_product_data" id="tableProductTrash"></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <table class="checkout_table">
                    <tr>
                        <td>Shipping:</td>
                        <td id="tableShippingPrice">£5</td>
                    </tr>
                    <tr>
                        <td>Total sum:</td>
                        <td id="tableTotalSum">£19</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="row-fluid">
        <div class="span12">
            <asp:Label ID="customer_name_label" AssociatedControlID="customer_name" Text="Name" runat="server"></asp:Label>
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
            <asp:TextBox ID" runat="server"></asp:TextBox>
            <asp:Label ID="customer_city_label" AssociatedControlID="customer_city" Text="City" runat="server"></asp:Label>
            <asp:TextBox ID="customer_city" runat="server"></asp:TextBox>
            <asp:Label ID="customer_country" AssociatedControlID="customer_country" Text="Country" runat="server"></asp:Label>
            <asp:TextBox ID="customer_country" runat="server"></asp:TextBox>
            <asp:Button ID="customer_submit_order" Text="Submit order" runat="server" OnClick="customer_submit_order_Click" />
        </div>
    </div>
</asp:Content>
