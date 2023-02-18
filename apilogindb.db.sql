BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "Users" (
	"Id"	INTEGER NOT NULL,
	"UserName"	TEXT NOT NULL,
	"PasswordHash"	BLOB NOT NULL,
	"PasswordSalt"	BLOB NOT NULL,
	CONSTRAINT "PK_Users" PRIMARY KEY("Id" AUTOINCREMENT)
);
INSERT INTO "__EFMigrationsHistory" VALUES ('20230130150955_CreateInitial','7.0.2');
INSERT INTO "Users" VALUES (1,'luise','�8I���O�[|a�O�y9�ǂF�''l����<A�JOѮj]��xC�x��Z
-�u�FƸ9��e+�',X'a2e4c17d97bd20eff7548d4cf097293f198fd0b5c6235951e817cf96e2320b4e70b527cbb04b209f0cbf5e7d2b9a47e58a36a94034ea19e4d95a2d2c5add703d3564be87007586f704f61a94e51ce673a848f4bb55fbe7434f7d0d8ca5e06c1ca037f451000535b3af605efa8f2439a19fee4b7567ad1c1daf7605257d358af6');
COMMIT;