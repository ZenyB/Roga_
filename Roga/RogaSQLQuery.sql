---------------------------------------------
-- create database
CREATE DATABASE RogaDatabase
---------------------------------------------
-- user info
CREATE TABLE USER_(
	id	int identity,
	username 	varchar(255) not null,
	password 	varchar(255) not null,
	fullname 	varchar(255) not null,
	avatar	image,
	constraint pk_ui primary key(id)
)
---------------------------------------------
-- user image
CREATE TABLE IMAGE_(
	id	int identity,
	img	image not null,
	userid	int not null,
	constraint pk_img primary key(id)
)

-- Khoa ngoai cho bang IMAGE_
ALTER TABLE IMAGE_ ADD CONSTRAINT fk01_IMAGE FOREIGN KEY(userid) REFERENCES USER_(id)