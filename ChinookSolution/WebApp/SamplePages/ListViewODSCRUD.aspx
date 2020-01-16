<%@ Page Title="ListView ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ListView ODS CRUD</h1>
    <blockquote class="alert alert-info">
        This page will demonstrate the ListView control doing a complete CRUD using only ODS.
        The page will demonstrate the use of the user control MessageUserControl as it pertains
        to ODS.
    </blockquote>
    <asp:ListView ID="AlbumList" runat="server">

    </asp:ListView>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        DataObjectTypeName="ChinookSystem.Data.Entities.Album"
        OldValuesParameterFormatString="original_{0}"
        TypeName="ChinookSystem.BLL.AlbumController"
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        SelectMethod="Album_List" 
        UpdateMethod="Album_Update">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artist_List" 
        TypeName="ChinookSystem.BLL.ArtistController">
    </asp:ObjectDataSource>
</asp:Content>
