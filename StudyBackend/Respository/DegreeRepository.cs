using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class DegreeRepository : GenericRepository<Degree , Guid> , IDegreeRepository
    {
        public DegreeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
