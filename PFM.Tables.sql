create database PFM_DB
use PFM_DB

-- This are Normal USers Tables 

Create table Roles
(
RoleId int identity(1,1) primary key,
RoleName nvarchar(100) not null,
RoleCode nvarchar(100) not null,
IsActive bit not null,
CreatedOn DateTime not null,
UpdatedOn DateTime 
)
select * from Roles


--------============================================================================================

create table Permission
(
PermissionId int identity (1,1) Primary key ,
PermissionName nvarchar(100) not null,
PermissionCode nvarchar(100) not null,
IsActive bit not null,
CreatedOn Datetime not null,
UpdatedOn datetime 
)
select * from Permission

--=========================================================================================================

create table Users
(
UserId Int Identity (1,1) Primary Key,
UserName Nvarchar(150) not null,
UserCode nvarchar (100) not null,
IsActive Bit Not null,
CreatedOn DateTime not null,
UpdatedOn DateTime
)
select * from Users

ALTER TABLE Users
ADD PasswordHash NVARCHAR(500) NULL;

-- this will be make thay usercode will be unique thay 2 person will be not use same code thay say (they already exists)
ALTER TABLE Users
ADD CONSTRAINT UQ_Users_UserCode UNIQUE (UserCode);

--===========================================================================================================
Create table UserRoles
(
UserRoleId int Identity (1,1) primary Key,
UserId int not null,
RoleId int not null,
IsActive bit Not null,
CreatedOn DateTime not null,
UpdatedOn DateTime 
)
select * from UserRoles

Alter table UserRoles 
Add constraint Fk_UserRoles_Users
Foreign key (UserId)
references Users(UserId)

alter table UserRoles
add constraint Fk_UserRoles_Roles
Foreign key (RoleId)
references Roles(RoleId)
--===========================================================================================================
Create table RolePermissions
(
RolePermissionId int identity(1,1) primary key,
RoleId int not null,
PermissionId int not null,
IsActive bit not null,
CreatedOn DateTime not null,
UpdatedOn DateTime ,
)

select * from RolePermissions


alter table RolePermissions
add Constraint Fk_RolePermissions_Roles
Foreign key (RoleId)
references Roles(RoleId)

Alter TAble RolePermissions
add Constraint Fk_RolePermissions_Permission
Foreign key(PermissionId)
References Permission(PermissionId)
--==============================================================================================================================================

-- THis Are Finance Tables


Create Table Accounts
(
AccountId int identity primary key,
UserId int not null,
AccountName nvarchar(200) not null,
AccountType nvarchar(200) not null,
CurrentBalance int not null,
IsActive bit not null,
CreatedOn datetime not null,
UpdatedOn datetime 
)
select * from Accounts

alter table Accounts 
add constraint Fk_Accounts_Users
Foreign key (UserId)
references Users(UserId)
--=================================================================================================================
create Table Categories
(
CategoriesId int identity(1,1) Primary key ,
UserId int not null,
CategoriesName nvarchar (100) not null,
CategoriesCode nvarchar(200) not null,
IsActive bit not null,
CreatedOn Datetime not null,
UpdatedOn datetime 
)
select * from Categories

alter table Categories
add constraint Fk_Categories_Users
Foreign key (UserId)
references Users(UserId)

--====================================================================================================================

Create Table Transactions 
(
TransactionId int identity (1,1)primary key ,
UserId int not null,
AccountId int not null,
CategoriesId int not null,
TransactionType nvarchar(300) not null,
Amount int not null,
TransactionDate DateTime not null,
Description nvarchar (500) ,
IsActive bit not null,
CreatedOn dateTime not null,
UpdatedOn Datetime
)
select * from Transactions

alter table Transactions
add constraint Fk_Transactions_Users
Foreign key (UserId)
references Users(UserId)

alter table Transactions
add constraint FK_Transactions_Accounts
Foreign key (AccountId)
references Accounts(AccountId)

alter table Transactions
add Constraint Fk_Transactions_Categories
foreign key (CategoriesId)
references Categories(CategoriesId)
--=======================================================================================================================
create table Budgets
(
BudgetId Int identity (1,1) Primary key,
UserId int not null,
CategoriesId int not null,
Amount int not null,
StartDate Date not null,
EndDate Date not null,
IsActive bit not null,
CreatedOn DateTime not null ,
UpdatedOn DateTime 
)
select * from Budgets

alter table Budgets
add Constraint FK_Budgets_Users
Foreign key (UserId)
references Users(UserId)

alter table Budgets
add Constraint FK_Budgets_Categories
Foreign key (CategoriesId)
references Categories(CategoriesId)

--====================================================================================================================

create table SavingsGoles
(
SavingsGolesId int Identity (1,1) primary key ,
UserId int not null,
GoleName nvarchar(200) not null,
TargetAmount int not null,
CurrentAmount int not null,
TargetDate Date ,
IsCompleted bit not null,
IsActive bit not null,
CreatedOn DateTime not null,
UPdatedOn DateTime,
)

Select * from SavingsGoles

alter  table SavingsGoles
add Constraint FK_SavingsGoles_Users
foreign key (UserId)
references Users(UserId)


--==================================================================================================================

create table Debts
(
DebtsId int Identity (1,1) primary key ,
UserId int not null,
DebtsName nvarchar(200) not null ,
ToltalAmount int not null,
RemainingAmount int not null,
InterestRate int ,
DueDate Date,
IsPaid bit  not null,
IsActive bit not null,
CreatedOn Datetime not null,
UpdatedOn DateTime ,
)

Select * from Debts

alter table Debts
add constraint FK_Debts_Users
Foreign key (UserId)
References Users(UserId)


--==================================================================================================================

create table Investments
(
InvestmentId INT IDENTITY(1,1) PRIMARY KEY,
UserId INT NOT NULL,
InvestmentName NVARCHAR(150) NOT NULL,
InvestmentType NVARCHAR(100) NOT NULL,
InvestedAmount int  NOT NULL,
CurrentValue int NOT NULL,
InvestmentDate DATE NOT NULL,
IsActive BIT NOT NULL,
CreatedOn Datetime not null,
UpdatedOn Datetime 
)
select * from Investments

alter table Investments
add constraint FK_Investments_Users
Foreign key (UserId)
References Users(UserId)

--===================================================================================================================


SELECT RoleId, RoleName, RoleCode, IsActive, CreatedOn, UpdatedOn
FROM dbo.Roles
ORDER BY RoleId;

