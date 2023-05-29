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
        <select id="cargoID" class="blackfont" runat="server" name="D2"></select>
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
        <asp:Button id="buttonReload" runat="server" Text="Обновить" Width="100px" OnClick="gridhide_Click" />
        <asp:Button id="buttonSave" runat="server" Text="Сохранить" Width="100px" OnClick="gridhide_Click" /><br />
        <asp:Button id="buttonAdd" runat="server" Text="Добавить" Width="100px" OnClick="gridhide_Click" />
        <asp:Button id="buttonDelete" runat="server" Text="Удалить" Width="100px" OnClick="gridhide_Click" />
    </div>
    <!--
    <hr class="whitefont"/>
    
    <div>
        <br /> -->
      <!--
        <div class="auto-style24" >
            <p>Выберие товар для заказа</p>
            <select id="productsForStock" runat="server" class="auto-style5"></select>
            <br />
            <p>Количество</p>
            <input id="productCount" runat="server" type="number" value="0" min="0" max="1000" step="10" class="auto-style26" /><br />
            <p>Дата доставки</p>
            <input id="dateTimePicker" runat="server" type="text" class="auto-style7" /><br />
            <asp:Button runat="server" OnClick="NewStock" Text="Заказать" CssClass="auto-style4" Width="555px" />
            <br />
            <asp:Button ID="button1" runat="server" OnClick="SendNotification" Text="Уведомить магазин" CssClass="auto-style4" Width="558px" />
            <br />
            <p id="orderResponse" runat="server" />
        </div>

    </div>
    <br/>
	-->
    <script>
        function showMessage()
        {
            alert("Отправлено уведомление магазину!");
            return false;
        }
        function notificationsDeleted() {
            alert("Все уведомления удалены!");
            return false;
        }
    </script>
    <!--
    <script type="text/javascript">
        function reloadPage() {
            window.opener.location.reload();
            window.close();
        }
        setTimeout("reloadPage()", 1000); // This will wait for 1 second before refreshing the page
    </script>
    -->
</asp:Content>
