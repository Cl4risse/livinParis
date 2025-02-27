DROP USER IF EXISTS 'responsable'@'localhost';
DROP USER IF EXISTS 'etudiant'@'localhost';

CREATE USER 'responsable'@'localhost' IDENTIFIED BY 'responsable' ;
GRANT ALL ON td3.* TO 'responsable'@'localhost';

SHOW GRANTS FOR 'responsable'@'localhost';


CREATE USER 'etudiant'@'localhost' IDENTIFIED BY 'etudiant' ;
GRANT SELECT ON td3.* TO 'etudiant'@'localhost';
GRANT INSERT, UPDATE, DELETE ON td3.emploi TO 'etudiant'@'localhost';
GRANT INSERT, UPDATE, DELETE ON td3.étudiant TO 'etudiant'@'localhost';
GRANT INSERT, UPDATE, DELETE ON td3.suit TO 'etudiant'@'localhost';
GRANT INSERT, UPDATE, DELETE ON td3.à_suivre TO 'etudiant'@'localhost';


SHOW GRANTS FOR 'etudiant'@'localhost';

