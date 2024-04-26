
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.DegreeDto;
using BackEndStructuer.DATA.DTOs.DegreeFilter;
using BackEndStructuer.DATA.DTOs.DegreeForm;
using BackEndStructuer.DATA.DTOs.DegreeUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;

namespace BackEndStructuer.Controllers
{
    public class DegreesController : BaseController
    {
        private readonly IDegreeServices _degreeServices;

        public DegreesController(IDegreeServices degreeServices)
        {
            _degreeServices = degreeServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<DegreeDto>>> GetAll([FromQuery] DegreeFilter filter) => Ok(await _degreeServices.GetAll(filter) , filter.PageNumber);
        
        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<DegreeDto>> GetById(Guid id) => Ok(await _degreeServices.GetById(id));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Degree>> Create([FromBody] DegreeForm degreeForm) => Ok(await _degreeServices.Create(degreeForm));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Degree>> Update([FromBody] DegreeUpdate degreeUpdate, Guid id) => Ok(await _degreeServices.Update(id , degreeUpdate));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Degree>> Delete(Guid id) =>  Ok( await _degreeServices.Delete(id));
        
    }
}
