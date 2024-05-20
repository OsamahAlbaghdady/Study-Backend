using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs.MedicalFeild;
using BackEndStructuer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndStructuer.Services;

public interface IMedicalFieldService
{
    Task<(MedicalField? medicalField, string? error)> Create(MedicalFieldForm medicalFieldRequest);

    // get all
    Task<(List<MedicalField>? medicalFields, int? totalCount, string? error)> GetAll(MedicalFieldFilter filter);

    Task<(MedicalField? medicalField, string? error)> Update(Guid id, MedicalFieldUpdate medicalFieldRequest);

    Task<(MedicalField? medicalField, string? error)> Delete(Guid id);
}

public class MedicalFieldService : IMedicalFieldService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public MedicalFieldService(
        DataContext context,
        IMapper mapper,
        IFileService fileService
    )
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<(MedicalField? medicalField, string? error)> Create(MedicalFieldForm medicalFieldRequest)
    {
        var medicalField = _mapper.Map<MedicalField>(medicalFieldRequest);
        var  (file, error) = await _fileService.Upload(medicalFieldRequest.Video);
        if (error != null)
            return (null, "error while upload file");

        medicalField.VideoUrl = file;
        await _context.MedicalFields.AddAsync(medicalField);
        await _context.SaveChangesAsync();
        return (medicalField, null);
    }


    public async Task<(List<MedicalField>? medicalFields, int? totalCount, string? error)> GetAll(
        MedicalFieldFilter filter)
    {
        var query = _context.MedicalFields.AsQueryable();
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }

        var totalCount = await query.CountAsync();
        var medicalFields =
            await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return (medicalFields, totalCount, null);
    }


    public async Task<(MedicalField? medicalField, string? error)> Update(Guid id,
        MedicalFieldUpdate medicalFieldRequest)
    {
        var medicalField = await _context.MedicalFields.FirstOrDefaultAsync(x => x.Id == id);
        if (medicalField == null)
        {
            return (null, "Medical Field not found");
        }

        
        if (medicalFieldRequest.Video != null)
        {
            var (file, error) = await _fileService.Upload(medicalFieldRequest.Video);
            if (error != null)
                return (null, "error while upload file");
            medicalField.VideoUrl = file;
        }
        
        _mapper.Map(medicalFieldRequest, medicalField);
        await _context.SaveChangesAsync();
        return (medicalField, null);
    }


    public async Task<(MedicalField? medicalField, string? error)> Delete(Guid id)
    {
        var medicalField = await _context.MedicalFields.FirstOrDefaultAsync(x => x.Id == id);
        if (medicalField == null)
        {
            return (null, "Medical Field not found");
        }

        _context.MedicalFields.Remove(medicalField);
        await _context.SaveChangesAsync();
        return (medicalField, null);
    }
}