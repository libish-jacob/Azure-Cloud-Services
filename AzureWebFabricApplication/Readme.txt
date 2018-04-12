This requires a local cluster if you want to run it locally.
https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started

Visual studio may have to run as admin to get the solution running properly.

For remoting to work, we need Microsoft.ServiceFabric.Services.Remoting nuget package.

This solution has an app fabric container application, a API webService and a microservice.
The communication between microservice is via remoting.