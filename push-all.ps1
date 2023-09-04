docker login -u futuraacr -p 33ga/2XOxfCRMiFw1SX+hXWsmSnT5ptS futuraacr.azurecr.io

docker tag ordersvc:latest futuraacr.azurecr.io/ordersvc:latest
docker tag basketsvc:latest futuraacr.azurecr.io/basketsvc:latest
docker tag catalogsvc:latest futuraacr.azurecr.io/catalogsvc:latest
docker tag restgateway:latest futuraacr.azurecr.io/restgateway:latest

docker push futuraacr.azurecr.io/ordersvc:latest
docker push futuraacr.azurecr.io/basketsvc:latest
docker push futuraacr.azurecr.io/catalogsvc:latest
docker push futuraacr.azurecr.io/restgateway:latest