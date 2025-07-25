using System;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System.Collections.Generic;

namespace MusicStoreManagement
{
    public static class ArtistManager
    {
        private static ArtistService _service = new ArtistService();
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Quản lý Artist ---");
                Console.WriteLine("1. Xem danh sách Artist");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ListArtists(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); Console.ReadKey(); break;
                }
            }
        }
        private static void ListArtists()
        {
            var artists = _service.GetAllArtists();
            Console.WriteLine("ID | Name");
            foreach (var a in artists)
            {
                Console.WriteLine($"{a.ArtistId} | {a.Name}");
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
} 