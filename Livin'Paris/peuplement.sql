LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/Clients.csv' 
INTO TABLE particulier 
FIELDS TERMINATED BY ';' 
LINES STARTING BY '' 
TERMINATED BY '\r\n' 
IGNORE 1 LINES
(@discard,nom,prenom,rue,numero,codepostal,ville,telephone,email,metroplusproche)
;

UPDATE particulier 
SET estClient = TRUE 
WHERE estClient = FALSE ;

INSERT INTO client (id_client) SELECT Id_particulier FROM particulier WHERE estClient = TRUE;
SELECT * FROM client;


LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/Cuisiniers.csv' 
INTO TABLE particulier 
FIELDS TERMINATED BY ';' 
LINES STARTING BY '' 
TERMINATED BY '\r\n' 
IGNORE 1 LINES
(@discard,nom,prenom,rue,numero,codepostal,ville,telephone,email,metroplusproche)
;

UPDATE particulier 
SET estCuisinier = TRUE 
WHERE estClient = FALSE ;


INSERT INTO cuisinier (id_cuisinier) SELECT Id_particulier FROM particulier WHERE estCuisinier = TRUE;
SELECT * FROM cuisinier;
INSERT INTO cuisinier (id_cuisinier) VALUES (1);



LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/Commandes.csv' 
INTO TABLE plat 
FIELDS TERMINATED BY ';' 
LINES STARTING BY '' 
TERMINATED BY '\r\n' 
IGNORE 1 LINES
(id_co, id_cuisinier, id_client, nom, prix_par_personne, nbportions, type, @date_fabrication, @date_peremption, regime, nationalite, 
 ingredients,@discard,@discard,@discard,@discard,@discard,@discard,@discard)
SET 
   `date_fabrication` = STR_TO_DATE(@date_fabrication, '%d/%m/%Y'), 
   `date_peremption` = STR_TO_DATE(@date_peremption, '%d/%m/%Y'),
   `id_recette` = NULL; 

