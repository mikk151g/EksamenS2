create table Spillere(
SpillerID int identity(1,1) primary key,
Navn varchar(30) not null,
InGameNavn varchar(30) not null,
Rang varchar(30) not null,
Telefonnummer varchar(14) not null,
Turneringstype varchar(3) not null
)
