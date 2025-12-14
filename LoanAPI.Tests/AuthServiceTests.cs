using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FLoanAPI.Data;
using FLoanAPI.Data.Services;
using FLoanAPI.Domain.Models;
using LoanAPI.Application;

namespace LoanAPI.Tests
{
    public class AuthServiceTests
    {
        private ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnTrue_WhenUserDoesNotExist()
        {
            
            var context = CreateDbContext();

            var hasher = new Mock<IPasswordHasher>();
            var tokenService = new Mock<ITokenService>();
            var logger = new Mock<ILoggerService>();

            hasher.Setup(h => h.HashPassword(It.IsAny<string>()))
                  .Returns("hashed_password");

            var authService = new AuthService(
                context,
                hasher.Object,
                tokenService.Object,
                logger.Object
            );

            var user = new USER { Username = "TestUser", Email = "test@test.com" };

            
            var result = await authService.RegisterAsync(user, "password123");

            
            Assert.True(result);
            Assert.Single(context.Users);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnFalse_WhenUserAlreadyExists()
        {
            
            var context = CreateDbContext();

            context.Users.Add(new USER { Username = "ExistingUser" });
            await context.SaveChangesAsync();

            var authService = new AuthService(
                context,
                Mock.Of<IPasswordHasher>(),
                Mock.Of<ITokenService>(),
                Mock.Of<ILoggerService>()
            );

            var user = new USER { Username = "ExistingUser" };

            var result = await authService.RegisterAsync(user, "password123");

            Assert.False(result);
            Assert.Single(context.Users);
        }




        [Fact]
        public async Task RegisterAsync_ShouldReturnTrue_WhenSavingToDbSucceeds()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            var context = new ApplicationDbContext(options); 

            var hasher = new Mock<IPasswordHasher>();
            hasher.Setup(h => h.HashPassword(It.IsAny<string>()))
                  .Returns("hashed");

            var logger = new Mock<ILoggerService>();

            var authService = new AuthService(
                context,
                hasher.Object,
                Mock.Of<ITokenService>(),
                logger.Object
            );

            var user = new USER { Username = "NormalUser" };

            
            var result = await authService.RegisterAsync(user, "password123");

            
            Assert.True(result); 
        }
    }
}
