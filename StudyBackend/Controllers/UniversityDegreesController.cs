
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.UniversityDegreeDto;
using BackEndStructuer.DATA.DTOs.UniversityDegreeFilter;
using BackEndStructuer.DATA.DTOs.UniversityDegreeForm;
using BackEndStructuer.DATA.DTOs.UniversityDegreeUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;

namespace BackEndStructuer.Controllers
{
    public class UniversityDegreesController : BaseController
    {
        
        private readonly IUniversityDegreeServices _universityDegreeServices;

        public UniversityDegreesController(IUniversityDegreeServices universityDegreeServices)
        {
            _universityDegreeServices = universityDegreeServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<UniversityDegreeDto>>> GetAll([FromQuery] UniversityDegreeFilter filter) => Ok(await _universityDegreeServices.GetAll(filter) , filter.PageNumber);
        
        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityDegreeDto>> GetById(Guid id) => Ok(await _universityDegreeServices.GetById(id));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UniversityDegree>> Create([FromBody] UniversityDegreeForm universityDegreeForm) => Ok(await _universityDegreeServices.Create(universityDegreeForm));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UniversityDegree>> Update([FromBody] UniversityDegreeUpdate universityDegreeUpdate, Guid id) => Ok(await _universityDegreeServices.Update(id , universityDegreeUpdate));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UniversityDegree>> Delete(Guid id) =>  Ok( await _universityDegreeServices.Delete(id));
        
    }
}
