USE ContactsDemo;

TRUNCATE TABLE Customer;
TRUNCATE TABLE ContactNumber;
TRUNCATE TABLE ContactType;

INSERT INTO ContactType
(ContactTypeName)
VALUES
('Home Number'),
('Work Number'),
('Mobile Number'),
('Emergency Number');