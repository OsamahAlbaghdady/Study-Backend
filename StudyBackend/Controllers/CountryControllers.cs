
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.CountryDto;
using BackEndStructuer.DATA.DTOs.CountryFilter;
using BackEndStructuer.DATA.DTOs.CountryForm;
using BackEndStructuer.DATA.DTOs.CountryUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;

namespace BackEndStructuer.Controllers
{
    public class CountrysController : BaseController
    {
        private readonly ICountryServices _countryServices;

        public CountrysController(ICountryServices countryServices)
        {
            _countryServices = countryServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<CountryDto>>> GetAll([FromQuery] CountryFilter filter) => Ok(await _countryServices.GetAll(filter) , filter.PageNumber);
        
        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetById(Guid id) => Ok(await _countryServices.GetById(id));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Country>> Create([FromBody] CountryForm countryForm) => Ok(await _countryServices.Create(countryForm));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Country>> Update([FromBody] CountryUpdate countryUpdate, Guid id) => Ok(await _countryServices.Update(id , countryUpdate));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> Delete(Guid id) =>  Ok( await _countryServices.Delete(id));
        
    }
}
