using AutoMapper;
using BackEndStructuer.DATA.DTOs.DegreeField;
using BackEndStructuer.DATA.DTOs.DegreeFieldFilter;
using BackEndStructuer.DATA.DTOs.DegreeFieldForm;
using BackEndStructuer.DATA.DTOs.DegreeFieldUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Services;

public interface IDegreeFieldServices
{
    Task<(DegreeField? degreeField, string? error)> Create(DegreeFieldForm degreeFieldForm);
    Task<(List<DegreeFieldDto> degreeFields, int? totalCount, string? error)> GetAll(DegreeFieldFilter filter);
    
    // get by id 
    Task<(DegreeFieldDto? degreeField, string? error)> GetById(Guid id);
    
    Task<(DegreeField? degreeField, string? error)> Update(Guid id, DegreeFieldUpdate degreeFieldUpdate);
    Task<(DegreeField? degreeField, string? error)> Delete(Guid id);
}

public class DegreeFieldServices : IDegreeFieldServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public DegreeFieldServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(DegreeField? degreeField, string? error)> Create(DegreeFieldForm degreeFieldForm)
    {
        var degreeField = _mapper.Map<DegreeField>(degreeFieldForm);
        var result = await _repositoryWrapper.DegreeField.Add(degreeField);
        if (result == null) return (null, "Error in creating degreeField");
        return (result, null);
    }

    public async Task<(List<DegreeFieldDto> degreeFields, int? totalCount, string? error)> GetAll(
        DegreeFieldFilter filter)
    {
        var (degreeFields, totalCount) = await _repositoryWrapper.DegreeField.GetAll<DegreeFieldDto>(
            x => (filter.DegreeId == null || x.DegreeId == filter.DegreeId) &&
                 (filter.FieldId == null || x.FieldId == filter.FieldId),
            filter.PageNumber, filter.PageSize
        );

        return (degreeFields, totalCount, null);
    }
    
    public async Task<(DegreeFieldDto? degreeField, string? error)> GetById(Guid id)
    {
        var degreeField = await _repositoryWrapper.DegreeField.Get<DegreeFieldDto>(
            x => x.Id == id
        );
        return degreeField == null ? (null, "DegreeField not found") : (degreeField, null);
    }

    public async Task<(DegreeField? degreeField, string? error)> Update(Guid id, DegreeFieldUpdate degreeFieldUpdate)
    {
        var degreeField = await _repositoryWrapper.DegreeField.GetById(id);
        if (degreeField == null) return (null, "DegreeField not found");
        _mapper.Map(degreeFieldUpdate, degreeField);
        var result = await _repositoryWrapper.DegreeField.Update(degreeField);
        if (result == null) return (null, "Error in updating degreeField");
        return (result, null);
    }

    public async Task<(DegreeField? degreeField, string? error)> Delete(Guid id)
    {
        var degreeField = await _repositoryWrapper.DegreeField.GetById(id);
        if (degreeField == null) return (null, "DegreeField not found");
        var result = await _repositoryWrapper.DegreeField.Delete(id);
        if (result == null) return (null, "Error in deleting degreeField");
        return (result, null);
    }
}