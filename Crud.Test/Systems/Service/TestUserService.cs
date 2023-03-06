using Crud.Application.Services;
using Crud.Core.Entities;
using Crud.Infraestructure.Interface;
using Crud.Infraestructure.Repository;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Crud.Test.Systems.Service
{
    public class TestUserService
    {
        [Fact]
        public async Task GetMessageFromMethodAddFromService()
        {
            var mockRepository = new Mock<IUserRepository>();
            var service = new UserService(mockRepository.Object);
            var userPaylog = new User("dunha", "dunha@gmail.com", DateTime.Now, "test");
            var result = await service.Add(userPaylog);
            var newUser = await service.GetByEmail(userPaylog.Email);
            if (newUser != null)
                result.Should().Be("Já existe um usuário com este e-mail!");
            else
                result.Should().Be("Usuário inserido");
        }

        [Fact]
        public async Task GetMessageFromMethodUpdateFromService()
        {
            var mockRepository = new Mock<IUserRepository>();
            var service = new UserService(mockRepository.Object);
            var userPaylod = new User(1,"dunha", "dunha@gmail.com", DateTime.Now, "test");
            var result = await service.Edit(userPaylod);
            var checkIfEmailExist = await service.GetByEmail(userPaylod.Email);
            var checkIfUserExist = await service.CheckIfUserExist(userPaylod.Id);
            if (!checkIfUserExist)
                result.Should().Be("Usuário não existe!");
            else
            {
                if (checkIfEmailExist != null)
                    result.Should().Be("Já existe um usuário com este email!");
                else
                    result.Should().Be("Usuário atualizado");
            }
        }

        [Fact]
        public async Task GetAnUserFromMethodGetByEmailFromService()
        {
            var mockRepository = new Mock<IUserRepository>();
            var service = new UserService(mockRepository.Object);
            var email = "dunha@gmail.com";
            var result = await service.GetByEmail(email);
            var chekUser = await service.GetByEmail(email);
            if (chekUser == null)
                result.Should().Be(null);
            else
                result.Should().Be(new User());
        }

        [Fact]
        public async Task CheckResultFromMethodRemove()
        {
            var mockRepository = new Mock<IUserRepository>();
            var service = new UserService(mockRepository.Object);
            var email = "test@test.com";
            mockRepository.Setup(repo => repo.DeleteByEmail(email));
            await service.RemoveByEmail(email);
            mockRepository.Verify(repo => repo.DeleteByEmail(email));
                                    
        }

        [Fact]
        public async Task GetResultOfListUsersFromMethodGetAll()
        {
            var mockRepository = new Mock<IUserRepository>();
            var service = new UserService(mockRepository.Object);
            var result = await service.GetAll();
            if (result.Count() <= 0)
                new List<User>();
            else
                result.Should().BeOfType<IEnumerable<User>>();
        }

    }
}
