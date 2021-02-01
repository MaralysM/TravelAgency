CREATE DATABASE TravelAgency
GO

USE [TravelAgency]
GO
CREATE SCHEMA [Security]
GO
CREATE SCHEMA [TravelAgency]
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
	[TX_FirstName] [varchar](20) NOT NULL,
	[TX_SecondName] [varchar](20) NULL,
	[TX_LastName] [varchar](20) NOT NULL,
	[TX_SecondLastName] [varchar](20) NULL,
	[TX_Phone] [varchar](20) NULL
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
               [TravelAgency].[Travellers]
********************************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [TravelAgency].[Travellers](
	[ID_Travellers] [int] IDENTITY(1,1) NOT NULL,
	[TX_FirstName] [varchar](20) NOT NULL,
	[TX_SecondName] [varchar](20) NULL,
	[TX_LastName] [varchar](20) NOT NULL,
	[TX_SecondLastName] [varchar](20) NULL,
	[TX_Phone] [varchar](20) NULL,
	[TX_IdentificationCard] [varchar](20) NULL,
	[TX_Address] [varchar](500) NULL	
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[ID_Travellers] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [TravelAgency].[Travellers] ADD  DEFAULT ('') FOR [TX_FirstName]
GO

ALTER TABLE [TravelAgency].[Travellers] ADD  DEFAULT ('') FOR [TX_LastName]
GO

/*******************************************************
               [TravelAgency].[Travels]
********************************************************/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'TravelAgency' AND TABLE_NAME = 'Travels' )
CREATE TABLE [TravelAgency].[Travels]
	(
	  ID_Travels INT NOT NULL IDENTITY(1,1),
	  NU_TravelCode BIGINT NOT NULL,
	  NU_NumberOfPlace INT NOT NULL,
	  TX_Destination VARCHAR(100) NOT NULL,
	  TX_Origin VARCHAR(100)  NOT NULL,
	  NU_Price DECIMAL(18,2) NOT NULL
	  PRIMARY KEY (ID_Travels),
	)
GO
/*******************************************************
               [TravelAgency].[Requests]
********************************************************/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'TravelAgency' AND TABLE_NAME = 'Requests' )
CREATE TABLE [TravelAgency].[Requests]
	(
	  ID_Requests INT NOT NULL IDENTITY(1,1),
	  ID_Travels INT NOT NULL,
	  ID_Travellers INT NOT NULL,
	  PRIMARY KEY (ID_Requests),
	  FOREIGN KEY (ID_Travels) REFERENCES [TravelAgency].[Travels](ID_Travels),
	  FOREIGN KEY (ID_Travellers) REFERENCES [TravelAgency].[Travellers](ID_Travellers)

	)
GO
/*******************************************************
           Insert   [Security].[SM_USER]
********************************************************/
IF NOT EXISTS (SELECT * FROM [Security].[SM_USER] WHERE TX_Email = 'cliente@Travel-Agency.com')
BEGIN
    INSERT INTO [Security].[SM_USER] ([TX_Email],[TX_Password],[TX_FirstName],[TX_SecondName],[TX_LastName],[TX_SecondLastName],[TX_Phone])
     VALUES('cliente@Travel-Agency.com','B2B0EBU0FJNECAMF1A2D66F63597D68DBDE933A0CC81694B0E8C6B798CAF0E3740A82A26A1B05E4F5F87502EA31610CB8348DA0920','Usuario','','Administrador','',435365365)
END
