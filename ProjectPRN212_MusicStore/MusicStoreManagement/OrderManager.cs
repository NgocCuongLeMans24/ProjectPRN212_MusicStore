using System;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System.Collections.Generic;

namespace MusicStoreManagement
{
    public static class OrderManager
    {
        private static OrderService _service = new OrderService();
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Quản lý Order ---");
                Console.WriteLine("1. Xem danh sách Order của User");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ListOrdersByUser(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); Console.ReadKey(); break;
                }
            }
        }
        private static void ListOrdersByUser()
        {
            Console.Write("Nhập UserId: ");
            int userId = int.Parse(Console.ReadLine());
            var orders = _service.GetAllOrdersByUserId(userId);
            Console.WriteLine("OrderId | OrderDate | Total | Status");
            foreach (var o in orders)
            {
                Console.WriteLine($"{o.OrderId} | {o.OrderDate} | {o.Total} | {o.Status}");
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
} 