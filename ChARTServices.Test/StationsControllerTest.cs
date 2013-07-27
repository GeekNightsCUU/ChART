using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChARTServices.Controllers;
using ChART.Domain.Entities;
using ChART.DataAccess.Abstract;
using Moq;
using System.Web.Mvc;
using ChARTServices.Models;
using ChARTServices.HtmlHelpers;

namespace ChARTServices.Test
{
    [TestClass]
    public class StationsControllerTest
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
            StationListViewModel result = (StationListViewModel)stationController.Index(2).Model;
            //Assert
            Station[] stationArray = result.Stations.ToArray();
            Assert.IsTrue(stationArray.Length == 2);
            Assert.AreEqual(stationArray[0].Name, "S4");
            Assert.AreEqual(stationArray[1].Name, "S5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper helper = null;
            PagingInfo pagingInfo = new PagingInfo { 
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>" 
                + @"<a class=""selected"" href=""Page2"">2</a>"
                + @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IStationRepository> mock = new Mock<IStationRepository>();
            mock.Setup(m => m.Stations).Returns(new Station[] { 
                new Station{Id= 1,Name="S1"},
                new Station{Id= 2,Name="S2"},
                new Station{Id= 3,Name="S3"},
                new Station{Id= 4,Name="S4"},
                new Station{Id= 5,Name="S5"}
            }.AsQueryable());
            StationsController stationController = new StationsController(mock.Object);
            stationController.PageSize = 3;

            StationListViewModel result = (StationListViewModel)stationController.Index(2).Model;
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }
    }
}
