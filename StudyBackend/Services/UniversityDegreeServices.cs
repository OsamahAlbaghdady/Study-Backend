using AutoMapper;
using BackEndStructuer.DATA.DTOs.UniversityDegreeDto;
using BackEndStructuer.DATA.DTOs.UniversityDegreeFilter;
using BackEndStructuer.DATA.DTOs.UniversityDegreeForm;
using BackEndStructuer.DATA.DTOs.UniversityDegreeUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Services;

public interface IUniversityDegreeServices
{
    Task<(UniversityDegree? universityDegree, string? error)> Create(UniversityDegreeForm universityDegreeForm);

    Task<(List<UniversityDegreeDto> universityDegrees, int? totalCount, string? error)> GetAll(
        UniversityDegreeFilter filter);
    
    Task<(UniversityDegreeDto? universityDegree, string? error)> GetById(Guid id);

    Task<(UniversityDegree? universityDegree, string? error)> Update(Guid id,
        UniversityDegreeUpdate universityDegreeUpdate);

    Task<(UniversityDegree? universityDegree, string? error)> Delete(Guid id);
}

public class UniversityDegreeServices : IUniversityDegreeServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public UniversityDegreeServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(UniversityDegree? universityDegree, string? error)> Create(
        UniversityDegreeForm universityDegreeForm)
    {
        var universityDegree = _mapper.Map<UniversityDegree>(universityDegreeForm);
        universityDegree = await _repositoryWrapper.UniversityDegree.Add(universityDegree);
        return  universityDegree == null ? (null, "Error") : (universityDegree, null);
        
    }

    public async Task<(List<UniversityDegreeDto> universityDegrees, int? totalCount, string? error)> GetAll(
        UniversityDegreeFilter filter)
    {
        var (universityDegrees , totalCount) = await _repositoryWrapper.UniversityDegree.GetAll<UniversityDegreeDto>(
            x => (filter.UniversityId == null || x.UniversityId == filter.UniversityId) &&
                 (filter.DegreeId == null || x.DegreeId == filter.DegreeId) ,
            filter.PageNumber, filter.PageSize
            );
        return (universityDegrees, totalCount, null);
    }
    
    // get by id 
    public async Task<(UniversityDegreeDto? universityDegree, string? error)> GetById(Guid id)
    {
        var universityDegree = await _repositoryWrapper.UniversityDegree.Get<UniversityDegreeDto>(
            x => x.Id == id
        );
        return universityDegree == null ? (null, "Not Found") : (universityDegree, null);
    }

    public async Task<(UniversityDegree? universityDegree, string? error)> Update(Guid id,
        UniversityDegreeUpdate universityDegreeUpdate)
    {
        var universityDegree = await _repositoryWrapper.UniversityDegree.GetById(id);
        if (universityDegree == null)
        {
            return (null, "Not Found");
        }

        _mapper.Map(universityDegreeUpdate, universityDegree);
        universityDegree = await _repositoryWrapper.UniversityDegree.Update(universityDegree);
        return universityDegree == null ? (null, "Error") : (universityDegree, null);
    }

    public async Task<(UniversityDegree? universityDegree, string? error)> Delete(Guid id)
    {
        var universityDegree = await _repositoryWrapper.UniversityDegree.GetById(id);
        if (universityDegree == null)
        {
            return (null, "Not Found");
        }

        universityDegree = await _repositoryWrapper.UniversityDegree.Delete(universityDegree.Id);
        return universityDegree == null ? (null, "Error") : (universityDegree, null);
    }
}