<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebScraper._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Web Scraper</h1>
        <p class="lead">This utility scans a web page and retrieves information about text and images. The word count and frequency will be displayed via table, while the images will be displayed via carousel.</p>
        <asp:TextBox runat="server" ID="tbUrl" placeholder="Enter a Url"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="tbUrl" Display="Static" ErrorMessage="Url is required" runat="server" />
        <br />
        <asp:Button runat="server" ID="btnScan" class="btn btn-primary btn-lg" Text="Scan &raquo;" OnClick="ScanBtn_Click"></asp:Button>
    </div>

    <asp:Label ID="lblError" runat="server" Visible="false">An error occured: </asp:Label>

    <%-- Table for word count --%>
    <asp:Table runat="server" ID="tblData" CellPadding="5" Visible="false" BorderStyle="Groove">
        <asp:TableHeaderRow BorderStyle="Groove">
            <asp:TableHeaderCell Text="Word" HorizontalAlign="Center" Width="50" />
            <asp:TableHeaderCell Text="Quantity" HorizontalAlign="Center" Width="50" />
        </asp:TableHeaderRow>
    </asp:Table>

    <br />

    <%-- Carosel for images --%>
    <div id="myCarousel" clientidmode="Static" runat="server" class="carousel slide" data-ride="carousel" visible="false">
        <!-- Indicators -->
        <asp:Literal ID="litCarouselIndicators" runat="server" />
        <!-- Images-->
        <div class="carousel-inner" role="listbox">
            <asp:Literal ID="litCarouselImages" runat="server" />
        </div>
        <!-- Left and right controls -->
        <a class="left carousel-control" href="#myCarousel" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
</asp:Content>
