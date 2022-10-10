echo 'Build DB Job...'
DOCKER_REPO_SLUG=192.168.1.38:5000/fewbox/dbjob PROJECTNAME=FewBox.Service.DBJob
dotnet restore $PROJECTNAME
dotnet publish -c Release $PROJECTNAME/$PROJECTNAME.csproj
cp ./Nas/Dockerfile $PROJECTNAME/bin/Release/net6.0/publish/Dockerfile
cp ./Nas/.dockerignore $PROJECTNAME/bin/Release/net6.0/publish/.dockerignore
cd $PROJECTNAME/bin/Release/net6.0/publish
docker build -t $DOCKER_REPO_SLUG:v1 .
cd ../../../../../
docker push $DOCKER_REPO_SLUG:v1
echo 'Finished!'