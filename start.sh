code .
cd ./api && alacritty -t api -e dotnet watch &
cd ./client && alacritty -t client -e dotnet watch &
sudo systemctl start docker
sudo docker-compose up