<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Startsida.aspx.cs" Inherits="E_Handel.Startsida"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="bannerHome">Banner för HomePage<br />
        <asp:Image ID="Image5" runat="server" /></div>
    <div id="TopProductsPics">Bilder för top Products <br />
    <asp:Image ID="Image1" runat="server" /><asp:Image ID="Image2" runat="server" /><asp:Image ID="Image3" runat="server" /><asp:Image ID="Image4" runat="server" />

    </div>
    <div id="ads">
    <aside >Aside</aside>
        </div>


</asp:Content>
