using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using Xunit;
using System;
using System.Linq;

namespace MusicStoreTests
{
    public class OrderServiceTests
    {
        private string GenerateTestUsername() => $"testuser_{Guid.NewGuid()}";
        private OrderService service = new OrderService();
        private UserService userService = new UserService();

        private User CreateTestUser(string username)
        {
            var user = new User
            {
                Name = "Order User",
                Username = username,
                Password = "123456",
                Email = "order@email.com",
                PhoneNumber = "0123456789",
                Address = "Test Address",
                RoleId = 1,
                CreatedAt = DateTime.Now,
                Status = "active",
                TotalAmount = 0
            };
            userService.AddUser(user);
            return userService.GetAllUsers().First(u => u.Username == username);
        }

        private void DeleteAllOrdersOfUser(int userId)
        {
            using (var context = new MusicStore.DAL.Models.MusicStorePrn212Context())
            {
                var orders = context.Orders.Where(o => o.Userid == userId).ToList();
                context.Orders.RemoveRange(orders);
                context.SaveChanges();
            }
        }

        private void DeleteTestUser(string username, int userId)
        {
            DeleteAllOrdersOfUser(userId);
            var toDelete = userService.GetAllUsers().FirstOrDefault(u => u.Username == username);
            if (toDelete != null)
                userService.DeleteUser(toDelete);
        }

        private Order CreateTestOrder(User user)
        {
            var order = new Order { Userid = user.Userid, OrderDate = DateTime.Now, Status = "pending", Total = 0, Address = "Test Address" };
            service.AddNewOrder(order);
            return service.GetAllOrdersByUserId(user.Userid).First(o => o.Address == "Test Address");
        }

        [Fact]
        public void AddNewOrder_ShouldAddOrder()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var order = CreateTestOrder(user);
            Assert.NotNull(order);
            // Cleanup
            DeleteTestUser(username, user.Userid);
        }

        [Fact]
        public void GetAllOrdersByUserId_ShouldReturnOrders()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var order = CreateTestOrder(user);
            var orders = service.GetAllOrdersByUserId(user.Userid);
            Assert.Contains(orders, o => o.OrderId == order.OrderId);
            // Cleanup
            DeleteTestUser(username, user.Userid);
        }

        [Fact]
        public void UpdateOrder_ShouldChangeStatus()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var order = CreateTestOrder(user);
            // Update status sang giá trị hợp lệ
            var toUpdate = service.GetAllOrdersByUserId(user.Userid).First(o => o.OrderId == order.OrderId);
            toUpdate.Status = "delivering";
            service.UpdateOrder(toUpdate);
            var updated = service.GetAllOrdersByUserId(user.Userid).First(o => o.OrderId == order.OrderId);
            Assert.Equal("delivering", updated.Status);
            // Cleanup
            DeleteTestUser(username, user.Userid);
        }

        [Fact]
        public void FindPendingOrder_ShouldReturnNullAfterStatusChange()
        {
            var username = GenerateTestUsername();
            var user = CreateTestUser(username);
            var order = CreateTestOrder(user);
            // Đổi status sang delivering để không còn pending
            var toUpdate = service.GetAllOrdersByUserId(user.Userid).First(o => o.OrderId == order.OrderId);
            toUpdate.Status = "delivering";
            service.UpdateOrder(toUpdate);
            var pending = service.FindPendingOrder(user.Userid);
            Assert.Null(pending);
            // Cleanup
            DeleteTestUser(username, user.Userid);
        }
    }
} 