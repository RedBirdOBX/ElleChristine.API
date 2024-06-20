-- Table: public.photos

-- DROP TABLE IF EXISTS public.photos;

CREATE TABLE IF NOT EXISTS public.photos
(
    id integer NOT NULL DEFAULT nextval('photos_id_seq'::regclass),
    file_name character varying COLLATE pg_catalog."default" NOT NULL,
    heading character varying COLLATE pg_catalog."default" NOT NULL,
    description character varying COLLATE pg_catalog."default" NOT NULL,
    photo_date date NOT NULL,
    added date NOT NULL,
    active boolean NOT NULL DEFAULT true,
    CONSTRAINT photos_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.photos
    OWNER to postgres;