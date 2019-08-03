/**/
-- Utilizar la base de datos por defecto
USE tempdb
GO

-- Eliminar la base de datos si existe
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'PlanillaDePagoMensual')
	BEGIN
		DROP DATABASE PlanillaDePagoMensual
	END
GO

-- Paso 1: Crear la base de datos
-- En este caso el nombre de la BD 
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'PlanillaDePagoMensual')
	BEGIN
		CREATE DATABASE PlanillaDePagoMensual
	END
GO

-- Paso 2: Seleccionar la base de datos
USE PlanillaDePagoMensual
GO

-- Paso 3: Crear el esquema a utilizar
CREATE SCHEMA Planilla
GO

-- Paso 4: Crear la tabla Empleado
CREATE TABLE Planilla.Empleado (
	Identidad CHAR(15) NOT NULL
		CONSTRAINT PK_Planilla_Empleado_Identidad
		PRIMARY KEY CLUSTERED (Identidad),
	CodigoCargo CHAR(6) NOT NULL,
	Nombre VARCHAR(30) NOT NULL,
	Apellido VARCHAR(30) NOT NULL,
	Genero CHAR(1) NOT NULL,
	SueldoOrdinario DECIMAL(10,2) NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE(),
	NivelAcademico CHAR(50)NOT NULL,
	Estado BIT NULL DEFAULT 1 --1 ACTIVO 0 INACTIVO

);
GO

-- Paso 5: Crear la tabla Cargo
CREATE TABLE Planilla.Cargo (
	Codigo CHAR(6) NOT NULL
		CONSTRAINT PK_Planilla_Cargo
		PRIMARY KEY CLUSTERED (Codigo),
	Nombre NVARCHAR(100) NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE()
);
GO

-- Paso 6: Crear la tabla TelefonoEmpleado
CREATE TABLE Planilla.TelefonoEmpleado (
	Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Planilla_TelefonoEmpleado_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	IdentidadEmpleado CHAR(15) NOT NULL,
	Telefono CHAR(9) NOT NULL,
	Fecha DATETIME NOT NULL
);
GO
-- Paso 7: Crear la tabla HoraFaltada
CREATE TABLE Planilla.HoraFaltada (
	Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Planilla_HoraFaltada_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	IdentidadEmpleado CHAR(15) NOT NULL,
	TotalHora INT NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE(),
	Motivo VARCHAR(50) NOT NULL
);
GO
-- Paso 8: Crear la tabla HoraExtra
CREATE TABLE Planilla.HoraExtra
    (Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Planilla_HoraExtra_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	IdentidadEmpleado CHAR(15) NOT NULL,
	TotalHora INT NOT NULL,
	CodigoPorcentajeHoraExtra CHAR(2) NOT NULL,
	Fecha DATETIME  NULL DEFAULT GETDATE()
);
GO

-- Paso 9: Crear la tabla PorcentajeHoraExtra
CREATE TABLE Planilla.PorcentajeHoraExtra 
    (Codigo CHAR(2) NOT NULL
		CONSTRAINT PK_Planilla_PorcentajeHoraExtra_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	TipoHora CHAR (20) NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE()
);
GO
-- Paso 10: Crear la tabla RAP
CREATE TABLE Planilla.RAP  (
	Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Planilla_RAP_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	Techo DECIMAL (10,2) NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE()
);
GO
-- Paso 11: Crear la tabla IHSS
CREATE TABLE Planilla.IHSS  
    (Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Planilla_IHSS_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	GastosMedicos DECIMAL (10,2) NOT NULL,
	SalarioTecho DECIMAL (10,2) NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE()
);
GO
 
-- Paso 12: Crear la tabla PlanillaFinal
CREATE TABLE Planilla.PlanillaFinal  
    (Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Planilla_PlanillaFinal_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	CodigoPlanillaFinal DATE NULL,
	IdentidadEmpleado CHAR(15)  NULL,
	SueldoOrdinario DECIMAL(10,2)  NULL,
	IHSS DECIMAL(10,2)  NULL,
	RAP DECIMAL(10,2)  NULL,
	HorasFaltadas DECIMAL(10,2)  NULL,
	HorasExtras DECIMAL(10,2)  NULL,
	SueldoNeto DECIMAL(10,2)  NULL,


);	
GO

-- Paso 13: Crear la tabla Usuarios
CREATE TABLE Planilla.Usuario 
	 (Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Usuario_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	Usuario VARCHAR(25)  NOT NULL,
	Contraseņa VARCHAR(25)  NOT NULL,
	IdentidadEmpleado CHAR(15) NULL,
	Fecha DATETIME NULL DEFAULT GETDATE()

);
GO

-- Paso 14: Crear la tabla PrestacionLaboral
CREATE TABLE Planilla.PrestacionLaboral
	 (Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Liquidacion_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	IdentidadEmpleado CHAR(15) NOT NULL,
    Cesantia DECIMAL(10,2)  NOT NULL,
	Vacaciones DECIMAL(10,2)  NOT NULL,
	DecimoTercero DECIMAL(10,2)  NOT NULL,
	DecimoCuarto DECIMAL(10,2)  NOT NULL,
	Motivo VARCHAR(50)  NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE()

);
GO

-- Paso 15: Crear la tabla PrestacionLaboralEmbarazo
CREATE TABLE Planilla.PrestacionLaboralEmbarazo
	 (Codigo INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_LiquidacionEmbarazo_Codigo
		PRIMARY KEY CLUSTERED (Codigo),
	IdentidadEmpleado CHAR(15) NOT NULL,
    Prenatal DECIMAL(10,2)  NOT NULL,
	Lactancia DECIMAL(10,2)  NOT NULL,
	Fecha DATETIME NULL DEFAULT GETDATE()

);
GO