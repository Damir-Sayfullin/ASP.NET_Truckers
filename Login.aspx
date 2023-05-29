<%@ Page Title="Авторизация" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ASP.NET_Truckers.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="whitefont">
        <h2><%: Title %></h2>
		<hr />
		<p id="responseLabel" runat="server">Вы не авторизованы</p>
		<h5>Логин</h5>
	</div>
	<input id="loginField" runat="server" type="text" class="blackfont" style="width: 400px"/>
	<h5 class="whitefont">Пароль</h5>
	<input id="passwordField" runat="server" type="text" class="blackfont" style="width: 400px"/>
	<div runat="server" class="blackfont" id="button0"><br /><asp:Button runat="server" OnClick="Authorize" Text="Авторизоваться" Width="400px"/></div>
	<div runat="server" class="blackfont" id="button1">
		<br /><asp:Button runat="server" OnClick="button2_Click" Text="Сменить пользователя" Width="400px"/>
		<br /><br /><asp:Button runat="server" OnClick="button1_Click" Text="Открыть панель управления" Width="400px"/>
	</div>
</asp:Content>