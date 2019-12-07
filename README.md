https://www.codeproject.com/Articles/482546/Creating-a-custom-user-login-form-with-NET-Csharp

MySQL

Create User table:

CREATE TABLE `sugjvcox_localblasts_test`.`Users` ( `Id` INT NOT NULL AUTO_INCREMENT , `Username` VARCHAR(50) NOT NULL , `Password` VARCHAR(65535) NOT NULL , `Created` DATE NOT NULL , `Modified` DATE NOT NULL , `Email` VARCHAR(100) NOT NULL , `Phone` INT(10) NULL , `IsActive` BOOLEAN NOT NULL DEFAULT TRUE , PRIMARY KEY (`Id`)) ENGINE = MyISAM;

HostGator IP:
50.116.84.117
DB Name: sugjvcox_localblasts_test
Port 3306
User: sugjvcox_admin
PW: Christ2020!