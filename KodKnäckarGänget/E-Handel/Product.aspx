<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="E_Handel.Product" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">

    <div class="row-fluid">
        <div class="span4">
            <div class="image_placeholder" runat="server">
                <img alt="" runat="server" id="productImage" />
            </div>
        </div>

        <div class="span8">
            <h1 runat="server" id="productTitle"></h1>
            <h3 class="price_tag" runat="server" id="productPrice"></h3>
            <p id="productDescription" runat="server"></p>
            <p>
                <label for="productQuantity">Quantity:</label><input id="productQuantity" runat="server" type="number" min="1" max="100" />
                <br />
                <asp:DropDownList ID="DropDownVariants" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SendVariantChoice_SelectChange"/>
            </p>
            <p><a class="btn buy_button" href="#">Add to cart</a></p>
        </div>
    </div>
    <hr />

</asp:Content>
