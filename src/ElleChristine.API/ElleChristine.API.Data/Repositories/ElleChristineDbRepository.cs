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

        //public async Task<Show?> GetShowAsync(int categoryId, bool includeSubCategories = true)
        //{

        //    if (includeSubCategories)
        //    {
        //        var category = await _dbContext.Categories.Include(c => c.SubCategories).Where(c => c.Id == categoryId).FirstOrDefaultAsync();
        //        return category;
        //    }
        //    else
        //    {
        //        var category = await _dbContext.Categories.Where(c => c.Id == categoryId).FirstOrDefaultAsync();
        //        return category;
        //    }
        //}


        // global
        public async Task<bool> SaveChangesAsync()
        {
            // returns count of entities which have been changed.
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}
