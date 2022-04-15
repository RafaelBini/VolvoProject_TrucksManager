# Volvo Test :page_facing_up:

Hey there! I really appreciate this oportunity to show a bit of my potential with this sample.

This project is composed of a .NET Core REST API and Vue.JS frontend. 

Bellow, I will explain the necessary steps in order to:

1. Run the backend
2. Run the unit tests
3. Run the frontend

## :one: Running The Backend

> Necessary tools:
> - [.NET Core 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
> - [SQL Server Local Database](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)
> - [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/)
> - [NuGet EntityFramework 6.04](https://www.nuget.org/packages/EntityFramework/6.4.0)

1. Open a folder and in your terminal run: **git clone https://github.com/RafaelBini/VolvoProject_TrucksManager.git**
2. Open the solution file **VolvoProject_TrucksManager\trucks_backend\trucks_backend.sln**
3. Build and run the trucks_backend solution (press F5) - The local database should be created automatically
4. After a while, Visual Studio should open a browser and show the swagger page on **https://localhost:7163/**:
<img src="https://user-images.githubusercontent.com/56660237/163598564-fcf239a6-17ee-4c09-b48b-6c95ef3f61ee.png" width="500">
5. You can try some endpoints using this swagger interface.

## :two: Unit testing

> Necessary tools:
> - [NuGet MSTest 2.2.7](https://www.nuget.org/packages/MSTest.TestFramework/2.2.7)

1. In order to run the unit tests, stop the application
2. Into the Visual Studio, go to **Menu -> Test -> Run All Tests**:
<img src="https://user-images.githubusercontent.com/56660237/163599221-7daa6c33-bc5b-4a97-ba93-9f02c71d034f.png" width="500">
3. Visual Studio will open a new window called Test Explorer and show the results (expand all tabs):
<img src="https://user-images.githubusercontent.com/56660237/163599771-2e9a552e-7605-4749-97d9-9d0f2b5f95d3.png" width="500">
4. You can see every single test by clicking on them or navigating to TruckControllerTest on TruckAPITester project.

## :three: Running The Frontend

> Necessary tools:
> - [Vue.js (Node.js)](https://vuejs.org/guide/quick-start.html)
> - [@vue/cli 5.0.4](https://cli.vuejs.org/guide/installation.html)

1. Open and run the backend solution on **localhost:7163**
2. Open the terminal on the folder **VolvoProject_TrucksManager\trucks_frontend**
3. Run the command **npm install**
4. After the packages installation was finished, run **npm run serve**
5. Once the vue app is running, open your browser on **http://localhost:8080** and you should be able to see this screen:
<img src="https://user-images.githubusercontent.com/56660237/163601324-e302ce58-1b22-475d-8aa7-7fe6129b1458.png" width="500">
6. Click on the blue button to create a new truck<br/>
7. Enter the truck information on the right hand side<br/>
8. Once the truck was created, it will appear on the left hand side<br/>
9. You can delete the truck by clicking on the red button<br/><br/>

I hope you've enjoyed this test application,

Thank you! :rocket:




