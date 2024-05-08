
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Helpers;
using BackEndStructuer.Properties;
using BackEndStructuer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndStructuer.DATA.DTOs.QuestionDto;
using BackEndStructuer.DATA.DTOs.QuestionFilter;
using BackEndStructuer.DATA.DTOs.QuestionForm;
using BackEndStructuer.DATA.DTOs.QuestionUpdate;
using BackEndStructuer.Entities;
using System.Threading.Tasks;

namespace BackEndStructuer.Controllers
{
    public class QuestionsController : BaseController
    {
        private readonly IQuestionServices _questionServices;

        public QuestionsController(IQuestionServices questionServices)
        {
            _questionServices = questionServices;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<QuestionDto>>> GetAll([FromQuery] QuestionFilter filter) => Ok(await _questionServices.GetAll(filter) , filter.PageNumber);

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Question>> Create([FromBody] QuestionForm questionForm) => Ok(await _questionServices.Create(questionForm));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Question>> Update([FromBody] QuestionUpdate questionUpdate, Guid id) => Ok(await _questionServices.Update(id , questionUpdate));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> Delete(Guid id) =>  Ok( await _questionServices.Delete(id));
        
    }
}
