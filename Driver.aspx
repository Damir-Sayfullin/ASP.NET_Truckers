<%@ Page Title="Водитель" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Driver.aspx.cs" Inherits="ASP.NET_Truckers.Driver" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="whitefont">
        <h2>Панель управления водителя</h2>
		<hr />
	    <h5 id="responseLabel" runat="server">Вы авторизованы как логист </h5>
    </div>
	<asp:Button id="buttonExit" OnClick="buttonExit_Click" runat="server" Text="« Назад" class="blackfont" Width="100px" /><br/>

    <hr class="whitefont"/>
    
    <asp:Button id="gridshow" runat="server" Text="Показать все грузы" class="blackfont" Width="400px" OnClick="gridshow_Click"/><br />
    <asp:Button id="gridhide" runat="server" Text="Скрыть все грузы" class="blackfont" Width="400px" OnClick="gridhide_Click" />
    <div id="GridViewShow" runat="server" class="blackfont"> 
        <br /><asp:GridView id="GridView1" class="whitefont" runat="server"></asp:GridView>
    </div>

    <hr class="whitefont"/>

    <div class="whitefont">
        <p id="errorMessage" runat="server" style="color: yellow"></p>
        <p>Выберие ID груза, чтобы получить информацию о грузе</p>
        <asp:DropDownList id="cargoID" class="blackfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cargoID_SelectedIndexChanged"/><br /><br />
        <p runat="server">ID водителя</p>
        <input id="cargoDriverID" runat="server" type="text" class="blackfont" style="width: 400px"/><br /><br />
        <p runat="server">Статус</p>
        <input id="cargoStatus" runat="server" type="text" class="blackfont" style="width: 400px"/><br /><br />
        <p runat="server">Наименование груза</p>
        <input id="cargoName" runat="server" type="text" class="blackfont" style="width: 400px"/><br /><br />
        <p runat="server">Масса груза в тоннах</p>
        <input id="cargoWeight" runat="server" type="text" class="blackfont" style="width: 400px"/><br /><br />
        <p runat="server">Пункт отправления</p>
        <input id="cargoFrom" runat="server" type="text" class="blackfont" style="width: 400px"/><br /><br />
        <p runat="server">Пункт назначения</p>
        <input id="cargoTo" runat="server" type="text" class="blackfont" style="width: 400px"/>
    </div>

    <hr class="whitefont" />

    <div class="blackfont">
        <asp:Button id="buttonCurrent" runat="server" Text="Текущий груз" Width="120px" OnClick="buttonCurrent_Click" />
        <asp:Button id="buttonAccept" runat="server" Text="Принять" Width="120px" OnClick="buttonAccept_Click" /><br />
        <asp:Button id="buttonCancel" runat="server" Text="Отказаться" Width="120px" OnClick="buttonCancel_Click" />
        <asp:Button id="buttonDelivery" runat="server" Text="Доставить" Width="120px" OnClick="buttonDelivery_Click" />
    </div>
</asp:Content>