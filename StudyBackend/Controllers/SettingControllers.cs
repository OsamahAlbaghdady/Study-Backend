
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.SettingDto;
using BackEndStructuer.DATA.DTOs.SettingFilter;
using BackEndStructuer.DATA.DTOs.SettingForm;
using BackEndStructuer.DATA.DTOs.SettingUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;

namespace BackEndStructuer.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly ISettingServices _settingServices;

        public SettingsController(ISettingServices settingServices)
        {
            _settingServices = settingServices;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<SettingDto>>> GetAll([FromQuery] SettingFilter filter) => Ok(await _settingServices.GetAll(filter));

   
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Setting>> Update([FromBody] SettingUpdate settingUpdate, Guid id) => Ok(await _settingServices.Update(id , settingUpdate));

     
    }
}
