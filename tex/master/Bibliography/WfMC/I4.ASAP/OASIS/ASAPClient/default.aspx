<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="ASAPClient._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ASAP Demo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<STYLE>
		</STYLE>
	</HEAD>
	<body bgColor="#c0c0c0">
		<form id="Form1" method="post" runat="server">
			<DIV style="DISPLAY: inline; Z-INDEX: 101; LEFT: 32px; WIDTH: 700px; FONT-FAMILY: Arial; POSITION: relative"
				ms_positioning="FlowLayout">You are at the starting point for testing 
				implementations of the ASAP protocol.&nbsp; You may either use one of the 
				services provided below, or enter the URL of the desired service's factory and click 
				submit.</DIV>
			<br>
			<br>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 102; LEFT: 56px; POSITION: relative" runat="server"
				Width="496px"></asp:TextBox>
			<asp:Button id="Button1" style="Z-INDEX: 103; LEFT: 70px; POSITION: relative" runat="server"
				Text="Submit"></asp:Button><br>
			<br><br>
			<asp:Table id="Table1" runat="server" CellSpacing="0" CellPadding="4" BorderWidth="0px" EnableViewState="False">
				<asp:TableRow BackColor="Teal">
					<asp:TableCell BackColor="Silver" Width="30px"></asp:TableCell>
					<asp:TableCell Text="Service&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
					<asp:TableCell Text="URL&amp;nbsp;&amp;nbsp;" CssClass="RowHead"></asp:TableCell>
					<asp:TableCell Text="" CssClass="RowHead"></asp:TableCell>
				</asp:TableRow>
			</asp:Table><br>
			<br><br>
			<asp:Label id="Label1" style="Z-INDEX: 104; LEFT: 32px; POSITION: relative" runat="server"
				Width="100%" Height="96px" ForeColor="Red" Visible="False">Label</asp:Label>
		</form>
	</body>
</HTML>
