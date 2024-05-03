using AutoMapper;
using BackEndStructuer.DATA.DTOs.DegreeDto;
using BackEndStructuer.DATA.DTOs.DegreeFilter;
using BackEndStructuer.DATA.DTOs.DegreeForm;
using BackEndStructuer.DATA.DTOs.DegreeUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Services;

public interface IDegreeServices
{
    Task<(Degree? degree, string? error)> Create(DegreeForm degreeForm);
    Task<(List<DegreeDto> degrees, int? totalCount, string? error)> GetAll(DegreeFilter filter);

    // get by id 
    Task<(DegreeDto? degree, string? error)> GetById(Guid id);

    Task<(Degree? degree, string? error)> Update(Guid id, DegreeUpdate degreeUpdate);
    Task<(Degree? degree, string? error)> Delete(Guid id);
}

public class DegreeServices : IDegreeServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IFileService _fileService;

    public DegreeServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper,
        IFileService fileService
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
        _fileService = fileService;
    }


    public async Task<(Degree? degree, string? error)> Create(DegreeForm degreeForm)
    {
        var degree = _mapper.Map<Degree>(degreeForm);
        var res = await _repositoryWrapper.Degree.Add(degree);
        return res == null ? (null, "Error while creation degree") : (res, null);
    }

    public async Task<(List<DegreeDto> degrees, int? totalCount, string? error)> GetAll(DegreeFilter filter)
    {
        var (degrees, totalCount) = await _repositoryWrapper.Degree.GetAll<DegreeDto>(
            x =>
                (filter.Name == null || x.Name.Contains(filter.Name)) &&
                (filter.CountryId == null ||
                 x.UniversityDegrees.Any(cd => cd.University.CountryId == filter.CountryId)) &&
                (filter.UniversityId == null || x.UniversityDegrees.Any(cd => cd.UniversityId == filter.UniversityId))
            ,
            filter.PageNumber, filter.PageSize
        );
        return (degrees, totalCount, null);
    }

    public async Task<(DegreeDto? degree, string? error)> GetById(Guid id)
    {
        var degree = await _repositoryWrapper.Degree.Get<DegreeDto>(
            x => x.Id == id
        );

        return degree == null ? (null, "Degree not found") : (degree, null);
    }


    public async Task<(Degree? degree, string? error)> Update(Guid id, DegreeUpdate degreeUpdate)
    {
        var degree = await _repositoryWrapper.Degree.GetById(id);
        if (degree == null)
        {
            return (null, "Degree not found");
        }

        _mapper.Map(degreeUpdate, degree);

        var res = await _repositoryWrapper.Degree.Update(degree);
        return res == null ? (null, "Error while updating degree") : (res, null);
    }

    public async Task<(Degree? degree, string? error)> Delete(Guid id)
    {
        var degree = await _repositoryWrapper.Degree.GetById(id);
        if (degree == null)
        {
            return (null, "Degree not found");
        }

        var res = await _repositoryWrapper.Degree.Delete(degree.Id);
        return res == null ? (null, "Error while deleting degree") : (res, null);
    }
}