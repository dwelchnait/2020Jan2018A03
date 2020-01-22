<%@ Page Title="ListView ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ListView ODS CRUD</h1>
    <blockquote class="alert alert-info">
        This page will demonstrate the ListView control doing a complete CRUD using only ODS.
        The page will demonstrate the use of the user control MessageUserControl as it pertains
        to ODS.
    </blockquote>
    <br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" 
         HeaderText="Correct the following concerns on the insert data."
         ValidationGroup="IGroup"/>
    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" 
         HeaderText="Correct the following concerns on the editted data."
         ValidationGroup="EGroup"/>
    <br />
    <%-- For the Delete to work on this ODS CRUD, you MUST include
         a parameter called DataKeyNames
         This parameter will be set to the pkey field name on the Entity
        
        For Insert and Edit templates use Bind("xxxx")
        For Alternating, Item and Select use Eval("xxxx")
        
        A change to layout/control on one template should be duplicated
        to other templates
        
        Remove navigational properties
        
        Remove NotMapped properties--%>
    <asp:ListView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" 
        InsertItemPosition="LastItem" DataKeyNames="AlbumId">

        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF; color: #284775;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton"
                         OnClientClick="return confirm(' Are you sure you wish to remove this album.')"/>
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' 
                        runat="server" ID="AlbumIdLabel" Width="50px" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" 
                        ID="TitleLabel"  Width="350px"/></td>
                <td>
                   
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistListODS" 
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                        selectedvalue='<%# Eval("ArtistId") %>'
                        Width="300px" Enabled="false"
                         AppendDataBoundItems="true">
                        <asp:ListItem Value="">none</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' 
                        runat="server" ID="ReleaseYearLabel" Width="50px"/></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxE" runat="server" 
                ErrorMessage="Title is required" Display="None"
                 ControlToValidate="TitleTextBoxE" ValidationGroup="EGroup">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExTitleTextBoxE" runat="server" 
                ErrorMessage="Title is limited to 160 characters" Display="None"
                 ControlToValidate="TitleTextBoxE" ValidationGroup="EGroup"
                  ValidationExpression="^.{1,160}$">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="ReExReleaseLabelTextBoxE" runat="server" 
                ErrorMessage="Label is limited to 50 characters" Display="None"
                 ControlToValidate="ReleaseLabelTextBoxE" ValidationGroup="EGroup"
                  ValidationExpression="^.{0,50}$">
            </asp:RegularExpressionValidator>
            <tr style="background-color: #999999;">
                <td>
                    <asp:Button runat="server" CommandName="Update" Text="Update" 
                        ID="UpdateButton" ValidationGroup="EGroup" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" 
                         Enabled="false" Width="50px"/></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" 
                        ID="TitleTextBoxE" Width="350px" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistListODS" 
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                        selectedvalue='<%# Bind("ArtistId") %>'
                        Width="300px">
                    </asp:DropDownList>

                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' 
                        runat="server" ID="ReleaseYearTextBox" Width="50px"/></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxE" /></td>
               
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxI" runat="server" 
                ErrorMessage="Title is required" Display="None"
                 ControlToValidate="TitleTextBoxI" ValidationGroup="IGroup">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExTitleTextBoxI" runat="server" 
                ErrorMessage="Title is limited to 160 characters" Display="None"
                 ControlToValidate="TitleTextBoxI" ValidationGroup="IGroup"
                  ValidationExpression="^.{1,160}$">
            </asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="ReExReleaseLabelTextBoxI" runat="server" 
                ErrorMessage="Label is limited to 50 characters" Display="None"
                 ControlToValidate="ReleaseLabelTextBoxI" ValidationGroup="IGroup"
                  ValidationExpression="^.{0,50}$">
            </asp:RegularExpressionValidator>
            <tr style="">
                <td>
                    <asp:Button runat="server" CommandName="Insert" Text="Insert" 
                        ID="InsertButton"  ValidationGroup="IGroup"/>
                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" 
                         Enabled="false" Width="50px"/></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" 
                        ID="TitleTextBoxI" Width="350px"/></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistListODS" 
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                        selectedvalue='<%# Bind("ArtistId") %>'
                        Width="300px">
                    </asp:DropDownList>

                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' 
                        runat="server" ID="ReleaseYearTextBox" Width="50px"/></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxI" /></td>
               
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton"
                        OnClientClick="return confirm(' Are you sure you wish to remove this album.')"/>
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' 
                        runat="server" ID="AlbumIdLabel" Width="50px"/></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" 
                        ID="TitleLabel" Width="350px"/></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistListODS" 
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                        selectedvalue='<%# Eval("ArtistId") %>'
                        Width="300px" Enabled="false"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="">none</asp:ListItem>

                    </asp:DropDownList>

                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' 
                        runat="server" ID="ReleaseYearLabel" Width="50px"/></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
              
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">Id</th>
                                <th runat="server">Title</th>
                                <th runat="server">Artist</th>
                                <th runat="server">Year</th>
                                <th runat="server">Label</th>

                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #c0c0c0; 
                            font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000">
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                    ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                <asp:NumericPagerField ></asp:NumericPagerField>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                    ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" 
                        OnClientClick="return confirm(' Are you sure you wish to remove this album.')"/>
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' 
                        runat="server" ID="AlbumIdLabel" Width="50px"/></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" 
                        ID="TitleLabel" Width="350px"/></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistListODS" 
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                        selectedvalue='<%# Eval("ArtistId") %>'
                        Width="300px" Enabled="false"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="">none</asp:ListItem>

                    </asp:DropDownList>

                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' 
                        runat="server" ID="ReleaseYearLabel" Width="50px"/></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
              
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        DataObjectTypeName="ChinookSystem.Data.Entities.Album"
        OldValuesParameterFormatString="original_{0}"
        TypeName="ChinookSystem.BLL.AlbumController"
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        SelectMethod="Album_List" 
        UpdateMethod="Album_Update"
         OnDeleted="DeleteCheckForException"
         OnUpdated="UpdateCheckForException"
         OnInserted="InsertCheckForException"
         OnSelected="SelectedCheckForException">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artist_List" 
        TypeName="ChinookSystem.BLL.ArtistController"
         OnSelected="SelectedCheckForException">
    </asp:ObjectDataSource>
</asp:Content>
