-- Table: public.shows

-- DROP TABLE IF EXISTS public.shows;

CREATE TABLE IF NOT EXISTS public.shows
(
    id integer NOT NULL DEFAULT nextval('shows_id_seq'::regclass),
    title character varying(100) COLLATE pg_catalog."default" NOT NULL,
    location character varying(100) COLLATE pg_catalog."default" NOT NULL,
    date date NOT NULL,
    "time" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    description character varying COLLATE pg_catalog."default" NOT NULL,
    image character varying(50) COLLATE pg_catalog."default" NOT NULL,
    url character varying(100) COLLATE pg_catalog."default",
    mapurl character varying(500) COLLATE pg_catalog."default",
    added date NOT NULL,
    active boolean NOT NULL,
    CONSTRAINT shows_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.shows
    OWNER to postgres;