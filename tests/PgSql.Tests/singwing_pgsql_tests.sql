CREATE DATABASE singwing_pgsql_tests
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'C.UTF-8'
    LC_CTYPE = 'C.UTF-8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
-- Table: public.books

-- DROP TABLE IF EXISTS public.books;

CREATE TABLE IF NOT EXISTS public.books
(
    "Id" bigint NOT NULL,
    "Title" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "AuthorId" bigint NOT NULL,
    CONSTRAINT books_pkey PRIMARY KEY ("Id")
        USING INDEX TABLESPACE pg_default
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.books
    OWNER to postgres;


-- Table: public.authors

-- DROP TABLE IF EXISTS public.authors;

CREATE TABLE IF NOT EXISTS public.authors
(
    "Id" bigint NOT NULL,
    "FirstName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "LastName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT authors_pkey PRIMARY KEY ("Id")
        USING INDEX TABLESPACE pg_default
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.authors
    OWNER to postgres;

INSERT INTO public.books("Id", "Title", "AuthorId") VALUES
(1, 'Exile', 1),
(2, 'Arrows', 2),
(3, 'Ships of Discovery', 3);

INSERT INTO public.authors("Id", "FirstName", "LastName") VALUES
(1, 'Kathryn', 'Lasky'),
(2, 'Dina', 'Anastasio'),
(3, 'William', 'Houseman');

-- public.book_details
CREATE OR REPLACE FUNCTION public.book_details("bookId$" bigint) 
RETURNS SETOF refcursor 
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
    "authorId$" bigint;
    "book$" refcursor := 'book$';
    "author$" refcursor := 'author$';
BEGIN
    SELECT
        "AuthorId"
    INTO
        "authorId$"
    FROM
        public.books
    WHERE
        "Id" = "bookId$"
    LIMIT 1;

    OPEN "book$" FOR
    SELECT
        "Id",
        "Title"
    FROM
        public.books
    WHERE
        "Id" = "bookId$"
    LIMIT 1;
    RETURN NEXT "book$";

    OPEN "author$" FOR
    SELECT
        "Id",
        "FirstName",
        "LastName"
    FROM
        public.authors
    WHERE
        "Id" = "authorId$"
    LIMIT 1;
    RETURN NEXT "author$";
END;
$BODY$;

-- public.books
CREATE OR REPLACE FUNCTION public.books("minBookId$" bigint) 
RETURNS refcursor 
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
    "books$" refcursor := 'books$';
BEGIN
    OPEN "books$" FOR
    SELECT
        "Id",
        "Title"
    FROM
        public.books
    WHERE
        "Id" >= "minBookId$";
    RETURN "books$";
END;
$BODY$;