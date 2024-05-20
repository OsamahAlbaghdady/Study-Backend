using BackEndStructuer.Controllers;
using BackEndStructuer.DATA.DTOs.MedicalFeild;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyBackend.Controllers;

public class MedicalFieldController : BaseController
{
    
    private readonly IMedicalFieldService _medicalFieldService;
    
    public MedicalFieldController(IMedicalFieldService medicalFieldService)
    {
        _medicalFieldService = medicalFieldService;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MedicalFieldDto>>> GetAll([FromQuery] MedicalFieldFilter filter) => Ok(await _medicalFieldService.GetAll(filter) , filter.PageNumber);
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<MedicalFieldDto>> Create([FromForm] MedicalFieldForm medicalFieldForm) => Ok(await _medicalFieldService.Create(medicalFieldForm));

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<MedicalFieldDto>> Update(Guid id, [FromForm] MedicalFieldUpdate medicalFieldUpdate) => Ok(await _medicalFieldService.Update(id, medicalFieldUpdate));

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MedicalFieldDto>> Delete(Guid id) => Ok(await _medicalFieldService.Delete(id));
    
}