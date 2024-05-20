using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using StudyBackend.Interface;

namespace BackEndStructuer.Repository;

public class MedicalFieldRepository : GenericRepository<MedicalField , Guid> , IMedicalFieldRepository
{
    public MedicalFieldRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}