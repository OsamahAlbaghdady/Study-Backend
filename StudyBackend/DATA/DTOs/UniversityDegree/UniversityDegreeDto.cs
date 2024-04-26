using BackEndStructuer.Entities;

namespace BackEndStructuer.DATA.DTOs.UniversityDegreeDto
{
    using UniversityDto;
    using DegreeDto;
    public class UniversityDegreeDto : BaseDto<Guid>
    {
        public UniversityDto? University { get; set; }
        
        public DegreeDto? Degree { get; set; }
        
    }
}
