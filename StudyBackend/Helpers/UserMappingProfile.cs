using AutoMapper;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.DATA.DTOs.ArticleDto;
using BackEndStructuer.DATA.DTOs.roles;
using BackEndStructuer.DATA.DTOs.User;
using BackEndStructuer.Entities;
using OneSignalApi.Model;
// here to implement
using BackEndStructuer.DATA.DTOs.QuestionDto;
using BackEndStructuer.DATA.DTOs.QuestionForm;
using BackEndStructuer.DATA.DTOs.QuestionUpdate;
using BackEndStructuer.DATA.DTOs.SettingDto;
using BackEndStructuer.DATA.DTOs.SettingForm;
using BackEndStructuer.DATA.DTOs.SettingUpdate;
using BackEndStructuer.DATA.DTOs.DegreeFieldForm;
using BackEndStructuer.DATA.DTOs.DegreeFieldUpdate;
using BackEndStructuer.DATA.DTOs.UniversityDegreeDto;
using BackEndStructuer.DATA.DTOs.UniversityDegreeForm;
using BackEndStructuer.DATA.DTOs.UniversityDegreeUpdate;
using BackEndStructuer.DATA.DTOs.FieldDto;
using BackEndStructuer.DATA.DTOs.FieldForm;
using BackEndStructuer.DATA.DTOs.FieldUpdate;
using BackEndStructuer.DATA.DTOs.DegreeDto;
using BackEndStructuer.DATA.DTOs.DegreeForm;
using BackEndStructuer.DATA.DTOs.DegreeUpdate;
using BackEndStructuer.DATA.DTOs.UniversityDto;
using BackEndStructuer.DATA.DTOs.UniversityForm;
using BackEndStructuer.DATA.DTOs.UniversityUpdate;
using BackEndStructuer.DATA.DTOs.CountryDto;
using BackEndStructuer.DATA.DTOs.CountryForm;
using BackEndStructuer.DATA.DTOs.CountryUpdate;
using BackEndStructuer.DATA.DTOs.DegreeField;
using BackEndStructuer.DATA.DTOs.MedicalFeild;

namespace BackEndStructuer.Helpers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AppUser, UserDto>()
                .ForMember(r => r.Role, src => src.MapFrom(src => src.Role.Name));
            CreateMap<RegisterForm, App>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Role, RoleDto>();
            CreateMap<AppUser, AppUser>();
            CreateMap<Permission, PermissionDto>();


            // here to add
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionForm, Question>();
            CreateMap<QuestionUpdate, Question>();


            CreateMap<MedicalField, MedicalFieldDto>();
            CreateMap<MedicalFieldForm, MedicalField>();
            CreateMap<MedicalFieldUpdate, MedicalField>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;

            CreateMap<Setting, SettingDto>();
            CreateMap<SettingForm, Setting>();
            CreateMap<SettingUpdate, Setting>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;

            CreateMap<DegreeField, DegreeFieldDto>()
                .ForMember(r => r.Degree, src => src.MapFrom(src => src.Degree))
                .ForMember(r => r.Field, src => src.MapFrom(src => src.Field))
                .ForMember(r => r.UniversityName, src => src.MapFrom(src => src.University.Name))
                .ForMember(r => r.CountryName, src => src.MapFrom(src => src.University.Country.Name))
                .ForMember(r => r.IsValid, src => src.MapFrom(src => src.EndDate > DateTime.UtcNow))
                ;


            CreateMap<DegreeFieldForm, DegreeField>();
            CreateMap<DegreeFieldUpdate, DegreeField>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;


            CreateMap<UniversityDegree, UniversityDegreeDto>();
            CreateMap<UniversityDegreeForm, UniversityDegree>();
            CreateMap<UniversityDegreeUpdate, UniversityDegree>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;


            CreateMap<Field, FieldDto>();
            CreateMap<FieldForm, Field>();
            CreateMap<FieldUpdate, Field>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;


            CreateMap<Degree, DegreeDto>();
            CreateMap<DegreeForm, Degree>();
            CreateMap<DegreeUpdate, Degree>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;

            CreateMap<University, UniversityDto>();
            CreateMap<UniversityForm, University>();
            CreateMap<UniversityUpdate, University>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;

            CreateMap<Country, CountryDto>();
            CreateMap<CountryForm, Country>();
            CreateMap<CountryUpdate, Country>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;
        }
    }
}