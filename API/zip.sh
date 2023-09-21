#!/usr/bin/env bash

find /var/www/fitness-index/wp-content/uploads -type f -mtime -90 | grep -E -v  '.php$|.log$|elementor' | zip -@ uploads.zip