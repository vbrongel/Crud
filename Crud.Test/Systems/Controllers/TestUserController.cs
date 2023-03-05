using AutoMapper;
using Crud.API.Controllers;
using Crud.API.Dtos;
using Crud.Application.Interface;
using Crud.Application.Services;
using Crud.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Crud.Test.Systems.Controllers
{
    public class TestUserController
    {

        [Fact]
        public async Task GetStatusCode200OnInvokRemoveController()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();
            var controller = new ControllerUser(mockUserService.Object, mockMapper.Object);
            var result = (OkObjectResult)await controller.Remove("dunha@gmail.com");
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task GetSucessInvokeUserService()
        {
            var mockMapper = new Mock<IMapper>();
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetAll())
                       .ReturnsAsync(new List<User>());
            var controller = new ControllerUser(mockService.Object, mockMapper.Object);
            var result = await controller.GetAll();
            mockService.Verify(service => service.GetAll(), Times.Once);

        }
        
        [Fact]
        public async Task GetStatusCode200OnInvokeGetAllController()
        {
            var mockMapper = new Mock<IMapper>();
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetAll())
                      .ReturnsAsync(new List<User>());
            var controller = new ControllerUser(mockService.Object, mockMapper.Object);
            var result = await controller.GetAll();
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<UserDto>>();
        }
    }
}
