CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240815094012_Initial_Implementation') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240815094012_Initial_Implementation') THEN

    CREATE TABLE `Employees` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `CreatedBy` int NOT NULL,
        `ModifiedBy` int NULL,
        `CreatedOn` datetime(6) NOT NULL,
        `ModifiedOn` datetime(6) NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        CONSTRAINT `PK_Employees` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240815094012_Initial_Implementation') THEN

    CREATE TABLE `TaskDetails` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `EmployeeId` int NOT NULL,
        `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `Details` varchar(5000) CHARACTER SET utf8mb4 NOT NULL,
        `Notes` varchar(5000) CHARACTER SET utf8mb4 NULL,
        `Status` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
        `StartDate` datetime(6) NOT NULL,
        `DueDate` datetime(6) NOT NULL,
        `CompletedDate` datetime(6) NULL,
        `Timezone` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
        `CreatedBy` int NOT NULL,
        `ModifiedBy` int NULL,
        `CreatedOn` datetime(6) NOT NULL,
        `ModifiedOn` datetime(6) NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        CONSTRAINT `PK_TaskDetails` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_TaskDetails_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240815094012_Initial_Implementation') THEN

    CREATE TABLE `Documents` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `Path` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `Type` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `TaskDetailId` int NULL,
        `CreatedBy` int NOT NULL,
        `ModifiedBy` int NULL,
        `CreatedOn` datetime(6) NOT NULL,
        `ModifiedOn` datetime(6) NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        CONSTRAINT `PK_Documents` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Documents_TaskDetails_TaskDetailId` FOREIGN KEY (`TaskDetailId`) REFERENCES `TaskDetails` (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240815094012_Initial_Implementation') THEN

    CREATE INDEX `IX_Documents_TaskDetailId` ON `Documents` (`TaskDetailId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240815094012_Initial_Implementation') THEN

    CREATE INDEX `IX_TaskDetails_EmployeeId` ON `TaskDetails` (`EmployeeId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240815094012_Initial_Implementation') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240815094012_Initial_Implementation', '8.0.8');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

