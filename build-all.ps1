docker build -t catalogsvc:latest -f .\catalog-service\Dockerfile .
docker build -t basketsvc:latest -f .\basket-service\Dockerfile .
docker build -t ordersvc:latest -f .\order-service\Dockerfile .
docker build -t restgateway:latest -f .\RestGateway\Dockerfile .