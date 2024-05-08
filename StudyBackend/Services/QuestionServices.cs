using AutoMapper;
using BackEndStructuer.DATA.DTOs.QuestionDto;
using BackEndStructuer.DATA.DTOs.QuestionFilter;
using BackEndStructuer.DATA.DTOs.QuestionForm;
using BackEndStructuer.DATA.DTOs.QuestionUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Services;

public interface IQuestionServices
{
    Task<(Question? question, string? error)> Create(QuestionForm questionForm);
    Task<(List<QuestionDto> questions, int? totalCount, string? error)> GetAll(QuestionFilter filter);
    Task<(Question? question, string? error)> Update(Guid id, QuestionUpdate questionUpdate);
    Task<(Question? question, string? error)> Delete(Guid id);
}

public class QuestionServices : IQuestionServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public QuestionServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(Question? question, string? error)> Create(QuestionForm questionForm)
    {
        var question = _mapper.Map<Question>(questionForm);
        var country = await _repositoryWrapper.Country.Get(x => x.Id == questionForm.CountryId);
        if (country == null)
        {
            return (null, "Country not found");
        }

        var result = await _repositoryWrapper.Question.Add(question);
        if (result == null)
        {
            return (null, "Failed to create question");
        }

        return (result, null);
    }

    public async Task<(List<QuestionDto> questions, int? totalCount, string? error)> GetAll(QuestionFilter filter)
    {
        var (questions, totalCount) = await _repositoryWrapper.Question.GetAll<QuestionDto>(
            x => (filter.CountryId == null || filter.CountryId == x.CountryId),
            filter.PageNumber, filter.PageSize
        );

        return (questions, totalCount, null);
    }

    public async Task<(Question? question, string? error)> Update(Guid id, QuestionUpdate questionUpdate)
    {
        var question = await _repositoryWrapper.Question.Get(x => x.Id == id);
        _mapper.Map(questionUpdate, question);
        var result = await _repositoryWrapper.Question.Update(question);
        if (result == null)
        {
            return (null, "Failed to update question");
        }

        return (result, null);
    }

    public async Task<(Question? question, string? error)> Delete(Guid id)
    {
        var question = await _repositoryWrapper.Question.Get(x => x.Id == id);
        if (question == null)
        {
            return (null, "Question not found");
        }

        var result = await _repositoryWrapper.Question.Delete(question.Id);
        if (result == null)
        {
            return (null, "Failed to delete question");
        }

        return (result, null);
    }
}