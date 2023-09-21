#!/usr/bin/env bash

sudo mariadb < create_db.sql
mariadb --host="localhost" --user="myuser" --database="woo_migration" --password="fAGondvzEANCsDQ" < dump.sql
mariadb --host="localhost" --user="myuser" --database="woo_migration" --password="fAGondvzEANCsDQ" < migrate.sql