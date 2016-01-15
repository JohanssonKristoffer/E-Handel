<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Startsida.aspx.cs" Inherits="E_Handel.Startsida" %>

<!--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">-->

    <div class="container">
        <div class="row">
            <div id="MainHome" class="col-md-8">
                <div id="HomeBanner">
                    <p>Banner för HomePage</p>
                </div>
            </div>

            <div id="MainAds" class="col-md-4 col-md-push-1">
                <div id="HomeAds">
                    <aside>Sidebar</aside>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div id="MainPopular">

                <div id="HomeProducts" class="col-md-3">
                    <p>Popular produkt</p>
                </div>

                <div id="HomeProducts" class="col-md-3">
                    <p>Popular produkt</p>
                </div>

                <div id="HomeProducts" class="col-md-3">
                    <p>Popular produkt</p>
                </div>

                <!--<div id="HomeProducts" class="col-md-2">
                    <p>Popular produkt</p>
                </div>-->

            </div>


        </div>
    </div>


</asp:Content>
