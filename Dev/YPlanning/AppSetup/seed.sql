INSERT INTO USERS (lastName, firstName, birthDate, email, phoneNumber, role) VALUES 
('Bob', 'TheBuilder', '1990-01-01', 'bob.thebuilder@example.com', '123-456-7890', 'Student'),
('Saitama', 'TwoHands', '1985-05-15', 'saitama.twohands@example.com', '098-765-4321', 'Teacher'),
('Kendrick', 'Drake', '1985-05-15', 'kendrick.drake@example.com', '0604060408', 'Admin'),
('UseFor', 'Testing', '1995-04-10', 'usefor.testing@example.com', '4545454', 'Student');

INSERT INTO ACCOUNTS (login, password, accountCreationDate, lastLoginDate, userId) VALUES 
('bob.thebuilder', '...', '2024-01-01', '2024-01-10', 1),
('saitama.twohands', '...', '2024-01-05', '2024-01-15', 3),
('kendrick.drake', '...', '2024-01-05', '2024-01-05', 2);

INSERT INTO CLASSES (subject, classDate, startTime, endTime, room) VALUES 
('Mathematics', '2024-06-01', '09:00:00', '10:30:00', '501'),
('History', '2024-06-02', '11:00:00', '12:30:00', '402'),
('History', '2024-06-03', '11:00:00', '12:30:00', '405'),
('English', '2024-06-03', '15:00:00', '16:30:00', '421');

INSERT INTO ATTENDANCES (classId, userId, status, reason) VALUES 
(1, 1, 'Present', NULL),
(1, 2, 'Absent', 'Sick'),
(2, 1, 'Absent', 'Unknown'),
(2, 2, 'Absent', 'RDV');

INSERT INTO TESTS (classId, userId, score) VALUES 
(1, 1, '85'),
(1, 2, '90'),
(2, 1, '35'),
(2, 2, '35');