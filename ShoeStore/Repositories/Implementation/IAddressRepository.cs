using ShoeStore.Entities;

namespace ShoeStore.Repositories.Implementation
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<ICollection<Address>> GetAddressesByCityAsync(string city);
    }
}
