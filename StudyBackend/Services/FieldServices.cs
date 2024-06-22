using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs.FieldDto;
using BackEndStructuer.DATA.DTOs.FieldFilter;
using BackEndStructuer.DATA.DTOs.FieldForm;
using BackEndStructuer.DATA.DTOs.FieldUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;
using Microsoft.EntityFrameworkCore;

namespace BackEndStructuer.Services;

public interface IFieldServices
{
    Task<(Field? field, string? error)> Create(FieldForm fieldForm);
    Task<(List<FieldDto> fields, int? totalCount, string? error)> GetAll(FieldFilter filter);

    Task<(FieldDto? field, string? error)> GetById(Guid id);

    Task<(Field? field, string? error)> Update(Guid id, FieldUpdate fieldUpdate);
    Task<(Field? field, string? error)> Delete(Guid id);
}

public class FieldServices : IFieldServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly DataContext _context;

    public FieldServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper,
        DataContext context
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
        _context = context;
    }


    public async Task<(Field? field, string? error)> Create(FieldForm fieldForm)
    {
        var field = _mapper.Map<Field>(fieldForm);
        var res = await _repositoryWrapper.Field.Add(field);
        return res == null ? (null, "Error while creation field") : (res, null);
    }

    public async Task<(List<FieldDto> fields, int? totalCount, string? error)> GetAll(FieldFilter filter)
    {
        var fields =  _context.Fields.OrderBy(x => x.Priority)
            .Where(x =>
                (filter.Name == null || x.Name!.Contains(filter.Name)) &&
                (filter.CountryId == null || x.DegreeFields.Any(x => x.University.CountryId == filter.CountryId)) &&
                (filter.DegreeId == null || x.DegreeFields.Any(x => x.DegreeId == filter.DegreeId)) 
            )
         ;

        var totalCount = await fields.CountAsync();


        var response = await fields
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ProjectTo<FieldDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return (response, totalCount, null);
    }

    public async Task<(FieldDto? field, string? error)> GetById(Guid id)
    {
        var field = await _repositoryWrapper.Field.Get<FieldDto>(
            x => x.Id == id
        );
        return field == null ? (null, "Field not found") : (field, null);
    }

    public async Task<(Field? field, string? error)> Update(Guid id, FieldUpdate fieldUpdate)
    {
        var field = await _repositoryWrapper.Field.GetById(id);
        if (field == null)
        {
            return (null, "Field not found");
        }

        _mapper.Map(fieldUpdate, field);
        var res = await _repositoryWrapper.Field.Update(field);
        return res == null ? (null, "Error while updating field") : (res, null);
    }

    public async Task<(Field? field, string? error)> Delete(Guid id)
    {
        var field = await _repositoryWrapper.Field.GetById(id);
        if (field == null)
        {
            return (null, "Field not found");
        }

        var res = await _repositoryWrapper.Field.Delete(field.Id);
        return res == null ? (null, "Error while deleting field") : (field, null);
    }
}