/*
    IMPORTANT : WHEN COPYING THE ENCRYPTED TOKEN, MAKE SURE TO NOT CLOSE THE POWERSHELL WINDOWS
    CLOSING IT, WILL RESET THE KEY AND IV FOR THE AES ENCRYPTION MEANING THAT THE ENCRYPTED TOKEN
    THAT YOU'RE COPYING HERE WILL NOT WORK

    **HOW TO USE** : 
        1. Using setup.ps1 script, choose option 5 : Generate Token

        2. Replace YOUR_TOKEN_HERE placeholder with the ENCRYPTED token
        (don't forget to copy the other token, you will need it (i.e Postman))

        3. /\ YOU'RE DONE /\. You can go back and continue following the tutorial inside
        the README.md on GitHub.
        TO NOTE : AFTER installing everything, the hashed token can be deleted.
*/

-- TEMPLATE
/*____________________________________________________________________________________________________*/

INSERT INTO USERS (lastName, firstName, birthDate, email, phoneNumber, role) VALUES
('LAST_NAME', 'FIRST_NAME', '2000-01-01', 'LAST_NAME.FIRST_NAME@EXAMPLE.COM', '123-456-7890', 'admin');

INSERT INTO TOKENS (value, role, userId) VALUES
('YOUR_TOKEN_HERE', 'admin', 1);

/*____________________________________________________________________________________________________*/

--------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------
-- **TO NOTE** : THIS IS SOLELY FOR TESTING PURPOSES, INSERTING DATA LIKE THIS WILL LEAVE
-- PASSWORDS UNHASHED. IF YOU WANT TO USE IT, REMOVE THE /* and */ (comments)
/*
INSERT INTO USERS (lastName, firstName, birthDate, email, phoneNumber, role) VALUES
('Bob', 'TheBuilder', '1990-01-01', 'bob.thebuilder@example.com', '123-456-7890', 'student'),
('Saitama', 'TwoHands', '1985-05-15', 'saitama.twohands@example.com', '098-765-4321', 'teacher'),
('Kendrick', 'Drake', '1985-05-15', 'kendrick.drake@example.com', '0604060408', 'student'),
('UseFor', 'Testing', '1995-04-10', 'usefor.testing@example.com', '4545454', 'student');

INSERT INTO ACCOUNTS (login, password, accountCreationDate, lastLoginDate, userId) VALUES
('bob.thebuilder', 'password123', '2024-01-01', '2024-01-10', 1),
('saitama.twohands', 'securepass', '2024-01-05', '2024-01-15', 3),
('kendrick.drake', 'nopasswd', '2024-01-05', '2024-01-05', 2);

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
*/