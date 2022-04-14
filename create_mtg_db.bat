ECHO off

sqlcmd -S localhost -E -i mtg_db.sql

rem server is localhost

ECHO . 
ECHO if no errors appear 
PAUSE