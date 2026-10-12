-- SQL script to create the IssueTracker database and Tickets table

CREATE DATABASE IssueTracker;
GO

USE IssueTracker;
GO

CREATE TABLE dbo.Tickets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(1000) NOT NULL,

    Status VARCHAR(30) NOT NULL
        CONSTRAINT DF_Tickets_Status DEFAULT 'Open',

    Priority VARCHAR(30) NOT NULL,

    CONSTRAINT CK_Tickets_Status
        CHECK (Status IN ('Open', 'InProgress', 'Closed')),

    CONSTRAINT CK_Tickets_Priority
        CHECK (Priority IN ('Low', 'Normal', 'High'))
);
GO

--Use below insert to add sample data to the Tickets table

USE IssueTracker;

INSERT INTO dbo.Tickets (Title, Description, Status, Priority)
VALUES
    (N'Kan inte logga in',
     N'Användaren kan inte logga in i systemet.',
     'Open', 'High'),

    (N'Skrivaren fungerar inte',
     N'Skrivaren på kontoret skriver inte ut några dokument.',
     'InProgress', 'Normal'),

    (N'Lösenord återställt',
     N'Användaren har fått hjälp att återställa sitt lösenord.',
     'Closed', 'Low');