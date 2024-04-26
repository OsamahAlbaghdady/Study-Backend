
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.DegreeFieldFilter;
using BackEndStructuer.DATA.DTOs.DegreeFieldForm;
using BackEndStructuer.DATA.DTOs.DegreeFieldUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;
using BackEndStructuer.DATA.DTOs.DegreeField;

namespace BackEndStructuer.Controllers
{
    public class DegreeFieldsController : BaseController
    {
        private readonly IDegreeFieldServices _degreefieldServices;

        public DegreeFieldsController(IDegreeFieldServices degreefieldServices)
        {
            _degreefieldServices = degreefieldServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<DegreeFieldDto>>> GetAll([FromQuery] DegreeFieldFilter filter) => Ok(await _degreefieldServices.GetAll(filter) , filter.PageNumber);
        
        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<DegreeFieldDto>> GetById(Guid id) => Ok(await _degreefieldServices.GetById(id));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DegreeField>> Create([FromBody] DegreeFieldForm degreefieldForm) => Ok(await _degreefieldServices.Create(degreefieldForm));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<DegreeField>> Update([FromBody] DegreeFieldUpdate degreefieldUpdate, Guid id) => Ok(await _degreefieldServices.Update(id , degreefieldUpdate));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DegreeField>> Delete(Guid id) =>  Ok( await _degreefieldServices.Delete(id));
        
    }
}
