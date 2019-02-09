using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> jobRepository;
        public JobService(IRepository<Job> jobRepository)
        {
            this.jobRepository = jobRepository;
        }
        public void Delete(string id)
        {
            var entity = jobRepository.Get(id);            
            jobRepository.Delete(entity);
        }

        public Job Get(string id)
        {
            return jobRepository.Get(id,"Company", "JobApplications");
        }

        public IEnumerable<Job> GetAll()
        {
          return jobRepository.GetAll("Company", "JobApplications");
        }

        public IEnumerable<Job> GetFeaturedJobs()
        {
            return jobRepository.GetMany(j=>j.IsFeatured == true && j.IsActive == true && j.PublishDate <=DateTime.Now && DateTime.Now<=j.EndDate, o=>o.Position,false,"Company","Company.County");
        }

        public void Insert(Job entity)
        {
            jobRepository.Insert(entity);
        }

        public void Update(Job entity)
        {
            jobRepository.Update(entity);
        }

        public IEnumerable<Job> GetAllByUserName(string userName)
        {
            return jobRepository.GetMany(m => m.CreatedBy == userName, o=>o.EndDate, true, "Company");
        }

        public IEnumerable<Job> Search(string search)
        {
            return jobRepository.GetMany(m => m.Title.Contains(search), o => o.EndDate, true, "Company");
        }
    }

    public interface IJobService
    {
        IEnumerable<Job> GetAll();
        IEnumerable<Job> GetFeaturedJobs();
        IEnumerable<Job> GetAllByUserName(string userName);
        Job Get(string id);
        void Insert(Job entity);
        void Update(Job entity);
        void Delete(string id);
        IEnumerable<Job> Search(string search);
    }
}
