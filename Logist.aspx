<%@ Page Title="Логист" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logist.aspx.cs" Inherits="ASP.NET_Truckers.Logist" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="whitefont">
        <h2>Панель управления логиста</h2>
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
        <p>Выберие ID груза, чтобы получить информацию о грузе</p>
        <select id="cargoID" class="blackfont" runat="server"></select>
        <asp:Button id="buttonChoose" runat="server" Text="Выбрать" class="blackfont" Width="100px" OnClick="buttonChoose_Click" /><br /><br />
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
        <asp:Button id="buttonReload" runat="server" Text="Обновить" Width="100px" OnClick="buttonReload_Click" />
        <asp:Button id="buttonSave" runat="server" Text="Сохранить" Width="100px" OnClick="buttonSave_Click" /><br />
        <asp:Button id="buttonAdd" runat="server" Text="Добавить" Width="100px" OnClick="buttonAdd_Click" />
        <asp:Button id="buttonDelete" runat="server" Text="Удалить" Width="100px" OnClick="buttonDelete_Click" />
    </div>
</asp:Content>
