using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using trucks_backend.Controllers;
using trucks_backend.Data;
using trucks_backend.Model;

namespace TrucksAPITester
{
    [TestClass]
    public class TruckCrontrollerTest
    {
        TrucksController? controller;

        [TestInitialize]
        public void Start()
        {
            DbContextOptions<trucks_backendContext> options;
            var builder = new DbContextOptionsBuilder<trucks_backendContext>();
            builder.UseInMemoryDatabase("TrucksDatabase");
            options = builder.Options;
            var context = new trucks_backendContext(options);

            controller = new TrucksController(context);
        }

        #region == POST ========

        [TestMethod]
        public void PostTruckOK()
        {
            var truck = new Truck();

            var response = controller?.PostTruck(truck).Result.Result as CreatedAtActionResult;
            Assert.IsTrue(response?.StatusCode == 201);
        }

        [TestMethod]
        public void PostTruckBadRequestModelYear()
        {
            var truck = new Truck();

            truck.ModelYear = 2020;

            var response = controller?.PostTruck(truck).Result.Result as BadRequestObjectResult;
            Assert.IsTrue(response?.Value.ToString().Contains("must be the current or next year"));
        }

        [TestMethod]
        public void PostTruckBadRequestManufecturingYear()
        {
            var truck = new Truck();

            truck.ManufacturingYear = 2020;

            var response = controller?.PostTruck(truck).Result.Result as BadRequestObjectResult;
            Assert.IsTrue(response?.Value.ToString().Contains("must be the current year"));
        }

        #endregion=

        #region == GET =========

        [TestMethod]
        public void GetTrucks()
        {
            var truck = new Truck();

            var postResponse = controller?.PostTruck(truck).Result.Result as CreatedAtActionResult;

            var getResponse = controller?.GetTruck().Result;
            var trucksList = getResponse?.Value as List<Truck>;

            Assert.IsTrue(trucksList.Count >= 1);
        }

        #endregion

        #region == PUT =========

        [TestMethod]
        public void PutTruckOK()
        {
            var truck = new Truck();

            var postResponse = controller?.PostTruck(truck).Result.Result as CreatedAtActionResult;
            truck = postResponse?.Value as Truck;
            truck.ModelName = "FH 215";

            var response = controller?.PutTruck(truck.Id, truck).Result as NoContentResult;
            Assert.IsTrue(response?.StatusCode == 204);
        }

        [TestMethod]
        public void PutTruckBadRequestModelYear()
        {
            var truck = new Truck(); 

            var postResponse = controller?.PostTruck(truck).Result.Result as CreatedAtActionResult;
            truck = postResponse?.Value as Truck;
            truck.ModelYear = 2020;

            var PutResponse = controller?.PutTruck(truck.Id,truck).Result as BadRequestObjectResult;
            Assert.IsTrue(PutResponse?.Value.ToString().Contains("must be the current or next year"));
        }

        [TestMethod]
        public void PutTruckBadRequestManufacturingYear()
        {
            var truck = new Truck();           

            var postResponse = controller?.PostTruck(truck).Result.Result as CreatedAtActionResult;

            truck = postResponse?.Value as Truck;
            truck.ManufacturingYear = 2020;

            var PutResponse = controller?.PutTruck(truck.Id, truck).Result as BadRequestObjectResult;
            Assert.IsTrue(PutResponse?.Value.ToString().Contains("must be the current year"));
        }

        #endregion

        #region == DELETE ======

        [TestMethod]
        public void DeleteTruckOK()
        {
            var truck = new Truck();
            truck.ModelName = "FM 132";
            truck.ModelYear = DateTime.Today.Year;
            truck.ManufacturingYear = DateTime.Today.Year;

            var postResponse = controller?.PostTruck(truck).Result.Result as CreatedAtActionResult;

            var DeleteResponse = controller?.DeleteTruck(1).Result as NoContentResult;
            Assert.IsTrue(DeleteResponse?.StatusCode == 204);
        }

        #endregion

    }
}