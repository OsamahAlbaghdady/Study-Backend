using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class UniversityRepository : GenericRepository<University , Guid> , IUniversityRepository
    {
        public UniversityRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
