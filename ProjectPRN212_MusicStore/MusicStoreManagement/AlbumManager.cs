using System;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System.Collections.Generic;

namespace MusicStoreManagement
{
    public static class AlbumManager
    {
        private static AlbumService _service = new AlbumService();
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Quản lý Album ---");
                Console.WriteLine("1. Xem danh sách Album");
                Console.WriteLine("2. Thêm Album");
                Console.WriteLine("3. Sửa Album");
                Console.WriteLine("4. Xóa Album");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ListAlbums(); break;
                    case "2": AddAlbum(); break;
                    case "3": EditAlbum(); break;
                    case "4": DeleteAlbum(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); Console.ReadKey(); break;
                }
            }
        }
        private static void ListAlbums()
        {
            var albums = _service.GetAllAlbums();
            Console.WriteLine("ID | Title | Price | Stock");
            foreach (var a in albums)
            {
                Console.WriteLine($"{a.AlbumId} | {a.Title} | {a.Price} | {a.Stock}");
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        private static void AddAlbum()
        {
            Console.Write("Nhập tên album: ");
            var title = Console.ReadLine();
            Console.Write("Nhập giá: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Nhập stock: ");
            int stock = int.Parse(Console.ReadLine());
            var album = new Album { Title = title, Price = price, Stock = stock, GenreId = 1, ArtistId = 1, AlbumUrl = "", IsTop10BestSeller = false };
            _service.AddNewAlbum(album);
            Console.WriteLine("Thêm album thành công!");
            Console.ReadKey();
        }
        private static void EditAlbum()
        {
            Console.Write("Nhập ID album cần sửa: ");
            int id = int.Parse(Console.ReadLine());
            var album = _service.GetAlbumById(id);
            if (album == null) { Console.WriteLine("Không tìm thấy album!"); Console.ReadKey(); return; }
            Console.Write($"Tên album ({album.Title}): ");
            var title = Console.ReadLine();
            if (!string.IsNullOrEmpty(title)) album.Title = title;
            Console.Write($"Giá ({album.Price}): ");
            var priceStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(priceStr)) album.Price = decimal.Parse(priceStr);
            Console.Write($"Stock ({album.Stock}): ");
            var stockStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(stockStr)) album.Stock = int.Parse(stockStr);
            _service.UpdateAlbum(album);
            Console.WriteLine("Cập nhật thành công!");
            Console.ReadKey();
        }
        private static void DeleteAlbum()
        {
            Console.Write("Nhập ID album cần xóa: ");
            int id = int.Parse(Console.ReadLine());
            var album = _service.GetAlbumById(id);
            if (album == null) { Console.WriteLine("Không tìm thấy album!"); Console.ReadKey(); return; }
            _service.DeleteAlbum(album);
            Console.WriteLine("Đã xóa album!");
            Console.ReadKey();
        }
    }
} 