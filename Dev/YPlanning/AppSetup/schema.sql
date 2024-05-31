CREATE TABLE USERS(
   id SERIAL,
   lastName VARCHAR(100) NOT NULL,
   firstName VARCHAR(100) NOT NULL,
   birthDate DATE NOT NULL,
   email VARCHAR(100) NOT NULL,
   phoneNumber VARCHAR(15),
   PRIMARY KEY (id)
);

CREATE TABLE ACCOUNT(
   id SERIAL,
   login VARCHAR(300) NOT NULL,
   password VARCHAR(300) NOT NULL,
   role VARCHAR(50),
   userId INTEGER NOT NULL,
   PRIMARY KEY(id),
   FOREIGN KEY(id) REFERENCES USERS(id)
);

CREATE TABLE CLASS(
   id SERIAL,
   professorId INTEGER NOT NULL,
   subject VARCHAR(100) NOT NULL,
   classDate DATE NOT NULL,
   startTime TIME NOT NULL,
   endTime TIME NOT NULL,
   PRIMARY KEY(id),
   FOREIGN KEY(professorId) REFERENCES USERS(id)
);

CREATE TABLE ATTENDANCE(
   id SERIAL,
   classId INTEGER NOT NULL,
   studentId INTEGER NOT NULL,
   status VARCHAR(50) NOT NULL,
   reason VARCHAR(200),
   PRIMARY KEY(id),
   FOREIGN KEY(classId) REFERENCES CLASS(id),
   FOREIGN KEY(studentId) REFERENCES USERS(id)
);

CREATE TABLE TEST(
   id SERIAL,
   professorId INTEGER NOT NULL,
   studentId INTEGER NOT NULL,
   grade VARCHAR(50) NOT NULL,
   PRIMARY KEY(id),
   FOREIGN KEY(professorId) REFERENCES USERS(id),
   FOREIGN KEY(studentId) REFERENCES USERS(id)
);