using System;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System.Collections.Generic;

namespace MusicStoreManagement
{
    public static class UserManager
    {
        private static UserService _service = new UserService();
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Quản lý User ---");
                Console.WriteLine("1. Xem danh sách User");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ListUsers(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); Console.ReadKey(); break;
                }
            }
        }
        private static void ListUsers()
        {
            var users = _service.GetAllUsers();
            Console.WriteLine("ID | Name | Username | Email | RoleId");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Userid} | {u.Name} | {u.Username} | {u.Email} | {u.RoleId}");
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
} 