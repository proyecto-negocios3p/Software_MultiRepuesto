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
	Fecha DATETIME NULL DEFAULT GETDATETIME(),
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
	Contraseña VARCHAR(25)  NOT NULL,
	IdentidadEmpleado CHAR(15) NULL,
	Fecha DATETIME NULL DEFAULT GETDATE(),
	Estado BIT NULL DEFAULT 1, --1 ES ACTIVO, 2 ES INATIVO

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

--REFERENCIAS

--Paso 16 Empleado.CodigoCargo a Cargo.Codigo
ALTER TABLE Planilla.Empleado
	ADD CONSTRAINT
		FK_Planilla_Empleado$Genera$CodigoCargo
		FOREIGN KEY (CodigoCargo) REFERENCES Planilla.Cargo(Codigo)
		ON UPDATE NO ACTION 
		ON DELETE NO ACTION
GO

--paso 17 TelefonoEmplead.IdentidadEmpleado a Identidad.Empleado 
ALTER TABLE Planilla.TelefonoEmpleado
	ADD CONSTRAINT
	FK_Planilla_TelefonoEmpleado$Genera$IdentidadEmpleado
	FOREIGN KEY (IdentidadEmpleado) REFERENCES Planilla.Empleado(Identidad)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION 
GO

--Paso 18 HoraFaltada.IdentidadEmpleado a Empleado.Identidad
ALTER TABLE Planilla.HoraFaltada
	ADD CONSTRAINT
	FK_Planilla_HoraFaltada$Genera$IdentidadEmpleado
	FOREIGN KEY (IdentidadEmpleado) REFERENCES Planilla.Empleado(Identidad)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

-- Paso 19 HoraExtra.IdentidadEmpleado a Empleado.Identidad
ALTER TABLE Planilla.HoraExtra
	ADD CONSTRAINT 
	FK_Planilla_HoraExtra$Genera$IdentidadEmpleado
	FOREIGN KEY (IdentidadEmpleado)  REFERENCES Planilla.Empleado(Identidad)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

-- Paso 20 HoraExtra.CodigoPorcentajeHoraExtra a Codigo.PorcentajeHoraExtra 
ALTER TABLE Planilla.HoraExtra
	ADD CONSTRAINT
	FK_Planilla_HoraExtra$Genera$CodigoPorcentajeHoraExtra
	FOREIGN KEY (CodigoPorcentajeHoraExtra) REFERENCES Planilla.PorcentajeHoraExtra(Codigo)
	ON UPDATE NO ACTION 
	ON DELETE NO ACTION
GO

--Paso 21 PrestacionLaboral.IdentidadEmpleado a Identidad.Empleado
ALTER TABLE Planilla.PrestacionLaboral
	ADD CONSTRAINT
	FK_Planilla_PrestacionLaboral$Genera$IdentidadEmpleado
	FOREIGN KEY (IdentidadEmpleado) REFERENCES Planilla.Empleado(Identidad)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

--Paso 22 PrestacionLaboralEmbarazo.CodigoPrestacionLaboral a Codigo.PrestacionLaboral
ALTER TABLE Planilla.PrestacionLaboralEmbarazo
	ADD CONSTRAINT 
	FK_Planilla_PrestacionLaboralEmbarazo$Genera$CodigoPrestacionLaboral
	FOREIGN KEY (Codigo) REFERENCES Planilla.PrestacionLaboral(Codigo)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

--Paso 23 PlanillaFinal.IdentidadEmpleado a Identidad.Empleado
ALTER TABLE Planilla.PlanillaFinal
	ADD CONSTRAINT 
	FK_Planilla_PlanillaFinal$Genera$IdentidadEmpleado
	FOREIGN KEY (IdentidadEmpleado) REFERENCES Planilla.Empleado(Identidad)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
--Paso 23.1 Empleado.IdentidadEmpleado a Empleado.Identidad
ALTER TABLE Planilla.Usuario
	ADD CONSTRAINT
		FK_Planilla_Usuario$Genera$IdentidadEmpleado
		FOREIGN KEY (IdentidadEmpleado) REFERENCES Planilla.Empleado(Identidad)
		ON UPDATE NO ACTION 
		ON DELETE NO ACTION
GO


--Constraint para verificar datos
-- Paso 24 Constrian para la tabla Empleado
ALTER TABLE Planilla.Empleado WITH CHECK 
	ADD CONSTRAINT CHK_FormatoDeIdentidad$Para$Empleado_Identidad
		CHECK(Identidad LIKE'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO
-- Paso 25 Constrian para la tabla TelefonoEmpleado
ALTER TABLE Planilla.TelefonoEmpleado WITH CHECK 
	ADD CONSTRAINT CHK_FormatoDeIdentidad$Para$TelefonoEmpleado_IdentidadEmpleado
		CHECK(IdentidadEmpleado LIKE'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO
-- Paso 26 Constrian para la tabla HoraFaltada
ALTER TABLE Planilla.HoraFaltada WITH CHECK 
	ADD CONSTRAINT CHK_FormatoDeIdentidad$Para$HoraFaltada_IdentidadEmpleado
		CHECK(IdentidadEmpleado LIKE'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO
-- Paso 27 Constrian para la tabla HoraExtra
ALTER TABLE Planilla.HoraExtra WITH CHECK 
	ADD CONSTRAINT CHK_FormatoDeIdentidad$Para$HoraExtra_IdentidadEmpleado
		CHECK(IdentidadEmpleado LIKE'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO
-- Paso 28 Constrian para la tabla PrestacionLaboral
ALTER TABLE Planilla.PrestacionLaboral WITH CHECK 
	ADD CONSTRAINT CHK_FormatoDeIdentidad$Para$PrestacionLaboral_IdentidadEmpleado
		CHECK(IdentidadEmpleado LIKE'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO
-- Paso 29 Constrian para la tabla PrestacionLaboralEmbarazo
ALTER TABLE Planilla.PrestacionLaboralEmbarazo WITH CHECK 
	ADD CONSTRAINT CHK_FormatoDeIdentidad$Para$PrestacionLaboralEmbarazo_IdentidadEmpleado
		CHECK(IdentidadEmpleado LIKE'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO
-- Paso 30 Constrian para la tabla planillaFinal
ALTER TABLE Planilla.PlanillaFinal WITH CHECK 
	ADD CONSTRAINT CHK_FormatoDeIdentidad$Para$PlanillaFinal_IdentidadEmpleado
		CHECK(IdentidadEmpleado LIKE'[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO
-- Insertando valores en la Tabla PorcentajeHorasExtras
INSERT INTO Planilla.PorcentajeHoraExtra (Codigo, TipoHora)
VALUES 	('HD','Hora diurna'),
		('HN','Hora nocturna'),
		('HM','Hora mixta')
GO

--Inicio de Triggers

-- TRIGGER 1: Calcula el IHSS, RAP y los inserta en la planilla final
-- para luego actualizar el sueldo neto en la planilla final.

CREATE TRIGGER CalculoIHSSyRAP
ON Planilla.Empleado
AFTER INSERT AS
BEGIN
	SET NOCOUNT ON
	-- En caso de que el cliente haya modificado el conteo
	-- de fila para filas insertadas o actualizadas por el trigger
	SET ROWCOUNT 0

	-- Variable que contiene el mensaje de error
	DECLARE @msg VARCHAR(2000),
		    -- Filas afectadas por el trigger
			@Techo DECIMAL(10,2) =(SELECT Techo FROM Planilla.RAP),
			@Identidad CHAR(15) =(SELECT Identidad FROM inserted),
			@SueldoOrdinario DECIMAL(10,2)=(SELECT SueldoOrdinario FROM inserted),
			@RAP DECIMAL(10,2),
			@MaternidadYGastosMedicos DECIMAL(10,2) =(SELECT GastosMedicos FROM Planilla.IHSS),
			@SalarioTecho DECIMAL(10,2) =(SELECT SalarioTecho FROM Planilla.IHSS),
			@IHSS DECIMAL(10,2),
			@CalculoPrimerTecho DECIMAL(10,2),
			@CalculoSegundoTecho DECIMAL(10,2),
			@HorasFaltadas DECIMAL(10,2),
			@Horasextra DECIMAL(10,2),
			@SueldoNeto DECIMAL(10,2),
			@rowsAffected INT = (SELECT COUNT(*) FROM inserted)

	-- Si no existen filas afectadas no hay necesidad de continuar
	IF @rowsAffected = 0 RETURN;

	BEGIN TRY
		-- [sección de validación]
		
		IF (@SueldoOrdinario>@Techo)
		BEGIN 
		SET @RAP=((@SueldoOrdinario-@Techo)*0.015)
	
		
		END

		IF (@SueldoOrdinario<@Techo)
		BEGIN 
		SET @RAP=(0)
		
		END

		--Entra cuando el sueldo ordinario sea menor que el salario techo
		--y sabemos por ley  que se calculara un 5% del salario ordinario
		IF (@SueldoOrdinario<@SalarioTecho)
		BEGIN 
		SET @IHSS=(0.05*@SueldoOrdinario)
	

		END
		
		--Entra cuando el sueldo Ordinario es mayor al primer techo y si este cumple se calcula el 2.5%
		--luego verifica si el sueldo ordinario es menor que el segundo techo, si esta condicion cumple
		--toma el sueldo ordinario y lo multiplica por el 2.5 
			IF (@SueldoOrdinario>@SalarioTecho)
		BEGIN 
		SET @CalculoPrimerTecho=(0.025*@SalarioTecho)
	
				IF (@SueldoOrdinario<@MaternidadYGastosMedicos)
					BEGIN
					SET @CalculoSegundoTecho=(0.025*@SueldoOrdinario)
				
					END
		SET @IHSS=(@CalculoPrimerTecho+@CalculoSegundoTecho)
	

		END
		--Entra cuando el sueldo ordinario es mayor a los techos calculados por ley
		--entonces evalua si el sueldo ordinario es mayor que el primer techo(salariotecho)
		--si la condicion se cumple se multiplica por el 2.5%, despues entra a evaluar si el sueldo 
		--ordinario es mayor que el segundotecho(maternidadygastosmedicos) si la condicion se cumple
		--se multiplica sueldo ordinario por el 2.5

		 IF (@SueldoOrdinario>@SalarioTecho)
		BEGIN 
		SET @CalculoPrimerTecho=(0.025*@SalarioTecho)
		
			IF(@SueldoOrdinario>@MaternidadYGastosMedicos)
			BEGIN
			SET @CalculoSegundoTecho=(0.025*@MaternidadYGastosMedicos)
			
			END
			SET @IHSS=(@CalculoPrimerTecho+@CalculoSegundoTecho)
			END

			-- Ingresamos los datos calculados en el trigger anteriormente a la planilla fonal


        INSERT INTO Planilla.PlanillaFinal(IdentidadEmpleado,CodigoPlanillaFinal,IHSS,RAP,SueldoOrdinario)
		VALUES 	(@Identidad,GETDATE(),@IHSS,@RAP,@SueldoOrdinario)

		
		SET @HorasFaltadas=(SELECT HorasFaltadas FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @HorasExtra=(SELECT HorasExtras FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @SueldoNeto= (((((@SueldoOrdinario)-@IHSS)-@RAP)-(ISNULL(@HorasFaltadas,0))+(ISNULL(@HorasExtra,0))))


		UPDATE Planilla.PlanillaFinal
		SET PlanillaFinal.SueldoNeto = @SueldoNeto
		 WHERE PlanillaFinal.IdentidadEmpleado=@Identidad;
				-- [sección de modificacion]

			END TRY
			BEGIN CATCH
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

					THROW;
			END CATCH
END
GO



-- TRIGGER 2: Calcula las horas faltadas de un empleado y actualiza el sueldo neto en la planilla final.
CREATE TRIGGER CalculoHorasFaltadas
ON Planilla.HoraFaltada
AFTER INSERT AS
BEGIN
	SET NOCOUNT ON
	-- En caso de que el cliente haya modificado el conteo
	-- de fila para filas insertadas o actualizadas por el trigger
	SET ROWCOUNT 0

	-- Variable que contiene el mensaje de error
	DECLARE @msg VARCHAR(2000),
		    -- Filas afectadas por el trigger
			@Identidad VARCHAR(15) =(SELECT IdentidadEmpleado FROM inserted),
			@SueldoOrdinario DECIMAL(10,2),
			@HorasFaltadas INT ,
			@DeduccionesHorasFaltadas DECIMAL(12,2),
			@SueldoPorHora DECIMAL(10,2),
			@IHSS DECIMAL(10,2),
			@RAP DECIMAL(10,2),
			@HorasExtra DECIMAL(10,2),
			@SueldoNeto DECIMAL(10,2),
			@rowsAffected INT = (SELECT COUNT(*) FROM inserted)

	-- Si no existen filas afectadas no hay necesidad de continuar
	IF @rowsAffected = 0 RETURN;

	BEGIN TRY

	    -- Actualizamos  en planilla final con los calculos ya hechos

		SET @SueldoOrdinario=(SELECT Empleado.SueldoOrdinario FROM Planilla.Empleado WHERE Empleado.Identidad=@Identidad)
		SET @SueldoPorHora=((@SueldoOrdinario/30)/8)
		SET @HorasFaltadas=(SELECT sum(TotalHora) FROM HoraFaltada WHERE IdentidadEmpleado=@Identidad)
		SET @DeduccionesHorasFaltadas=(@HorasFaltadas*@SueldoPorHora)
		PRINT( @SueldoPorHora );
		PRINT'SUELDO HORAS';
			PRINT( @HorasFaltadas );
		UPDATE Planilla.PlanillaFinal
		SET PlanillaFinal.HorasFaltadas = @DeduccionesHorasFaltadas
		 WHERE PlanillaFinal.IdentidadEmpleado=@Identidad;


          -- Actualizamos  en planilla final con los calculos ya hechos
		 
		SET @IHSS=(SELECT IHSS FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @RAP=(SELECT RAP FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @HorasExtra=(SELECT HorasExtras FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @SueldoNeto= (((((@SueldoOrdinario)-@DeduccionesHorasFaltadas)-(ISNULL(@IHSS,0)))-(ISNULL(@RAP,0))+(ISNULL(@HorasExtra,0))))


		UPDATE Planilla.PlanillaFinal
		SET PlanillaFinal.SueldoNeto = @SueldoNeto
		 WHERE PlanillaFinal.IdentidadEmpleado=@Identidad;

	END TRY
		BEGIN CATCH
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

					THROW;
			END CATCH
END
GO


-- TRIGGER 3: Calcula las horas extras y actualiza la planilla final con los datos

CREATE TRIGGER CalculoHorasExtras
ON Planilla.HoraExtra
AFTER INSERT AS
BEGIN
	SET NOCOUNT ON
	-- En caso de que el cliente haya modificado el conteo
	-- de fila para filas insertadas o actualizadas por el trigger
	SET ROWCOUNT 0

	-- Variable que contiene el mensaje de error
	DECLARE @msg VARCHAR(2000),
		    -- Filas afectadas por el trigger
			@Identidad VARCHAR(15) =(SELECT IdentidadEmpleado FROM inserted),
			@SueldoOrdinario DECIMAL(10,2),
			@HorasDiurnas INT,
			@HorasNocturnas INT ,
			@HorasMixtas INT ,
			@SueldoporHorasDiurnas DECIMAL(10,2) ,
			@SueldoporHorasNocturnas DECIMAL(10,2) ,
			@SueldoporHorasMixtas DECIMAL(10,2) ,
			@TotalHorasExtra DECIMAL(10,2),
			@SueldoPorHora DECIMAL(10,2),
			@IHSS DECIMAL(10,2),
			@RAP DECIMAL(10,2),
			@HorasFaltadas DECIMAL(10,2),
			@SueldoNeto DECIMAL(10,2),
			@rowsAffected INT = (SELECT COUNT(*) FROM inserted)

	-- Si no existen filas afectadas no hay necesidad de continuar
	IF @rowsAffected = 0 RETURN;

	BEGIN TRY

		SET @SueldoOrdinario=(SELECT Empleado.SueldoOrdinario FROM Planilla.Empleado WHERE Empleado.Identidad=@Identidad)
		SET @HorasDiurnas=(SELECT SUM(TotalHora) FROM Planilla.HoraExtra WHERE IdentidadEmpleado=@Identidad AND CodigoPorcentajeHoraExtra='HD')
		SET @HorasNocturnas=(SELECT SUM(TotalHora) FROM Planilla.HoraExtra WHERE IdentidadEmpleado=@Identidad AND CodigoPorcentajeHoraExtra='HN')
		SET @HorasMixtas=(SELECT SUM(TotalHora) FROM Planilla.HoraExtra WHERE IdentidadEmpleado=@Identidad AND CodigoPorcentajeHoraExtra='HM' )

		SET @HorasDiurnas=(ISNULL(@HorasDiurnas,0))
		
		SET @HorasNocturnas=(ISNULL(@HorasNocturnas,0))
		
		SET @HorasMixtas=(ISNULL(@HorasMixtas,0))

		SET @SueldoPorHora=((@SueldoOrdinario/30)/8)
		
		SET @SueldoporHorasDiurnas=((@SueldoPorHora*@HorasDiurnas)*1.25)
				
		SET @SueldoporHorasNocturnas=((@SueldoPorHora*@HorasNocturnas)*1.50)
				
		SET @SueldoporHorasMixtas=((@SueldoPorHora*@HorasMixtas)*1.75)
		
		SET @TotalHorasExtra=(@SueldoporHorasDiurnas+@SueldoporHorasNocturnas)
		print @TotalHorasExtra
		
		UPDATE Planilla.PlanillaFinal
		SET PlanillaFinal.HorasExtras = @TotalHorasExtra
		 WHERE PlanillaFinal.IdentidadEmpleado=@Identidad;

		  
		SET @IHSS=(SELECT IHSS FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @RAP=(SELECT RAP FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @HorasFaltadas=(SELECT HorasFaltadas FROM Planilla.PlanillaFinal WHERE IdentidadEmpleado=@Identidad )
		SET @SueldoNeto= (((((@SueldoOrdinario)-(ISNULL(@HorasFaltadas,0))))-(ISNULL(@IHSS,0)))-(ISNULL(@RAP,0))+@TotalHorasExtra)


		UPDATE Planilla.PlanillaFinal
		SET PlanillaFinal.SueldoNeto = @SueldoNeto
		 WHERE PlanillaFinal.IdentidadEmpleado=@Identidad;


	END TRY
		BEGIN CATCH
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

					THROW;
			END CATCH
END
GO

--Fin Triggers