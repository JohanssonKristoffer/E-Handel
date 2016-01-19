<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="E_Handel.CartPage" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="Style/CartPage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container-fluid">
        <div class="row-fluid">
                <table  class="span9" id="ProductTable">
                    <tr>
                        <th>Image: </th>
                        <th id="ProductName">Productname</th>
                        <th>Price</th>
                        <th>Amount</th>
                        <th>Total cost</th>

                    </tr>
                </table>
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
            <div id="ChoiceButtons">
                <input type="button" value="Go to Checkout"/>
                <input type="button" value="Keep shopping"/>
            </div>
        </div>
    </div>
</asp:Content>
