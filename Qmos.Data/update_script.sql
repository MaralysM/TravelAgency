
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



/*******************************************************
           Update   [Security].[SM_ELEMENT] 
********************************************************/


UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/MTDProduction'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Production')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/MTDAverage'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Average')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/MTDTapTempand02PPM'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and 02 PPM')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/MTDMissedHeats'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Missed Heats')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/MTDDelays'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Delays')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/IronYield'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Iron Yield')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/ScrapTonPerHour'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Scrap Ton per Hour')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/KWhPerScrapTon'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'KWh per Scrap Ton')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/TapWtTarget'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'TapWt - TapWtTarget')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/TargetTemp'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap Temp -  Target Temp')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/TargetPPM'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap PPM -  Target PPM')
GO




--IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Qmos' AND TABLE_NAME = 'update_time' )
----DROP SCHEMA IF EXISTS [Qmos] 
----GO
--USE [QMOS]
--GO
--CREATE SCHEMA [Qmos]
--GO

--CREATE TABLE [Qmos].[update_time]
--	(
--	  id SMALLINT NOT NULL IDENTITY(1,1),
--      time_refresh TIME(2) NULL
--	)
--GO
