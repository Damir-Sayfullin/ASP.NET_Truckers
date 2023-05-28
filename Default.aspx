<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NET_Truckers._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="whitefont">
        <h2><b>О проекте</b></h2>
        <hr />
        <p>Этот проект создан с использованием технологии ASP.NET.</p>
        <p><b>Название:</b> Панель управления грузами</p>
        <p><b>Цель:</b> выполнение курсовой работы по дисциплине "Технологии программирования".</p>
        <p><b>Функции:</b> регистрация и авторизация логиста или водителя. Панель управления зависит от должности.</p>
        <ul>
            <li>Логист</li>
                <ol>
                    <li>Редактировать данные о грузах</li>
                    <li>Добавлять грузы</li>
                    <li>Удалять грузы</li>
                </ol>
            <li>Водитель</li>
                <ol>
                    <li>Просматривать информацию о текущем грузе</li>
                    <li>Получать груз</li>
                    <li>Отменять полученный груз</li>
                    <li>Доставлять груз</li>
                </ol>
        </ul>
        <hr />
        <a class="btn btn-default" href="/Login">Войти &raquo;</a>
    </div>
</asp:Content>
