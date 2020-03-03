drop table dbo.ReservationPanierDuJour;
drop table dbo.ProduitsPanierDuJour;
drop table dbo.PanierDuJour;
drop table dbo.Produits;

-- Produits
create table dbo.Produits (
	id int primary key,
	nom varchar(50) not null,
	prix decimal(6,2) not null
);

insert into dbo.Produits
select 1, 'Patates', 2.3 union
select 2, 'Oignons', 3.4 union
select 3, 'Poireaux', 4.5 union
select 4, 'Œufs', 0.2;


-- Panier du jour
create table dbo.PanierDuJour (
	id int primary key,
	prix decimal(6,2) not null
);

insert into dbo.PanierDuJour
values (1, 10);

-- Produits du panier du jour
create table dbo.ProduitsPanierDuJour (
	panierId int not null,
	produitId int not null,
	quantiteAuPoids decimal null,
	quantiteUnitaire int null
);

insert into dbo.ProduitsPanierDuJour
select 1, 1, 2.50, null union
select 1, 2, 0.80, null union
select 1, 3, 0.75, null union
select 1, 4, null, 6;

-- Réservation Panier du jour
create table dbo.ReservationPanierDuJour (
    clientId int not null,
    quantite int not null
)