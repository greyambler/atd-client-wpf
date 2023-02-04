
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 01/27/2023 16:31:59
-- Generated from EDMX file: D:\MyProjects\C#\AnalysisrackData2022\ShMI.BaseMain\ModelDb.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    ALTER TABLE [CURRENT_TRANSACTION] DROP CONSTRAINT [FK_NCassaCURRENT_TRANSACTION];
GO
    ALTER TABLE [PUMP_TOTALS] DROP CONSTRAINT [FK_NCassaPUMP_TOTALS];
GO
    ALTER TABLE [READY_TRANSACTION] DROP CONSTRAINT [FK_NCassaREADY_TRANSACTION];
GO
    ALTER TABLE [TANK_READING] DROP CONSTRAINT [FK_NCassaTankReading_Cassa];
GO
    ALTER TABLE [NCassas] DROP CONSTRAINT [FK_NObjectNCassa];
GO
    ALTER TABLE [NStrunas] DROP CONSTRAINT [FK_NObjectNStruna];
GO
    ALTER TABLE [Task_Device] DROP CONSTRAINT [FK_NObjectTask_Device];
GO
    ALTER TABLE [NTanks] DROP CONSTRAINT [FK_NStrunaNTank];
GO
    ALTER TABLE [TankReadings] DROP CONSTRAINT [FK_NStrunaTankReading];
GO
    ALTER TABLE [SendMSGs] DROP CONSTRAINT [FK_NTankSendMSG];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [CURRENT_TRANSACTION];
GO
    DROP TABLE [NCassas];
GO
    DROP TABLE [NObjects];
GO
    DROP TABLE [NStrunas];
GO
    DROP TABLE [NTanks];
GO
    DROP TABLE [PUMP_TOTALS];
GO
    DROP TABLE [READY_TRANSACTION];
GO
    DROP TABLE [ReCodesTables];
GO
    DROP TABLE [SendMSGs];
GO
    DROP TABLE [TANK_READING];
GO
    DROP TABLE [TankReadings];
GO
    DROP TABLE [Task_Device];
GO
    DROP TABLE [TestTables];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CURRENT_TRANSACTION'
CREATE TABLE [CURRENT_TRANSACTION] (
    [Id] uniqueidentifier  NOT NULL,
    [SiteID] nvarchar(4000)  NULL,
    [ReferenceTime] datetime  NOT NULL,
    [DateCreatedLineCassa] datetime  NOT NULL,
    [RecordID] int  NOT NULL,
    [Pump] nvarchar(4000)  NULL,
    [Nozzle] nvarchar(4000)  NULL,
    [Grade] nvarchar(4000)  NULL,
    [Volume] nvarchar(4000)  NULL,
    [Value] nvarchar(4000)  NULL,
    [Price] nvarchar(4000)  NULL,
    [TranNumber] nvarchar(4000)  NULL,
    [NCassaId] uniqueidentifier  NOT NULL,
    [Type] nvarchar(4000)  NULL
);
GO

-- Creating table 'NCassas'
CREATE TABLE [NCassas] (
    [Id] uniqueidentifier  NOT NULL,
    [IP] nvarchar(100)  NOT NULL,
    [Port] int  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [DateStart] datetime  NULL,
    [NObjectId] uniqueidentifier  NOT NULL,
    [LastID] int  NOT NULL,
    [CountReadLine] int  NOT NULL,
    [NCassa_Active] bit  NOT NULL,
    [Cash_type] nvarchar(4000)  NULL,
    [NObjectId_Name] nvarchar(200)  NULL
);
GO

-- Creating table 'NObjects'
CREATE TABLE [NObjects] (
    [Id] uniqueidentifier  NOT NULL,
    [Name_Object] nvarchar(100)  NOT NULL,
    [SiteID] nvarchar(100)  NOT NULL,
    [Address] nvarchar(4000)  NULL
);
GO

-- Creating table 'NStrunas'
CREATE TABLE [NStrunas] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(4000)  NOT NULL,
    [IP] nvarchar(100)  NULL,
    [Port] int  NULL,
    [NObjectId] uniqueidentifier  NOT NULL,
    [NameBatFile] nvarchar(4000)  NULL,
    [Type_Level] nvarchar(4000)  NULL
);
GO

-- Creating table 'NTanks'
CREATE TABLE [NTanks] (
    [Id] uniqueidentifier  NOT NULL,
    [TankNumber] nvarchar(4000)  NOT NULL,
    [Grade] nvarchar(4000)  NOT NULL,
    [Grade_Name] nvarchar(4000)  NULL,
    [NStrunaId] uniqueidentifier  NOT NULL,
    [Max_Volume] nvarchar(4000)  NULL,
    [WaterLevel_Allow] nvarchar(4000)  NULL,
    [Water_alarm] nvarchar(10)  NULL,
    [Level_alarm] nvarchar(10)  NULL
);
GO

-- Creating table 'PUMP_TOTALS'
CREATE TABLE [PUMP_TOTALS] (
    [Id] uniqueidentifier  NOT NULL,
    [SiteID] nvarchar(4000)  NULL,
    [ReferenceTime] datetime  NOT NULL,
    [DateCreatedLineCassa] datetime  NOT NULL,
    [RecordID] int  NOT NULL,
    [Pump] nvarchar(4000)  NULL,
    [Nozzle] nvarchar(4000)  NULL,
    [Grade] nvarchar(4000)  NULL,
    [Volume] nvarchar(4000)  NULL,
    [Status] nvarchar(4000)  NULL,
    [StatusMsg] nvarchar(4000)  NULL,
    [NCassaId] uniqueidentifier  NOT NULL,
    [Type] nvarchar(4000)  NULL
);
GO

-- Creating table 'READY_TRANSACTION'
CREATE TABLE [READY_TRANSACTION] (
    [Id] uniqueidentifier  NOT NULL,
    [SiteID] nvarchar(4000)  NULL,
    [ReferenceTime] datetime  NOT NULL,
    [DateCreatedLineCassa] datetime  NOT NULL,
    [RecordID] int  NOT NULL,
    [Pump] nvarchar(4000)  NULL,
    [Nozzle] nvarchar(4000)  NULL,
    [Grade] nvarchar(4000)  NULL,
    [Volume] nvarchar(4000)  NULL,
    [Value] nvarchar(4000)  NULL,
    [Price] nvarchar(4000)  NULL,
    [TranNumber] int  NULL,
    [NCassaId] uniqueidentifier  NOT NULL,
    [Type] nvarchar(4000)  NULL
);
GO

-- Creating table 'ReCodesTables'
CREATE TABLE [ReCodesTables] (
    [Id] uniqueidentifier  NOT NULL,
    [GlobalCode] nvarchar(4000)  NOT NULL,
    [LocalCode] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'SendMSGs'
CREATE TABLE [SendMSGs] (
    [Id] uniqueidentifier  NOT NULL,
    [DateSendLast] datetime  NOT NULL,
    [Status] nvarchar(4000)  NOT NULL,
    [StatusMsg] nvarchar(4000)  NULL,
    [TypeWarn] nvarchar(4000)  NOT NULL,
    [WaterLevel] nvarchar(4000)  NULL,
    [NTankId] uniqueidentifier  NOT NULL,
    [TankNumber] nvarchar(4000)  NOT NULL,
    [Grade] nvarchar(4000)  NOT NULL,
    [Grade_Name] nvarchar(4000)  NULL
);
GO

-- Creating table 'TANK_READING'
CREATE TABLE [TANK_READING] (
    [Id] uniqueidentifier  NOT NULL,
    [SiteID] nvarchar(4000)  NOT NULL,
    [ReferenceTime] datetime  NOT NULL,
    [DateCreatedLineCassa] datetime  NOT NULL,
    [RecordID] int  NOT NULL,
    [TankNumber] nvarchar(4000)  NOT NULL,
    [Grade] nvarchar(4000)  NOT NULL,
    [Volume] nvarchar(4000)  NOT NULL,
    [FuelLevel] nvarchar(4000)  NOT NULL,
    [Temperature] nvarchar(4000)  NOT NULL,
    [FuelWeight] nvarchar(4000)  NOT NULL,
    [Density] nvarchar(4000)  NOT NULL,
    [WaterLevel] nvarchar(4000)  NOT NULL,
    [Status] nvarchar(4000)  NULL,
    [StatusMsg] nvarchar(4000)  NULL,
    [NCassaId] uniqueidentifier  NOT NULL,
    [Type] nvarchar(4000)  NULL
);
GO

-- Creating table 'TankReadings'
CREATE TABLE [TankReadings] (
    [Id] uniqueidentifier  NOT NULL,
    [SiteID] nvarchar(4000)  NOT NULL,
    [ReferenceTime] datetime  NOT NULL,
    [DateCreatedLineStruna] datetime  NOT NULL,
    [RecordID] nvarchar(4000)  NULL,
    [TankNumber] nvarchar(4000)  NOT NULL,
    [Grade] nvarchar(4000)  NOT NULL,
    [Volume] nvarchar(4000)  NOT NULL,
    [FuelLevel] nvarchar(4000)  NOT NULL,
    [Temperature] nvarchar(4000)  NOT NULL,
    [FuelWeight] nvarchar(4000)  NOT NULL,
    [Density] nvarchar(4000)  NOT NULL,
    [WaterLevel] nvarchar(4000)  NOT NULL,
    [Status] nvarchar(4000)  NULL,
    [StatusMsg] ntext  NULL,
    [NStrunaId] uniqueidentifier  NOT NULL,
    [Type] nvarchar(4000)  NULL
);
GO

-- Creating table 'Task_Device'
CREATE TABLE [Task_Device] (
    [Id] uniqueidentifier  NOT NULL,
    [Date_LastLine_Struna] datetime  NULL,
    [Date_LastLine_Cassa] datetime  NULL,
    [Name_Task] nvarchar(4000)  NOT NULL,
    [Period_Task] int  NOT NULL,
    [Type_Device] nvarchar(4000)  NOT NULL,
    [Zip_Dir] nvarchar(4000)  NOT NULL,
    [NObjectId] uniqueidentifier  NOT NULL,
    [CountDaySave] int  NOT NULL,
    [Period_Ping] int  NULL,
    [Is_TaskOk] bit  NULL,
    [Status] nvarchar(4000)  NULL,
    [StatusMsg] ntext  NULL,
    [Type_Task] nvarchar(4000)  NULL
);
GO

-- Creating table 'TestTables'
CREATE TABLE [TestTables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(4000)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CURRENT_TRANSACTION'
ALTER TABLE [CURRENT_TRANSACTION]
ADD CONSTRAINT [PK_CURRENT_TRANSACTION]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'NCassas'
ALTER TABLE [NCassas]
ADD CONSTRAINT [PK_NCassas]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'NObjects'
ALTER TABLE [NObjects]
ADD CONSTRAINT [PK_NObjects]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'NStrunas'
ALTER TABLE [NStrunas]
ADD CONSTRAINT [PK_NStrunas]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'NTanks'
ALTER TABLE [NTanks]
ADD CONSTRAINT [PK_NTanks]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'PUMP_TOTALS'
ALTER TABLE [PUMP_TOTALS]
ADD CONSTRAINT [PK_PUMP_TOTALS]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'READY_TRANSACTION'
ALTER TABLE [READY_TRANSACTION]
ADD CONSTRAINT [PK_READY_TRANSACTION]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'ReCodesTables'
ALTER TABLE [ReCodesTables]
ADD CONSTRAINT [PK_ReCodesTables]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'SendMSGs'
ALTER TABLE [SendMSGs]
ADD CONSTRAINT [PK_SendMSGs]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'TANK_READING'
ALTER TABLE [TANK_READING]
ADD CONSTRAINT [PK_TANK_READING]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'TankReadings'
ALTER TABLE [TankReadings]
ADD CONSTRAINT [PK_TankReadings]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Task_Device'
ALTER TABLE [Task_Device]
ADD CONSTRAINT [PK_Task_Device]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'TestTables'
ALTER TABLE [TestTables]
ADD CONSTRAINT [PK_TestTables]
    PRIMARY KEY ([Id] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [NCassaId] in table 'CURRENT_TRANSACTION'
ALTER TABLE [CURRENT_TRANSACTION]
ADD CONSTRAINT [FK_NCassaCURRENT_TRANSACTION]
    FOREIGN KEY ([NCassaId])
    REFERENCES [NCassas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NCassaCURRENT_TRANSACTION'
CREATE INDEX [IX_FK_NCassaCURRENT_TRANSACTION]
ON [CURRENT_TRANSACTION]
    ([NCassaId]);
GO

-- Creating foreign key on [NCassaId] in table 'PUMP_TOTALS'
ALTER TABLE [PUMP_TOTALS]
ADD CONSTRAINT [FK_NCassaPUMP_TOTALS]
    FOREIGN KEY ([NCassaId])
    REFERENCES [NCassas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NCassaPUMP_TOTALS'
CREATE INDEX [IX_FK_NCassaPUMP_TOTALS]
ON [PUMP_TOTALS]
    ([NCassaId]);
GO

-- Creating foreign key on [NCassaId] in table 'READY_TRANSACTION'
ALTER TABLE [READY_TRANSACTION]
ADD CONSTRAINT [FK_NCassaREADY_TRANSACTION]
    FOREIGN KEY ([NCassaId])
    REFERENCES [NCassas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NCassaREADY_TRANSACTION'
CREATE INDEX [IX_FK_NCassaREADY_TRANSACTION]
ON [READY_TRANSACTION]
    ([NCassaId]);
GO

-- Creating foreign key on [NCassaId] in table 'TANK_READING'
ALTER TABLE [TANK_READING]
ADD CONSTRAINT [FK_NCassaTankReading_Cassa]
    FOREIGN KEY ([NCassaId])
    REFERENCES [NCassas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NCassaTankReading_Cassa'
CREATE INDEX [IX_FK_NCassaTankReading_Cassa]
ON [TANK_READING]
    ([NCassaId]);
GO

-- Creating foreign key on [NObjectId] in table 'NCassas'
ALTER TABLE [NCassas]
ADD CONSTRAINT [FK_NObjectNCassa]
    FOREIGN KEY ([NObjectId])
    REFERENCES [NObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NObjectNCassa'
CREATE INDEX [IX_FK_NObjectNCassa]
ON [NCassas]
    ([NObjectId]);
GO

-- Creating foreign key on [NObjectId] in table 'NStrunas'
ALTER TABLE [NStrunas]
ADD CONSTRAINT [FK_NObjectNStruna]
    FOREIGN KEY ([NObjectId])
    REFERENCES [NObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NObjectNStruna'
CREATE INDEX [IX_FK_NObjectNStruna]
ON [NStrunas]
    ([NObjectId]);
GO

-- Creating foreign key on [NObjectId] in table 'Task_Device'
ALTER TABLE [Task_Device]
ADD CONSTRAINT [FK_NObjectTask_Device]
    FOREIGN KEY ([NObjectId])
    REFERENCES [NObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NObjectTask_Device'
CREATE INDEX [IX_FK_NObjectTask_Device]
ON [Task_Device]
    ([NObjectId]);
GO

-- Creating foreign key on [NStrunaId] in table 'NTanks'
ALTER TABLE [NTanks]
ADD CONSTRAINT [FK_NStrunaNTank]
    FOREIGN KEY ([NStrunaId])
    REFERENCES [NStrunas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NStrunaNTank'
CREATE INDEX [IX_FK_NStrunaNTank]
ON [NTanks]
    ([NStrunaId]);
GO

-- Creating foreign key on [NStrunaId] in table 'TankReadings'
ALTER TABLE [TankReadings]
ADD CONSTRAINT [FK_NStrunaTankReading]
    FOREIGN KEY ([NStrunaId])
    REFERENCES [NStrunas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NStrunaTankReading'
CREATE INDEX [IX_FK_NStrunaTankReading]
ON [TankReadings]
    ([NStrunaId]);
GO

-- Creating foreign key on [NTankId] in table 'SendMSGs'
ALTER TABLE [SendMSGs]
ADD CONSTRAINT [FK_NTankSendMSG]
    FOREIGN KEY ([NTankId])
    REFERENCES [NTanks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NTankSendMSG'
CREATE INDEX [IX_FK_NTankSendMSG]
ON [SendMSGs]
    ([NTankId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------