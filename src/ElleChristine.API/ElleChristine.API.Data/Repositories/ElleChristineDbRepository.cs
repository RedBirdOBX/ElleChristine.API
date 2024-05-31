using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace ElleChristine.API.Data.Repositories
{
    public class ElleChristineDbRepository : IElleChristineDbRepository
    {
        private ElleChristineDbContext _dbContext;

        public ElleChristineDbRepository(ElleChristineDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(ElleChristineDbContext));
        }

        // shows
        public async Task<IEnumerable<Show>> GetShowsAsync(bool showAll)
        {
            var results = new List<Show>();
            if (showAll)
            {
                results = await _dbContext.Shows.OrderBy(s => s.Date).ToListAsync();
            }
            else
            {
                results = await _dbContext.Shows.Where(c => c.Active == true).OrderBy(c => c.Date).ToListAsync();
            }
            return results;
        }

        public async Task<Show?> GetShowAsync(int showId)
        {
            var show = await _dbContext.Shows.Where(c => c.Id == showId).FirstOrDefaultAsync();
            return show;
        }

        public async Task<bool> DoesShowExistAsync(int showId)
        {
            return await _dbContext.Shows.AnyAsync(c => c.Id == showId);
        }


        // global
        public async Task<bool> SaveChangesAsync()
        {
            // returns count of entities which have been changed.
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}
