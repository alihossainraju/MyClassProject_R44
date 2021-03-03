
USE master
CREATE DATABASE R44DB_NVIT
GO

USE R44DB_NVIT
CREATE TABLE Student
(
StudentID int PRIMARY KEY IDENTITY NOT NULL,
Name varchar(30) NULL,
Gender Varchar(10) null,
DOB varchar(20) null
);
GO

SELECT * FROM Student

INSERT INTO Student(Name,Gender,DOB) VALUES('Sayeed','Male','01-01-1990')

UPDATE Student SET Name='Israt',Gender='Female',DOB='01-01-1985' WHERE StudentID=1

DELETE FROM Student WHERE StudentID=2







