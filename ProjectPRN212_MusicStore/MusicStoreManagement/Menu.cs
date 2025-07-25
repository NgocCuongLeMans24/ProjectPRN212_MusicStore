using System;

namespace MusicStoreManagement
{
    public static class Menu
    {
        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== MUSIC STORE MANAGEMENT =====");
                Console.WriteLine("1. Quản lý Album");
                Console.WriteLine("2. Quản lý Artist");
                Console.WriteLine("3. Quản lý Genre");
                Console.WriteLine("4. Quản lý User");
                Console.WriteLine("5. Quản lý Order");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AlbumManager.ShowMenu(); break;
                    case "2": ArtistManager.ShowMenu(); break;
                    case "3": GenreManager.ShowMenu(); break;
                    case "4": UserManager.ShowMenu(); break;
                    case "5": OrderManager.ShowMenu(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); Console.ReadKey(); break;
                }
            }
        }
    }
} 