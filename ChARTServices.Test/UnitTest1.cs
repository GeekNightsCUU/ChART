using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChARTServices.Controllers;
using ChART.Domain.Entities;
using ChART.DataAccess.Abstract;
using Moq;

namespace ChARTServices.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Setup
            Mock<IStationRepository> mock = new Mock<IStationRepository>();
            mock.Setup(m => m. Stations).Returns(new Station[] { 
                new Station{Id= 1,Name="S1"},
                new Station{Id= 2,Name="S2"},
                new Station{Id= 3,Name="S3"},
                new Station{Id= 4,Name="S4"},
                new Station{Id= 5,Name="S5"}
            }.AsQueryable());
            StationsController stationController = new StationsController(mock.Object);
            stationController.PageSize = 3;

            //Act
            IEnumerable<Station> result = (IEnumerable<Station>)stationController.Index(2).Model;


            //Assert
            Station[] stationArray = result.ToArray();
            Assert.IsTrue(stationArray.Length == 2);
            Assert.AreEqual(stationArray[0].Name, "S4");
            Assert.AreEqual(stationArray[1].Name, "S5");
        }
    }
}
