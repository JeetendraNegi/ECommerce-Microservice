
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using UserManagementService.Controllers;
using UserManagementService.Models;
using UserManagementService.Services;

namespace UserManagementTest.Controllers
{
    public class UsersControllerTests
    {
        private Mock<IDataAccessService<UserDetails>> mockService;
        private UsersController target;

        private readonly UserDetails _userDetails = new UserDetails()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Test",
            LastName = "Test",
            ContactNo = "Test",
        };

        [SetUp]
        public void Setup()
        {
            mockService = new Mock<IDataAccessService<UserDetails>>();
            target = new UsersController(mockService.Object);
        }

        [Test]
        public async Task GetAll_ValidData_ReturnOK()
        {
            var response = new List<UserDetails>();
            response.Add(_userDetails);

            this.mockService.Setup(x => x.GetAllData()).ReturnsAsync(response);

            var result = await this.target.GetAllUsers() as ObjectResult;

            Assert.NotNull(result);
            var data = result.Value as List<UserDetails>;
            Assert.AreEqual(data.First(), _userDetails);
        }
    }
}
