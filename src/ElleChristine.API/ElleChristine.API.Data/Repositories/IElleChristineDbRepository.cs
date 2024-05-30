using ElleChristine.API.Data.Entities;

namespace ElleChristine.API.Data.Repositories
{
    public interface IElleChristineDbRepository
    {
        // shows
        Task<IEnumerable<Show>> GetShowsAsync(bool showAll);

        //Task<Show?> GetShowAsync(int showId);
    }
}
