DROP DATABASE IF EXISTS bdd;
CREATE DATABASE IF NOT EXISTS bdd;
USE bdd;


DROP TABLE IF EXISTS livraison;
CREATE TABLE IF NOT EXISTS livraison(
   id_livraison INT,
   adresse VARCHAR(50),
   PRIMARY KEY(id_livraison, adresse)
);


DROP TABLE IF EXISTS recette;
CREATE TABLE IF NOT EXISTS recette(
   id_recette INT,
   PRIMARY KEY(id_recette)
);


DROP TABLE IF EXISTS metro;
CREATE TABLE IF NOT EXISTS metro(
   station VARCHAR(50),
   ligne VARCHAR(50),
   PRIMARY KEY(station, ligne)
);


DROP TABLE IF EXISTS cuisinier;
CREATE TABLE IF NOT EXISTS cuisinier(
   id_cuisinier INT,
   radie BOOLEAN DEFAULT FALSE,
   id_livraison INT,
   adresse VARCHAR(50),
   PRIMARY KEY(id_cuisinier),
   FOREIGN KEY(id_livraison, adresse) REFERENCES livraison(id_livraison, adresse) ON DELETE CASCADE
);


DROP TABLE IF EXISTS particulier;
CREATE TABLE IF NOT EXISTS particulier(
   Id_particulier INT AUTO_INCREMENT,
   nom VARCHAR(50),
   prenom VARCHAR(50),
   rue VARCHAR(100),
   numero INT,
   ville VARCHAR(50),
   codePostal VARCHAR(50),
   telephone VARCHAR(50),
   email VARCHAR(50),
   metroplusproche VARCHAR(50),
   motdepasse VARCHAR(50),
   estClient BOOLEAN DEFAULT FALSE,
   estCuisinier BOOLEAN  DEFAULT FALSE,
   PRIMARY KEY(Id_particulier)
);


DROP TABLE IF EXISTS entreprise;
CREATE TABLE IF NOT EXISTS entreprise(
   id_entreprise INT,
   nom VARCHAR(50),
   referent VARCHAR(50),
   motdepasse VARCHAR(50),
   PRIMARY KEY(id_entreprise)
);


DROP TABLE IF EXISTS client;
CREATE TABLE IF NOT EXISTS client(
   id_client INT,
   radie BOOLEAN DEFAULT FALSE,
   id_entreprise INT,
   Id_particulier INT,
   PRIMARY KEY(id_client),
   UNIQUE(id_entreprise),
   UNIQUE(Id_particulier),
   FOREIGN KEY(id_entreprise) REFERENCES entreprise(id_entreprise),
   FOREIGN KEY(Id_particulier) REFERENCES particulier(Id_particulier)
);


DROP TABLE IF EXISTS plat;
CREATE TABLE IF NOT EXISTS plat(
   id_plat INT NOT NULL AUTO_INCREMENT,
   id_cuisinier INT NULL,
   id_client INT NULL,
   id_co INT NULL,
   nom VARCHAR(50),
   type ENUM('entree', 'plat', 'dessert'),
   nbportions INT CHECK (nbportions > 0),
   date_fabrication TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
   date_peremption TIMESTAMP,
   prix_par_personne FLOAT(10,2),
   nationalite VARCHAR(50),
   regime VARCHAR(50),
   ingredients TEXT(100),
   photo TEXT,
   id_recette INT,
   PRIMARY KEY(id_plat),
   UNIQUE(id_recette),
   FOREIGN KEY(id_client) REFERENCES client(id_client) ON DELETE CASCADE,
   FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier) ON DELETE CASCADE,
   FOREIGN KEY(id_recette) REFERENCES recette(id_recette) ON DELETE CASCADE
);


DROP TABLE IF EXISTS commande;
CREATE TABLE IF NOT EXISTS commande(
   id_commande INT,
   id_cuisinier INT NULL,
   id_client INT NULL,
   id_livraison INT NULL,
   adresse VARCHAR(50),
   id_plat INT,  -- Ajout d'une colonne pour référencer directement un plat
   PRIMARY KEY(id_commande),
   FOREIGN KEY(id_plat) REFERENCES plat(id_plat) ON DELETE CASCADE,  -- Référence correcte vers plat
   FOREIGN KEY(id_client) REFERENCES client(id_client) ON DELETE CASCADE,
   FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier) ON DELETE CASCADE,
   FOREIGN KEY(id_livraison, adresse) REFERENCES livraison(id_livraison, adresse) ON DELETE CASCADE
);





DROP TABLE IF EXISTS prepare;
CREATE TABLE IF NOT EXISTS prepare(
   id_cuisinier INT,
   id_plat INT,
   PRIMARY KEY(id_cuisinier, id_plat),
   FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier) ON DELETE CASCADE,
   FOREIGN KEY(id_plat) REFERENCES plat(id_plat) ON DELETE CASCADE
);


DROP TABLE IF EXISTS trajet;
CREATE TABLE IF NOT EXISTS trajet(
   station VARCHAR(50),
   ligne VARCHAR(50),
   station_arrivee VARCHAR(50),
   ligne_arrivee VARCHAR(50),
   PRIMARY KEY(station, ligne, station_arrivee, ligne_arrivee),
   FOREIGN KEY(station, ligne) REFERENCES metro(station, ligne) ON DELETE CASCADE,
   FOREIGN KEY(station_arrivee, ligne_arrivee) REFERENCES metro(station, ligne) ON DELETE CASCADE
);


DROP TABLE IF EXISTS livre;
CREATE TABLE IF NOT EXISTS livre(
   id_livraison INT,
   adresse VARCHAR(50),
   station VARCHAR(50),
   ligne VARCHAR(50),
   station_arrivee VARCHAR(50),
   ligne_arrivee VARCHAR(50),
   PRIMARY KEY(id_livraison, adresse, station, ligne, station_arrivee, ligne_arrivee),
   FOREIGN KEY(id_livraison, adresse) REFERENCES livraison(id_livraison, adresse) ON DELETE CASCADE,
   FOREIGN KEY(station, ligne, station_arrivee, ligne_arrivee) REFERENCES trajet(station, ligne, station_arrivee, ligne_arrivee) ON DELETE CASCADE
);


DROP TABLE IF EXISTS retours_clients;
CREATE TABLE IF NOT EXISTS retours_clients(
   id_client INT,
   id_commande INT,
   id_cuisinier INT,
   note TINYINT CHECK (note >= 0 AND note <= 5),
   commentaire TEXT,
   PRIMARY KEY(id_client, id_commande, id_cuisinier),
   FOREIGN KEY(id_client) REFERENCES client(id_client) ON DELETE CASCADE,
   FOREIGN KEY(id_commande) REFERENCES commande(id_commande) ON DELETE CASCADE,
   FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier) ON DELETE CASCADE
);


DROP TABLE IF EXISTS transaction;
CREATE TABLE IF NOT EXISTS transaction(
   id_transaction VARCHAR(50),
   montant FLOAT(10,2),
   date_transaction DATETIME,
   id_commande INT NOT NULL, 
   PRIMARY KEY(id_transaction),
   FOREIGN KEY(id_commande) REFERENCES commande(id_commande) ON DELETE CASCADE
);

