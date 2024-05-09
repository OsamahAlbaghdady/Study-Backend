using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs.SettingDto;
using BackEndStructuer.DATA.DTOs.SettingFilter;
using BackEndStructuer.DATA.DTOs.SettingForm;
using BackEndStructuer.DATA.DTOs.SettingUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;
using Microsoft.EntityFrameworkCore;

namespace BackEndStructuer.Services;

public interface ISettingServices
{
    Task<(List<SettingDto> settings , int? totalCount, string? error)> GetAll();
    Task<(Setting? setting, string? error)> Update(SettingUpdate settingUpdate);
    
}

public class SettingServices : ISettingServices
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public SettingServices(
        IMapper mapper,
        DataContext context ,
        IFileService fileService
    )
    {
        _mapper = mapper;
        _context = context;
        _fileService = fileService;
    }


 
    public async Task<(List<SettingDto> settings , int? totalCount , string? error)> GetAll()
    {
        var settings = await _context.Settings
            .Select(s => _mapper.Map<SettingDto>(s))
            .ToListAsync();
        return (settings, settings.Count, null);
    }

    public async Task<(Setting? setting, string? error)> Update(SettingUpdate settingUpdate)
    {
        var setting = await _context.Settings.FirstOrDefaultAsync();
        if (setting == null) return (null, "Setting not found");
        string? file = null; 
        if (settingUpdate.WelcomeVideoUrl != null)
        {
            var (uploadedFile, error) = await _fileService.Upload(settingUpdate.WelcomeVideoUrl);
            if (error != null)
            {
                return (null, error);
            }
            file = uploadedFile;
        }
        
        setting = _mapper.Map(settingUpdate, setting);
        if (file != null) setting.WelcomeVideoUrl = file;
        await _context.SaveChangesAsync();
        return (setting, null);
    }

   
}