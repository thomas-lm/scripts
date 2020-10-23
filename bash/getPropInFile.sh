#!/bin/bash
# parse properties file and return the value for key
# param1 = key like my.prop.content1
# param2 = filepath.properties

pcregrep -M '^'$1' *=(.*\\\n)*.*$' $2|cut -d'=' -f2|sed ':a;N;$!ba;s|[[:space:]]*\\\n[[:space:]]*| |g'| sed -e 's/^[ \t]*//'