using System.Threading.Tasks;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Interfaces;

namespace PortalRandkowy.API.Repository
{
    public class GenericRepository : IGenericRepository
    {
        public DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }



        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll(){
           return await _context.SaveChangesAsync() > 0;
        }
    }
}