using System;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System.Collections.Generic;

namespace MusicStoreManagement
{
    public static class GenreManager
    {
        private static GenreService _service = new GenreService();
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Quản lý Genre ---");
                Console.WriteLine("1. Xem danh sách Genre");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ListGenres(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); Console.ReadKey(); break;
                }
            }
        }
        private static void ListGenres()
        {
            var genres = _service.GetAllGenres();
            Console.WriteLine("ID | Name | Description");
            foreach (var g in genres)
            {
                Console.WriteLine($"{g.GenreId} | {g.Name} | {g.Description}");
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
} 