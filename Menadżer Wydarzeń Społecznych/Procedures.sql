USE master
GO

USE MWS
GO

CREATE PROCEDURE dbo.Wiadomosc_Insert
	@idnadawcy int,
	@idodbiorcy int,
	@dzien	DATE,
	@godzina	TIME,
	@tytul	varchar(64),
	@tresc	varchar(256)
AS
BEGIN
	INSERT INTO dbo.Wiadomosc (
		idnadawcy,
		idodbiorcy,
		dzien,	
		godzina,
		tytul,
		tresc	 
	)
	OUTPUT INSERTED.*
	VALUES (
		@idnadawcy,
		@idodbiorcy,
		@dzien,		 
		@godzina,	
		@tytul,
		@tresc	 
	)
END
GO

CREATE PROCEDURE dbo.Wiadomosc_Update
	@id	int,
	@idnadawcy int,
	@idodbiorcy int,
	@dzien	DATE,
	@godzina	TIME,
	@tytul	varchar(64),
	@tresc	varchar(256)
AS
BEGIN
	UPDATE dbo.Wiadomosc
	SET
	idnadawcy  = @idnadawcy,
	idodbiorcy  = @idodbiorcy,
	dzien 		  = @dzien,
	godzina 	  = @godzina,
	tytul		  = @tytul,
	tresc		  = @tresc
	WHERE id = @id
	
END
GO

CREATE PROCEDURE dbo.Wiadomosc_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Wiadomosc
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Wiadomosc_GetRecord
	@idnadawcy int,
	@idodbiorcy int,
	@dzien	DATE,
	@godzina	TIME,
	@tytul	varchar(64),
	@tresc	varchar(256)
AS
BEGIN
	SELECT * FROM dbo.Wiadomosc
	WHERE 
		idnadawcy = @idnadawcy
	AND	idodbiorcy = @idodbiorcy
	AND	dzien 		  = @dzien
	AND	godzina 	  = @godzina
	AND tytul		  = @tytul
	AND	tresc 		  = @tresc
END
GO

CREATE PROCEDURE dbo.Wiadomosc_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Wiadomosc
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Wiadomosc_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Wiadomosc
END
GO

GO

CREATE PROCEDURE dbo.Bilet_Insert
	@idwydarzenia	int,
	@nazwa		varchar(32),
	@cena		decimal(19,2),
	@opis		varchar(256)
AS
BEGIN
	INSERT INTO dbo.Bilet (	idwydarzenia, nazwa, opis, cena )
	OUTPUT INSERTED.*
	VALUES (@idwydarzenia, @nazwa, @opis, @cena)
END
GO

CREATE PROCEDURE dbo.Bilet_Update
	@id			int,
	@idwydarzenia int,
	@nazwa		varchar(32),
	@cena		decimal(19,2),
	@opis		varchar(256)
AS
BEGIN
	UPDATE dbo.Bilet
	SET idwydarzenia = @idwydarzenia,
		nazwa = @nazwa,
		cena  = @cena,
		opis  = @opis
	WHERE
		id = @id
END
GO

CREATE PROCEDURE dbo.Bilet_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Bilet
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Bilet_GetRecord
	@idwydarzenia	int,
	@nazwa		varchar(32),
	@cena		decimal(19,2),
	@opis		varchar(256)
AS
BEGIN
	SELECT * FROM dbo.Bilet
	WHERE idwydarzenia = @idwydarzenia
	AND	nazwa = @nazwa 
	AND cena = @cena
	AND opis = opis
END
GO

CREATE PROCEDURE dbo.Bilet_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Bilet
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Bilet_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Bilet
END
GO


CREATE PROCEDURE dbo.Dotacja_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Dotacja
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Dotacja_Insert
	@idwydarzenia	int NULL,
	@idsponsora		int NULL,
	@oczekiwania	varchar(256),
	@kwota			decimal(19,2),
	@zatwierdzone	bit
AS
BEGIN
	INSERT INTO dbo.Dotacja(
			 idwydarzenia,
			 idsponsora	,
			 oczekiwania,
			 kwota		,
			 zatwierdzone
			 )
	OUTPUT INSERTED.*
	VALUES ( @idwydarzenia,
			 @idsponsora,
			 @oczekiwania,
			 @kwota,		
			 @zatwierdzone
			 )
END
GO

CREATE PROCEDURE dbo.Dotacja_Update
	@id				int,
	@idwydarzenia	int NULL,
	@idsponsora		int NULL,
	@oczekiwania	varchar(256),
	@kwota			decimal(19,2),
	@zatwierdzone	bit
AS
BEGIN
	UPDATE dbo.Dotacja
	SET 
	idwydarzenia = @idwydarzenia,
	idsponsora	 = @idsponsora,	
	oczekiwania	 = @oczekiwania,
	kwota		 = @kwota,		
	zatwierdzone = @zatwierdzone
	WHERE
	id = @id
END
GO

CREATE PROCEDURE dbo.Dotacja_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Dotacja
	WHERE id = @id

END
GO

CREATE PROCEDURE dbo.Dotacja_GetRecord
	@idwydarzenia	int NULL,
	@idsponsora		int NULL,
	@oczekiwania	varchar(256),
	@kwota			decimal(19,2),
	@zatwierdzone	bit
AS
BEGIN
 SELECT * FROM dbo.Dotacja
 WHERE idwydarzenia =  @idwydarzenia
 AND   idsponsora	 = @idsponsora
 AND   oczekiwania	 = @oczekiwania
 AND   kwota		 = @kwota
 AND   zatwierdzone =  @zatwierdzone
END
GO

CREATE PROCEDURE dbo.Dotacja_GetCollection
AS
BEGIN
 SELECT * FROM dbo.Dotacja
END
GO


CREATE PROCEDURE dbo.KartaPlatnicza_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.KartaPlatnicza
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.KartaPlatnicza_Insert
	@wlasciciel		varchar(32),
	@numer			varchar(32),
	@wygasniecie	varchar(5),
	@kbezpiecz		varchar(3),
	@kontakt		int
AS
BEGIN
	INSERT INTO dbo.KartaPlatnicza
		(wlasciciel,
		numer,
		wygasniecie,
		kbezpiecz,
		kontakt	)
	OUTPUT INSERTED.*
	VALUES 
		(@wlasciciel,
		@numer,		
		@wygasniecie,
		@kbezpiecz,
		@kontakt)	
END
GO

CREATE PROCEDURE dbo.KartaPlatnicza_Update
	@id				int,
	@wlasciciel		varchar(32),
	@numer			varchar(32),
	@wygasniecie	varchar(5),
	@kbezpiecz		varchar(3),
	@kontakt		int
AS
BEGIN
	UPDATE dbo.KartaPlatnicza
	SET wlasciciel	= @wlasciciel,
		numer		= @numer,		
		wygasniecie	= @wygasniecie,
		kbezpiecz	= @kbezpiecz,
		kontakt		= @kontakt	
	WHERE
		id			= @id

END
GO

CREATE PROCEDURE dbo.KartaPlatnicza_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.KartaPlatnicza
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.KartaPlatnicza_GetRecord
	@wlasciciel		varchar(32),
	@numer			varchar(32),
	@wygasniecie	varchar(5),
	@kbezpiecz		varchar(3),
	@kontakt		int
AS
BEGIN
	SELECT * FROM dbo.KartaPlatnicza
	WHERE wlasciciel  = @wlasciciel	
	AND	  numer		  = @numer		
	AND	  wygasniecie = @wygasniecie
	AND	  kbezpiecz	  = @kbezpiecz	
	AND	  kontakt	  = @kontakt	
END
GO

CREATE PROCEDURE dbo.KartaPlatnicza_GetCollection
AS
BEGIN
	SELECT * FROM dbo.KartaPlatnicza
END
GO


CREATE PROCEDURE dbo.Kontakt_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Kontakt
	WHERE id = @id
END
GO

GO

CREATE PROCEDURE dbo.Kontakt_Insert
	@imie			varchar(16),
	@nazwisko		varchar(16),
	@telefon		varchar(16),
	@email			varchar(32),
	@miejscowosc	varchar(16),
	@nrdomu			varchar(8),
	@miasto			varchar(32),
	@poczta			varchar(6),
	@ulica			varchar(16),
	@idpracownika	int,
	@iduczestnika	int
AS
BEGIN
	INSERT INTO dbo.Kontakt (
	imie,
	nazwisko,
	telefon,
	email,
	miejscowosc,
	nrdomu,
	miasto,
	poczta,
	ulica,
	idpracownika,
	iduczestnika
	)
	OUTPUT INSERTED.*
	VALUES
	(
	@imie		,	
	@nazwisko	,	
	@telefon	,	
	@email		,	
	@miejscowosc,	
	@nrdomu		,	
	@miasto		,	
	@poczta		,	
	@ulica		,	
	@idpracownika,	
	@iduczestnika
	)
END
GO

CREATE PROCEDURE dbo.Kontakt_Update
	@id				int,
	@imie			varchar(16),
	@nazwisko		varchar(16),
	@telefon		varchar(16),
	@email			varchar(32),
	@miejscowosc	varchar(16),
	@nrdomu			varchar(8),
	@miasto			varchar(32),
	@poczta			varchar(6),
	@ulica			varchar(16),
	@idpracownika	int,
	@iduczestnika	int
AS
BEGIN
	UPDATE dbo.Kontakt
	SET imie		 = @imie			,
		nazwisko	 = @nazwisko		,
		telefon		 = @telefon			,
		email		 = @email			,
		miejscowosc	 = @miejscowosc		,
		nrdomu		 = @nrdomu			,	
		miasto		 = @miasto			,	
		poczta		 = @poczta			,	
		ulica		 = @ulica			,
		idpracownika = @idpracownika	,
		iduczestnika = @iduczestnika	
	WHERE
		id = @id		
END
GO

CREATE PROCEDURE dbo.Kontakt_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Kontakt
	WHERE @id = id
END
GO

CREATE PROCEDURE dbo.Kontakt_GetRecord
	@imie			varchar(16),
	@nazwisko		varchar(16),
	@telefon		varchar(16),
	@email			varchar(32),
	@miejscowosc	varchar(16),
	@nrdomu			varchar(8),
	@miasto			varchar(32),
	@poczta			varchar(6),
	@ulica			varchar(16),
	@idpracownika	int,
	@iduczestnika	int
AS
BEGIN
	SELECT * FROM dbo.Kontakt
	WHERE imie			= @imie		
	AND	  nazwisko		= @nazwisko	
	AND	  telefon		= @telefon	
	AND	  email			= @email		
	AND	  miejscowosc	= @miejscowosc
	AND	  nrdomu		= @nrdomu		
	AND	  miasto		= @miasto		
	AND	  poczta		= @poczta		
	AND	  ulica			= @ulica		
	AND	  idpracownika	= @idpracownika
	AND	  iduczestnika	= @iduczestnika
END
GO

CREATE PROCEDURE dbo.Kontakt_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Kontakt
END
GO


CREATE PROCEDURE dbo.Logowanie_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Logowanie
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Logowanie_Insert
	@login			varchar(16),
	@haslo			varchar(32),
	@idpracownika	int, 
	@idsponsora		int,
	@iduczestnika	int
AS
BEGIN
	INSERT INTO dbo.Logowanie (
		login,		
		haslo,		
		idpracownika,
		idsponsora,	
		iduczestnika
	)
	OUTPUT INSERTED.*
	VALUES (
		@login,		
		@haslo,		
		@idpracownika,
		@idsponsora,	
		@iduczestnika
	)
END
GO

CREATE PROCEDURE dbo.Logowanie_Update
	@id	int,
	@login			varchar(16),
	@haslo			varchar(32),
	@idpracownika	int, 
	@idsponsora		int,
	@iduczestnika	int
AS
BEGIN
	UPDATE dbo.Logowanie
	SET 
	login		 = @login,		
	haslo		 = @haslo,		
	idpracownika = @idpracownika,
	idsponsora	 = @idsponsora,	
	iduczestnika = @iduczestnika
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Logowanie_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Logowanie
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Logowanie_GetRecord
	@login			varchar(16),
	@haslo			varchar(32),
	@idpracownika	int, 
	@idsponsora		int,
	@iduczestnika	int
AS
BEGIN
	SELECT * FROM dbo.Logowanie
	WHERE login			= @login
	AND	  haslo			= @haslo
	AND	  idpracownika	= @idpracownika
	AND	  idsponsora	= @idsponsora	
	AND	  iduczestnika	= @iduczestnika
END
GO

CREATE PROCEDURE dbo.Logowanie_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Logowanie
END
GO


CREATE PROCEDURE dbo.Logowanie_CheckCredentials
	@login		varchar(16),
	@haslo	varchar(32)
AS
BEGIN
	SELECT * FROM dbo.Logowanie
	WHERE 
		login = @login
		and haslo = @haslo
END
GO


CREATE PROCEDURE dbo.Platnosc_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Platnosc
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Platnosc_Insert
	@idkarty	int,
	@idwydarzenia	int,
	@kwota		decimal(19,2),
	@dzien		date,
	@godzina	time
AS
BEGIN
	INSERT INTO dbo.Platnosc (
		idkarty,
		idwydarzenia,
		kwota,		
		dzien,		
		godzina
	)
	OUTPUT INSERTED.*
	VALUES (
		@idkarty,
		@idwydarzenia,
		@kwota,		
		@dzien,		
		@godzina
	)
END
GO

CREATE PROCEDURE dbo.Platnosc_Update
	@id		int,
	@idkarty	int,
	@idwydarzenia	int,
	@kwota		decimal(19,2),
	@dzien		date,
	@godzina	time
AS
BEGIN
	UPDATE dbo.Platnosc
	SET 
		idkarty		= @idkarty,
		idwydarzenia	= @idwydarzenia,
		kwota		= @kwota,		
		dzien		= @dzien,		
		godzina		= @godzina
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Platnosc_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Platnosc
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Platnosc_GetRecord
	@idkarty	int,
	@idwydarzenia	int,
	@kwota		decimal(19,2),
	@dzien		date,
	@godzina	time
AS
BEGIN
	SELECT * FROM dbo.Platnosc
	WHERE idkarty		= @idkarty
	AND   idwydarzenia	= @idwydarzenia	
	AND	  kwota			= @kwota		
	AND	  dzien			= @dzien		
	AND	  godzina		= @godzina	

END
GO

CREATE PROCEDURE dbo.Platnosc_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Platnosc
END
GO


CREATE PROCEDURE dbo.Pracownik_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Pracownik
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Pracownik_Insert
	@stanowisko		varchar(32),
	@wynagrodzenie	decimal(19,2)
AS
BEGIN
	INSERT INTO dbo.Pracownik (
	stanowisko,	
	wynagrodzenie
	)
	OUTPUT INSERTED.*
	VALUES (
	@stanowisko,	
	@wynagrodzenie
	)
END
GO

CREATE PROCEDURE dbo.Pracownik_Update
	@id	int,
	@stanowisko	varchar(32),
	@wynagrodzenie	decimal(19,2)
AS
BEGIN
	UPDATE dbo.Pracownik
	SET 
	stanowisko	= @stanowisko,	
	wynagrodzenie= @wynagrodzenie
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Pracownik_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Pracownik
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Pracownik_GetRecord
	@stanowisko		varchar(32),
	@wynagrodzenie	decimal(19,2)
AS
BEGIN
	SELECT * FROM dbo.Pracownik
	WHERE stanowisko	= @stanowisko
	AND	  wynagrodzenie = @wynagrodzenie
END
GO

CREATE PROCEDURE dbo.Pracownik_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Pracownik
END
GO

CREATE PROCEDURE dbo.Uczestnik_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Uczestnik
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Uczestnik_Insert
	@fid int
AS
BEGIN
	INSERT INTO dbo.Uczestnik (
		fid
	)
	OUTPUT INSERTED.*
	VALUES (
		@fid
	)
END
GO

CREATE PROCEDURE dbo.Uczestnik_Update
	@id	int,
	@fid int
AS
BEGIN
	UPDATE dbo.Uczestnik
	SET  fid = @fid
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Uczestnik_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Uczestnik
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Uczestnik_GetRecord
	@fid int
AS
BEGIN
	SELECT * FROM dbo.Uczestnik
	WHERE fid = @fid
END
GO

CREATE PROCEDURE dbo.Uczestnik_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Uczestnik
END
GO

CREATE PROCEDURE dbo.Sponsor_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Sponsor
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Sponsor_Insert
	@nazwa varchar(64)
AS
BEGIN
	INSERT INTO dbo.Sponsor (
		nazwa
	)
	OUTPUT INSERTED.*
	VALUES (
		@nazwa
	)
END
GO

CREATE PROCEDURE dbo.Sponsor_Update
	@id	int,
	@nazwa varchar(64)
AS
BEGIN
	UPDATE dbo.Sponsor
	SET 
	nazwa = @nazwa
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Sponsor_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Sponsor
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Sponsor_GetRecord
	@nazwa varchar(64)
AS
BEGIN
	SELECT * FROM dbo.Sponsor
	WHERE nazwa = @nazwa
END
GO

CREATE PROCEDURE dbo.Sponsor_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Sponsor
END
GO

USE MWS 
GO


CREATE PROCEDURE dbo.Wydarzenie_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Wydarzenie
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Wydarzenie_Insert
	@nazwa			varchar(64),
	@opis			varchar(256),
	@miejsce		varchar(64),
	@dzien			date,
	@godzina		time(0),
	@budzet			decimal(19,4)
AS
BEGIN
	INSERT INTO dbo.Wydarzenie(nazwa,opis,miejsce,dzien,godzina,budzet)
	OUTPUT INSERTED.*
	VALUES (@nazwa,@opis,@miejsce,@dzien,@godzina,@budzet);
END
GO

CREATE PROCEDURE dbo.Wydarzenie_Update
	@id	int,
	@nazwa			varchar(64),
	@opis			varchar(256),
	@miejsce		varchar(64),
	@dzien			date,
	@godzina		time(0),
	@budzet			decimal(19,4)
AS
BEGIN
	UPDATE dbo.Wydarzenie
	SET nazwa	= @nazwa,
		opis	 = @opis,	
		miejsce	 = @miejsce,
		dzien	 = @dzien,	
		godzina	 = @godzina,
		budzet	 = @budzet
	WHERE id = @id;
END
GO

CREATE PROCEDURE dbo.Wydarzenie_Delete
	@id			int
AS
BEGIN
	DELETE FROM dbo.Wydarzenie
	WHERE id = @id;
END
GO

CREATE PROCEDURE dbo.Wydarzenie_GetRecord
	@nazwa			varchar(64),
	@opis			varchar(256),
	@miejsce		varchar(64),
	@dzien			date,
	@godzina		time(0),
	@budzet			decimal(19,4)
AS
BEGIN
	SELECT * FROM dbo.Wydarzenie
	WHERE nazwa	= @nazwa	
	AND	  opis		= @opis	
	AND	  miejsce	= @miejsce
	AND	  dzien	= @dzien	
	AND	  godzina	= @godzina
	AND	  budzet	= @budzet	
END
GO

CREATE PROCEDURE dbo.Wydarzenie_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Wydarzenie
END
GO


CREATE PROCEDURE  dbo.Pracownik_Pracownik_Insert
	@idorganizatora int,
	@idpracownika	int
AS
BEGIN
	INSERT INTO dbo.Pracownik_Pracownik
	(idorganizatora,
	 idpracownika	)
	VALUES
	(@idorganizatora,
	 @idpracownika	
	 )
END
GO

CREATE PROCEDURE  dbo.Pracownik_Pracownik_Delete
	@idorganizatora int,
	@idpracownika	int
AS
BEGIN
	DELETE FROM dbo.Pracownik_Pracownik
	WHERE 	idorganizatora = @idorganizatora 
	AND		idpracownika = @idpracownika
END
GO

CREATE PROCEDURE dbo.Pracownik_Pracownik_GetCollection
AS
BEGIN
	SELECT * FROM Pracownik_Pracownik
END
GO


CREATE PROCEDURE  dbo.Uczestnik_Bilet_Insert
	@iduczestnika int,
	@idbiletu	int
AS
BEGIN
	INSERT INTO dbo.Uczestnik_Bilet
	(iduczestnika,
	 idbiletu	)
	VALUES
	(@iduczestnika,
	 @idbiletu	
	 )
END
GO

CREATE PROCEDURE  dbo.Uczestnik_Bilet_Delete
	@iduczestnika int,
	@idbiletu	int
AS
BEGIN
	DELETE FROM dbo.Uczestnik_Bilet
	WHERE 	iduczestnika = @iduczestnika 
	AND		idbiletu = @idbiletu
END
GO

CREATE PROCEDURE dbo.Uczestnik_Bilet_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Uczestnik_Bilet
END
GO

CREATE PROCEDURE  dbo.Wydarzenie_Sponsor_Insert
	@idwydarzenia int,
	@idsponsora	int
AS
BEGIN
	INSERT INTO dbo.Wydarzenie_Sponsor
	(idwydarzenia,
	 idsponsora	)
	VALUES
	(@idwydarzenia,
	 @idsponsora	
	 )
END
GO

CREATE PROCEDURE  dbo.Wydarzenie_Sponsor_Delete
	@idwydarzenia int,
	@idsponsora	int
AS
BEGIN
	DELETE FROM dbo.Wydarzenie_Sponsor
	WHERE 	idwydarzenia = @idwydarzenia 
	AND		idsponsora = @idsponsora
END
GO

CREATE PROCEDURE dbo.Wydarzenie_Sponsor_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Wydarzenie_Sponsor
END
GO

CREATE PROCEDURE  dbo.Wydarzenie_Pracownik_Insert
	@idwydarzenia int,
	@idpracownika	int
AS
BEGIN
	INSERT INTO dbo.Wydarzenie_Pracownik
	(idwydarzenia,
	 idpracownika	)
	VALUES
	(@idwydarzenia,
	 @idpracownika	
	 )
END
GO

CREATE PROCEDURE  dbo.Wydarzenie_Pracownik_Delete
	@idwydarzenia int,
	@idpracownika	int
AS
BEGIN
	DELETE FROM dbo.Wydarzenie_Pracownik
	WHERE 	idwydarzenia = @idwydarzenia 
	AND		idpracownika = @idpracownika
END
GO

CREATE PROCEDURE dbo.Wydarzenie_Pracownik_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Wydarzenie_Pracownik
END
GO


CREATE PROCEDURE  dbo.Wydarzenie_Uczestnik_Insert
	@idwydarzenia int,
	@iduczestnika	int
AS
BEGIN
	INSERT INTO dbo.Wydarzenie_Uczestnik
	(idwydarzenia,
	 iduczestnika	)
	VALUES
	(@idwydarzenia,
	 @iduczestnika	
	 )
END
GO

CREATE PROCEDURE  dbo.Wydarzenie_Uczestnik_Delete
	@idwydarzenia int,
	@iduczestnika	int
AS
BEGIN
	DELETE FROM dbo.Wydarzenie_Uczestnik
	WHERE 	idwydarzenia = @idwydarzenia 
	AND		iduczestnika = @iduczestnika
END
GO

CREATE PROCEDURE dbo.Wydarzenie_Uczestnik_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Wydarzenie_Uczestnik
END
GO

CREATE PROCEDURE dbo.Wniosek_GetRecordById
	@id	int
AS
BEGIN
	SELECT * FROM dbo.Wniosek
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Wniosek_Insert
	@idwiadomosci int,
	@kwota	decimal(19,2),
	@akcja	varchar(16),
	@zatwierdzone	bit
AS
BEGIN
	INSERT INTO dbo.Wniosek(
		idwiadomosci,
		kwota,
		akcja,
		zatwierdzone
	)
	OUTPUT INSERTED.*
	VALUES (
		@idwiadomosci,
		@kwota		 ,
		@akcja		 ,
		@zatwierdzone
	)
END
GO

CREATE PROCEDURE dbo.Wniosek_Update
	@id	int,
	@idwiadomosci int, 
	@kwota	decimal(19,2),
	@akcja	varchar(16),
	@zatwierdzone	bit
AS
BEGIN
	UPDATE dbo.Wniosek
	SET idwiadomosci = @idwiadomosci,
		kwota		 = @kwota,
		akcja		 = @akcja,
		zatwierdzone = @zatwierdzone
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Wniosek_Delete
	@id	int
AS
BEGIN
	DELETE FROM dbo.Wniosek
	WHERE id = @id
END
GO

CREATE PROCEDURE dbo.Wniosek_GetRecord
	@idwiadomosci int,
	@kwota	decimal(19,2),
	@akcja	varchar(16),
	@zatwierdzone	bit
AS
BEGIN
	SELECT * FROM dbo.Wniosek
	WHERE idwiadomosci = @idwiadomosci
	AND	  kwota		   = @kwota
	AND	  akcja		   = @akcja
	AND	  zatwierdzone = @zatwierdzone
END
GO

CREATE PROCEDURE dbo.Wniosek_GetCollection
AS
BEGIN
	SELECT * FROM dbo.Wniosek
END
GO

CREATE PROCEDURE  dbo.Logowanie_Logowanie_Insert
	@idlogowania1	int,
	@idlogowania2	int
AS
BEGIN
	INSERT INTO dbo.Logowanie_Logowanie
	(
		idlogowania1,
		idlogowania2
	 )
	VALUES
	(@idlogowania1,
	 @idlogowania2
	 )
END
GO

CREATE PROCEDURE  dbo.Logowanie_Logowanie_Delete
	@idlogowania1 int,
	@idlogowania2 int
AS
BEGIN
	DELETE FROM dbo.Logowanie_Logowanie
	WHERE 	idlogowania1 = @idlogowania1
	AND		idlogowania2 = @idlogowania2
END
GO

CREATE PROCEDURE dbo.Logowanie_Logowanie_GetCollection
AS
BEGIN
	SELECT * FROM Logowanie_Logowanie
END
GO
