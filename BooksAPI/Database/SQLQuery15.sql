CREATE PROCEDURE InsertIntoBooks
    @title VARCHAR(50),
    @author_name VARCHAR(50),
    @publisher_name VARCHAR(50),
    @description VARCHAR(1000),
    @language VARCHAR(20),
    @maturityRating VARCHAR(30),
    @pageCount INT,
    @categories VARCHAR(50),
    @publishedDate VARCHAR(50),
    @retailPrice DECIMAL(6, 3)
AS
BEGIN
    DECLARE @author_id INT;
    DECLARE @publisher_id INT;

    -- Check if the author already exists; if not, insert the author
    IF NOT EXISTS (SELECT 1 FROM Author WHERE author_name = @author_name)
    BEGIN
        INSERT INTO Author (author_name) VALUES (@author_name);
    END

    -- Check if the publisher already exists; if not, insert the publisher
    IF NOT EXISTS (SELECT 1 FROM Publisher WHERE publisher_name = @publisher_name)
    BEGIN
        INSERT INTO Publisher (publisher_name) VALUES (@publisher_name);
    END

    -- Get the author_id and publisher_id
    SELECT @author_id = auth_id FROM Author WHERE author_name = @author_name;
    SELECT @publisher_id = pub_id FROM Publisher WHERE publisher_name = @publisher_name;

    -- Insert data into the Books table
    INSERT INTO Books (id, title, author_id, publisher_id, description)
    VALUES (NEXT VALUE FOR dbo.Books.id, @title, @author_id, @publisher_id, @description);

    -- Get the book_id
    DECLARE @book_id INT;
    SET @book_id = SCOPE_IDENTITY();

    -- Insert data into the BookInfo table
    INSERT INTO BookInfo (id, language, maturityRating, pageCount, categories, publishedDate, retailPrice)
    VALUES (@book_id, @language, @maturityRating, @pageCount, @categories, @publishedDate, @retailPrice);
END;


EXEC InsertIntoBooks
    @title = 'Born King',
    @author_name = 'Kiran R',
    @publisher_name = 'Wagoner',
    @description = 'Description of the sample book by Kiran Kumar',
    @language = 'English',
    @maturityRating = 'Mature',
    @pageCount = 200,
    @categories = 'Biography',
    @publishedDate = '2023-01-11',
    @retailPrice = 89.99;


--

SELECT * FROM Books WHERE title = 'Born King';

