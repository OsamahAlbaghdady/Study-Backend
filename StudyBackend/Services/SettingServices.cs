using AutoMapper;
using BackEndStructuer.DATA.DTOs.SettingDto;
using BackEndStructuer.DATA.DTOs.SettingFilter;
using BackEndStructuer.DATA.DTOs.SettingForm;
using BackEndStructuer.DATA.DTOs.SettingUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Services;

public interface ISettingServices
{
    Task<(List<SettingDto> settings, int? totalCount, string? error)> GetAll(SettingFilter filter);
    Task<(Setting? setting, string? error)> Update(Guid id, SettingUpdate settingUpdate);
    
}

public class SettingServices : ISettingServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public SettingServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


 
    public async Task<(List<SettingDto> settings, int? totalCount, string? error)> GetAll(SettingFilter filter)
    {
        var (settings, totalCount) = await _repositoryWrapper.Setting.GetAll(
            filter.PageNumber,
            filter.PageSize
        );
        var settingDtos = _mapper.Map<List<SettingDto>>(settings);
        return (settingDtos, totalCount, null);
    }

    public async Task<(Setting? setting, string? error)> Update(Guid id, SettingUpdate settingUpdate)
    {
        var setting = await _repositoryWrapper.Setting.Get(x => x.Id == id);
        if (setting == null) return (null, "Setting not found");
        setting = _mapper.Map(settingUpdate, setting);
        await _repositoryWrapper.Setting.Update(setting);
        return (setting, null);
            
    }

   
}