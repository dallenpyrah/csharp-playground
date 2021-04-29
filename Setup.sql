USE csharpplayground;

-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );

CREATE TABLE cars
(
    id INT NOT NULL AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    brand VARCHAR(255) NOT NULL,
    model VARCHAR(255) NOT NULL,
    color VARCHAR(255) NOT NULL,
    price INT,
    topSpeed INT,
    year INT NOT NULL,

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE
);