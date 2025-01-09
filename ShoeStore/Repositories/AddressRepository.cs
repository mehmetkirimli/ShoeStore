using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Entities;
using ShoeStore.Repositories.Implementation;

namespace ShoeStore.Repositories
{
    public class AddressRepository : Repository<Address> , IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Address>> GetAddressesByCityAsync(string city)
        {
            return await _dbSet.Where(x => x.City == city).ToListAsync();
        }
    }
}
