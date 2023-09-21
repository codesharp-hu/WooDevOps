#!/usr/bin/env bash
 
source_ip=1.1.1.1
target_ip=2.2.2.2

scp -i ssh-key.pem ./zip.sh ubuntu@$source_ip:/home/ubuntu/zip.sh
ssh -i ssh-key.pem ubuntu@$source_ip 'chmod +x /home/ubuntu/zip.sh'
ssh -i ssh-key.pem ubuntu@$source_ip /home/ubuntu/zip.sh
scp -i ssh-key.pem ubuntu@$source_ip:/home/ubuntu/uploads.zip ./uploads.zip
scp -i ssh-key.pem ./dump.sh ubuntu@$source_ip:/home/ubuntu/dump.sh
ssh -i ssh-key.pem ubuntu@$source_ip 'chmod +x /home/ubuntu/dump.sh'
ssh -i ssh-key.pem ubuntu@$source_ip /home/ubuntu/dump.sh
scp -i ssh-key.pem ubuntu@$source_ip:/home/ubuntu/dump.sql ./dump.sql
ssh -i ssh-key.pem ubuntu@$source_ip 'rm /home/ubuntu/dump.sql'
ssh -i ssh-key.pem ubuntu@$source_ip 'rm /home/ubuntu/dump.sh'
ssh -i ssh-key.pem ubuntu@$source_ip 'rm /home/ubuntu/zip.sh'
ssh -i ssh-key.pem ubuntu@$source_ip 'rm /home/ubuntu/uploads.zip'

dotnet run config.json clean

scp -i ssh-key.pem ./create_db.sql ubuntu@$target_ip:/home/ubuntu/create_db.sql
scp -i ssh-key.pem ./dump.sql ubuntu@$target_ip:/home/ubuntu/dump.sql
scp -i ssh-key.pem ./migrate.sql ubuntu@$target_ip:/home/ubuntu/migrate.sql
scp -i ssh-key.pem ./migrate.sh ubuntu@$target_ip:/home/ubuntu/migrate.sh
ssh -i ssh-key.pem ubuntu@$target_ip 'chmod +x /home/ubuntu/migrate.sh'
ssh -i ssh-key.pem ubuntu@$target_ip /home/ubuntu/migrate.sh
ssh -i ssh-key.pem ubuntu@$target_ip 'rm /home/ubuntu/create_db.sql'
ssh -i ssh-key.pem ubuntu@$target_ip 'rm /home/ubuntu/dump.sql'
ssh -i ssh-key.pem ubuntu@$target_ip 'rm /home/ubuntu/migrate.sql'
ssh -i ssh-key.pem ubuntu@$target_ip 'rm /home/ubuntu/migrate.sh'

dotnet run config.json export
dotnet run config.json orders
dotnet run config.json media
dotnet run config.json products
dotnet run config.json imageRef


scp -i ssh-key.pem ./fix_image_refs.sql ubuntu@$target_ip:/home/ubuntu/fix_image_refs.sql
scp -i ssh-key.pem ./fix_image_refs.sh ubuntu@$target_ip:/home/ubuntu/fix_image_refs.sh
ssh -i ssh-key.pem ubuntu@$target_ip 'chmod +x /home/ubuntu/fix_image_refs.sh'
ssh -i ssh-key.pem ubuntu@$target_ip /home/ubuntu/fix_image_refs.sh
ssh -i ssh-key.pem ubuntu@$target_ip 'rm /home/ubuntu/fix_image_refs.sql'
ssh -i ssh-key.pem ubuntu@$target_ip 'rm /home/ubuntu/fix_image_refs.sh'

scp -i ssh-key.pem ./uploads.zip ubuntu@$target_ip:/home/ubuntu/uploads.zip
ssh -i ssh-key.pem ubuntu@$target_ip 'sudo -u www-data unzip -o /home/ubuntu/uploads.zip -d /'
ssh -i ssh-key.pem ubuntu@$target_ip 'rm /home/ubuntu/uploads.zip'
