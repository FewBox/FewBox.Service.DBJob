echo 'Build DB Job...'
DOCKER_REPO_SLUG=fewbox/dbjob PROJECTNAME=FewBox.Service.DBJob
dotnet restore $PROJECTNAME
dotnet publish -c Release $PROJECTNAME/$PROJECTNAME.csproj -p:FileVersion=$TRAVIS_JOB_NUMBER
cp ./Dockerfile $PROJECTNAME/bin/Release/net6.0/publish/Dockerfile
cp ./.dockerignore $PROJECTNAME/bin/Release/net6.0/publish/.dockerignore
cd $PROJECTNAME/bin/Release/net6.0/publish
docker build -t $DOCKER_REPO_SLUG:latest .
docker tag $DOCKER_REPO_SLUG:latest $DOCKER_REPO_SLUG:v1
#docker push $PT_DOCKER_REPO_SLUG:v1
cd ../../../../../
echo 'Finished!'