CREATE DATABASE usersdb;

use usersdb;
create table users
(
id INT primary key auto_increment NOT NULL,
name varchar(150) NOT NULL,
date_birth date NOT NULL,
active BIT NOT NULL DEFAULT 1
)