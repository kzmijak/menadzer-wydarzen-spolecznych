USE master
GO

CREATE DATABASE MWS
GO
USE MWS
GO

CREATE TABLE dbo.Wydarzenie(
	id				int IDENTITY PRIMARY KEY,
	nazwa			varchar(64),
	opis			varchar(256),
	miejsce			varchar(64),
	dzien			date,
	godzina			time(0),
	budzet			Decimal(19,2)
	)

CREATE TABLE dbo.Sponsor(
	id				int IDENTITY PRIMARY KEY,
	nazwa			varchar(64),
	)

CREATE TABLE dbo.Dotacja(	
	id				int IDENTITY PRIMARY KEY,
	idwydarzenia	int,
	idsponsora		int,
	oczekiwania		varchar(256),
	kwota			Decimal(19,2),
	zatwierdzone	bit
	FOREIGN KEY (idsponsora)
	REFERENCES	dbo.Sponsor(id)
	ON UPDATE CASCADE
	ON DELETE CASCADE,

	FOREIGN KEY (idwydarzenia)
	REFERENCES  dbo.Wydarzenie(id)
	ON UPDATE CASCADE
	ON DELETE CASCADE
	)

CREATE TABLE dbo.Pracownik(
	id				int IDENTITY PRIMARY KEY,
	stanowisko		varchar(32),
	wynagrodzenie	Decimal(19,2)
)

CREATE TABLE dbo.Uczestnik(
	id			 int IDENTITY PRIMARY KEY,
	fid			 int
)

CREATE TABLE dbo.Kontakt(
	id				int IDENTITY PRIMARY KEY,
	imie			varchar(16),
	nazwisko		varchar(16),
	telefon			varchar(16),
	email			varchar(32),
	miejscowosc		varchar(16),
	nrdomu			varchar(8),
	miasto			varchar(32),
	poczta			varchar(6),
	ulica			varchar(16),
	idpracownika	int,
	iduczestnika	int

	FOREIGN KEY (idpracownika)
	REFERENCES	dbo.Pracownik(id)
	ON UPDATE	CASCADE
	ON DELETE	CASCADE,

	FOREIGN KEY (iduczestnika)
	REFERENCES	dbo.Uczestnik(id)
	ON UPDATE	CASCADE
	ON DELETE	CASCADE
)

CREATE TABLE dbo.KartaPlatnicza(
	id			int IDENTITY PRIMARY KEY,
	wlasciciel	varchar(32),
	numer		varchar(32),
	wygasniecie	varchar(5),
	kbezpiecz	varchar(3),
	kontakt		int

	FOREIGN KEY (kontakt)
	REFERENCES dbo.Kontakt(id)
	ON UPDATE CASCADE
	ON DELETE CASCADE
	)

CREATE TABLE dbo.Platnosc(
	id			int IDENTITY PRIMARY KEY,
	idkarty		int,
	idwydarzenia int,
	kwota		Decimal(19,2),
	dzien		date,
	godzina		time(0)

	FOREIGN KEY (idwydarzenia)
	REFERENCES dbo.Wydarzenie(id)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION,

	FOREIGN KEY (idkarty)
	REFERENCES dbo.KartaPlatnicza(id)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
	)

CREATE TABLE dbo.Logowanie(
	id				int IDENTITY PRIMARY KEY,
	login			varchar(16),
	haslo			varchar(32),
	idpracownika	int NULL, 
	idsponsora		int NULL,
	iduczestnika	int NULL

	FOREIGN KEY (idpracownika)
	REFERENCES	dbo.Pracownik(id)
	ON UPDATE	CASCADE
	ON DELETE	CASCADE,

	FOREIGN KEY (idsponsora)
	REFERENCES	dbo.Sponsor(id)
	ON UPDATE	CASCADE
	ON DELETE	CASCADE,

	FOREIGN KEY (iduczestnika)
	REFERENCES	dbo.Uczestnik(id)
	ON UPDATE	CASCADE
	ON DELETE	CASCADE

)

CREATE TABLE dbo.Bilet( -- MANY-TO-MANY  UCZESTNIK-BILET
	id			int IDENTITY PRIMARY KEY,
	idwydarzenia int,
	nazwa		varchar(32),
	cena		Decimal(19,2),
	opis		varchar(256)

	FOREIGN KEY (idwydarzenia)
	REFERENCES dbo.Wydarzenie(id)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)

CREATE TABLE dbo.Wiadomosc(
	id int IDENTITY PRIMARY KEY,
	idnadawcy int,
	idodbiorcy int,
	dzien	DATE,
	godzina	TIME,
	tytul	varchar(64),
	tresc	varchar(256),
	
	FOREIGN KEY (idnadawcy)
	REFERENCES dbo.Logowanie(id)
	ON DELETE NO ACTION
	ON UPDATE NO ACTION,

	FOREIGN KEY (idodbiorcy)
	REFERENCES dbo.Logowanie(id)
	ON DELETE NO ACTION
	ON UPDATE NO ACTION,
	)

CREATE TABLE dbo.Wniosek(
	id int IDENTITY PRIMARY KEY,
	idwiadomosci int,
	kwota	decimal(19,2),
	akcja	varchar(16),
	zatwierdzone	bit

	FOREIGN KEY (idwiadomosci)
	REFERENCES dbo.Wiadomosc(id)
	)

CREATE TABLE dbo.Wydarzenie_Sponsor(
	idwydarzenia	int,
	idsponsora		int
	
	FOREIGN KEY (idwydarzenia)
	REFERENCES dbo.Wydarzenie(id)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION,

	FOREIGN KEY (idsponsora)
	REFERENCES dbo.Sponsor(id)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
	)

CREATE TABLE dbo.Wydarzenie_Pracownik(
	idwydarzenia	int,
	idpracownika	int

	FOREIGN KEY (idwydarzenia)
	REFERENCES	dbo.Wydarzenie(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION,

	FOREIGN KEY (idpracownika)
	REFERENCES	dbo.Pracownik(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION
	)

CREATE TABLE dbo.Wydarzenie_Uczestnik(
	idwydarzenia	int,
	iduczestnika	int

	FOREIGN KEY (idwydarzenia)
	REFERENCES	dbo.Wydarzenie(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION,

	FOREIGN KEY (iduczestnika)
	REFERENCES	dbo.Uczestnik(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION
	)

CREATE TABLE dbo.Pracownik_Pracownik(
	idorganizatora	int,
	idpracownika	int

	FOREIGN KEY (idorganizatora)
	REFERENCES	dbo.Pracownik(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION,
	
	FOREIGN KEY (idpracownika)
	REFERENCES	dbo.Pracownik(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION
	)

CREATE TABLE dbo.Uczestnik_Bilet(
	iduczestnika	int NOT NULL,
	idbiletu		int NOT NULL,

	FOREIGN KEY (iduczestnika)
	REFERENCES	dbo.Uczestnik(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION,
	
	FOREIGN KEY (idbiletu)
	REFERENCES	dbo.Bilet(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION
	)

CREATE TABLE dbo.Logowanie_Logowanie(
	idlogowania1	int NOT NULL,
	idlogowania2	int NOT NULL,

	FOREIGN KEY (idlogowania1)
	REFERENCES	dbo.Logowanie(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION,
	
	FOREIGN KEY (idlogowania2)
	REFERENCES	dbo.Logowanie(id)
	ON UPDATE	NO ACTION
	ON DELETE	NO ACTION
	)

SET IDENTITY_INSERT dbo.Pracownik ON
INSERT INTO dbo.Pracownik ( id )
	   VALUES ( 0 )
	   SET IDENTITY_INSERT dbo.Pracownik OFF 
GO
SET IDENTITY_INSERT dbo.Sponsor ON
INSERT INTO dbo.Sponsor ( id )
		VALUES ( 0 )
SET IDENTITY_INSERT dbo.Sponsor OFF 
GO

SET IDENTITY_INSERT dbo.Uczestnik ON 
INSERT INTO dbo.Uczestnik ( id )
		VALUES ( 0 )
SET IDENTITY_INSERT dbo.Uczestnik OFF 
GO

