use SellPoint

GO
CREATE PROCEDURE ACCESO @USERNAME VARCHAR(60), @CONTRASENA VARCHAR(60)
AS
SELECT * FROM Entidades WHERE UserNameEntidad=@USERNAME AND PasswordEntidad=@CONTRASENA
GO
CREATE PROCEDURE GETALL
AS 
SELECT * FROM Entidades

GO
CREATE PROCEDURE REGISTRAR @USERNAME VARCHAR(60), @CONTRASENA VARCHAR(60)
AS 
INSERT INTO ENTIDADES VALUES ('Pendiente Requerido', 'Pendiente Requerido', '', '', '', 001, 'Pendiente Requerido', 
								'', '', '', '', '', '', '', 
								1, @USERNAME, @CONTRASENA, '', '', '', 1, GETDATE())

SELECT * FROM Entidades
GO

CREATE PROCEDURE GETONE @ID INT
AS 
SELECT * FROM Entidades WHERE IdEntidades=@ID
GO
CREATE PROCEDURE EDIT 
	@ID INT,
	@Descripcion VARCHAR(120),
	@Direccion TEXT,
	@Localidad VARCHAR(40),
	@TipoEntidad VARCHAR(8),
	@TipoDocumento VARCHAR(9),
	@NumeroDocumento INT,
	@Telefonos VARCHAR(60),
	@URLPaginaWeb VARCHAR(120),
	@URLFacebook VARCHAR(120),
	@URLInstagram VARCHAR(120),
	@URLTwitter VARCHAR(120),
	@URLTikTok VARCHAR(120),
	@CodigoPostal VARCHAR(20),
	@CoordenadasGPS VARCHAR(255),
	@LimiteCredito INT,
	@UserNameEntidad VARCHAR(60),
	@PasswordEntidad VARCHAR(60),
	@RolUserEntidad VARCHAR(10),
	@Comentario TEXT,
	@Status_ VARCHAR(10),
	@NiEliminable BIT,
	@FechaRegistro Date 
AS
	UPDATE Entidades
	SET Descripcion=@Descripcion, Direccion=@Direccion, Localidad=@Localidad, TipoEntidad=@TipoEntidad,
		TipoDocumento=@TipoDocumento, NumeroDocumento=@NumeroDocumento, Telefonos=@Telefonos, URLPaginaWeb=@URLPaginaWeb,
		URLFacebook=@URLFacebook, URLInstagram=@URLInstagram, URLTwitter=@URLTwitter, URLTikTok=@URLTikTok, 
		CodigoPostal=@CodigoPostal, CoordenadasGPS=@CoordenadasGPS, LimiteCredito=@LimiteCredito, 
		UserNameEntidad=@UserNameEntidad, PasswordEntidad=@PasswordEntidad, RolUserEntidad=@RolUserEntidad, Comentario=@Comentario,
		Status_=@Status_, NiEliminable=@NiEliminable, FechaRegistro=@FechaRegistro
	WHERE IdEntidades = @ID
GO
CREATE PROCEDURE DEL @ID INT
AS 
	DELETE FROM Entidades WHERE IdEntidades=@ID
GO

DROP PROCEDURE REGISTRAR


EXEC REGISTRAR 'Daury Rosario', '0202'
EXEC ACCESO 'Username', 'Password'
EXEC GETALL
EXEC GETONE 3
EXEC EDIT 3,'Description Test', 'Direccion Test', 'Localidad Test', 'Fisica', 'RNC', 1, '809-555-5555', 
								'WWW.TESING.COM', 'JOE DOE', '@JoeDoe', '@JoeDoe', '@JoeDoe', '101542', '1234665', 
								150000, 'Username', 'Password', 'User', 'Comentario test', 'Activa', 1, '8-5-2022'
EXEC DEL 4