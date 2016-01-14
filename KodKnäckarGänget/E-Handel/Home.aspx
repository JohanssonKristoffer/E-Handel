<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StartsidaBeta.aspx.cs" Inherits="E_Handel.StartsidaBeta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- A.J. Extra skapad css-->
    <link href="http://getbootstrap.com/2.3.2/assets/css/bootstrap.css" rel="stylesheet">
       <link href="http://getbootstrap.com/2.3.2/assets/css/bootstrap-responsive.css" rel="stylesheet">
    <style type="text/css">
        body {
            padding-top: 60px;
            padding-bottom: 40px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }

        @media (max-width: 980px) {
            .navbar-text.pull-right {
                float: none;
                padding-left: 5px;
                padding-right: 5px;
            }
        }
    </style>
    <!-- A.J. Extra skapad css-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container-fluid">
                <a class="brand" href="#">E-Handel</a>
                
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>

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
                        <li class="nav-header">Popular Products</li>
                        <li><a href="#">Product 1</a></li>
                        <li><a href="#">Product 2</a></li>
                        <li><a href="#">Product 3</a></li>
                        <li><a href="#">Product 4</a></li>
                        <li><a href="#">Product 5</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <hr>
    </div>


</asp:Content>