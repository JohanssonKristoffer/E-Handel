<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="E_Handel.Home" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/Home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
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

                    <div class="span4">
                        <h2>Product 3</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                        <p><a class="btn" href="#">Add to cart</a></p>
                    </div>
                </div>
            </div>
            <div class="span3">
                <div class="well sidebar-nav">
                    <ul class="nav nav-list">
                        <li class="nav-header">Ads</li>
                        <li><a href="#">Product 1</a></li>
                        <li><a href="#">Product 2</a></li>
                        <li><a href="#">Product 3</a></li>
                        <li><a href="#">Product 4</a></li>
                        <li><a href="#">Product 5</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
