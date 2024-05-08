using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;

namespace BackEndStructuer.Repository
{

    public class QuestionRepository : GenericRepository<Question , Guid> , IQuestionRepository
    {
        public QuestionRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
