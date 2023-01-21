using NUnit.Framework;
using System.Collections.Generic;
using System;
using TollFee.Api.Services;

namespace TollFee.Api.Test
{
    public class TollServiceTests
    {
        private TollService _service;

        [SetUp]
        public void Setup()
        {
           _service = new TollService();
        }

        [Test]
        public void TollService_CalculateToll_CalculaeSomeFee()
        {
            //Arrange
            var request = new List<DateTime>()
            {
                new DateTime(2021, 12, 1, 7, 30, 1),
                new DateTime(2021, 12, 1, 9, 30, 1),
                new DateTime(2021, 1, 1),
                new DateTime(2021, 1, 2)
            }.ToArray();

            //Act
            var response = _service.CalculateFee(request);

            //Assert
            Assert.AreEqual(31, response.TotalFee);
            Assert.AreEqual(7, response.AverageFeePerDay);
        }
    }
}