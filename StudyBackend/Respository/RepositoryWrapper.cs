
using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Interface;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private IUserRepository _user;  
        private IPermissionRepository _permission;
        private IRoleRepository _role;
        
        public IRoleRepository Role {  get {
            if(_role == null)
            {
                _role = new RoleRepository(_context,_mapper);
            }
            return _role;
        } }
        
        public IPermissionRepository Permission {  get {
            if(_permission == null)
            {
                _permission = new PermissionRepository(_context,_mapper);
            }
            return _permission;
        } }


 

        
        public IUserRepository User {  get {
            if(_user == null)
            {
                _user = new UserRepository(_context,_mapper);
            }
            return _user;
        } }

       

        public RepositoryWrapper(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        
        
        // here to add
private IQuestionRepository _Question;

public IQuestionRepository Question {
    get {
        if(_Question == null) {
            _Question = new QuestionRepository(_context, _mapper);
        }
        return _Question;
    }
}
private ISettingRepository _Setting;

public ISettingRepository Setting {
    get {
        if(_Setting == null) {
            _Setting = new SettingRepository(_context, _mapper);
        }
        return _Setting;
    }
}
private IDegreeFieldRepository _DegreeField;

public IDegreeFieldRepository DegreeField {
    get {
        if(_DegreeField == null) {
            _DegreeField = new DegreeFieldRepository(_context, _mapper);
        }
        return _DegreeField;
    }
}
private IUniversityDegreeRepository _UniversityDegree;

public IUniversityDegreeRepository UniversityDegree {
    get {
        if(_UniversityDegree == null) {
            _UniversityDegree = new UniversityDegreeRepository(_context, _mapper);
        }
        return _UniversityDegree;
    }
}
private IFieldRepository _Field;

public IFieldRepository Field {
    get {
        if(_Field == null) {
            _Field = new FieldRepository(_context, _mapper);
        }
        return _Field;
    }
}
private IDegreeRepository _Degree;

public IDegreeRepository Degree {
    get {
        if(_Degree == null) {
            _Degree = new DegreeRepository(_context, _mapper);
        }
        return _Degree;
    }
}
private IUniversityRepository _University;

public IUniversityRepository University {
    get {
        if(_University == null) {
            _University = new UniversityRepository(_context, _mapper);
        }
        return _University;
    }
}
private ICountryRepository _Country;

public ICountryRepository Country {
    get {
        if(_Country == null) {
            _Country = new CountryRepository(_context, _mapper);
        }
        return _Country;
    }
}

      



    }
}
