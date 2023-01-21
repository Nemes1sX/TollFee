using NUnit.Framework;
using System.Collections.Generic;
using System;
using TollFee.Api.Services;
using TollFee.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TollFee.Api.Test
{
    public class TollServiceTests
    {
        private TollService _service;
        private DbContextOptions<TollDBContext> dbContext = new DbContextOptionsBuilder<TollDBContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

        [SetUp]
        public void Setup()
        {
            var context = new TollDBContext(dbContext);
            new TollSeeding(context).SeedData(2021);
            _service = new TollService(new TollFreeService(context));
        }

        [Test]
        public void TollService_CalculateToll_CalculaeSomeFee()
        {
            //Arrange
            var request = new List<DateTime>()
            {
                new DateTime(2021, 12, 1, 7, 30, 1),
                new DateTime(2021, 12, 1, 9, 30, 1),
                new DateTime(2021, 1, 1, 6, 0, 1),
                new DateTime(2021, 1, 2, 6, 0, 1)
            }.ToArray();
  
            //Act
            var response = _service.CalculateFee(request);

            //Assert
            Assert.AreEqual(31, response.TotalFee);
            Assert.AreEqual(7, response.AverageFeePerDay);
        }

        [Test]
        public void TollService_CalculateToll_NoFeeFreeDays()
        {
            //Arrange
            var request = new List<DateTime>()
            {
                new DateTime(2021, 1, 1, 6, 0, 1),
                new DateTime(2021, 4, 5, 6, 0 , 1),
                new DateTime(2021, 12, 24, 6, 0, 1),
                new DateTime(2021, 12, 23, 6, 0, 1)
            }.ToArray();


            //Act
            var response = _service.CalculateFee(request);

            //Assert
            Assert.AreEqual(0, response.TotalFee);
            Assert.AreEqual(0, response.AverageFeePerDay);
        }


    }
}