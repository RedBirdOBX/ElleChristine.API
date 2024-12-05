-- Table: public.logs

-- DROP TABLE IF EXISTS public.logs;

CREATE TABLE IF NOT EXISTS public.logs
(
    id integer NOT NULL DEFAULT nextval('logs_id_seq'::regclass),
    message character varying COLLATE pg_catalog."default",
    message_template character varying COLLATE pg_catalog."default",
    level character varying(128) COLLATE pg_catalog."default",
    raise_date date NOT NULL,
    exception character varying COLLATE pg_catalog."default" NOT NULL,
    properties jsonb NOT NULL,
    props_test jsonb,
    machine_name character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT logs_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.logs
    OWNER to postgres;