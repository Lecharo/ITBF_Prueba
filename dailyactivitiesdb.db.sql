BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "Activities" (
	"Id"	INTEGER NOT NULL,
	"ActivityName"	TEXT NOT NULL,
	CONSTRAINT "PK_Activities" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Employees" (
	"Id"	INTEGER NOT NULL,
	"EmployeeName"	TEXT NOT NULL,
	CONSTRAINT "PK_Employees" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Labors" (
	"Id"	INTEGER NOT NULL,
	"LaborName"	TEXT NOT NULL,
	CONSTRAINT "PK_Tasks" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "DailyActivities" (
	"Id"	INTEGER NOT NULL,
	"EmployeeId"	INTEGER NOT NULL,
	"ActivityId"	INTEGER NOT NULL,
	"LaborId"	INTEGER NOT NULL,
	"WorkDay"	TEXT NOT NULL,
	"DurationLabor"	INTEGER NOT NULL,
	"Comments"	INTEGER,
	CONSTRAINT "PK_DailyActivities" PRIMARY KEY("Id" AUTOINCREMENT)
);
INSERT INTO "__EFMigrationsHistory" VALUES ('20230128163231_InitialCreate','7.0.2');
INSERT INTO "Activities" VALUES (1,'Desarrollo de Sistemas');
INSERT INTO "Activities" VALUES (3,'Despliegue Sistemas');
INSERT INTO "Activities" VALUES (4,'string');
INSERT INTO "Activities" VALUES (5,'Analisis y Diseño Sistemas');
INSERT INTO "Employees" VALUES (1,'Santiago Giraldo');
INSERT INTO "Employees" VALUES (2,'Maria Cortes');
INSERT INTO "Labors" VALUES (1,'Nuevo Desarrollo Backend Requerimiento');
INSERT INTO "Labors" VALUES (2,'Ajuste Mantenimiento Backend Requerimiento');
INSERT INTO "Labors" VALUES (3,'Ejecución Pruebas Backend Requerimiento');
INSERT INTO "DailyActivities" VALUES (3,1,1,1,'2023-01-29 00:00:00',4,'Pruebas de Backend general');
INSERT INTO "DailyActivities" VALUES (4,1,1,1,'2023-01-29 00:00:00',2,'Pruebas Frontend y Backend');
INSERT INTO "DailyActivities" VALUES (6,1,1,1,'2023-01-30 00:00:00',3,'Desarrollo angular');
INSERT INTO "DailyActivities" VALUES (7,1,5,1,'2023-01-30 00:00:00',4,'Inicio desarrollo API Revisión de detalle con el usuario final');
COMMIT;
