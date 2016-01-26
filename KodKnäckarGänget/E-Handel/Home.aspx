<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="E_Handel.Home" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/Home.css" rel="stylesheet" />
    <script src="Scripts/parallax.js"></script>
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-sm-8">
                <div class="hero-unit">
                    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="3"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="4"></li>
                        </ol>
                        <div class="carousel-inner">
                            <div class="item active">
                                <asp:HyperLink NavigateUrl="/Result.aspx?categoryId=17" runat="server">
                                <img class="carouselImage" src="ImgHandler.ashx?categoryId=17">
                                </asp:HyperLink>
                                <div class="carousel-caption">
                                    <h3 style="color: #FFFFFF">Action</h3>
                                </div>
                            </div>
                            <div class="item">
                                <asp:HyperLink NavigateUrl="/Result.aspx?categoryId=18" runat="server">
                                    <img class="carouselImage" src="ImgHandler.ashx?categoryId=18">
                                </asp:HyperLink>
                                <div class="carousel-caption">
                                    <h3 style="color: #FFFFFF">Thriller</h3>
                                </div>
                            </div>
                            <div class="item">
                                <asp:HyperLink NavigateUrl="/Result.aspx?categoryId=19" runat="server">
                                <img class="carouselImage" src="ImgHandler.ashx?categoryId=19">
                                </asp:HyperLink>
                                <div class="carousel-caption">
                                    <h3 style="color: #ffffff">Animation</h3>
                                </div>
                            </div>
                            <div class="item">
                                <asp:HyperLink NavigateUrl="/Result.aspx?categoryId=20" runat="server">
                                <img class="carouselImage" src="ImgHandler.ashx?categoryId=20">
                                </asp:HyperLink>
                                <div class="carousel-caption">
                                    <h3 style="color: #ffffff">Comedy</h3>
                                </div>
                            </div>
                            <div class="item">
                                <asp:HyperLink NavigateUrl="/Result.aspx?categoryId=22" runat="server">
                                <img class="carouselImage" src="ImgHandler.ashx?categoryId=22">
                                </asp:HyperLink>
                                <div class="carousel-caption">
                                    <h3 style="color: #ffffff">Drama</h3>
                                </div>
                            </div>
                        </div>
                        <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </a>
                        <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </a>
                    </div>
                </div>
            </div>

            <div id="ads" class="col-md-4 ">
                <asp:Panel ID="PanelAd1" runat="server">
                    <asp:HyperLink runat="server" ID="LinkAd1">
                        <asp:Image ID="ImageAd1" runat="server" CssClass="adImage" />
                    </asp:HyperLink>
                    <asp:Label ID="TitleAd1" CssClass="adTitle" runat="server"></asp:Label>
                    <asp:Label ID="OriginalPriceAd1" CssClass="adOriginalPrice" runat="server"></asp:Label>
                    <asp:Label ID="DiscountPriceAd1" CssClass="adDiscountPrice" runat="server"></asp:Label>
                    <asp:Button ID="AddToCartAd1" runat="server" Text="Add to Cart" class="btn btn-primary" OnClick="AddToCartAd1_Click" />
                </asp:Panel>
                <asp:Panel ID="PanelAd2" runat="server">
                    <asp:HyperLink runat="server" ID="LinkAd2">
                        <asp:Image ID="ImageAd2" runat="server" CssClass="adImage" />
                    </asp:HyperLink>
                    <asp:Label ID="TitleAd2" CssClass="adTitle" runat="server"></asp:Label>
                    <asp:Label ID="OriginalPriceAd2" CssClass="adOriginalPrice" runat="server"></asp:Label>
                    <asp:Label ID="DiscountPriceAd2" runat="server" CssClass="adDiscountPrice"></asp:Label>
                    <asp:Button ID="AddToCartAd2" class="btn btn-primary" runat="server" Text="Add to Cart" OnClick="AddToCartAd2_Click" />
                </asp:Panel>
            </div>

        </div>
    </div>

    <div class="parallax-window" data-parallax="scroll" data-image-src="/Images/ShrekDisplay.jpg">
        <h1>Popular Products</h1>
        <div class="col-md-4 col-sm-4">

            <asp:HyperLink runat="server" ID="LinkPop1">
                <asp:Image ID="ImagePop1" runat="server" CssClass="popImage" />
            </asp:HyperLink>
            <asp:Label ID="TitlePop1" CssClass="popTitle" runat="server"></asp:Label>
            <asp:Label ID="OriginalPricePop1" CssClass="popOriginalPrice" runat="server"></asp:Label>
            <asp:Label ID="DiscountPricePop1" CssClass="popDiscountPrice" runat="server"></asp:Label>
            <asp:Button ID="AddtoCartPop1" runat="server" Text="Add to Cart" class="btn btn-primary" OnClick="AddtoCartPop1_Click" />
        </div>

        <div class="col-md-4 col-sm-4">
            <asp:HyperLink runat="server" ID="LinkPop2">
                <asp:Image ID="ImagePop2" runat="server" CssClass="popImage" />
            </asp:HyperLink>
            <asp:Label ID="TitlePop2" CssClass="popTitle" runat="server"></asp:Label>
            <asp:Label ID="OriginalPricePop2" CssClass="popOriginalPrice" runat="server"></asp:Label>
            <asp:Label ID="DiscountPricePop2" CssClass="popDiscountPrice" runat="server"></asp:Label>
            <asp:Button ID="AddtoCartPop2" runat="server" Text="Add to Cart" class="btn btn-primary" OnClick="AddtoCartPop2_Click" />
        </div>

        <div class="col-md-4 col-sm-4">
            <asp:HyperLink runat="server" ID="LinkPop3">
                <asp:Image ID="ImagePop3" runat="server" CssClass="popImage" />
            </asp:HyperLink>
            <asp:Label ID="TitlePop3" CssClass="popTitle" runat="server"></asp:Label>
            <asp:Label ID="OriginalPricePop3" CssClass="popOriginalPrice" runat="server"></asp:Label>
            <asp:Label ID="DiscountPricePop3" CssClass="popDiscountPrice" runat="server"></asp:Label>
            <asp:Button ID="AddtoCartPop3" runat="server" Text="Add to Cart" class="btn btn-primary" OnClick="AddtoCartPop3_Click" />
        </div>
    </div>
    <script src="Scripts/bootstrap.min.js"></script>
</asp:Content>
