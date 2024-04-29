using AutoMapper;
using BackEndStructuer.DATA.DTOs.UniversityDto;
using BackEndStructuer.DATA.DTOs.UniversityFilter;
using BackEndStructuer.DATA.DTOs.UniversityForm;
using BackEndStructuer.DATA.DTOs.UniversityUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Services;

public interface IUniversityServices
{
    Task<(University? university, string? error)> Create(UniversityForm universityForm);
    Task<(List<UniversityDto> universitys, int? totalCount, string? error)> GetAll(UniversityFilter filter);
    
    // get by id
    Task<(UniversityDto? university, string? error)> GetById(Guid id);
    Task<(University? university, string? error)> Update(Guid id, UniversityUpdate universityUpdate);
    Task<(University? university, string? error)> Delete(Guid id);
}

public class UniversityServices : IUniversityServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public UniversityServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(University? university, string? error)> Create(UniversityForm universityForm)
    {
        var country = await _repositoryWrapper.Country.GetById(universityForm.CountryId);
        if (country == null) return (null, "country not found");
        var university = _mapper.Map<University>(universityForm);
        var result = await _repositoryWrapper.University.Add(university);
        return result == null ? (null, "university could not create") : (university, null);
    }

    public async Task<(List<UniversityDto> universitys, int? totalCount, string? error)> GetAll(UniversityFilter filter)
    {
        var (universitys, totalCount) = await _repositoryWrapper.University.GetAll<UniversityDto>(
            x => (
                (filter.Name == null || filter.Name.Contains(x.Name)) &&
                (filter.CountryId == null || filter.CountryId == x.CountryId) &&
                (filter.FieldId == null || x.UniversityDegrees.Any(ud => ud.Degree.DegreeFields.Any(df => df.FieldId == filter.FieldId)))
                ),
            filter.PageNumber, filter.PageSize
        );
        return (universitys, totalCount, null);
    }
    
    public async Task<(UniversityDto? university, string? error)> GetById(Guid id)
    {
        var university = await _repositoryWrapper.University.Get<UniversityDto>(    
            x => x.Id == id
        );
        return university == null ? (null, "university not found") : (university, null);
    }

    public async Task<(University? university, string? error)> Update(Guid id, UniversityUpdate universityUpdate)
    {
        var university = await _repositoryWrapper.University.GetById(id);
        if (university == null)
        {
            return (null, "university not found");
        }

        university = _mapper.Map(universityUpdate, university);
        var result = await _repositoryWrapper.University.Update(university);
        return result == null ? (null, "university could not update") : (university, null);
    }

    public async Task<(University? university, string? error)> Delete(Guid id)
    {
        var university = await _repositoryWrapper.University.GetById(id);
        if (university == null)
        {
            return (null, "university not found");
        }

        var result = await _repositoryWrapper.University.Delete(university.Id);
        return result == null ? (null, "university could not delete") : (university, null);
    }
}