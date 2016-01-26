<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="E_Handel.Product" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Style/Product.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container">
        <div class="row-fluid">
            <div class="col-md-4">
                <div class="image_placeholder" runat="server">
                    <img alt="" runat="server" id="productImage" />
                </div>
                <a id="linkViewTrailer" target="_blank" runat="server" visible="False">View Trailer</a>
            </div>

            <div class="col-md-8">
                <h1 runat="server" id="productTitle"></h1>
                <h3 class="original_price_tag" runat="server" id="originalProductPrice" visible="False"></h3>
                <h3 class="price_tag" runat="server" id="productPrice"></h3>
                <p id="productDescription" runat="server"></p>
                <p>
                    <label for="productQuantity">Quantity:</label><input id="productQuantity" runat="server" type="number" min="1" max="100" value="1" />
                    <br />
                    <asp:DropDownList ID="DropDownVariants" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SendVariantChoice_SelectChange" />
                </p>
                <p>
                    <asp:Button ID="ButtonAddToCart" runat="server" OnClick="AddToCart_Click" Text="Add To Cart" /></p>
            </div>
        </div>
    </div>
</asp:Content>
