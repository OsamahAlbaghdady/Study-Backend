using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class UniversityDegreeRepository : GenericRepository<UniversityDegree , Guid> , IUniversityDegreeRepository
    {
        public UniversityDegreeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
