using AutoMapper;
using Crud.API.Controllers;
using Crud.API.Dtos;
using Crud.Application.Interface;
using Crud.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Formats.Asn1;

namespace Crud.Test.Systems.Controllers
{
    public class TestUserController
    {

        [Fact]
        public async Task GetStatusCode200OnInvokRemoveController()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<User>>();
            var controller = new ControllerUser(mockUserService.Object, mockMapper.Object, mockLogger.Object);
            var userDto = new UserDtoSelectOrDelete("dunha@gmai.com");
            var result = (OkObjectResult)await controller.Remove(userDto);
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task CheckMethodAddByInvokeUserService()
        {
            var mockMapper = new Mock<IMapper>();
            var mockService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<User>>();
            var controller = new ControllerUser(mockService.Object, mockMapper.Object, mockLogger.Object);
            var userDto = new UserDto("test", "test@test.com");
            var result = (OkObjectResult)await controller.Add(userDto);
            result.StatusCode.Should().Be(200);

        }
        
        [Fact]
        public async Task GetStatusCode200OnInvokeGetAllController()
        {
            var mockMapper = new Mock<IMapper>();
            var mockService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<User>>();
            var controller = new ControllerUser(mockService.Object, mockMapper.Object, mockLogger.Object);
            var result = (OkObjectResult)await controller.GetAll();
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task GetResultFromMethodGetByEmail()
        {
            var mockMapper = new Mock<IMapper>();
            var mockService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<User>>();
            var controller = new ControllerUser(mockService.Object, mockMapper.Object, mockLogger.Object);
            var userDto = new UserDtoSelectOrDelete("test@test.com");
            var result = (OkObjectResult)await controller.GetByEmail(userDto);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetResultFromMethodEdit()
        {
            var mockMapper = new Mock<IMapper>();
            var mockService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<User>>();
            var controller = new ControllerUser(mockService.Object, mockMapper.Object, mockLogger.Object);
            var userDto = new UserDtoUpdate(1,"test", "test@test.com");
            var result = (OkObjectResult)await controller.Edit(userDto);
            result.StatusCode.Should().Be(200);
        }
    }
}
