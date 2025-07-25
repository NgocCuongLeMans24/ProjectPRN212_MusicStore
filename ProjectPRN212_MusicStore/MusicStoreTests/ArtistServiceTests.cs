using MusicStore.BLL.Services;
using Xunit;

namespace MusicStoreTests
{
    public class ArtistServiceTests
    {
        [Fact]
        public void GetAllArtists_ReturnsList()
        {
            var service = new ArtistService();
            var artists = service.GetAllArtists();
            Assert.NotNull(artists);
            Assert.True(artists.Count >= 0); // Có thể là 0 nếu db rỗng
        }
    }
}