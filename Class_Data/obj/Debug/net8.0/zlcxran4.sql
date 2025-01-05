IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Medicines] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Manufacturer] nvarchar(max) NOT NULL,
    [Stock] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Medicines] PRIMARY KEY ([Id])
);

CREATE TABLE [Pharmacists] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Pharmacists] PRIMARY KEY ([Id])
);

CREATE TABLE [ContactInfos] (
    [Id] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ContactInfos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContactInfos_Pharmacists_Id] FOREIGN KEY ([Id]) REFERENCES [Pharmacists] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Prescriptions] (
    [Id] int NOT NULL IDENTITY,
    [PatientName] nvarchar(max) NOT NULL,
    [Quantity] int NOT NULL,
    [IssuedDate] datetime2 NOT NULL,
    [PharmacistId] int NOT NULL,
    CONSTRAINT [PK_Prescriptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Prescriptions_Pharmacists_PharmacistId] FOREIGN KEY ([PharmacistId]) REFERENCES [Pharmacists] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [MedicinePrescription] (
    [MedicinesId] int NOT NULL,
    [PrescriptionId] int NOT NULL,
    CONSTRAINT [PK_MedicinePrescription] PRIMARY KEY ([MedicinesId], [PrescriptionId]),
    CONSTRAINT [FK_MedicinePrescription_Medicines_MedicinesId] FOREIGN KEY ([MedicinesId]) REFERENCES [Medicines] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MedicinePrescription_Prescriptions_PrescriptionId] FOREIGN KEY ([PrescriptionId]) REFERENCES [Prescriptions] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_MedicinePrescription_PrescriptionId] ON [MedicinePrescription] ([PrescriptionId]);

CREATE INDEX [IX_Prescriptions_PharmacistId] ON [Prescriptions] ([PharmacistId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250102192504_Initial_start', N'9.0.0');

COMMIT;
GO

