using AutoMapper;
using BackEndStructuer.DATA.DTOs.CountryDto;
using BackEndStructuer.DATA.DTOs.CountryFilter;
using BackEndStructuer.DATA.DTOs.CountryForm;
using BackEndStructuer.DATA.DTOs.CountryUpdate;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;

namespace BackEndStructuer.Services;

public interface ICountryServices
{
    Task<(Country? country, string? error)> Create(CountryForm countryForm);
    Task<(List<CountryDto> countrys, int? totalCount, string? error)> GetAll(CountryFilter filter);
    
    // get by id
    Task<(Country? country, string? error)> GetById(Guid id);
    
    Task<(Country? country, string? error)> Update(Guid id, CountryUpdate countryUpdate);
    Task<(Country? country, string? error)> Delete(Guid id);
}

public class CountryServices : ICountryServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public CountryServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(Country? country, string? error)> Create(CountryForm countryForm)
    {
        var country = _mapper.Map<Country>(countryForm);
        var result = await _repositoryWrapper.Country.Add(country);
        return result == null ? (null, "country could not add") : (country, null);
    }

    public async Task<(List<CountryDto> countrys, int? totalCount, string? error)> GetAll(CountryFilter filter)
    {
        var (countrys, totalCount) = await _repositoryWrapper.Country.GetAll<CountryDto>(
            x => (filter.Name == null ||x.Name.Contains(filter.Name)),
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
}