
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.FieldDto;
using BackEndStructuer.DATA.DTOs.FieldFilter;
using BackEndStructuer.DATA.DTOs.FieldForm;
using BackEndStructuer.DATA.DTOs.FieldUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;

namespace BackEndStructuer.Controllers
{
    public class FieldsController : BaseController
    {
        private readonly IFieldServices _fieldServices;

        public FieldsController(IFieldServices fieldServices)
        {
            _fieldServices = fieldServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<FieldDto>>> GetAll([FromQuery] FieldFilter filter) => Ok(await _fieldServices.GetAll(filter) , filter.PageNumber);
        
        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldDto>> GetById(Guid id) => Ok(await _fieldServices.GetById(id));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Field>> Create([FromBody] FieldForm fieldForm) => Ok(await _fieldServices.Create(fieldForm));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Field>> Update([FromBody] FieldUpdate fieldUpdate, Guid id) => Ok(await _fieldServices.Update(id , fieldUpdate));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Field>> Delete(Guid id) =>  Ok( await _fieldServices.Delete(id));
        
    }
}
