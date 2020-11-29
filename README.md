# Cards

<p>
  Платформа на WinForms. Позволяет сделать простую картотеку (операции чтения/записи и фильтрация данных) на основе структуры БД SQL Server.
</p>
<p>
  Требуется для запуска .Net Framework 4.0 (Windows XP+)
</p>

# Quick Start Guide

## Настройка БД

1. Скачать папку [SQL Server](SQLServer)
2. Выполнить в папке команду `docker-compose up -d`
3. Подключиться к БД используя данные для подключения из файла `docker-compose.yml`
4. Создать **тестовую** схему БД:
```SQL
create database TEST_DB
go

create table Users
(
	id int identity
		constraint User_pk
			primary key nonclustered,
	username nvarchar(50),
	email nvarchar(50),
	phone nvarchar(50)
)
go

create table Work
(
	id int identity
		constraint Work_pk
			primary key nonclustered,
	WorkName nvarchar(50)
)
go

create table UserWork
(
	id int identity
		constraint Position_pk
			primary key nonclustered,
	id_user int
		constraint Position_User_id_fk
			references Users,
	id_work int
		constraint Position_Work_id_fk
			references Work,
	date_from date,
	date_to date,
	position_name nvarchar(50)
)
go
```
где TEST_DB имя базы, Users таблица с пользователями, Work таблица организаций, UserWork таблица с данными о местах работы пользователей в организациях.

## Настройка программы CARDS

1. Создайте в любом месте файл `test.cards`
2. Запустите **Settings.exe** из файлов с программой. Откроется диалоговое окно выбора `*.cards` файла. Выберите ранее созданный файл `test.cards`
3. Откроется окно:
<br/> ![Settings 001](Images/Settings_001.png)
4. Необходимо нажать "Изменить настройки соединения" (1) и ввести данные для соединения с SQL Server (2), затем сохранить (3)
<br/> ![Settings 002](Images/Settings_002.png)
5. Загрузится список таблиц и полей из текущей схемы базы
<br/> ![Settings 003](Images/Settings_003.png)
6. Изменить параметры таблиц и полей как показано на следующих скриншотах
<br/> ![Settings 004](Images/Settings_004.png)
<br/> ![Settings 005](Images/Settings_005.png)
<br/> ![Settings 006](Images/Settings_006.png)
7. Далее для каждой из таблиц нужно создать Форму в "Дизайнере форм". Для этого нужно выбрать таблицу и нажать "Форма" -> "Редактировать текущую форму". На следующих скриншотах показаны настроенные формы для текущих таблиц.
<br/> ![Designer 001](Images/Designer_001.png)
<br/> ![Designer 002](Images/Designer_002.png)
<br/> ![Designer 003](Images/Designer_003.png)
