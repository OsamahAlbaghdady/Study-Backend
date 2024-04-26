using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class CountryRepository : GenericRepository<Country , Guid> , ICountryRepository
    {
        public CountryRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
