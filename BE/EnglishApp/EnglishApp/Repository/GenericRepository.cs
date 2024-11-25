using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Repository
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        public TContext englishContext = null;
        public DbSet<TEntity> entities = null;

        public GenericRepository()
        {
            this.englishContext = new TContext();
            this.entities = englishContext.Set<TEntity>();
        }
        public GenericRepository(TContext context)
        {
            this.englishContext = context;
            entities = englishContext.Set<TEntity>();
        }
    }
}
