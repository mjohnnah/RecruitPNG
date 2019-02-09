using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
    public class ResumeFileService : IResumeFileService
    {
        private readonly IRepository<ResumeFile> resumeFileRepository;
        public ResumeFileService(IRepository<ResumeFile> resumeFileRepository)
        {
            this.resumeFileRepository = resumeFileRepository;
        }
        public void Delete(string id)
        {
            var entity = resumeFileRepository.Get(id);
            resumeFileRepository.Delete(entity);
        }

        public ResumeFile Get(string id)
        {
            return resumeFileRepository.Get(id, "Resume");
        }

        public IEnumerable<ResumeFile> GetAll()
        {
            return resumeFileRepository.GetAll("Resume");
        }

        public void Insert(ResumeFile entity)
        {
            resumeFileRepository.Insert(entity);
        }

        public void Update(ResumeFile entity)
        {
            resumeFileRepository.Update(entity);
        }
    }
    public interface IResumeFileService
    {
        IEnumerable<ResumeFile> GetAll();
        ResumeFile Get(string id);
        void Insert(ResumeFile entity);
        void Update(ResumeFile entity);
        void Delete(string id);
    }
}
