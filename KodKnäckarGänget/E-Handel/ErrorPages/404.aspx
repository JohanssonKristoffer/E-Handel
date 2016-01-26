<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="E_Handel.ErrorPages._404" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="../Style/Error.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container errorDiv">
        <h1>Page not found!</h1>
        <img alt="Page not found!" src="../Images/404.jpg" />
    </div>
</asp:Content>
