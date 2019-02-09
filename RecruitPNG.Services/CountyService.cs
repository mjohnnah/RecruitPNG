using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
  public class CountyService : ICountyService
    {
        private readonly IRepository<County> countyRepository;
        public CountyService(IRepository<County> countyRepository)
        {
            this.countyRepository = countyRepository;
        }
        public void Delete(string id)
        {
            var entity = countyRepository.Get(id);
            countyRepository.Delete(entity);
        }

        public County Get(string id)
        {
            return countyRepository.Get(id, "City");
        }

        public IEnumerable<County> GetAll()
        {
            return countyRepository.GetAll("City");
        }

        public IEnumerable<County> GetAllByCityId(string cityId)
        {
            return countyRepository.GetMany(c => c.CityId == cityId, o=>o.Name);
        }

        public void Insert(County entity)
        {
            countyRepository.Insert(entity);
        }

        public void Update(County entity)
        {
            countyRepository.Update(entity);
        }
    }
    public interface ICountyService
    {
        IEnumerable<County> GetAll();
        IEnumerable<County> GetAllByCityId(string countyId);
        County Get(string id);
        void Insert(County entity);
        void Update(County entity);
        void Delete(string id);
    }
}
