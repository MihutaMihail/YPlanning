CREATE TABLE MEMBER(
   id SERIAL,
   lastName VARCHAR(100) NOT NULL,
   firstName VARCHAR(100) NOT NULL,
   birthDate DATE NOT NULL,
   email VARCHAR(100) NOT NULL,
   phoneNumber VARCHAR(15),
   role VARCHAR(50),
   PRIMARY KEY (id)
);

CREATE TABLE ACCOUNT(
   id SERIAL,
   login VARCHAR(300) NOT NULL UNIQUE,
   password VARCHAR(300) NOT NULL,
   accountCreationDate DATE,
   lastLoginDate DATE,
   memberId INTEGER NOT NULL,
   PRIMARY KEY(id),
   FOREIGN KEY(memberId) REFERENCES MEMBER(id)
);

CREATE TABLE CLASS(
   id SERIAL,
   subject VARCHAR(100) NOT NULL,
   classDate DATE NOT NULL,
   startTime TIME NOT NULL,
   endTime TIME NOT NULL,
   PRIMARY KEY(id)
);

CREATE TABLE ATTENDANCE(
   classId INTEGER NOT NULL,
   memberId INTEGER NOT NULL,
   status VARCHAR(50) NOT NULL,
   reason VARCHAR(200),
   PRIMARY KEY(classId, memberId),
   FOREIGN KEY(classId) REFERENCES CLASS(id),
   FOREIGN KEY(memberId) REFERENCES MEMBER(id)
);

CREATE TABLE TEST(
   id SERIAL,
   classId INTEGER NOT NULL,
   studentId INTEGER NOT NULL,
   score VARCHAR(50) NOT NULL,
   PRIMARY KEY(id),
   FOREIGN KEY(classId) REFERENCES CLASS(id),
   FOREIGN KEY(studentId) REFERENCES MEMBER(id)
);