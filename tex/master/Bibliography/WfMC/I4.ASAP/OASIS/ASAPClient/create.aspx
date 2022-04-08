<%@ Page language="c#" Codebehind="create.aspx.cs" AutoEventWireup="false" Inherits="ASAPClient.create" enableViewState="True" validateRequest="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ASAP Demo - Create Instance</title>
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
			<asp:Label id="Title" runat="server" CssClass="Header">Create Instance</asp:Label>
			<TABLE id="Table1" cellSpacing="10" cellPadding="0" width="700" border="0">
				<TR>
					<TD width="10"></TD>
					<TD width="95" class="RowHead" vAlign="top">
						<asp:Label id="Label1" runat="server">Name</asp:Label></TD>
					<TD class="RowTail">
						<asp:TextBox id="NameBox" runat="server" Width="100%"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD width="10"></TD>
					<TD width="95" class="RowHead" vAlign="top">
						<asp:Label id="Label2" runat="server">Subject</asp:Label></TD>
					<TD class="RowTail">
						<asp:TextBox id="SubjectBox" runat="server" Width="100%"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD width="10"></TD>
					<TD width="95" class="RowHead" vAlign="top">
						<asp:Label id="Label3" runat="server">Description</asp:Label></TD>
					<TD class="RowTail">
						<asp:TextBox id="DescriptionBox" runat="server" Width="100%" TextMode="MultiLine" Rows="4"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD width="10"></TD>
					<TD width="95" class="RowHead" vAlign="top">
						<asp:Label id="Label4" runat="server">Start?</asp:Label></TD>
					<TD class="RowTail">
						<asp:RadioButton id="StartYes" runat="server" Width="60px" Text="Yes" Checked="True" GroupName="StartNow"></asp:RadioButton>
						<asp:RadioButton id="StartNo" runat="server" Text="No" GroupName="StartNow"></asp:RadioButton></TD>
				</TR>
				<TR>
					<TD width="10"></TD>
					<TD class="RowHead" vAlign="top" width="95"></TD>
					<TD class="RowTail"></TD>
				</TR>
			</TABLE>
			<asp:Label id="Label5" runat="server" CssClass="Header">Context Data Schema</asp:Label>
			<asp:Table id="CSDTable" runat="server" CellSpacing="0" CellPadding="4" BorderWidth="0px" EnableViewState="False">
				<asp:TableRow>
					<asp:TableCell BackColor="Silver" Width="30"></asp:TableCell>
					<asp:TableCell>
						<asp:Label id="CDSchema" runat="server" Font-Names="Courier New"></asp:Label>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<asp:Label id="Label7" runat="server" CssClass="Header">Context Data</asp:Label>
			<asp:Table id="CDTable" runat="server" CellSpacing="0" CellPadding="4" BorderWidth="0px" EnableViewState="False"
				Width="700px">
				<asp:TableRow>
					<asp:TableCell BackColor="Silver" Width="30"></asp:TableCell>
					<asp:TableCell>
						<asp:TextBox id="CDData" runat="server" Font-Names="Courier New" Width="100%" TextMode="MultiLine"
							Rows="10"></asp:TextBox>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<TABLE id="Table3" cellSpacing="30" cellPadding="1" border="0">
				<TR>
					<TD align="center" width="60"></TD>
					<TD align="center">
						<asp:Button id="CreateButton" runat="server" Text="Create"></asp:Button></TD>
					<TD align="center">
						<asp:Button id="CancelButton" runat="server" Text="Cancel"></asp:Button></TD>
				</TR>
			</TABLE>
			<asp:Label id="ErrorBox" runat="server" Width="700px" Visible="False" ForeColor="Red">Label</asp:Label>
		</form>
	</body>
</HTML>
