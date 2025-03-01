DROP USER IF EXISTS 'client_particulier'@'localhost';
DROP USER IF EXISTS 'client_entreprise'@'localhost';
DROP USER IF EXISTS 'cuisinier'@'localhost';
DROP USER IF EXISTS 'admin'@'localhost';

CREATE USER 'admin'@'localhost' IDENTIFIED BY 'root';
GRANT ALL ON bdd.* TO 'admin'@'localhost';

CREATE USER 'cuisinier'@'localhost' IDENTIFIED BY '123' ;
GRANT ALL ON bdd.cuisinier TO 'cuisinier'@'localhost';
GRANT UPDATE ON bdd.particulier TO 'cuisinier'@'localhost';
GRANT SELECT ON bdd.commande TO 'cuisinier'@'localhost';
GRANT SELECT ON bdd.livraison TO 'cuisinier'@'localhost';
GRANT SELECT ON bdd.livre TO 'cuisinier'@'localhost';
GRANT ALL ON bdd.plat TO 'cuisinier'@'localhost';
GRANT INSERT, UPDATE, SELECT ON bdd.recette TO 'cuisinier'@'localhost';
GRANT SELECT ON bdd.retours_clients TO 'cuisinier'@'localhost';
GRANT ALL ON bdd.trajet TO 'cuisinier'@'localhost';

SHOW GRANTS FOR 'cuisinier'@'localhost';


CREATE USER 'client_entreprise'@'localhost' IDENTIFIED BY '123' ;
GRANT ALL ON bdd.client TO 'client_entreprise'@'localhost';
GRANT INSERT ON bdd.retours_clients TO 'client_entreprise'@'localhost';
GRANT UPDATE ON bdd.entreprise TO 'client_entreprise'@'localhost';
GRANT ALL ON bdd.commande TO 'client_entreprise'@'localhost';
GRANT ALL ON bdd.contenu_commande TO 'client_entreprise'@'localhost';
GRANT SELECT ON bdd.livraison TO 'client_entreprise'@'localhost';
GRANT ALL ON bdd.plat TO 'client_entreprise'@'localhost';
GRANT INSERT, UPDATE, SELECT ON bdd.recette TO 'client_entreprise'@'localhost';
GRANT ALL ON bdd.trajet TO 'client_entreprise'@'localhost';

SHOW GRANTS FOR 'client_entreprise'@'localhost';


CREATE USER 'client_particulier'@'localhost' IDENTIFIED BY '123' ;
GRANT ALL ON bdd.client TO 'client_particulier'@'localhost';
GRANT INSERT ON bdd.retours_clients TO 'client_particulier'@'localhost';
GRANT UPDATE ON bdd.particulier TO 'client_particulier'@'localhost';
GRANT ALL ON bdd.commande TO 'client_particulier'@'localhost';
GRANT ALL ON bdd.contenu_commande TO 'client_particulier'@'localhost';
GRANT SELECT ON bdd.livraison TO 'client_particulier'@'localhost';
GRANT ALL ON bdd.plat TO 'client_particulier'@'localhost';
GRANT INSERT, UPDATE, SELECT ON bdd.recette TO 'client_particulier'@'localhost';
GRANT ALL ON bdd.trajet TO 'client_particulier'@'localhost';

SHOW GRANTS FOR 'client_particulier'@'localhost';


