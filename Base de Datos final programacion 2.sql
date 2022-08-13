create database SellPoint
use SellPoint

DROP TABLE Entidades

CREATE TABLE Entidades (
	IdEntidades INT IDENTITY PRIMARY KEY NOT NULL,
	Descripcion VARCHAR(120) NOT NULL ,
	Direccion TEXT NOT NULL,
	Localidad VARCHAR(40) NOT NULL,
	TipoEntidad VARCHAR(8) DEFAULT('Juridica'),
	TipoDocumento VARCHAR(9) DEFAULT('RNC'),
	NumeroDocumento INT NOT NULL,
	Telefonos VARCHAR(60) NOT NULL,
	URLPaginaWeb VARCHAR(120),
	URLFacebook VARCHAR(120),
	URLInstagram VARCHAR(120),
	URLTwitter VARCHAR(120),
	URLTikTok VARCHAR(120),
	CodigoPostal VARCHAR(20),
	CoordenadasGPS VARCHAR(255),
	LimiteCredito INT,
	UserNameEntidad VARCHAR(60) NOT NULL,
	PasswordEntidad VARCHAR(60) NOT NULL,
	RolUserEntidad VARCHAR(10) DEFAULT('User'),
	Comentario TEXT,
	Status_ VARCHAR(10) DEFAULT('Activa'),
	NiEliminable BIT,
	FechaRegistro Date 
)

INSERT INTO ENTIDADES VALUES ('Description Test', 'Direccion Test', 'Localidad Test', 'Fisica', 'RNC', 1, '809-555-5555', 
								'WWW.TESING.COM', 'JOE DOE', '@JoeDoe', '@JoeDoe', '@JoeDoe', '101542', '1234665', 
								150000, 'Username', 'Password', 'User', 'Comentario test', 'Activa', 1, '8-5-2022')

CREATE INDEX EntidadesIndex
on Entidades(Descripcion, TipoEntidad, TipoDocumento, NumeroDocumento)

SELECT * FROM ENTIDADES