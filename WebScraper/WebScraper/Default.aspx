<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebScraper._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Web Scraper</h1>
        <p class="lead">This utility scans a web page and retrieves information about text and images.</p>
        <p>The word count and frequency will be displayed via graphs, while the images will be displayed via carousel.</p>
        <asp:TextBox runat="server" ID="tbUrl" placeholder="Enter a Url"></asp:TextBox>
        <asp:Button runat="server" ID="btnScan" class="btn btn-primary btn-lg" Text="Scan &raquo;" OnClick="ScanBtn_Click"></asp:Button>
    </div>

    <div>
        <br />
        <asp:Table runat="server" ID="tblData" CellPadding="5" Visible="false">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Word" HorizontalAlign="Center" Width="50"/>
                <asp:TableHeaderCell Text="Quantity" HorizontalAlign="Center" Width="50" />
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

</asp:Content>
