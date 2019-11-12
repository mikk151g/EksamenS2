create table Ansatte(
MedarbejderID int identity(1,1) primary key,
Navn varchar(30) not null,
Telefonnummer varchar(14) not null,
Løn varchar(30) not null,
Jobtype varchar(30),
DommerLevel int
)