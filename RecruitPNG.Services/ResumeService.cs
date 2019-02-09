using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IRepository<Resume> resumeRepository;
        public ResumeService(IRepository<Resume> resumeRepository)
        {
            this.resumeRepository = resumeRepository;
        }

        public void Delete(string id)
        {
            var entity = resumeRepository.Get(id);
            resumeRepository.Delete(entity);
        }

        public Resume Get(string id)
        {
            return resumeRepository.Get(id, "ResumeFiles", "JobApplications", "Occupation","Country","City","County");
        }

        public IEnumerable<Resume> GetAll()
        {
            return resumeRepository.GetAll("ResumeFiles", "JobApplications", "Occupation", "Country", "City", "County");
        }

        public IEnumerable<Resume> GetAllByUserName(string userName)
        {
            return resumeRepository.GetMany(m => m.CreatedBy == userName, o=>o.UpdateDate,true, "ResumeFiles", "JobApplications", "Occupation", "Country", "City", "County");
        }

        public void Insert(Resume entity)
        {
            resumeRepository.Insert(entity);
        }

        public void Update(Resume entity)
        {
            resumeRepository.Update(entity);
        }

        public void AddExperiance(Resume entity)
        {
            resumeRepository.Insert(entity);
        }

        public void RemoveExperiance(Resume entity)
        {
            resumeRepository.Delete(entity);
        }
    }

    public interface IResumeService
    {
        IEnumerable<Resume> GetAll();
        IEnumerable<Resume> GetAllByUserName(string userName);
        Resume Get(string id);
        void Insert(Resume entity);
        void Update(Resume entity);
        void AddExperiance(Resume entity);
        void RemoveExperiance(Resume entity);
        void Delete(string id);
    }
}
