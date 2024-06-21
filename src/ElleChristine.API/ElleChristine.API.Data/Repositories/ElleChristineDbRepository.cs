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
                results = await _dbContext.Shows.Where(s => s.Active == true).OrderBy(s => s.Date).ToListAsync();
            }
            return results;
        }

        public async Task<Show?> GetShowAsync(int showId)
        {
            var show = await _dbContext.Shows.Where(s => s.Id == showId).FirstOrDefaultAsync();
            return show;
        }

        public async Task<Show?> GetNextShowAsync()
        {
            var shows = await _dbContext.Shows.Where(s => s.Active == true).ToListAsync();
            var show = shows.Where(s => s.Date >= DateTime.Today && s.Active == true).OrderBy(s => s.Date).FirstOrDefault()
                ?? shows.Where(s => s.Date == shows.Max(s => s.Date)).FirstOrDefault();
            return show;
        }

        public async Task<bool> DoesShowExistAsync(int showId)
        {
            return await _dbContext.Shows.AnyAsync(s => s.Id == showId);
        }

        // photos
        public async Task<IEnumerable<Photo>> GetPhotosAsync(bool showAll)
        {
            var results = new List<Photo>();
            if (showAll)
            {
                results = await _dbContext.Photos.OrderByDescending(p => p.Date).ToListAsync();
            }
            else
            {
                results = await _dbContext.Photos.Where(p => p.Active == true).OrderByDescending(p => p.Date).ToListAsync();
            }
            return results;
        }

        public async Task<Photo?> GetPhotoAsync(int photoId)
        {
            var photo = await _dbContext.Photos.Where(p => p.Id == photoId).FirstOrDefaultAsync();
            return photo;
        }

        public async Task<bool> DoesPhotoExistAsync(int photoId)
        {
            return await _dbContext.Photos.AnyAsync(p => p.Id == photoId);
        }

        // global
        public async Task<bool> SaveChangesAsync()
        {
            // returns count of entities which have been changed.
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}
