<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="MessageUserControl.ascx.cs" 
    Inherits="DMIT2018Common.UserControls.MessageUserControl" %>

    <asp:Panel ID="MessagePanel" runat="server">
        <div class="panel-heading">
            <asp:Label ID="MessageTitleIcon" runat="server"> </asp:Label>
            <asp:Label ID="MessageTitle" runat="server" />
        </div>
        <div class="panel-body">
            <asp:Label ID="MessageLabel" runat="server" />
            <asp:Repeater ID="MessageDetailsRepeater" runat="server" EnableViewState="false">
                <headertemplate>
                    <ul>
                </headertemplate>
                <itemtemplate>
                    <li><%# Eval("Error") %></li>
                </itemtemplate>
                <footertemplate>
                    </ul>
                </footertemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>
