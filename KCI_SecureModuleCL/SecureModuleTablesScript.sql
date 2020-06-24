USE [TimeSheet]
GO

/****** Object:  Schema [Security]    Script Date: 2/20/2020 6:59:02 PM ******/
DROP SCHEMA IF EXISTS [Security] 
GO

/****** Object:  Schema [Security]    Script Date: 2/20/2020 6:59:02 PM ******/
CREATE SCHEMA [Security]
GO

CREATE TABLE [Security].[SM_ELEMENT](
	[ID_Element] [int] IDENTITY(1,1) NOT NULL,
	[TX_Name] [varchar](255) NOT NULL,
	[TX_Icon] [varchar](50) NULL,
	[TX_Url] [varchar](255) NULL,
	[ID_ElementParent] [int] NOT NULL,
	[ID_Type] [int] NOT NULL,
 CONSTRAINT [PK_ELEMENTS] PRIMARY KEY CLUSTERED 
(
	[ID_Element] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Security].[SM_ELEMENT_TYPE]    Script Date: 2/20/2020 6:52:47 PM ******/
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
/****** Object:  Table [Security].[SM_ROLE]    Script Date: 2/20/2020 6:52:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[SM_ROLE](
	[ID_Role] [int] IDENTITY(1,1) NOT NULL,
	[TX_Role] [varchar](255) NOT NULL,
	[TX_Description] [varchar](max) NULL,
	[ID_Element] [int] NULL,
 CONSTRAINT [PK_SM_ROLES] PRIMARY KEY CLUSTERED 
(
	[ID_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Security].[SM_ROLE_ELEMENT]    Script Date: 2/20/2020 6:52:47 PM ******/
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
/****** Object:  Table [Security].[SM_ROLE_USER]    Script Date: 2/20/2020 6:52:47 PM ******/
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
/****** Object:  Table [Security].[SM_USER]    Script Date: 2/20/2020 6:52:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[SM_USER](
	[ID_User] [int] IDENTITY(1,1) NOT NULL,
	[TX_FullName] [varchar](255) NULL,
	[TX_Email] [varchar](255) NULL,
	[TX_Password] [varchar](255) NOT NULL,
	[BO_Active] [bit] NOT NULL,
	[BO_PasswordExpired] [bit] NOT NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Security].[SM_ELEMENT] ON 
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (1, N'Time Sheet', N'fa-calendar', N'http://keycore.fortiddns.com:8100', 10, 1)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (2, N'Dashboard', N'fa-panels', N'/Index', 1, 2)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (3, N'Imagen de Home', NULL, NULL, 2, 3)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (4, N'Tracking Repair', N'fa-lupa', N'http://keycore.fortiddns.com:8104', 10, 1)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (6, N'Boton Eliminar', N'fa-delete', N'img-home', 2, 3)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (7, N'Administracion', N'fa-config', NULL, 1, 2)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (8, N'Empleados', N'fa-user', NULL, 7, 2)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (9, N'Parametros', N'fa-cogs', NULL, 7, 2)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (10, N'Intranet', N'fa', NULL, 0, 1)
GO
INSERT [Security].[SM_ELEMENT] ([ID_Element], [TX_Name], [TX_Icon], [TX_Url], [ID_ElementParent], [ID_Type]) VALUES (18, N'Elemento 1', NULL, N'Url 1', 4, 3)
GO
SET IDENTITY_INSERT [Security].[SM_ELEMENT] OFF
GO
SET IDENTITY_INSERT [Security].[SM_ELEMENT_TYPE] ON 
GO
INSERT [Security].[SM_ELEMENT_TYPE] ([ID_Type], [TX_Type]) VALUES (1, N'Application')
GO
INSERT [Security].[SM_ELEMENT_TYPE] ([ID_Type], [TX_Type]) VALUES (2, N'Menu')
GO
INSERT [Security].[SM_ELEMENT_TYPE] ([ID_Type], [TX_Type]) VALUES (3, N'Item')
GO
SET IDENTITY_INSERT [Security].[SM_ELEMENT_TYPE] OFF
GO
SET IDENTITY_INSERT [Security].[SM_ROLE] ON 
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (7, N'Super Admin', NULL, 10)
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (8, N'User', NULL, 10)
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (10, N'Admin', NULL, 1)
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (11, N'User', NULL, 1)
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (12, N'Supervisor', NULL, 1)
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (13, N'Buyer', NULL, 4)
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (14, N'Admin', NULL, 4)
GO
INSERT [Security].[SM_ROLE] ([ID_Role], [TX_Role], [TX_Description], [ID_Element]) VALUES (15, N'Shipper', NULL, 4)
GO
SET IDENTITY_INSERT [Security].[SM_ROLE] OFF
GO
SET IDENTITY_INSERT [Security].[SM_ROLE_ELEMENT] ON 
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (37, 7, 1)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (38, 7, 2)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (39, 7, 3)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (40, 7, 4)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (41, 7, 6)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (42, 7, 7)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (43, 7, 8)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (44, 7, 9)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (45, 7, 10)
GO
INSERT [Security].[SM_ROLE_ELEMENT] ([ID_RoleElement], [ID_Role], [ID_Element]) VALUES (46, 7, 18)
GO
SET IDENTITY_INSERT [Security].[SM_ROLE_ELEMENT] OFF
GO
SET IDENTITY_INSERT [Security].[SM_ROLE_USER] ON 
GO
INSERT [Security].[SM_ROLE_USER] ([ID_UserRoleApplication], [ID_User], [ID_Role]) VALUES (2, 2, 7)
GO
SET IDENTITY_INSERT [Security].[SM_ROLE_USER] OFF
GO
SET IDENTITY_INSERT [Security].[SM_USER] ON 
GO
INSERT [Security].[SM_USER] ([ID_User],  [TX_Email],  [TX_Password], [BO_Active], [BO_PasswordExpired]) VALUES (2, N'ronner.velazquez@key-core.com', N'8BC05BF6A/CTKS+73CA07BE24F385233F27D4C567D45823AAD4F019CBD2A0EFF36D8BEE7CCAC99B5E162D279E0801C2E08775F75F0', 1, 0)
GO
SET IDENTITY_INSERT [Security].[SM_USER] OFF
GO
ALTER TABLE [Security].[SM_ELEMENT]  WITH CHECK ADD  CONSTRAINT [FK_SM_ELEMENT_SM_ELEMENT_TYPE] FOREIGN KEY([ID_Type])
REFERENCES [Security].[SM_ELEMENT_TYPE] ([ID_Type])
GO
ALTER TABLE [Security].[SM_ELEMENT] CHECK CONSTRAINT [FK_SM_ELEMENT_SM_ELEMENT_TYPE]
GO
ALTER TABLE [Security].[SM_ROLE]  WITH CHECK ADD  CONSTRAINT [FK_SM_ROLE_SM_ELEMENT] FOREIGN KEY([ID_Element])
REFERENCES [Security].[SM_ELEMENT] ([ID_Element])
GO
ALTER TABLE [Security].[SM_ROLE] CHECK CONSTRAINT [FK_SM_ROLE_SM_ELEMENT]
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

