using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class FieldRepository : GenericRepository<Field , Guid> , IFieldRepository
    {
        public FieldRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
