create database authentication_min_api;

create table "user"
(
    id            serial
        constraint user_pk primary key,
    username      text      not null,
    password_hash bytea     not null,
    password_salt bytea     not null,
    first_name    text      not null,
    last_name     text      not null,
    email         text      not null,
    created_date  timestamp not null,
    modified_date timestamp not null default timezone('utc', now())
);

create unique index user_username_uindex
    on "user" (username);


