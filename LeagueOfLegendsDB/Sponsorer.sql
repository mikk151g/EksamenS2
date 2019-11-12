create table Sponsorer(
SponsorID int identity(1,1) primary key,
Firmanavn varchar(30) not null,
Branche varchar(30) not null,
SpillerID int,
Spillernavn varchar(30),
Udgift int not null,
constraint FK_SponsorSpillere foreign key (SpillerID) references dbo.Spillere (SpillerID)
)