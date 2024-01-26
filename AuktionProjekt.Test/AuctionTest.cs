using AuktionProjekt.Controllers;
using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository;
using AuktionProjekt.Repository.Interfaces;
using AuktionProjekt.Repository.Repo;
using AuktionProjekt.ServiceLayer.IService;
using AuktionProjekt.ServiceLayer.Service;
using Autofac.Extras.Moq;
using Castle.Core.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace AuktionProjekt.Test
{
    public class AuctionTest
    {

        [Fact]
        public void Testing_UpdateAuction_In_AuctionService__Auction_To_Update_Does_Not_Exist()
        {
            //Arrange
            int expectedReturn = -1;

            var auctionRepo = new Mock<IAuctionRepo>();
            var bidRepo = new Mock<IBidRepo>();

            //Act
            auctionRepo.Setup(Repo => Repo.GetAuctionById(It.IsAny<int>())).Returns((Auction?)null);

            var Service = new AuctionService(auctionRepo.Object, bidRepo.Object);

            var result = Service.UpdateAuction(new Auction { AuctionID = 1 });

            // Assert
            Assert.Equal(expectedReturn, result);


        }

        [Fact]
        public void Testing_UpdateAUction__In_AuctionService_Unantherized_To_Update()
        {
            //Arrange
            int expectedReturn = 0;

            var auctionRepo = new Mock<IAuctionRepo>();
            var bidRepo = new Mock<IBidRepo>();


            var auctionMock1 = new Auction(new User(1, "", ""));
            var auctionMock2 = new Auction(new User(2, "", ""));


            //Act
            auctionRepo.Setup(Repo => Repo.GetAuctionById(It.IsAny<int>())).Returns(auctionMock1);

            var Service = new AuctionService(auctionRepo.Object, bidRepo.Object);

            var result = Service.UpdateAuction(auctionMock2); ;

            // Assert
            Assert.Equal(expectedReturn, result);


        }

        [Fact]
        public void Testing_UpdateAUction_In_AuctionService__UpdateCorectly_With_New_Price()
        {
            //Arrange
            int expectedReturn = 1;

            var auctionRepo = new Mock<IAuctionRepo>();
            var bidRepo = new Mock<IBidRepo>();

            var auctionMock1 = new Auction(new User(1, "", ""));
            var auctionMock2 = new Auction(new User(1, "", ""));

            //Act
            auctionRepo.Setup(Repo => Repo.GetAuctionById(It.IsAny<int>())).Returns(auctionMock1);
            bidRepo.Setup(repo => repo.GetBids(It.IsAny<int>())).Returns((List<Bid>?)null);

            var Service = new AuctionService(auctionRepo.Object, bidRepo.Object);

            var result = Service.UpdateAuction(auctionMock2); ;

            // Assert
            Assert.Equal(expectedReturn, result);


        }


    }





}
