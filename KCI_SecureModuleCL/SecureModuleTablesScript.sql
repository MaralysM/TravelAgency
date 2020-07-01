CREATE DATABASE QMOS1
GO

--DROP SCHEMA IF EXISTS [Security] 
--GO
USE [QMOS1]
GO
CREATE SCHEMA [Security]
GO


/*******************************************************
                 [Security].[SM_ELEMENT_TYPE]
********************************************************/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Security].[SM_ELEMENT_TYPE](
	[ID_Type] [int] IDENTITY(1,1) NOT NULL,
	[TX_Type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SM_ELEMENTS_TYPES] PRIMARY KEY CLUSTERED 
(
	[ID_Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/*******************************************************
                  [Security].[SM_ELEMENT]
********************************************************/

CREATE TABLE [Security].[SM_ELEMENT](
	[ID_Element] [int] IDENTITY(1,1) NOT NULL,
	[TX_Name] [varchar](255) NOT NULL,
	[TX_Icon] [varchar](50) NULL,
	[TX_Url] [varchar](255) NULL,
	[ID_ElementParent] [int] NULL,
	[ID_Type] [int] NOT NULL,
	[BO_Default] [bit] NOT NULL,
 CONSTRAINT [PK_ELEMENTS] PRIMARY KEY CLUSTERED 
(
	[ID_Element] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Security].[SM_ELEMENT] ADD  CONSTRAINT [DF_SM_ELEMENT_BO_Activo]  DEFAULT ((0)) FOR [BO_Default]
GO

ALTER TABLE [Security].[SM_ELEMENT]  WITH CHECK ADD  CONSTRAINT [FK_SM_ELEMENT_SM_ELEMENT_TYPE] FOREIGN KEY([ID_Type])
REFERENCES [Security].[SM_ELEMENT_TYPE] ([ID_Type])
GO

ALTER TABLE [Security].[SM_ELEMENT] CHECK CONSTRAINT [FK_SM_ELEMENT_SM_ELEMENT_TYPE]
GO



/*******************************************************
                 [Security].[SM_ROLE]
********************************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Security].[SM_ROLE](
	[ID_Role] [int] IDENTITY(1,1) NOT NULL,
	[TX_Role] [varchar](255) NOT NULL,
	[TX_Description] [varchar](max) NULL,
	[ID_Element] [int] NULL,
	[BO_VisibleCliente] [bit] NULL,
 CONSTRAINT [PK_SM_ROLES] PRIMARY KEY CLUSTERED 
(
	[ID_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Security].[SM_ROLE] ADD  DEFAULT ((0)) FOR [BO_VisibleCliente]
GO

ALTER TABLE [Security].[SM_ROLE]  WITH CHECK ADD  CONSTRAINT [FK_SM_ROLE_SM_ELEMENT] FOREIGN KEY([ID_Element])
REFERENCES [Security].[SM_ELEMENT] ([ID_Element])
GO

ALTER TABLE [Security].[SM_ROLE] CHECK CONSTRAINT [FK_SM_ROLE_SM_ELEMENT]
GO

/*******************************************************
                 [Security].[SM_ROLE_ELEMENT]  
********************************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Security].[SM_ROLE_ELEMENT](
	[ID_RoleElement] [int] IDENTITY(1,1) NOT NULL,
	[ID_Role] [int] NOT NULL,
	[ID_Element] [int] NOT NULL,
 CONSTRAINT [PK_LINK_ROLES_ELEMENTS] PRIMARY KEY CLUSTERED 
(
	[ID_RoleElement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Security].[SM_ROLE_ELEMENT]  WITH CHECK ADD  CONSTRAINT [FK_SM_ROLE_ELEMENT_SM_ELEMENT] FOREIGN KEY([ID_Element])
REFERENCES [Security].[SM_ELEMENT] ([ID_Element])
GO

ALTER TABLE [Security].[SM_ROLE_ELEMENT] CHECK CONSTRAINT [FK_SM_ROLE_ELEMENT_SM_ELEMENT]
GO

ALTER TABLE [Security].[SM_ROLE_ELEMENT]  WITH CHECK ADD  CONSTRAINT [FK_SM_ROLE_ELEMENT_SM_ROLE] FOREIGN KEY([ID_Role])
REFERENCES [Security].[SM_ROLE] ([ID_Role])
GO

ALTER TABLE [Security].[SM_ROLE_ELEMENT] CHECK CONSTRAINT [FK_SM_ROLE_ELEMENT_SM_ROLE]
GO
/*******************************************************
               [Security].[SM_USER]
********************************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Security].[SM_USER](
	[ID_User] [int] IDENTITY(1,1) NOT NULL,
	[TX_Email] [varchar](255) NULL,
	[TX_Password] [varchar](255) NOT NULL,
	[BO_Active] [bit] NOT NULL,
	[BO_PasswordExpired] [bit] NOT NULL,
	[TX_Link] [varchar](255) NULL,
	[TX_FirstName] [varchar](20) NOT NULL,
	[TX_SecondName] [varchar](20) NULL,
	[TX_LastName] [varchar](20) NOT NULL,
	[TX_SecondLastName] [varchar](20) NULL,
	[TX_Phone] [varchar](20) NULL,
	[ID_PriceList] [int] NULL,
	[DT_ValidDatePasswordRecoveryLink] [datetime] NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Security].[SM_USER] ADD  DEFAULT ('') FOR [TX_FirstName]
GO

ALTER TABLE [Security].[SM_USER] ADD  DEFAULT ('') FOR [TX_LastName]
GO

/*******************************************************
                [Security].[SM_ROLE_USER] 
********************************************************/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Security].[SM_ROLE_USER](
	[ID_UserRoleApplication] [int] IDENTITY(1,1) NOT NULL,
	[ID_User] [int] NOT NULL,
	[ID_Role] [int] NOT NULL,
 CONSTRAINT [PK_SM_ROLES_USER_ELEMENTS] PRIMARY KEY CLUSTERED 
(
	[ID_UserRoleApplication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Security].[SM_ROLE_USER]  WITH CHECK ADD  CONSTRAINT [FK_SM_ROLE_USER_SM_ROLE] FOREIGN KEY([ID_Role])
REFERENCES [Security].[SM_ROLE] ([ID_Role])
GO

ALTER TABLE [Security].[SM_ROLE_USER] CHECK CONSTRAINT [FK_SM_ROLE_USER_SM_ROLE]
GO

ALTER TABLE [Security].[SM_ROLE_USER]  WITH CHECK ADD  CONSTRAINT [FK_SM_ROLE_USER_SM_USER] FOREIGN KEY([ID_User])
REFERENCES [Security].[SM_USER] ([ID_User])
GO

ALTER TABLE [Security].[SM_ROLE_USER] CHECK CONSTRAINT [FK_SM_ROLE_USER_SM_USER]
GO



/*******************************************************
           Insert  [Security].[SM_ELEMENT_TYPE]
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT_TYPE] WHERE TX_Type = 'Application')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT_TYPE] ([TX_Type])
     VALUES('Application')
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT_TYPE] WHERE TX_Type = 'Menu')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT_TYPE] ([TX_Type])
     VALUES('Menu')
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT_TYPE] WHERE TX_Type = 'Item')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT_TYPE] ([TX_Type])
     VALUES('Item')
END



/*******************************************************
           Insert   [Security].[SM_USER]
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_USER] WHERE TX_Email = 'ronner.velazquez@key-core.com')
BEGIN
    INSERT INTO [Security].[SM_USER] ([TX_Email],[TX_Password],[BO_Active],[BO_PasswordExpired],[TX_Link],[TX_FirstName],[TX_SecondName],[TX_LastName],[TX_SecondLastName],[TX_Phone],[ID_PriceList],[DT_ValidDatePasswordRecoveryLink])
     VALUES('ronner.velazquez@key-core.com','B2B0EBU0FJNECAMF1A2D66F63597D68DBDE933A0CC81694B0E8C6B798CAF0E3740A82A26A1B05E4F5F87502EA31610CB8348DA0920',1,0,123,'Ronner','','Velazquez','',435365365,1,NULL)
END

/*******************************************************
           Insert   [Security].[SM_ELEMENT] 
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Qmos')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
     VALUES('Qmos','fas fa-money-bill-alt','https://localhost:44344/' ,0,1 ,0)
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Management')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
     VALUES('Management','fas fa-cogs','#' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Qmos'),2 ,0)
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Roles')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
     VALUES('Roles','fas fa-key','Roles' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management'),2 ,0)
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Users')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
     VALUES('Users','fas fa-user','Users' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management'),2 ,0)
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Menu & Elements')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
     VALUES('Menu & Elements','fas fa-bars','Elements' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management'),2 ,0)
END




IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Dashboards')
BEGIN
INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Dashboards','fas fa-tachometer-alt','Dashboard' ,1,2 ,0)
 END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Tap PPM -  Target PPM')
BEGIN
INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Tap PPM -  Target PPM','','Dashboard' , (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
END


IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Tap Temp -  Target Temp')
BEGIN
 INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Tap Temp -  Target Temp','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
 END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'TapWt - TapWtTarget')
BEGIN
  INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('TapWt - TapWtTarget','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'KWh per Scrap Ton')
BEGIN
   INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('KWh per Scrap Ton','','Dashboard/KWhPerScrapTon' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
 END

  IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Scrap Ton per Hour')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Scrap Ton per Hour','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
  END

    IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Iron Yield')
BEGIN
     INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Iron Yield','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
  END

    IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Delays')
BEGIN
      INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Delays','','Dashboard/MTDDelays' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
   END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Missed Heats')
BEGIN
INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Missed Heats','','Dashboard/MTDMissedHeats' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
  END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Tap Temp and 02 PPM')
BEGIN
 INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Tap Temp and 02 PPM','','Dashboard/MTDTapTempand02PPM' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Average')
BEGIN
  INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Average','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
 END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Production')
BEGIN
   INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Production','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
  END


  /*******************************************************
           Insert   [Security].[SM_ROLE]
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE] WHERE TX_Role = 'Super Administrator')
BEGIN
    INSERT INTO [Security].[SM_ROLE] ([TX_Role],[TX_Description],[ID_Element],[BO_VisibleCliente])
     VALUES('Super Administrator','Site Administrator',1,1)
END

/*******************************************************
           Insert [Security].[SM_ROLE_USER]
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_USER] WHERE ID_User =1 and ID_Role=1)
BEGIN
    INSERT INTO [Security].[SM_ROLE_USER]([ID_User],[ID_Role])
     VALUES(1,1)
END
  /*******************************************************
           Insert   [Security].[SM_ROLE_ELEMENT]
********************************************************/
 IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management'))
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Roles') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Roles'))
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Users') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Users'))
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Menu & Elements') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Menu & Elements'))
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'))
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap PPM -  Target PPM') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap PPM -  Target PPM'))
END


IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap Temp -  Target Temp') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap Temp -  Target Temp'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'TapWt - TapWtTarget') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'TapWt - TapWtTarget'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'KWh per Scrap Ton') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'KWh per Scrap Ton'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Scrap Ton per Hour') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Scrap Ton per Hour'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Iron Yield') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Iron Yield'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Delays') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Delays'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Missed Heats') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Missed Heats'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and 02 PPM') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and 02 PPM'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Average') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Average'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Production') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Production'))
END

