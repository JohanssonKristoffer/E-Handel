<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="E_Handel.Home" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/Home.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 237px;
        }
        .auto-style2 {
            margin-left: 50px;
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
                <div class="hero-unit" style="background-color: #000000 ;width:800px; height:330px" >
                    <h1 class="auto-style4" style="color: #FFFFFF; margin-top:-30px; margin-bottom:20px">DVD Store</h1>
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
      <img src="Images/sale2016.png" alt="bild3" class="auto-style3"/>
      <div class="carousel-caption">
          <h3 style="color: #FFFFFF">SALES</h3>
      </div>
    </div>
    <div class="item">
                <img src="Images/media-pawn.png" alt="bil1" style="width:1200px;height:315px"/>
      <div class="carousel-caption">
          <h3 style="color: #FFFFFF">THE MOST POPULAR MOVIES</h3>
      </div>
    </div>
    <div class="item">
      <img src="Images/ShrekDisplay.jpg"  alt="bild2" style="width:1200px;height:315px"/>
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
                        <h2>Product 1</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                        <p><a class="btn" href="#">Add to cart</a></p>
                    </div>

                    <div class="span4">
                        <h2>Product 2</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                        <p><a class="btn" href="#">Add to cart</a></p>
                    </div>

                    <div class="span4">
                        <h2>Product 3</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                        <p><a class="btn" href="#">Add to cart</a></p>
                    </div>
                </div>
            </div>
            <div id="ads" class="auto-style1">
               <%-- <h2 id="kampanj" class="text-center">SALE</h2>
                <p id="kampanjText" class="auto-style2" style="border-style: solid; border-color: #000000; margin: 10px; font-family:verdana, Geneva, Tahoma, sans-serif; font-style: normal; font-variant: normal; color: #0066FF; background-color: #FFFFFF; font-weight: bold; padding-right: 10px; padding-left: 10px; padding-top: 10px;" background-color:>
                    Dont miss out of this great sale of among our great products with uo to <span class="auto-style3">30 %</span> discount. This sale only applies to the products in our current stock. Dont miss out!! First come, first served!<br />
                    <br />
                </p>--%>
                <asp:Panel ID="PanelAd1" runat="server">
                    <asp:Image ID="ImageAd1" runat="server" Width="150px" CssClass="auto-style2"/>
                    &nbsp;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="OriginalPriceAd1" CssClass="adOriginalPrice" runat="server" Font-Bold="True"></asp:Label>
                    &nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="DiscountPriceAd1" CssClass="adDiscountPrice" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                </asp:Panel>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="Button1" runat="server" Text="Add to Cart" class="btn btn-primary btn-large" />
                <br />
                <br />
                <asp:Panel ID="PanelAd2" runat="server">
                    <asp:Image ID="ImageAd2" runat="server" Width="150px" CssClass="auto-style2"/>
                    &nbsp;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="OriginalPriceAd2" CssClass="adOriginalPrice"  runat="server" Font-Bold="True" ></asp:Label>
                    &nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="DiscountPriceAd2" runat="server" CssClass="adDiscountPrice" ForeColor="Red" Font-Bold="True" ></asp:Label>
                </asp:Panel>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <p style="color:red">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2"  class="btn btn-primary btn-large" runat="server" Text="Add to Cart"/>
                &nbsp;
                </p>
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
