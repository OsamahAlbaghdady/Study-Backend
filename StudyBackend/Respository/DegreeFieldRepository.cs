using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class DegreeFieldRepository : GenericRepository<DegreeField , Guid> , IDegreeFieldRepository
    {
        public DegreeFieldRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
