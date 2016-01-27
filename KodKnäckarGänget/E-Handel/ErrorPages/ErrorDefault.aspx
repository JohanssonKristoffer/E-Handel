<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorDefault.aspx.cs" Inherits="E_Handel.ErrorPages.ErrorDefault" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="../Style/Error.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container errorDiv">
        <h1>An error has occurred!</h1>
        <p>
            Something clever something something, something clever something, try using our return button below and carry on shopping, if that fails and the error persists, choose alternative two and please report it to our "something clever".
        </p>
        <asp:Button ID="GoBackErrorButton" CssClass="backErrorButton" runat="server" Text="Go Back" OnClick="GoBackErrorButton_OnClick"/>
        <asp:Button ID="ReportErrorButton" CssClass="reportErrorButton" runat="server" Text="Report Error"/>
    </div>
</asp:Content>
