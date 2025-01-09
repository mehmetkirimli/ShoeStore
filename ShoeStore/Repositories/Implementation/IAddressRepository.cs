using ShoeStore.Entities;

namespace ShoeStore.Repositories.Implementation
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<List<Address>> GetAddressesByCityAsync(string city);
    }
}
