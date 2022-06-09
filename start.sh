# code .
cd ./api && alacritty -t api -e dotnet run &
#cd ./client && alacritty -t client -e dotnet run &
sudo systemctl start docker
sudo docker-compose up
