CREATE TABLE [dbo].[LOCALITY](
	[LocalityId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[PostalCode] [int] NOT NULL,
	
	CONSTRAINT [PK_LOCALITY] PRIMARY KEY CLUSTERED
)

CREATE TABLE [dbo].[ADDRESS](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[HouseNumber] [nvarchar](5) NOT NULL,
	[BoxNumber] [nvarchar](5) NULL,
	[LocalityIdAddress] [bigint] NOT NULL,
	
	CONSTRAINT [PK_ADDRESS] PRIMARY KEY CLUSTERED,
	CONSTRAINT [FK_LOCALITY] FOREIGN KEY([LocalityIdAddress])
		REFERENCES [dbo].[LOCALITY] ([LocalityId])
)

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[BirthDate] [date] NULL,
	[AddressIdUser] [bigint] NULL,
	[VerCol] [timestamp] NOT NULL,
	[AndroidToken] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (NULL) FOR [BirthDate]
GO

ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (NULL) FOR [AddressIdUser]
GO

ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_ADDRESS] FOREIGN KEY([AddressIdUser])
REFERENCES [dbo].[ADDRESS] ([AddressId])
GO

ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_ADDRESS]
GO

CREATE TABLE [dbo].[ORDER](
	[OrderNumber] [bigint] IDENTITY(1,1) NOT NULL,
	[State] [nvarchar](40) NOT NULL,
	[PickUpDate] [date] NOT NULL,
	[PickUpStartTime] [varchar](50) NOT NULL,
	[PickUpEndTime] [varchar](50) NOT NULL,
	[PickUpTime] [varchar](50) NULL,
	[DepositDate] [date] NOT NULL,
	[DepositStartTime] [varchar](50) NOT NULL,
	[DepositEndTime] [varchar](50) NOT NULL,
	[DepositTime] [varchar](50) NULL,
	[DeliveryType] [int] NOT NULL,
	[Price] [decimal](3, 2) NULL,
	[UserIdOrder] [nvarchar](450) NOT NULL,
	[CoursierIdOrder] [nvarchar](450) NULL,
	[PickUpAddress] [bigint] NOT NULL,
	[DepositAddress] [bigint] NOT NULL,
	[BillingAddress] [bigint] NOT NULL,
	[VerCol] [timestamp] NOT NULL,
	
	CONSTRAINT [PK_ORDER] PRIMARY KEY CLUSTERED,
	CONSTRAINT [FK_BILLING_ADDRESS] FOREIGN KEY([BillingAddress])
		REFERENCES [dbo].[ADDRESS] ([AddressId]),
	CONSTRAINT [FK_COURSIER] FOREIGN KEY([CoursierIdOrder])
		REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_DEPOSIT_ADDRESS] FOREIGN KEY([DepositAddress])
		REFERENCES [dbo].[ADDRESS] ([AddressId]),
	CONSTRAINT [FK_PICK_UP_ADDRESS] FOREIGN KEY([PickUpAddress])
		REFERENCES [dbo].[ADDRESS] ([AddressId]),
	CONSTRAINT [FK_USER] FOREIGN KEY([UserIdOrder])
		REFERENCES [dbo].[AspNetUsers] ([Id])
			ON DELETE CASCADE
)

CREATE TABLE [dbo].[LETTER](
	[LetterId] [bigint] IDENTITY(1,1) NOT NULL,
	[IsImportant] [bit] NOT NULL,
	[OrderNumberLetter] [bigint] NOT NULL,
	
	CONSTRAINT [PK_LETTER] PRIMARY KEY CLUSTERED,
	
	CONSTRAINT [FK_ORDER_LETTER] FOREIGN KEY([OrderNumberLetter])
		REFERENCES [dbo].[ORDER] ([OrderNumber])
			ON DELETE CASCADE
)

CREATE TABLE [dbo].[PARCEL](
	[ParcelId] [bigint] IDENTITY(1,1) NOT NULL,
	[ParcelType] [int] NOT NULL,
	[OrderNumberParcel] [bigint] NOT NULL,
	
	CONSTRAINT [PK_PARCEL] PRIMARY KEY CLUSTERED,
	CONSTRAINT [FK_ORDER_PARCEL] FOREIGN KEY([OrderNumberParcel])
		REFERENCES [dbo].[ORDER] ([OrderNumber])
			ON DELETE CASCADE
)	