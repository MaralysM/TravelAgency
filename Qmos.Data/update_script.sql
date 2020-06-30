USE Qmos;
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'TX_FirstName')
ALTER TABLE [Security].[SM_USER] ADD TX_FirstName VARCHAR(20) NOT NULL DEFAULT '';
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'TX_SecondName')
ALTER TABLE [Security].[SM_USER] ADD TX_SecondName VARCHAR(20) NULL;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'TX_LastName')
ALTER TABLE [Security].[SM_USER] ADD TX_LastName VARCHAR(20) NOT NULL DEFAULT '';
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'TX_SecondLastName')
ALTER TABLE [Security].[SM_USER] ADD TX_SecondLastName VARCHAR(20) NULL ;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'TX_Phone')
ALTER TABLE [Security].[SM_USER] ADD TX_Phone VARCHAR(20) NULL;
GO

  IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'TX_FullName')
 ALTER TABLE [Qmos].[Security].[SM_USER] DROP COLUMN  TX_FullName
GO
IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'TX_UserName')
 ALTER TABLE [Qmos].[Security].[SM_USER] DROP COLUMN  TX_UserName
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER'  AND COLUMN_NAME = 'Id_PriceList')
    ALTER TABLE [Security].[SM_USER] ADD ID_PriceList INT NULL ;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_USER' 
AND COLUMN_NAME = 'DT_ValidDatePasswordRecoveryLink')
	ALTER TABLE [Qmos].[Security].[SM_USER] ADD [DT_ValidDatePasswordRecoveryLink] DATETIME NULL
GO

ALTER TABLE [Qmos].[Security].[SM_ROLE] ADD [BO_VisibleCliente] BIT DEFAULT 0
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Security' AND TABLE_NAME = 'SM_ROLE' AND COLUMN_NAME = 'BO_VisibleCliente')
BEGIN
	UPDATE [Qmos].[Security].[SM_ROLE] SET BO_VisibleCliente = 0
END


IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Dashboards')
BEGIN
INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Dashboards','fas fa-tachometer-alt','Dashboard' ,1
 ,2 ,0)
 END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Tap PPM -  Target PPM')
BEGIN
INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Tap PPM -  Target PPM','','Dashboard' , (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
END


IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Tap Temp -  Target Temp')
BEGIN
 INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Tap Temp -  Target Temp','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
 END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'TapWt - TapWtTarget')
BEGIN
  INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('TapWt - TapWtTarget','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'KWh per Scrap Ton')
BEGIN
   INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('KWh per Scrap Ton','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
 END

  IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Scrap Ton per Hour (POn)')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Scrap Ton per Hour (POn)','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
  END

    IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Iron Yield')
BEGIN
     INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Iron Yield','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
  END

    IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Delays (min per heat)')
BEGIN
      INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Delays (min per heat)','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
   END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Missed Heats')
BEGIN
INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Missed Heats','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
  END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Tap Temp and 02 PPM')
BEGIN
 INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Tap Temp and 02 PPM','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Average (per scrap ton)')
BEGIN
  INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Average (per scrap ton)','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
 END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Production')
BEGIN
   INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Production','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
 ,2 ,0)
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

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Scrap Ton per Hour (POn)') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Scrap Ton per Hour (POn)'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Iron Yield') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Iron Yield'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Delays (min per heat)') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Delays (min per heat)'))
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

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Average (per scrap ton)') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Average (per scrap ton)'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Production') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Production'))
END

