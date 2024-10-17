using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ElleChristine.API.Dtos.Filters;


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
        public async Task<IEnumerable<Show>> GetShowsAsync()
        {
            return await _dbContext.Shows.OrderBy(s => s.Date).ToListAsync();
        }

        public async Task<IEnumerable<Show>> GetShowsFilteredAsync(ShowFilter filter)
        {
            //var results = new List<Show>();

            // default
            if (filter.Active == null && filter.Date == null)
            {
                return await _dbContext.Shows.OrderBy(s => s.Date).ToListAsync();
            }

            // active only
            if (filter.Active != null && filter.Date == null)
            {
                return await _dbContext.Shows.Where(s => s.Active == filter.Active).OrderBy(s => s.Date).ToListAsync();
            }

            // date only
            else if (filter.Active == null && filter.Date != null)
            {
                return await _dbContext.Shows.Where(s => s.Date >= filter.Date).OrderBy(s => s.Date).ToListAsync();
            }

            // date and active
            else
            {
                return await _dbContext.Shows.Where(s => s.Date >= filter.Date && s.Active == filter.Active).OrderBy(s => s.Date).ToListAsync();
            }
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
                results = await _dbContext.Photos.OrderByDescending(p => p.PhotoDate).ToListAsync();
            }
            else
            {
                results = await _dbContext.Photos.Where(p => p.Active == true).OrderByDescending(p => p.PhotoDate).ToListAsync();
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

        // videos
        public async Task<IEnumerable<Video>> GetVideosAsync(bool showAll)
        {
            var results = new List<Video>();
            if (showAll)
            {
                results = await _dbContext.Videos.OrderByDescending(v => v.Added).ToListAsync();
            }
            else
            {
                results = await _dbContext.Videos.Where(v => v.Active == true).OrderByDescending(v => v.Added).ToListAsync();
            }
            return results;
        }

        public async Task<Video?> GetVideoAsync(int videoId)
        {
            var photo = await _dbContext.Videos.Where(p => p.Id == videoId).FirstOrDefaultAsync();
            return photo;
        }

        public async Task<bool> DoesVideoExistAsync(int videoId)
        {
            return await _dbContext.Videos.AnyAsync(v => v.Id == videoId);
        }

        // global
        public async Task<bool> SaveChangesAsync()
        {
            // returns count of entities which have been changed.
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}
