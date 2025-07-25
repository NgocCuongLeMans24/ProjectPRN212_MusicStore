using MusicStore.BLL.Services;
using Xunit;

namespace MusicStoreTests
{
    public class GenreServiceTests
    {
        [Fact]
        public void GetAllGenres_ReturnsList()
        {
            var service = new GenreService();
            var genres = service.GetAllGenres();
            Assert.NotNull(genres);
            Assert.True(genres.Count >= 0); // Có thể là 0 nếu db rỗng
        }
    }
} 