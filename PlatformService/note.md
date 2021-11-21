#### 命令
dotnet --version
dotnet new webapi -n PlatformService
code -r PlatformService

#### 安装依赖包
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore -v 5.0.12

#### 生成
dotnet build

#### 运行
dotnet run

#### EF命令
dotnet tool install --global dotnet-ef
dotnet ef migrations add initialmigration

#### docker命令
docker build -t kimgin/platformservice .
docker run -p 8080:80 -d kimgin/platformservice
docker ps
docker stop <id>

#### docker镜像源
 "registry-mirrors": [
    "https://registry.docker-cn.com",
    "https://dockerhub.azk8s.cn",
    "https://docker.mirrors.ustc.edu.cn",
    "https://osqpce4o.mirror.aliyuncs.com"
  ]

#### Kubernetes命令
kubectl apply -f platforms-depl.yaml
kubectl rollout restart deployment platforms-depl

kubectl delete deployments platforms-depl
kubectl get deployments
kubectl get pods
kubectl get services

kubectl get namespace
kubectl get pods --namespace=ingress-nginx
kubectl delete namespace ingress-nginx
kubectl describe pod ingress-nginx-admission-create-qxgng --namespace=ingress-nginx

kubectl get pvc
kubectl get storageclass

kubectl get secret
kubectl delete secret mssql
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pass123!"

minikube start --driver='docker' --image-mirror-country='cn' --image-repository='registry.cn-hangzhou.aliyuncs.com/google_containers'

#### 解决Https
dotnet dev-certs https --trust

#### 解决sa登录密码不正确
由于前面的持久化pvc创建的时候初始化密码设定和后面的密码不一致，删除pvc重新部署

#### Kubernetes的三种外部访问方式
ClusterIP 服务是 Kubernetes 的默认服务。它给你一个集群内的服务，集群内的其它应用都可以访问该服务。集群外部无法访问它。但是我们可以通过 Kubernetes 的 proxy 模式来访问该服务

NodePort 服务是引导外部流量到你的服务的最原始方式

LoadBalancer 服务是暴露服务到 internet 的标准方式。在 GKE 上，这种方式会启动一个 Network Load Balancer，它将给你一个单独的 IP 地址，转发所有流量到你的服务。

Ingress 事实上不是一种服务类型。相反，它处于多个服务的前端，扮演着“智能路由”或者集群入口的角色。
