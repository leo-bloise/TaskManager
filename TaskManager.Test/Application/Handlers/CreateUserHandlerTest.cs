using System.Text;
using Microsoft.AspNetCore.Identity;
using Moq;
using TaskManager.Api.Application.Handlers;
using TaskManager.Api.Application.Requests;
using TaskManager.Data.Repositories;
using TaskManager.Domain;

namespace TaskManager.Test.Application.Handlers
{
    public class CreateUserHandlerTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IPasswordHasher<User>> _passwordHasherMock;
        public CreateUserHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        }
        [Fact]
        public async Task Handle_UserRequest_ReturnsUserCreated()
        {
            _userRepositoryMock.Setup(m => m.Create(It.IsAny<User>())).Returns((User user) =>
            {
                return new User()
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Email = user.Email,
                    Password = user.Password,
                    Username = user.Username
                };
            });
            _passwordHasherMock.Setup(m => m.HashPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(() =>
            {
                var r = new Random();
                byte[] buffer = new byte[128];
                r.NextBytes(buffer);
                return Encoding.UTF8.GetString(buffer);
            });
            CreateUserHandler createUserHandler = new CreateUserHandler(_userRepositoryMock.Object, _passwordHasherMock.Object);
            CreateUserRequest createUserRequest = new CreateUserRequest("teste", "teste", "teste@gmail.com");
            User user = await createUserHandler.Handle(createUserRequest, CancellationToken.None);
            Assert.Equal(1, user.Id);
            Assert.Equal("teste", user.Username);
            Assert.NotEqual("teste", user.Password);
            Assert.Equal("teste@gmail.com", user.Email);
            _userRepositoryMock.Verify(mock => mock.ExistsByUsernameOrEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _userRepositoryMock.Verify(mock => mock.Create(It.IsAny<User>()), Times.Once());
            _passwordHasherMock.Verify(mock => mock.HashPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once());
        }
        [Fact]
        public async Task Handle_UserRequest_ThrowsApplicationException()
        {
            _userRepositoryMock.Setup(m => m.ExistsByUsernameOrEmail(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            CreateUserHandler createUserHandler = new CreateUserHandler(_userRepositoryMock.Object, _passwordHasherMock.Object);
            CreateUserRequest createUserRequest = new CreateUserRequest("teste", "teste", "teste@gmail.com");
            await Assert.ThrowsAsync<ApplicationException>(() => createUserHandler.Handle(createUserRequest, CancellationToken.None));
            _userRepositoryMock.Verify(mock => mock.ExistsByUsernameOrEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}