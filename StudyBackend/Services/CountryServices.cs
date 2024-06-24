using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs.CountryDto;
using BackEndStructuer.DATA.DTOs.CountryFilter;
using BackEndStructuer.DATA.DTOs.CountryForm;
using BackEndStructuer.DATA.DTOs.CountryUpdate;
using BackEndStructuer.DATA.DTOs.File;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace BackEndStructuer.Services;

public interface ICountryServices
{
    Task<(Country? country, string? error)> Create(CountryForm countryForm);
    Task<(List<CountryDto> countrys, int? totalCount, string? error)> GetAll(CountryFilter filter);

    // get by id
    Task<(Country? country, string? error)> GetById(Guid id);

    Task<(Country? country, string? error)> Update(Guid id, CountryUpdate countryUpdate);
    Task<(Country? country, string? error)> Delete(Guid id);

    // add degree to all univercity and update if found
    Task<(string? file, string? error)> AddDegrees();

    // file excel 
    Task<(string? file, string? error)> ExportExcel(FileForm file, Guid UnivercityId);
}

public class CountryServices : ICountryServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IFileService _fileService;

    private readonly DataContext _context;

    public CountryServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper,
        IFileService fileService,
        DataContext context
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
        _fileService = fileService;
        _context = context;
    }


    public async Task<(Country? country, string? error)> Create(CountryForm countryForm)
    {
        var country = _mapper.Map<Country>(countryForm);

        var (file, error) = await _fileService.Upload(countryForm.Video);
        if (error != null)
        {
            return (null, error);
        }

        country.VideoUrl = file;

        var result = await _repositoryWrapper.Country.Add(country);
        return result == null ? (null, "country could not add") : (country, null);
    }

    public async Task<(List<CountryDto> countrys, int? totalCount, string? error)> GetAll(CountryFilter filter)
    {
        var (countrys, totalCount) = await _repositoryWrapper.Country.GetAll<CountryDto>(
            x => (filter.Name == null || x.Name.Contains(filter.Name)),
            filter.PageNumber, filter.PageSize 
        );
        return (countrys, totalCount, null);
    }

    public async Task<(Country? country, string? error)> GetById(Guid id)
    {
        var country = await _repositoryWrapper.Country.GetById(id);
        return country == null ? (null, "country not found") : (country, null);
    }

    public async Task<(Country? country, string? error)> Update(Guid id, CountryUpdate countryUpdate)
    {
        var country = await _repositoryWrapper.Country.GetById(id);
        if (country == null)
        {
            return (null, "country not found");
        }

        if (countryUpdate.Video != null)
        {
            var (file, error) = await _fileService.Upload(countryUpdate.Video);
            if (error != null)
            {
                return (null, error);
            }

            country.VideoUrl = file;
        }

        country = _mapper.Map(countryUpdate, country);
        var result = await _repositoryWrapper.Country.Update(country);
        return result == null ? (null, "country could not update") : (country, null);
    }

    public async Task<(Country? country, string? error)> Delete(Guid id)
    {
        var country = await _repositoryWrapper.Country.GetById(id);
        if (country == null)
        {
            return (null, "country not found");
        }

        var result = await _repositoryWrapper.Country.Delete(country.Id);
        return result == null ? (null, "country could not delete") : (country, null);
    }

    public async Task<(string? file, string? error)> AddDegrees()
    {
        var univercities = await _context.Universities.ToListAsync();
        var degrees = await _context.Degrees.ToListAsync();
        var univercityDegrees = await _context.UniversityDegrees.ToListAsync();
        
        foreach (var univercity in univercities)
        {
            foreach (var degree in degrees)
            {
                if (univercityDegrees.Any(x => x.UniversityId == univercity.Id && x.DegreeId == degree.Id))
                {
                    continue;
                }

                var univercityDegree = new UniversityDegree
                {
                    UniversityId = univercity.Id,
                    DegreeId = degree.Id
                };

                await _context.UniversityDegrees.AddAsync(univercityDegree);
            }
            
        }

        await _context.SaveChangesAsync();

        return ("Degrees added successfully.", null);
    }

    public async Task<(string? file, string? error)> ExportExcel(FileForm files, Guid UnivercityId)
    {
        // Ensure the file is provided

        // file first 
        var file = files.File;


        if (file == null || file.Length == 0)
        {
            return (null, "No file provided or file is empty.");
        }

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var fileds = await _context.Fields.ToListAsync();
        var dgreeFeilds = await _context.DegreeFields.ToListAsync();

        var BecId = Guid.Parse("ddb9bb4f-e168-4028-8d4e-171d00ffe9bb");
        var MasId = Guid.Parse("4761a608-33a1-43a4-aa96-bd3d9d525c3a");
        var DocId = Guid.Parse("c0d271c7-b3e2-49b5-807a-aae4a59ae87c");

        try
        {
            // Read data from the uploaded file using a MemoryStream
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0; // Reset stream position to the beginning


                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];


                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (worksheet.Cells[row, 1]?.Value == null)
                        {
                            continue;
                        }


                        var filed = fileds.FirstOrDefault(x =>
                            x.Name == worksheet.Cells[row, 1]?.Value?.ToString()?.Trim());

                        if (filed == null)
                        {
                            filed = new Field
                            {
                                Id = Guid.NewGuid(),
                                Name = worksheet?.Cells[row, 1]?.Value?.ToString()?.Trim()
                            };
                            await _context.Fields.AddAsync(filed);
                        }

                        if (dgreeFeilds.Any(x =>
                                x.FieldId == filed.Id && x.UniversityId == UnivercityId && x.DegreeId == BecId))
                        {
                            // update price then continue
                            var oldDegreeFeild = dgreeFeilds.FirstOrDefault(x =>
                                x.FieldId == filed.Id && x.UniversityId == UnivercityId && x.DegreeId == BecId);
                            oldDegreeFeild.Price = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim().Replace(",", "") ?? "0";
                            _context.DegreeFields.Update(oldDegreeFeild);
                            continue;
                        }


                        var degreeFeild = new DegreeField
                        {
                            FieldId = filed.Id,
                            UniversityId = UnivercityId,
                            DegreeId = BecId,
                            Deleted = false,
                            Price = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim().Replace(",", "") ?? "0",
                            StartDate = null,
                            EndDate = null
                        };


                        await _context.DegreeFields.AddAsync(degreeFeild);
                        // await _context.SaveChangesAsync();
                    }


                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (worksheet.Cells[row, 3]?.Value == null)
                        {
                            continue;
                        }

                        var filed = fileds.FirstOrDefault(x =>
                            x.Name == worksheet.Cells[row, 3]?.Value?.ToString()?.Trim());
                        if (filed == null)
                        {
                            filed = new Field
                            {
                                Name = worksheet.Cells[row, 3].Value.ToString()
                            };
                            await _context.Fields.AddAsync(filed);
                        }

                        if (dgreeFeilds.Any(x =>
                                x.FieldId == filed.Id && x.UniversityId == UnivercityId && x.DegreeId == MasId))
                        {
                            // update price then continue
                            var oldDegreeFeild = dgreeFeilds.FirstOrDefault(x =>
                                x.FieldId == filed.Id && x.UniversityId == UnivercityId && x.DegreeId == MasId);
                            oldDegreeFeild.Price = worksheet?.Cells[row, 4]?.Value?.ToString()?.Trim().Replace(",", "") ?? "0";
                            _context.DegreeFields.Update(oldDegreeFeild);
                            continue;
                        }


                        var degreeFeild = new DegreeField
                        {
                            FieldId = filed.Id,
                            UniversityId = UnivercityId,
                            DegreeId = MasId,
                            Deleted = false,
                            Price = worksheet?.Cells[row, 4]?.Value?.ToString()?.Trim().Replace(",", "") ?? "0",
                            StartDate = null,
                            EndDate = null
                        };

                        await _context.DegreeFields.AddAsync(degreeFeild);
                        // await _context.SaveChangesAsync();
                    }


                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (worksheet.Cells[row, 5]?.Value == null)
                        {
                            continue;
                        }

                        var filed = fileds.FirstOrDefault(x =>
                            x.Name == worksheet?.Cells[row, 5]?.Value?.ToString()?.Trim());
                        if (filed == null)
                        {
                            filed = new Field
                            {
                                Name = worksheet.Cells[row, 5].Value.ToString()
                            };
                            await _context.Fields.AddAsync(filed);
                        }

                        if (dgreeFeilds.Any(x =>
                                x.FieldId == filed.Id && x.UniversityId == UnivercityId && x.DegreeId == DocId))
                        {
                            // update price then continue
                            var oldDegreeFeild = dgreeFeilds.FirstOrDefault(x =>
                                x.FieldId == filed.Id && x.UniversityId == UnivercityId && x.DegreeId == DocId);
                            oldDegreeFeild.Price = worksheet?.Cells[row, 6]?.Value?.ToString()?.Trim().Replace(",", "") ?? "0";
                            _context.DegreeFields.Update(oldDegreeFeild);
                            continue;
                        }

                        var degreeFeild = new DegreeField
                        {
                            FieldId = filed.Id,
                            UniversityId = UnivercityId,
                            DegreeId = DocId,
                            Deleted = false,
                            Price = worksheet?.Cells[row, 6]?.Value?.ToString()?.Trim().Replace(",", "") ?? "0",
                            StartDate = null,
                            EndDate = null
                        };

                        await _context.DegreeFields.AddAsync(degreeFeild);
                        // await _context.SaveChangesAsync();
                    }


                    await _context.SaveChangesAsync();
                }
            }


            return ("File processed successfully.", null);
        }
        catch (Exception ex)
        {
            return (null, $"Error processing file: {ex.Message}");
        }
        
        
    }
}

public enum DegreesEnum
{
    Bachelor,
    Master,
    Doctorate
}


