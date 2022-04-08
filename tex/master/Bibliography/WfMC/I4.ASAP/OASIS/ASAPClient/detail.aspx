<%@ Page language="c#" Codebehind="detail.aspx.cs" AutoEventWireup="false" Inherits="ASAPClient.detail" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ASAP Demo - Instance Details</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style>.RowHead { FONT-WEIGHT: bold; FONT-FAMILY: Arial; TEXT-ALIGN: left }
	.RowTail { FONT-FAMILY: Arial }
	BODY { FONT-FAMILY: Arial; BACKGROUND-COLOR: #c0c0c0 }
	.Header { FONT-WEIGHT: bold; FONT-SIZE: large; FONT-FAMILY: Arial; HEIGHT: 40px }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:label id="Title" runat="server" CssClass="Header">Instance Details</asp:label>
			<TABLE id="Table1" cellSpacing="10" cellPadding="0" width="700" border="0">
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95"><asp:label id="Label2" runat="server">Name</asp:label></TD>
					<TD class="RowTail" align="left"><asp:label id="Name" runat="server">Label</asp:label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95"><asp:label id="Label3" runat="server">Subject</asp:label></TD>
					<TD class="RowTail" align="left"><asp:label id="Subject" runat="server"> This is</asp:label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95"><asp:label id="Label4" runat="server">Description</asp:label></TD>
					<TD class="RowTail" align="left"><asp:label id="Description" runat="server">Label</asp:label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95">
						<asp:Label id="Label1" runat="server">State</asp:Label></TD>
					<TD class="RowTail" align="left">
						<asp:Label id="State" runat="server">Label</asp:Label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95">
						<asp:Label id="Label6" runat="server">Key</asp:Label></TD>
					<TD class="RowTail" align="left">
						<asp:Label id="Key" runat="server">Label</asp:Label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95">
						<asp:Label id="Label5" runat="server">Factory</asp:Label></TD>
					<TD class="RowTail" align="left">
						<asp:Label id="Factory" runat="server">Label</asp:Label></TD>
				</TR>
				<TR>
					<TD align="left" width="10"></TD>
					<TD class="RowHead" vAlign="top" align="left" width="95"></TD>
					<TD class="RowTail" align="left"></TD>
				</TR>
			</TABLE>
			<asp:Label id="Label7" runat="server" CssClass="Header">History</asp:Label>
			<asp:Table id="HistoryTable" runat="server" CellSpacing="0" CellPadding="4" BorderWidth="0px"
				EnableViewState="False">
				<asp:TableRow BackColor="Teal">
					<asp:TableCell BackColor="Silver" Width="30px"></asp:TableCell>
					<asp:TableCell Text="Time&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
					<asp:TableCell Text="Event&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
					<asp:TableCell Wrap="False" Text="State&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
					<asp:TableCell Wrap="False" Text="Source Key" CssClass="RowHead"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<asp:Label id="CDLabel" runat="server" CssClass="Header">Context Data</asp:Label>
			<asp:Table id="CDTable" runat="server" CellSpacing="0" CellPadding="4" BorderWidth="0px" EnableViewState="False">
				<asp:TableRow>
					<asp:TableCell BackColor="Silver" Width="30px"></asp:TableCell>
					<asp:TableCell>
						<asp:Label id="CD" Runat="server" Font-Names="Courier New"></asp:Label>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<asp:Label id="RDLabel" runat="server" CssClass="Header">Result Data</asp:Label>
			<asp:Table id="RDTable" runat="server" CellSpacing="0" CellPadding="4" BorderWidth="0px" EnableViewState="False">
				<asp:TableRow>
					<asp:TableCell BackColor="Silver" Width="30px"></asp:TableCell>
					<asp:TableCell>
						<asp:Label id="RD" Runat="server" Font-Names="Courier New"></asp:Label>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<TABLE id="Table3" cellSpacing="30" cellPadding="1" border="0">
				<TR>
					<TD align="center" width="60"></TD>
					<TD align="center">
						<asp:Button id="RefreshButton" runat="server" Text="Refresh"></asp:Button></TD>
					<TD align="center">
						<asp:Button id="BackButtn" runat="server" Text="Back"></asp:Button></TD>
					<TD align="center">
						<asp:Button id="StartButton" runat="server" Text="Start"></asp:Button></TD>
				</TR>
			</TABLE>
			The above information is the server's view of this instance.
		</form>
	</body>
</HTML>
