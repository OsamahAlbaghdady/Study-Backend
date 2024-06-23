using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs.DegreeField;
using BackEndStructuer.DATA.DTOs.DegreeFieldFilter;
using BackEndStructuer.DATA.DTOs.DegreeFieldForm;
using BackEndStructuer.DATA.DTOs.DegreeFieldUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;
using Microsoft.EntityFrameworkCore;
using StudyBackend.DATA.DTOs.DegreeField;

namespace BackEndStructuer.Services;

public interface IDegreeFieldServices
{
    Task<(DegreeField? degreeField, string? error)> Create(DegreeFieldForm degreeFieldForm);

    // multi create
    Task<(List<DegreeField> degreeFields, string? error)> Create(List<DegreeFieldForm> degreeFieldForms);

    // update all 
    Task<(List<DegreeField> degreeFields, string? error)> Update(List<DegreeFieldMultiUpdate> degreeFieldUpdates);

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
    private readonly DataContext _context;

    public DegreeFieldServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper,
        DataContext context
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
        _context = context;
    }


    public async Task<(DegreeField? degreeField, string? error)> Create(DegreeFieldForm degreeFieldForm)
    {
        var degreeField = new DegreeField()
        {
            EndDate = degreeFieldForm.EndDate.ToUniversalTime(),
            StartDate = degreeFieldForm.StartDate.ToUniversalTime(),
            DegreeId = degreeFieldForm.DegreeId,
            FieldId = degreeFieldForm.FieldId,
            Price = degreeFieldForm.Price,
            UniversityId = degreeFieldForm.UniversityId
        };
        var result = await _repositoryWrapper.DegreeField.Add(degreeField);
        if (result == null) return (null, "Error in creating degreeField");
        return (result, null);
    }

    // multi create 
    public async Task<(List<DegreeField> degreeFields, string? error)> Create(List<DegreeFieldForm> degreeFieldForms)
    {
        try
        {
            var degreeFields = new List<DegreeField>();
            foreach (var degreeFieldForm in degreeFieldForms)
            {
                var degreeField = new DegreeField()
                {
                    EndDate = degreeFieldForm.EndDate.ToUniversalTime(),
                    StartDate = degreeFieldForm.StartDate.ToUniversalTime(),
                    DegreeId = degreeFieldForm.DegreeId,
                    FieldId = degreeFieldForm.FieldId,
                    Price = degreeFieldForm.Price,
                    UniversityId = degreeFieldForm.UniversityId
                };
                degreeFields.Add(degreeField);
            }

            await _context.DegreeFields.AddRangeAsync(degreeFields);
            await _context.SaveChangesAsync();

            return (degreeFields, null);
        }
        catch (Exception e)
        {
            return (null, e.Message);
        }
    }

    // multi update 
    public async Task<(List<DegreeField> degreeFields, string? error)> Update(
        List<DegreeFieldMultiUpdate> degreeFieldUpdates)
    {
        try
        {
            var degreeFields = new List<DegreeField>();
            foreach (var degreeFieldUpdate in degreeFieldUpdates)
            {
                var degreeField = await _context.DegreeFields.FirstOrDefaultAsync(x => x.Id == degreeFieldUpdate.Id);
                if (degreeField == null) return (null, "DegreeField not found")!;
                _mapper.Map(degreeFieldUpdate, degreeField);
                degreeFields.Add(degreeField);
            }

            _context.DegreeFields.UpdateRange(degreeFields);
            await _context.SaveChangesAsync();

            return (degreeFields, null);
        }
        catch (Exception e)
        {
            return (null, e.Message)!;
        }
    }

    public async Task<(List<DegreeFieldDto> degreeFields, int? totalCount, string? error)> GetAll(
        DegreeFieldFilter filter)
    {
        var query = _context.DegreeFields
                .OrderByDescending(x => x.CreationDate)
                .Where(x => (filter.DegreeId == null || x.DegreeId == filter.DegreeId) &&
                            (filter.FieldId == null || x.FieldId == filter.FieldId) &&
                            (filter.CountryId == null || x.University.CountryId == filter.CountryId) &&
                            (filter.StartDate == null ||
                             x.StartDate.Value.ToUniversalTime() >= filter.StartDate.Value.ToUniversalTime()) &&
                            (filter.EndDate == null ||
                             x.EndDate.Value.ToUniversalTime() <= filter.EndDate.Value.ToUniversalTime()) &&
                            (filter.UniversityId == null || x.UniversityId == filter.UniversityId))
            ;


        var totalCount = await query.CountAsync();

        var degreeFields = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ProjectTo<DegreeFieldDto>(_mapper.ConfigurationProvider).ToListAsync();

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

        degreeFieldUpdate.StartDate = degreeFieldUpdate.StartDate?.ToUniversalTime();
        degreeFieldUpdate.EndDate = degreeFieldUpdate.EndDate?.ToUniversalTime();
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