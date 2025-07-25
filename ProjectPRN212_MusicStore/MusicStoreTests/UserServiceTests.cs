using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using Xunit;
using System;
using System.Linq;

namespace MusicStoreTests
{
    public class UserServiceTests
    {
        private string GenerateTestUsername() => $"testuser_{Guid.NewGuid()}";
        private UserService service = new UserService();

        private User CreateTestUser(string username)
        {
            var user = new User
            {
                Name = "Test User",
                Username = username,
                Password = "123456",
                Email = "test@email.com",
                PhoneNumber = "0123456789",
                Address = "Test Address",
                RoleId = 1,
                CreatedAt = DateTime.Now,
                Status = "active",
                TotalAmount = 0
            };
            service.AddUser(user);
            return service.GetAllUsers().First(u => u.Username == username);
        }

        private void DeleteTestUser(string username)
        {
            var toDelete = service.GetAllUsers().FirstOrDefault(u => u.Username == username);
            if (toDelete != null)
                service.DeleteUser(toDelete);
        }

        [Fact]
        public void AddUser_ShouldAddUser()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            Assert.NotNull(user);
            // Cleanup
            DeleteTestUser(username);
        }

        [Fact]
        public void GetAllUsers_ShouldContainAddedUser()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var users = service.GetAllUsers();
            Assert.Contains(users, u => u.Username == username);
            // Cleanup
            DeleteTestUser(username);
        }

        [Fact]
        public void Authenticate_ShouldReturnUser()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var auth = service.Authenticate(username, "123456");
            Assert.NotNull(auth);
            // Cleanup
            DeleteTestUser(username);
        }

        [Fact]
        public void UpdateUser_ShouldChangeName()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var toUpdate = service.GetAllUsers().FirstOrDefault(u => u.Username == username);
            toUpdate.Name = "Updated Name";
            service.UpdateUser(toUpdate);
            var updated = service.GetAllUsers().FirstOrDefault(u => u.Username == username);
            Assert.Equal("Updated Name", updated.Name);
            // Cleanup
            DeleteTestUser(username);
        }

        [Fact]
        public void CheckUserame_ShouldReturnUser()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var check = service.CheckUserame(username);
            Assert.NotNull(check);
            // Cleanup
            DeleteTestUser(username);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUser()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            DeleteTestUser(username);
            var users = service.GetAllUsers();
            Assert.DoesNotContain(users, u => u.Username == username);
        }
    }
} 