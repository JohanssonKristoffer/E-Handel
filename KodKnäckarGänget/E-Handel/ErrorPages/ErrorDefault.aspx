<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorDefault.aspx.cs" Inherits="E_Handel.ErrorPages.ErrorDefault" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
    <link href="../Style/Error.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container errorDiv">
        <h1>An error has occurred!</h1>
        <asp:Label ID="ErrorOutputLabel" runat="server" CssClass="errorDescription"></asp:Label>
        <asp:Button ID="ReportErrorButton" CssClass="errorButton" runat="server" Text="Report Error"/>
    </div>
</asp:Content>
