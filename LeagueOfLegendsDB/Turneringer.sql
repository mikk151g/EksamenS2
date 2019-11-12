create table Turneringer(
TurneringsID int identity(1,1) primary key,
Turneringsnavn varchar(30) not null,
SpillerID int,
Spillernavn varchar(30),
SpillerTelefonnummer varchar(14),
DommerID int,
Dommernavn varchar(30),
Dommertelefonnummer varchar(14),
DommerLevel int,
constraint FK_TurneringsAnsatte foreign key (DommerID) references dbo.Ansatte (MedarbejderID),
constraint FK_TurneringsSpillere foreign key (SpillerID) references dbo.Spillere (SpillerID)
)