
CREATE TRIGGER InsertTrigger
ON AllBooksInfo
INSTEAD OF INSERT
AS
BEGIN
    -- Perform the INSERT operation on the underlying tables
    INSERT INTO Books (id, title, author_id, publisher_id, description)
    SELECT
        i.book_id,
        i.book_title,
        a.auth_id,
        p.pub_id,
        i.book_description
    FROM inserted i
    LEFT JOIN Author a ON i.author_name = a.author_name
    LEFT JOIN Publisher p ON i.publisher_name = p.publisher_name;

    INSERT INTO BookInfo (id, language, maturityRating, pageCount, categories, publishedDate, retailPrice)
    SELECT
        i.book_id,
        i.book_language,
        i.book_maturityRating,
        i.book_pageCount,
        i.book_categories,
        i.book_publishedDate,
        i.book_retailPrice
    FROM inserted i;
END;




