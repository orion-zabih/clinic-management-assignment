
create table Patient
(
	Id 	integer identity(1,1) primary key,
	Name  varchar(32) not null,
	DoB date not null,
	Gender char(1) not null,
	Phone  varchar(32) not null,
	Address  varchar(128),
	IsActive bit not null,
	CreateDateTime		datetime default getdate(),
	UpdateDateTime		datetime default getdate()
);

create table Doctor
(
	Id 	integer identity(1,1) primary key,
	Name  varchar(128),
	IsActive bit not null,
	CreateDateTime		datetime default getdate(),
	UpdateDateTime		datetime default getdate()
);
create table Visit
(
	Id 	integer identity(1,1) primary key,
	PatientId integer FOREIGN KEY REFERENCES Patient,
	VisitDetail  varchar(512),
	DoctorId integer FOREIGN KEY REFERENCES Doctor,
	CreateDateTime		datetime default getdate(),
	UpdateDateTime		datetime default getdate()
);

create table Disease
(
	Id 	integer identity(1,1) primary key,
	VisitId integer FOREIGN KEY REFERENCES Visit,
	DiseaseName  varchar(128) not null,
	DiseaseDetail  varchar(512),
	CreateDateTime		datetime default getdate(),
	UpdateDateTime		datetime default getdate()
);