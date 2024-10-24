#!/bin/bash

gcc disk_use.c -o disk_use

crontab -l > _cron

echo "*/15 * * * * ~/disk_use / 30" >> _cron

crontab _cron

rm _cron
