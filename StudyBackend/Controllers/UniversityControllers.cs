
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.UniversityDto;
using BackEndStructuer.DATA.DTOs.UniversityFilter;
using BackEndStructuer.DATA.DTOs.UniversityForm;
using BackEndStructuer.DATA.DTOs.UniversityUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;

namespace BackEndStructuer.Controllers
{
    public class UniversitysController : BaseController
    {
        private readonly IUniversityServices _universityServices;

        public UniversitysController(IUniversityServices universityServices)
        {
            _universityServices = universityServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<UniversityDto>>> GetAll([FromQuery] UniversityFilter filter) => Ok(await _universityServices.GetAll(filter) , filter.PageNumber);
        
        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityDto>> GetById(Guid id) => Ok(await _universityServices.GetById(id));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<University>> Create([FromBody] UniversityForm universityForm) => Ok(await _universityServices.Create(universityForm));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<University>> Update([FromBody] UniversityUpdate universityUpdate, Guid id) => Ok(await _universityServices.Update(id , universityUpdate));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<University>> Delete(Guid id) =>  Ok( await _universityServices.Delete(id));
        
    }
}
