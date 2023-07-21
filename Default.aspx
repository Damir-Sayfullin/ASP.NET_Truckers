<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NET_Truckers._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="whitefont">
        <h2>О проекте</h2>
        <hr />
        <p>Этот проект создан с использованием технологии ASP.NET</p>
        <p><b>Название:</b> Панель управления грузами Truckers</p>
        <p><b>Цель:</b> выполнение курсовой работы по дисциплине "Технологии программирования"</p>
        <p><b>Функции:</b> возможность авторизации для логиста или водителя. Панель управления зависит от должности. Источником данных о пользователях и грузах является база данных, расположенная в папке проекта <i>data/TruckersDB.mdb</i>.</p>
        <ul>
            <li>Возможности логиста:</li>
                <ol>
                    <li>Просматривать данные о грузах</li>
                    <li>Редактировать данные о грузах</li>
                    <li>Добавлять грузы</li>
                    <li>Удалять грузы</li>
                </ol>
            <li>Возможности водителя:</li>
                <ol>
                    <li>Просматривать информацию о текущем грузе</li>
                    <li>Получать груз</li>
                    <li>Отменять полученный груз</li>
                    <li>Доставлять груз</li>
                </ol>
        </ul>
        <hr />
        <asp:Button runat="server" class="blackfont" Text="Перейти &raquo;" Width="100px" OnClick="Button_Click"/>
    </div>
</asp:Content>
