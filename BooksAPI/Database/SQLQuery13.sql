
CREATE TRIGGER InsertTrigger
ON AllBooksInfo
INSTEAD OF INSERT
AS
BEGIN
  
    INSERT INTO Books (id, title, author_id, publisher_id, description)
    SELECT
        i.id,
        i.title,
        a.auth_id,
        p.pub_id,
        i.description
    FROM inserted i
    LEFT JOIN Author a ON i.author_name = a.author_name
    LEFT JOIN Publisher p ON i.publisher_name = p.publisher_name;

    INSERT INTO BookInfo (id, language, maturityRating, pageCount, categories, publishedDate, retailPrice)
    SELECT
        i.id,
        i.language,
        i.maturityRating,
        i.pageCount,
        i.book_categories,
        i.publishedDate,
        i.retailPrice
    FROM inserted i;
END;


--select * from AllBooksInfo;

-----------


-- Insert data into the AllBooksInfo view
INSERT INTO AllBooksInfo (
    id, 
    title, 
    author_name, 
    publisher_name, 
    description, 
    language, 
    maturityRating, 
    pageCount, 
    book_categories, 
    publishedDate, 
    retailPrice
)
VALUES (
    8, 
    'Sample Book', 
    'Sample Author', 
    'Sample Publisher', 
    'Description of the sample book', 
    'English', 
    'G', 
    200, 
    'Fiction', 
    '2023-09-10', 
    19.99
);

DROP TRIGGER InsertTrigger;
--trigger dropped
