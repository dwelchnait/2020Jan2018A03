<%@ Page Title="ODS Review" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSReview.aspx.cs" Inherits="WebApp.SamplePages.ODSReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Query: Albums by Artist</h1>
    
<%--    <asp:Label ID="Label1" runat="server" Text="Select an artist"></asp:Label>&nbsp;&nbsp;
    <asp:DropDownList ID="ArtistList" runat="server" 
        DataSourceID="ArtistListODS" 
        DataTextField="Name" 
        DataValueField="ArtistId">
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:Button ID="Fetch" runat="server" Text="Fetch" />
    <br /><br />--%>

    <asp:Label ID="Label6" runat="server" Text="Enter album name"></asp:Label>&nbsp;&nbsp;
    <asp:TextBox ID="AlbumTitleArg" runat="server"></asp:TextBox>&nbsp;&nbsp;
    <asp:Button ID="Fetch" runat="server" Text="Fetch" />
    <br /><br />

    <asp:GridView ID="AlbumList" runat="server"
        DataSourceID="AlbumListODS" AllowPaging="True" PageSize="15"
        GridLines="Horizontal" BorderStyle="None" CssClass="table table-striped" 
        AutoGenerateColumns="False" Caption="Albums for Artist">

        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="AlbumId" runat="server" 
                        Text='<%# Eval("AlbumId") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Eval("Title") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Artist">
                <ItemTemplate>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistListODS"  Enabled="false"
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                         AppendDataBoundItems="true" Width="200px"
                         selectedvalue='<%# Eval("ArtistId") %>'>
                        <asp:ListItem Value="select">select ...</asp:ListItem>
                        <asp:ListItem Value="">none</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" 
                        Text='<%# Eval("ReleaseYear") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Label">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" 
                        Text='<%# Eval("ReleaseLabel") == null ? "----------" : Eval("ReleaseLabel") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No albums to display at this time.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="Album_FindByTitle"
        TypeName="ChinookSystem.BLL.AlbumController">
        <SelectParameters>
            <asp:ControlParameter ControlID="AlbumTitleArg"
                PropertyName="Text" DefaultValue="zxcdsf"
                Name="albumtitle" Type="String">
            </asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artist_List" 
        TypeName="ChinookSystem.BLL.ArtistController">
    </asp:ObjectDataSource>
</asp:Content>
