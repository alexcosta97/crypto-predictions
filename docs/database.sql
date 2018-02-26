CREATE TABLE dbo.Exchanges(
    ExchangeID INT IDENTITY(1,1) NOT NULL,
    ExchangeName VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Exchange PRIMARY KEY (ExchangeID)
);

CREATE TABLE dbo.Stats(
    StatID INT Identity(1,1) not null,
    StartDate DateTime,
    EndDate DateTime,
    ExchangeID Int,
    HighestLatestDiff Float,
    AvgDiff Float,
    AvgGrowthTime bigInt,
    AvgDeclineTime bigInt,
    Constraint PK_Stat Primary Key (StatID),
    Constraint FK_StatExchange Foreign Key (ExchangeID) REFERENCES Exchanges(ExchangeID)
);