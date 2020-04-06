#! /bin/bash

systemctl is-active --quiet create_ap && systemctl restart create_ap
