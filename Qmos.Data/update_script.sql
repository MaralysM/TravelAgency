
/*******************************************************
           Insert   [Security].[SM_USER]
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_USER] WHERE TX_Email = 'ronner.velazquez@key-core.com')
BEGIN
    INSERT INTO [Security].[SM_USER] ([TX_Email],[TX_Password],[BO_Active],[BO_PasswordExpired],[TX_Link],[TX_FirstName],[TX_SecondName],[TX_LastName],[TX_SecondLastName],[TX_Phone],[ID_PriceList],[DT_ValidDatePasswordRecoveryLink])
     VALUES('ronner.velazquez@key-core.com','B2B0EBU0FJNECAMF1A2D66F63597D68DBDE933A0CC81694B0E8C6B798CAF0E3740A82A26A1B05E4F5F87502EA31610CB8348DA0920',1,0,123,'Ronner','','Velazquez','',435365365,1,NULL)
END


/*******************************************************           
                        Adjustment
********************************************************/

  if ((select cOUNT(ID_Element) from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and O2 PPM') > 1)
  begin 
    DELETE FROM [Security].[SM_ROLE_ELEMENT]  WHERE id_element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and O2 PPM')
    DELETE FROM [Qmos].[transition_parameters_details] WHERE id_element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and O2 PPM')
	DELETE FROM [Security].[SM_ELEMENT] WHERE TX_Name= 'MTD Tap Temp and O2 PPM'
  end

 if ((select cOUNT(ID_Element) from [Security].[SM_ELEMENT] where  TX_Name= 'Ton per Hour') > 1)
  begin 
    DELETE FROM [Security].[SM_ROLE_ELEMENT]  WHERE id_element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Ton per Hour')
    DELETE FROM [Qmos].[transition_parameters_details] WHERE id_element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Ton per Hour')
	DELETE FROM [Security].[SM_ELEMENT] WHERE TX_Name= 'Ton per Hour'
  end

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

IF EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'KWh per Scrap Ton')
BEGIN
Update [Security].[SM_ELEMENT]  set TX_Name = 'KWh per Ton' where TX_Name = 'KWh per Scrap Ton'
 END

  IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'KWh per Ton')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('KWh per Ton','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
  END


  IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Ton per Hour')
BEGIN
    INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Ton per Hour','','Dashboard' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
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

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Tap Temp and O2 PPM')
BEGIN
 INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('MTD Tap Temp and O2 PPM','','Dashboard/MTDTapTempandO2PPM' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards'),2 ,0)
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



   IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Update Time')
BEGIN
   INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Update Time','fas fa-refresh','UpdateTime' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management'),2 ,0)
  END



IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Transition Parameters')
BEGIN
   INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Transition Parameters','fa fa-history','TransitionParameters' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management'),2 ,0)
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

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'KWh per Ton') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'KWh per Ton'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Ton per Hour') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Ton per Hour'))
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

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and O2 PPM') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and O2 PPM'))
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

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Update Time') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Update Time'))
END

IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Transition Parameters') AND ID_Role =1)
BEGIN 
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Transition Parameters'))
END

/*******************************************************
           Update   [Security].[SM_ELEMENT] 
********************************************************/


UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = ('Dashboard/MTDProduction?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Production')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Production')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/MTDAverage'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Average')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = ('Dashboard/MTDTapTempandO2PPM?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and O2 PPM')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Tap Temp and O2 PPM')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Url] = 'Dashboard/MTDMissedHeats'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Missed Heats')
GO

UPDATE [Security].[SM_ELEMENT]
      SET [TX_Url] = ('Dashboard/MTDDelays?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Delays')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'MTD Delays')
GO

UPDATE [Security].[SM_ELEMENT]
      SET [TX_Url] = ('Dashboard/IronYield?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Iron Yield')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Iron Yield')
GO

UPDATE [Security].[SM_ELEMENT]
      SET [TX_Url] = ('Dashboard/TonPerHour?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Ton per Hour')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Ton per Hour')
GO

UPDATE [Security].[SM_ELEMENT]
      SET [TX_Url] = ('Dashboard/KWhPerScrapTon?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'KWh per Ton')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'KWh per Ton')
GO

UPDATE [Security].[SM_ELEMENT]
      SET [TX_Url] = ('Dashboard/TapWtTarget?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'TapWt - TapWtTarget')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'TapWt - TapWtTarget')
GO

UPDATE [Security].[SM_ELEMENT]
      SET [TX_Url] = ('Dashboard/TargetTemp?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap Temp -  Target Temp')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap Temp -  Target Temp')
GO

UPDATE [Security].[SM_ELEMENT]
      SET [TX_Url] = ('Dashboard/TargetPPM?id='+ Convert(varchar,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap PPM -  Target PPM')))
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Tap PPM -  Target PPM')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Icon] = 'fa fa-money-bill-alt'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Qmos')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Icon] = 'fa fa-cogs'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Icon] = 'fa fa-key'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Roles')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Icon] = 'fa fa-user'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Users')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Icon] = 'fa fa-bars'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Menu & Elements')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Icon] = 'fa fa-dashboard'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Dashboards')
GO

UPDATE [Security].[SM_ELEMENT]
   SET [TX_Icon] = 'fa fa-refresh'
 WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Update Time')
GO
/*******************************************************           
                        Management
********************************************************/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Qmos' AND TABLE_NAME = 'update_time' )
CREATE TABLE [Qmos].[update_time]
	(
	  id SMALLINT NOT NULL IDENTITY(1,1),
      time_refresh TIME(2) NULL
	)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Qmos' AND TABLE_NAME = 'transition_parameters_header' )
CREATE TABLE [Qmos].[transition_parameters_header]
	(
	  id SMALLINT NOT NULL IDENTITY(1,1),
      name VARCHAR(100) NULL,
	  active bit NULL,
	  PRIMARY KEY(id)
	)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Qmos' AND TABLE_NAME = 'transition_parameters_details' )
CREATE TABLE [Qmos].[transition_parameters_details]
	(
	  id SMALLINT NOT NULL IDENTITY(1,1),
	  id_transition_parameters_header SMALLINT NOT NULL,
	  time_transition TIME(2) NULL,
      order_transition SMALLINT NOT NULL,
	  id_element  INT NOT NULL,
	  PRIMARY KEY(id),
	  FOREIGN KEY (id_element) REFERENCES [Security].[SM_ELEMENT](id_element),
	  FOREIGN KEY (id_transition_parameters_header) REFERENCES [Qmos].[transition_parameters_header](id)
	)
GO

/*******************************************************           
                        Adjustment
********************************************************/
  UPDATE [Qmos].[Security].[SM_ELEMENT] 
   SET [BO_Default] = 1
 WHERE TX_Name= 'TapWt - TapWtTarget'

IF EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'MTD Tap Temp and 02 PPM')
BEGIN
 UPDATE [Qmos].[Security].[SM_ELEMENT] SET TX_Name = 'MTD Tap Temp and O2 PPM' WHERE [TX_Url] = 'Dashboard/MTDTapTempand02PPM'
 UPDATE [Qmos].[Security].[SM_ELEMENT] SET [TX_Url] = 'Dashboard/MTDTapTempandO2PPM' WHERE TX_Name = 'MTD Tap Temp and O2 PPM' 
END


IF EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Scrap Ton per Hour')
BEGIN
 UPDATE [Qmos].[Security].[SM_ELEMENT] SET TX_Name = 'Ton per Hour' WHERE [TX_Url] = 'Dashboard/ScrapTonPerHour'
 UPDATE [Qmos].[Security].[SM_ELEMENT] SET [TX_Url] = 'Dashboard/TonPerHour' WHERE TX_Name = 'Ton per Hour' 
END

/*******************************************************                                  
                Automatic transition
********************************************************/

IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Automatic Transition')
BEGIN
INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
 VALUES('Automatic Transition','fa fa-spinner','AutomaticTransition' ,1,2 ,0)
END

 IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Automatic Transition') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Automatic Transition'))
END

/*******************************************************                                  
                Reference Parameters
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_ELEMENT] WHERE TX_Name = 'Reference Parameters')
	BEGIN
	INSERT INTO [Security].[SM_ELEMENT]([TX_Name],[TX_Icon],[TX_Url],[ID_ElementParent] ,[ID_Type] ,[BO_Default])
	VALUES('Reference Parameters','fa fa-cog','ReferenceParameters' ,(select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Management'),2 ,0)
	END

     IF NOT EXISTS (SELECT * FROM [Security].[SM_ROLE_ELEMENT] WHERE ID_Element = (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Reference Parameters') AND ID_Role =1)
BEGIN  
INSERT INTO [Security].[SM_ROLE_ELEMENT]([ID_Role],[ID_Element]) VALUES
           (1, (select top 1 ID_Element from [Security].[SM_ELEMENT] where  TX_Name= 'Reference Parameters'))
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Qmos' AND TABLE_NAME = 'reference_Parameters')
 CREATE TABLE [Qmos].[reference_parameters]
	(
	  id SMALLINT NOT NULL IDENTITY(1,1),
	  id_element  INT NOT NULL,
	  reference DECIMAL(18,2) NULL, 
	  PRIMARY KEY(id),
	  FOREIGN KEY (id_element) REFERENCES [Security].[SM_ELEMENT](id_element)
	)
GO
