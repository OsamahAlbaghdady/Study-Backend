using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class SettingRepository : GenericRepository<Setting , Guid> , ISettingRepository
    {
        public SettingRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
