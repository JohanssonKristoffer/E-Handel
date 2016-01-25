<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="E_Handel.Home" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/Home.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 237px;
        }

        .auto-style3 {
            width: 1200px;
            height: 315px;
        }

        .auto-style4 {
            width: 328px;
        }
    </style>
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span9">
                <div class="hero-unit" style="background-color: #000000; width: 800px; height: 330px">
                    <h1 class="auto-style4" style="color: #FFFFFF; margin-top: -30px; margin-bottom: 20px">DVD Store</h1>
                    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                        </ol>
                        <!-- Wrapper for slides -->
                        <div class="carousel-inner">
                            <div class="item active">
                                <img src="Images/sale2016.png" alt="bild3" class="auto-style3" />
                                <div class="carousel-caption">
                                    <h3 style="color: #FFFFFF">SALES</h3>
                                </div>
                            </div>
                            <div class="item">
                                <img src="Images/media-pawn.png" alt="bil1" style="width: 1200px; height: 315px" />
                                <div class="carousel-caption">
                                    <h3 style="color: #FFFFFF">THE MOST POPULAR MOVIES</h3>
                                </div>
                            </div>
                            <div class="item">
                                <img src="Images/ShrekDisplay.jpg" alt="bild2" style="width: 1200px; height: 315px" />
                                <div class="carousel-caption">
                                    <h3 style="color: #ffffff">SHREK</h3>
                                </div>
                            </div>
                        </div>
                        <!-- Controls -->
                        <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </a>
                        <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </a>
                    </div>
                    <!-- Carousel -->
                    <%--<h3>Featured product banner (carousel.js)</h3>--%>
                    <%--  <p><a href="#" class="btn btn-primary btn-large">More info || Add to cart</a></p>--%>
                </div>
                <div class="row-fluid">
                    <div class="span4">
                        <asp:HyperLink runat="server" ID="LinkPop1">
                            <asp:Image ID="ImagePop1" runat="server" CssClass="popImage" />
                        </asp:HyperLink>
                        <asp:Label ID="TitlePop1" CssClass="popTitle" runat="server"></asp:Label>
                        <asp:Label ID="OriginalPricePop1" CssClass="popOriginalPrice" runat="server"></asp:Label>
                        <asp:Label ID="DiscountPricePop1" CssClass="popDiscountPrice" runat="server"></asp:Label>
                        <asp:Button ID="AddtoCartPop1" runat="server" Text="Add to Cart" class="btn btn-primary btn-large" OnClick="AddtoCartPop1_Click"/>
                    </div>

                    <div class="span4">
                        <asp:HyperLink runat="server" ID="LinkPop2">
                            <asp:Image ID="ImagePop2" runat="server" CssClass="popImage" />
                        </asp:HyperLink>
                        <asp:Label ID="TitlePop2" CssClass="popTitle" runat="server"></asp:Label>
                        <asp:Label ID="OriginalPricePop2" CssClass="popOriginalPrice" runat="server"></asp:Label>
                        <asp:Label ID="DiscountPricePop2" CssClass="popDiscountPrice" runat="server"></asp:Label>
                        <asp:Button ID="AddtoCartPop2" runat="server" Text="Add to Cart" class="btn btn-primary btn-large" OnClick="AddtoCartPop2_Click" />
                    </div>

                    <div class="span4">
                        <asp:HyperLink runat="server" ID="LinkPop3">
                            <asp:Image ID="ImagePop3" runat="server" CssClass="popImage" />
                        </asp:HyperLink>
                        <asp:Label ID="TitlePop3" CssClass="popTitle" runat="server"></asp:Label>
                        <asp:Label ID="OriginalPricePop3" CssClass="popOriginalPrice" runat="server"></asp:Label>
                        <asp:Label ID="DiscountPricePop3" CssClass="popDiscountPrice" runat="server"></asp:Label>
                        <asp:Button ID="AddtoCartPop3" runat="server" Text="Add to Cart" class="btn btn-primary btn-large" OnClick="AddtoCartPop3_Click"/>
                    </div>
                </div>
            </div>
            <div id="ads" class="auto-style1">
                <asp:Panel ID="PanelAd1" runat="server">
                    <asp:HyperLink runat="server" ID="LinkAd1">
                        <asp:Image ID="ImageAd1" runat="server" CssClass="adImage" />
                    </asp:HyperLink>
                    <asp:Label ID="TitleAd1" CssClass="adTitle" runat="server"></asp:Label>
                    <asp:Label ID="OriginalPriceAd1" CssClass="adOriginalPrice" runat="server"></asp:Label>
                    <asp:Label ID="DiscountPriceAd1" CssClass="adDiscountPrice" runat="server"></asp:Label>
                    <asp:Button ID="AddToCartAd1" runat="server" Text="Add to Cart" class="btn btn-primary btn-large" OnClick="AddToCartAd1_Click" />
                </asp:Panel>
                <asp:Panel ID="PanelAd2" runat="server">
                    <asp:HyperLink runat="server" ID="LinkAd2">
                        <asp:Image ID="ImageAd2" runat="server" CssClass="adImage" />
                    </asp:HyperLink>
                    <asp:Label ID="TitleAd2" CssClass="adTitle" runat="server"></asp:Label>
                    <asp:Label ID="OriginalPriceAd2" CssClass="adOriginalPrice" runat="server"></asp:Label>
                    <asp:Label ID="DiscountPriceAd2" runat="server" CssClass="adDiscountPrice"></asp:Label>
                    <asp:Button ID="AddToCartAd2" class="btn btn-primary btn-large" runat="server" Text="Add to Cart" OnClick="AddToCartAd2_Click" />
                </asp:Panel>
            </div>
        </div>
    </div>
    <script>
        $('.carousel').carousel({
            interval: 3000
        })
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</asp:Content>
