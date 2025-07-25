using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using Xunit;
using System;
using System.Linq;

namespace MusicStoreTests
{
    public class AlbumServiceTests
    {
        private string GenerateTestTitle() => $"Test Album {Guid.NewGuid()}";
        private AlbumService service = new AlbumService();

        private Album CreateTestAlbum(string title)
        {
            var album = new Album { Title = title, Price = 10, Stock = 5, GenreId = 1, ArtistId = 1, AlbumUrl = "", IsTop10BestSeller = false };
            service.AddNewAlbum(album);
            return service.GetAllAlbums().First(a => a.Title == title);
        }

        private void DeleteTestAlbum(string title)
        {
            var toDelete = service.GetAllAlbums().FirstOrDefault(a => a.Title == title);
            if (toDelete != null)
                service.DeleteAlbum(toDelete);
        }

        [Fact]
        public void AddNewAlbum_ShouldAddAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            Assert.NotNull(album);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void GetAllAlbums_ShouldContainAddedAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            var albums = service.GetAllAlbums();
            Assert.Contains(albums, a => a.Title == title);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void GetAlbumsByGenre_ShouldContainAddedAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            var byGenre = service.GetAlbumsByGenre(album.GenreId);
            Assert.Contains(byGenre, a => a.Title == title);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void GetAlbumsByArtist_ShouldContainAddedAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            var byArtist = service.GetAlbumsByArtist(album.ArtistId);
            Assert.Contains(byArtist, a => a.Title == title);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void SeacrhAlbumByTitle_ShouldReturnAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            var search = service.SeacrhAlbumByTitle(title);
            Assert.Contains(search, a => a.Title == title);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void GetAlbumById_ShouldReturnAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            var byId = service.GetAlbumById(album.AlbumId);
            Assert.NotNull(byId);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void CheckTitle_ShouldReturnAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            var check = service.CheckTitle(title);
            Assert.NotNull(check);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void UpdateAlbum_ShouldChangePrice()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            var toUpdate = service.GetAllAlbums().FirstOrDefault(a => a.Title == title);
            toUpdate.Price = 20;
            service.UpdateAlbum(toUpdate);
            var updated = service.GetAlbumById(album.AlbumId);
            Assert.Equal(20, updated.Price);
            // Cleanup
            DeleteTestAlbum(title);
        }

        [Fact]
        public void DeleteAlbum_ShouldRemoveAlbum()
        {
            var title = GenerateTestTitle();
            var album = CreateTestAlbum(title);
            DeleteTestAlbum(title);
            var albums = service.GetAllAlbums();
            Assert.DoesNotContain(albums, a => a.Title == title);
        }
    }
} 