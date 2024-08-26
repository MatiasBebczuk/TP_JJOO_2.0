-- Crear la base de datos
CREATE DATABASE JJOO;
GO

-- Usar la base de datos recién creada
USE JJOO;
GO

-- Crear tabla Deportes
CREATE TABLE Deportes (
    IdDeporte INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Foto VARCHAR(MAX) NULL
);
GO

-- Crear tabla Paises
CREATE TABLE Paises (
    IdPais INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Bandera VARCHAR(MAX) NULL
);
GO

-- Crear tabla Deportistas
CREATE TABLE Deportistas (
    IdDeportista INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    IdDeporte INT NOT NULL,
    IdPais INT NOT NULL,
    Imagen VARCHAR(MAX) NULL,
    FechaNacimiento DATE NOT NULL,
    CONSTRAINT FK_Deportistas_Deportes FOREIGN KEY (IdDeporte) REFERENCES Deportes(IdDeporte),
    CONSTRAINT FK_Deportistas_Paises FOREIGN KEY (IdPais) REFERENCES Paises(IdPais)
);
GO
