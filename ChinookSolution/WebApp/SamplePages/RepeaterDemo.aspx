<%@ Page Title="Repeater Demo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterDemo.aspx.cs" Inherits="WebApp.SamplePages.RepeaterDemo" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater for Nested Query</h1>
    <blockquote>This page will demonstrate the Repeater control. This control is a 
        great web control to display the structure of a DTO/POCO query. The control
        can be nested within itself to be used to display the POCO component of the
        DTO structure.<br /><br />
        To ease the working with the properties in your class on this control use
        the ItemType attribute and assign the class name of your data defintion. The 
        control uses a series of templates to fashion your display.
    </blockquote>
    <div class="row">
        <div class="col-md-12 text-center">
            Enter the size of playlist to view:&nbsp;&nbsp;
            <asp:TextBox ID="NumberOfTracks" runat="server">
            </asp:TextBox>&nbsp;&nbsp;
            <asp:Button ID="Submit" runat="server" Text="Submit" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:RequiredFieldValidator ID="RequiredNumberOfTracks" runat="server" 
                ErrorMessage="Tracks size is required" Display="None" SetFocusOnError="true"
                 ForeColor="Firebrick" ControlToValidate="NumberOfTracks">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareNumberOfTracks" runat="server" 
                ErrorMessage="Tracks size must be a postive whole number" Display="None" SetFocusOnError="true"
                 ForeColor="Firebrick" ControlToValidate="NumberOfTracks" Type="Integer"
                 ValueToCompare="0" Operator="GreaterThan">
            </asp:CompareValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
     <div class="row">
        <div class="col-md-12 text-center">
            <asp:Repeater ID="ClientPlayListDTO" runat="server" 
                DataSourceID="ClientPlayListDTOODS"
                 ItemType="ChinookSystem.Data.DTOs.MyPlayList">
                <HeaderTemplate>
                    <h2>Client PlayLists for Requested Size</h2>
                </HeaderTemplate>
                <ItemTemplate>
                    <h3>
                    <%# Item.Name %>  Playtime: <%# Item.PlayTime %>
                    </h3>
                  <%--  <asp:GridView ID="SongList" runat="server"
                         DataSource="<%# Item.PlaylistSongs %>"
                         ItemType="ChinookSystem.Data.POCOs.PlayListSong">
                    </asp:GridView>--%>
                    <asp:Repeater ID="SongList" runat="server"
                        DataSource="<%# Item.PlaylistSongs %>"
                         ItemType="ChinookSystem.Data.POCOs.PlayListSong">
                        <ItemTemplate>
                            <%# Item.SongName %>&nbsp;&nbsp;<%# Item.Genre %><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:ObjectDataSource ID="ClientPlayListDTOODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Playlist_GetBySize" 
        OnSelected="SelectedCheckForException"
        TypeName="ChinookSystem.BLL.PlaylistController">
        <SelectParameters>
            <asp:ControlParameter ControlID="NumberOfTracks" 
                PropertyName="Text" DefaultValue="1" 
                Name="numberoftracks" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
