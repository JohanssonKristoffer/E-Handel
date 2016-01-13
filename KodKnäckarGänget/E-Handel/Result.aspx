<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="E_Handel.Result" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="resultname">&nbsp;<asp:Label ID="Label1" runat="server" Text="result's name"></asp:Label>
    </div>
    <div id="resultimage" class="auto-style1">Banner for Result  
        <br />
        <asp:Image ID="Image1" runat="server"  />
    </div>
    <aside id="resultdescription">
        <asp:Label ID="Label2" runat="server" Text="label for result Description"></asp:Label>
        <br />
    </aside>
    <div id="resultlist" >result related images list 
        <br />
        <asp:Image ID="Image2" runat="server" />
        <asp:Image ID="Image3" runat="server" />
        <asp:Image ID="Image4" runat="server" />
        <asp:Image ID="Image5" runat="server" />
    </div>
</asp:Content>
