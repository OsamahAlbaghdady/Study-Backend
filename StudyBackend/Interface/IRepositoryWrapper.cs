using BackEndStructuer.Interface;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IPermissionRepository Permission { get; }

        IRoleRepository Role { get; }

        // here to add
IQuestionRepository Question{get;}
ISettingRepository Setting{get;}
IDegreeFieldRepository DegreeField{get;}
IUniversityDegreeRepository UniversityDegree{get;}
IFieldRepository Field{get;}
        IDegreeRepository Degree { get; }
        IUniversityRepository University { get; }
        ICountryRepository Country { get; }
    }
}
