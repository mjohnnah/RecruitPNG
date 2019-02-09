using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
   public class CityService: ICityService
    {
        private readonly IRepository<City> cityRepository;
        public CityService(IRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public void Delete(string id)
        {
            var entity = cityRepository.Get(id);
            cityRepository.Delete(entity);
        }

        public City Get(string id)
        {
            return cityRepository.Get(id, "Country");
        }

        public IEnumerable<City> GetAll()
        {
            return cityRepository.GetAll("Country");
        }

        public IEnumerable<City> GetAllByCountryId(string countryId)
        {
            return cityRepository.GetMany(c => c.CountryId == countryId, o=>o.Name);
        }

        public void Insert(City entity)
        {
            cityRepository.Insert(entity);
        }

        public void Update(City entity)
        {
            cityRepository.Update(entity);
        }
    }
    public interface ICityService
    {
        IEnumerable<City> GetAll();
        IEnumerable<City> GetAllByCountryId(string countryId);
        City Get(string id);
        void Insert(City entity);
        void Update(City entity);
        void Delete(string id);
    }
}
