using Crud.Core.Entities;
using Crud.Infraestructure.Interface;
using Crud.Infraestructure.Repository;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Configuration;

namespace Crud.Test.Systems.Repository
{
    public class TestUserRepository
    {
        [Fact]
        public async Task GetUsersFromMethodGetAll()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockLog = new Mock<ILogRepository>();
            var repository = new UserRepository(mockConfiguration.Object, mockLog.Object);
            var result = await repository.SelectAll();
            result.Should().BeOfType<IEnumerable<User>>();
        }

        [Fact]
        public async Task GetNullFromMethodGetAll()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockLog = new Mock<ILogRepository>();
            var repository = new UserRepository(mockConfiguration.Object, mockLog.Object);
            var result = await repository.SelectAll();
            result.Should().BeOfType(null);
        }

        [Fact]
        public async Task CheckResultFromDelete()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockLog = new Mock<ILogRepository>();
            var repository = new UserRepository(mockConfiguration.Object, mockLog.Object);
            var mockRepo = new Mock<IUserRepository>();
            var email = "test@test.com";
            mockRepo.Setup(repo => repo.DeleteByEmail(email));
            await repository.DeleteByEmail(email);
            mockRepo.Verify(repo => repo.DeleteByEmail(email));
        }

        [Fact]
        public void CheckResultFromUpdate()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockLog = new Mock<ILogRepository>();
            var repository = new UserRepository(mockConfiguration.Object, mockLog.Object);
            var mockRepo = new Mock<IUserRepository>();
            var user = new User("test", "test@gmail.com");
            mockRepo.Setup(repo => repo.Update(user));
            mockRepo.Verify(repo => repo.Update(user));
        }

        [Fact]
        public async Task GetResultFromSelectByEmail()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockLog = new Mock<ILogRepository>();
            var repository = new UserRepository(mockConfiguration.Object, mockLog.Object);
            var result = await repository.SelectByEmail("test@test.com");
            result.Should().Be(new User());
        }


    }
}
