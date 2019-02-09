using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IRepository<JobApplication> jobApplicationRepository;
        public JobApplicationService(IRepository<JobApplication> jobApplicationRepository)
        {
            this.jobApplicationRepository = jobApplicationRepository;
        }
        public void Delete(string id)
        {
            var entity = jobApplicationRepository.Get(id);
            jobApplicationRepository.Delete(entity);
        }

        public JobApplication Get(string id)
        {
            return jobApplicationRepository.Get(id, "Resume", "Job");
        }

        public IEnumerable<JobApplication> GetAll()
        {
            return jobApplicationRepository.GetAll("Resume", "Job");
        }

        public void Insert(JobApplication entity)
        {
            jobApplicationRepository.Insert(entity);
        }

        public void Update(JobApplication entity)
        {
            jobApplicationRepository.Update(entity);
        }
        public IEnumerable<JobApplication> GetAllByJobs(IList<string> jobIds)
        {
            return jobApplicationRepository.GetMany(j => jobIds.Contains(j.JobId),o=>o.CreateDate,true, "Resume", "Job");
        }
        public IEnumerable<JobApplication> GetAllByUserName(string userName)
        {
            return jobApplicationRepository.GetMany(j => j.CreatedBy == userName, o=>o.CreateDate, true, "Resume", "Job");
        }
    }

    public interface IJobApplicationService
    {
        IEnumerable<JobApplication> GetAllByJobs(IList<string> jobsIds); // list of applicati==ons
        IEnumerable<JobApplication> GetAllByUserName(string userName); // by applicats
        IEnumerable<JobApplication> GetAll();
        JobApplication Get(string id);
        void Insert(JobApplication entity);
        void Update(JobApplication entity);
        void Delete(string id);       
    }
}
