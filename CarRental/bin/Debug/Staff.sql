CREATE DATABASE Staff
go

use Staff
go


select * from [Job]
go

select * from [EmployeeInfo]

CREATE TABLE [Role]
(
	RoleID int primary key identity,
	RoleName nvarchar(50) not null
)

CREATE TABLE [EStatus]
(
	EStatusID int primary key identity,
	EStatusName nvarchar(50) not null
)

CREATE TABLE [JStatus]
(
	JStatusID int primary key identity,
	JStatusName nvarchar(50) not null
)

CREATE TABLE [EmploymentType]
(
	EmploymentTypeID int primary key identity,
	EmploymentTypeName nvarchar(50) not null
)

CREATE TABLE [Department]
(
	DepartmentID int primary key identity,
	DepartmentName nvarchar(50) not null,
	DepartmentHead nvarchar(50) not null,
	DepartmentDescription nvarchar(50) not null
)


CREATE TABLE [TypeFile]
(
	TypeFileID int primary key identity,
	TypeFileName nvarchar(50) not null
)


CREATE TABLE [EmployeeInfo]
(
	EmployeeID int primary key identity,
	EmployeeRole int foreign key references [Role](RoleID) not null,
	EmployeeJob int foreign key references [Job](JobID) not null,
	EmployeeLogin nvarchar(max) not null,
	EmployeePassword nvarchar(max) not null,
	ESurname nvarchar(max) not null,
	EName nvarchar(max) not null,
	EPatronymic nvarchar(max) not null,
	Email nvarchar(max) not null,
	DateOfBirth datetime not null,
	Gender nvarchar(max) not null,
	EmploymentStartDate datetime not null,
	EmployeeStatus int foreign key references [EStatus](EStatusID) not null
)

select * from [EmployeeInfo]

CREATE TABLE [Job]
(
	JobID int primary key identity,
	JobTitle nvarchar(max) not null,
	JobDescription nvarchar(max) not null,
	JobDepartment int foreign key references [Department](DepartmentID) not null,
	JobSupervisor nvarchar(max) not null,
	JobSalary decimal not null,
	JobEmploymentType int foreign key references [EmploymentType](EmploymentTypeID) not null,
	JobStatus int foreign key references [JStatus](JStatusID) not null
)

CREATE TABLE [EFile]
(
	EFileID int primary key identity,
	EFileEmployee int foreign key references [EmployeeInfo](EmployeeID) not null,
	EFileName nvarchar(max) not null,
	EFileDescription nvarchar(max) not null,
	EFileType int foreign key references [TypeFile](TypeFileID) not null,
	EFileUploadDate datetime not null,
	EFileLastSaveDate datetime not null,
	EFileData varbinary(max) not null
)

select * from [EmployeeInfo]
