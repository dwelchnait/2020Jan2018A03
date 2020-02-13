<%@ Page Title="Accessing Data" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccessingDataSetControls.aspx.cs" Inherits="WebApp.SamplePages.AccessingDataSetControls" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row jumbotron">
        <h3>Demonstrating Access to GridView and ListView</h3>
    </div>
    <div class="row">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>
    <div class="row">
        <asp:Panel ID="ControlPanel" runat="server">
            <asp:Label ID="TracksBy" runat="server" Text="Album"></asp:Label>&nbsp;
            <asp:DropDownList ID="AlbumList" runat="server" AppendDataBoundItems="true" 
                DataSourceID="AlbumListODS" DataTextField="Title"
                 DataValueField="AlbumId">
                <asp:ListItem Value="0">Select Album</asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:LinkButton ID="Fetch" runat="server">Fetch Tracks</asp:LinkButton>
            <asp:GridView ID="TrackListGV" runat="server" AutoGenerateColumns="False" DataSourceID="TracksDataODS" OnSelectedIndexChanged="TrackListGV_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="400px"></asp:BoundField>
                    <asp:BoundField DataField="Milliseconds" HeaderText="Milliseconds" SortExpression="Milliseconds"></asp:BoundField>
                     <asp:TemplateField HeaderText="Size">
                      <ItemTemplate>
                          <asp:Label ID="Bytes" runat="server" Text='<%# Eval("Bytes") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="$">
                      <ItemTemplate>
                          <asp:TextBox ID="UnitPrice" runat="server" 
                              Text='<%# string.Format("{0:0.00}", Eval("UnitPrice")) %>'
                               Width="75px"></asp:TextBox>
                      </ItemTemplate>
                  </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                </Columns>
                <EmptyDataTemplate>
                    No data to display at this time
                </EmptyDataTemplate>
            </asp:GridView>
            <br />
            <asp:LinkButton ID="WalkThroughGV" runat="server" OnClick="WalkThroughGV_Click">Walk Through</asp:LinkButton>
            <br /><br />
            <asp:ListView ID="TrackListLV" runat="server" 
                DataSourceID="TracksDataODS"
                 OnItemCommand="TrackListLV_ItemCommand">
                <EmptyDataTemplate>
                     No data to display at this time
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th runat="server"></th>
                                        <th runat="server">Name</th>
                                        <th runat="server">MSecs</th>
                                        <th runat="server">Bytes</th>
                                        <th runat="server">$</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                 <ItemTemplate>
                    <tr style=" color: #000000;">
                        <td>
                            <asp:Button runat="server" CommandName="Select" Text="Select" ID="SelectButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="Name" /></td>
                       
                        <td>
                            <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="Milliseconds" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="Bytes" /></td>
                        <td>
                            <asp:TextBox Text='<%# string.Format("{0:0.00}", Eval("UnitPrice")) %>' runat="server"
                                 ID="UnitPrice" /></td>

                    </tr>
                </ItemTemplate>
            </asp:ListView><br />
            <asp:LinkButton ID="WalkThroughLV" runat="server" OnClick="WalkThroughLV_Click" >Walk Through</asp:LinkButton>
        </asp:Panel>
        <asp:ObjectDataSource ID="TracksDataODS" runat="server" 
        DataObjectTypeName="ChinookSystem.Data.Entities.Track" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get_TracksForPlaylistSelection" 
        TypeName="ChinookSystem.BLL.TrackController"
            OnSelected="CheckForException">
        <SelectParameters>
            <asp:ControlParameter ControlID="AlbumList" PropertyName="Text" 
                Name="id" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="TracksBy" PropertyName="Text" 
                Name="fetchby" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Album_List" 
            TypeName="ChinookSystem.BLL.AlbumController"
             OnSelected="CheckForException">
        </asp:ObjectDataSource>
    </div>
</asp:Content>
