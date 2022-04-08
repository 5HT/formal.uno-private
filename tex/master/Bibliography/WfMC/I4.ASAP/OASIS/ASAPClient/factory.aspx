<%@ Page language="c#" Codebehind="factory.aspx.cs" AutoEventWireup="false" Inherits="ASAPClient.factory" EnableSessionState="True" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ASAP Demo - Factory Details</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style>.RowHead { FONT-WEIGHT: bold; FONT-FAMILY: Arial; TEXT-ALIGN: left }
	.RowTail { FONT-FAMILY: Arial }
	BODY { FONT-FAMILY: Arial; BACKGROUND-COLOR: #c0c0c0 }
	.Header { FONT-WEIGHT: bold; FONT-SIZE: large; FONT-FAMILY: Arial; HEIGHT: 40px }
	#Message { LEFT: 35px; POSITION: absolute }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:label id="Title" runat="server" CssClass="Header">Factory ASAPDemo</asp:label>
			<TABLE id="Table1" cellSpacing="10" cellPadding="0" width="700" border="0">
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95"><asp:label id="Label2" runat="server">Subject</asp:label></TD>
					<TD class="RowTail" align="left"><asp:label id="Subject" runat="server">Label</asp:label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95"><asp:label id="Label3" runat="server">Description</asp:label></TD>
					<TD class="RowTail" align="left"><asp:label id="Description" runat="server">This is a failrly long description of the demo ASAP server.  It needs to be long enough to wrap across several lines  Is it long enough yet?  I hope so!!This is a failrly long description of the demo ASAP server.  It needs to be long enough to wrap across several lines  Is it long enough yet?  I hope so!!This is a failrly long description of the demo ASAP server.  It needs to be long enough to wrap across several lines  Is it long enough yet?  I hope so!!</asp:label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95"><asp:label id="Label4" runat="server">Expiration</asp:label></TD>
					<TD class="RowTail" align="left"><asp:label id="Expiration" runat="server">Label</asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="30" cellPadding="1" border="0">
				<TR>
					<TD align="center" width="60"></TD>
					<TD align="center"><asp:button id="CreateButton" runat="server" Text="Create instance"></asp:button></TD>
					<TD align="center"><asp:button id="ListButton" runat="server" Text="List all instances"></asp:button></TD>
					<TD align="center"><asp:button id="ChangeButton" runat="server" Text="Change factory"></asp:button></TD>
				</TR>
			</TABLE>
			<asp:label id="Label5" runat="server" CssClass="Header">Your Instances&nbsp;&nbsp;</asp:label><asp:panel id="Panel1" runat="server">
				<asp:Table id="Table2" runat="server" EnableViewState="False" BorderWidth="0px" CellSpacing="0"
					CellPadding="4">
					<asp:TableRow BackColor="Teal">
						<asp:TableCell BackColor="Silver" Width="30px"></asp:TableCell>
						<asp:TableCell Text="Name&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
						<asp:TableCell Text="Subject&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
						<asp:TableCell Wrap="False" Text="State&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
						<asp:TableCell Width="20px"></asp:TableCell>
						<asp:TableCell></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
				<TABLE id="Table4" cellSpacing="30" cellPadding="1" border="0">
					<TR>
						<TD align="center" width="60"></TD>
						<TD align="center">
							<asp:Button id="RefreshButton" runat="server" Text="Refresh"></asp:Button></TD>
						<TD align="center">
							<asp:Button id="RemovelButton" runat="server" Text="Remove Checked"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel><br>
			<asp:label id="Message" runat="server">Label</asp:label>
			<br>
			<br>
			<span style="WIDTH:700px">The above information is what the client knows about the 
				instances you created in this browser session. The server was not contacted in 
				order to generate this page, though it does reflect any asynchronous 
				notifications the server sent this client.</span>
		</form>
	</body>
</HTML>
