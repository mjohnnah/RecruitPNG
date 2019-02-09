using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
   public class CompanyService:ICompanyService
    {
        private readonly IRepository<Company> companyRepository;
        public CompanyService(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void Delete(string id)
        {
            var entity = companyRepository.Get(id);
            companyRepository.Delete(entity);
        }

        public Company Get(string id)
        {
            return companyRepository.Get(id, "Country", "City", "County", "Sector", "Jobs");
        }

        public IEnumerable<Company> GetAll()
        {
            return companyRepository.GetAll("Country", "City", "County", "Sector");
        }
        public IEnumerable<Company> GetAllByUserName(string userName)
        {
            return companyRepository.GetMany(m => m.CreatedBy == userName, o=>o.CreateDate, true,"Country", "City", "County", "Sector");
        }

        public void Insert(Company entity)
        {
            companyRepository.Insert(entity);
        }

        public void Update(Company entity)
        {
            companyRepository.Update(entity);
        }
    }
    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
        IEnumerable<Company> GetAllByUserName(string userName);
        Company Get(string id);
        void Insert(Company entity);
        void Update(Company entity);
        void Delete(string id);
    }
}
