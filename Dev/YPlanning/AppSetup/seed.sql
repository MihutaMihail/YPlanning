INSERT INTO USERS (lastName, firstName, birthDate, email, phoneNumber, role) VALUES 
('Doe', 'John', '1990-01-01', 'john.doe@example.com', '123-456-7890', 'Student'),
('Smith', 'Jane', '1985-05-15', 'jane.smith@example.com', '098-765-4321', 'Teacher');

INSERT INTO ACCOUNTS (login, password, accountCreationDate, lastLoginDate, userId) VALUES 
('john.doe', 'password123', '2024-01-01', '2024-01-10', 1),
('jane.smith', 'securepass', '2024-01-05', '2024-01-15', 2);

INSERT INTO CLASSES (subject, classDate, startTime, endTime) VALUES 
('Mathematics', '2024-06-01', '09:00:00'::time, '10:30:00'::time),
('History', '2024-06-02', '11:00:00'::time, '12:30:00'::time);

INSERT INTO ATTENDANCES (classId, userId, status, reason) VALUES 
(1, 1, 'Present', NULL),
(2, 1, 'Absent', 'Sick');

INSERT INTO TESTS (classId, userId, score) VALUES 
(1, 1, '85'),
(2, 1, '90');