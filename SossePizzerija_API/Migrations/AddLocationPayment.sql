-- Pokrenite ovaj SQL script na vasoj SossePizzerija bazi podataka
-- da dodate nove kolone za lokaciju dostave, nacin placanja i pracenje dostavljaca

ALTER TABLE Narudzbe ADD NacinPlacanja NVARCHAR(50) NULL;
ALTER TABLE Narudzbe ADD Latitude FLOAT NULL;
ALTER TABLE Narudzbe ADD Longitude FLOAT NULL;
ALTER TABLE Narudzbe ADD DostavljacLatitude FLOAT NULL;
ALTER TABLE Narudzbe ADD DostavljacLongitude FLOAT NULL;

-- Provjera
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Narudzbe'
ORDER BY ORDINAL_POSITION;
