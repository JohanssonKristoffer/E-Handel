<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="E_Handel.Home" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/Home.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 237px;
        }
        .auto-style2 {
            width: 237px;
            margin-left: 742;
        }
        .auto-style3 {
            width: 228px;
            height: 190px;
        }
    </style>
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
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
      <img src="Images/sale2016.png" alt="bild3" style="width:1200px;height:315px"/>
      <div class="carousel-caption">
          <h3>Write Here</h3>
      </div>
    </div>
    <div class="item">
                <img src="Images/media-pawn.png" alt="bil1" style="width:1200px;height:315px"/>
      <div class="carousel-caption">
          <h3>Write here</h3>
      </div>
    </div>
    <div class="item">
      <img src="Images/ShrekDisplay.jpg"  alt="bild2" style="width:1200px;height:315px"/>
      <div class="carousel-caption">
          <h3>Write here</h3>
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
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span9">
                <div class="hero-unit">
                    <h1>DVD Store</h1>
                    <h3>Featured product banner (carousel.js)</h3>
                    <p><a href="#" class="btn btn-primary btn-large">More info || Add to cart</a></p>
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

                    <div class="auto-style3">
                        <h2>Product 3</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                        <p><a class="btn" href="#">Add to cart</a></p>
                    </div>
                </div>
            </div>
            <div id="ads" class="auto-style1">
                <h2 id="kampanj" class="text-center">SALE</h2>
                <p id="kampanjText" class="auto-style2" style="font-family:Verdana" background-color:>
                    Dont miss out of this great sale of among our great products with 25% discount. This sale only applies to the products in our current stock. Dont miss out!! First come, first served!<br />
                    <br />
                </p>
                <asp:Panel ID="PanelAd1">
                    <asp:Image ID="ImageAd1" />
                    <asp:Label ID="OrigianlPriceAd1" CssClass="adOriginalPrice"></asp:Label>
                    <asp:Label ID="DiscountPriceAd1" CssClass="adOriginalPrice"></asp:Label>
                </asp:Panel>
                <%--<h5>
            <asp:AdRotator ID="AdRotator1" runat="server" AdvertisementFile="~/AdsData.xml"  />

                </h5>
                <h5>

                    <strike>Original Price: 10£</strike> 
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </h5>
                <h5 style="color:red">
                    Discount Price: 7£<asp:Label ID="Label2" runat="server"></asp:Label>
                </h5>
                <p style="color:red">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="Add to Cart" OnClick="Button1_Click" />
                &nbsp;
                </p>
                <p style="color:red">
                    &nbsp;</p>
                <p style="color:red">
                    <asp:AdRotator ID="AdRotator2" runat="server" AdvertisementFile="~/AdsData2.xml" OnAdCreated="AdRotator2_AdCreated" />
                </p>
               <h5>

                    <strike>Original Price: 7£</strike> 
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </h5>
                <h5 style="color:red">--%>
                    <%--Discount Price: 5$<asp:Label ID="Label4" runat="server"></asp:Label>--%>
                </h5>
                <p style="color:red">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Add to Cart" OnClick="Button1_Click" />
                &nbsp;
                </p>
                <%-- Url: ImgHandler.ashx?productId=15 // ?productIdThumb=15 // ?categoryId=17  --%>
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
