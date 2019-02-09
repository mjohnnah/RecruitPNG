using System.Collections.Generic;
using RecruitPNG.Data;
using RecruitPNG.Models;

namespace RecruitPNG.Services
{
    public class ExperienceService: IExperienceService
    {
        private readonly IRepository<Experience> experienceRepository;
        public ExperienceService(IRepository<Experience> experienceRepository)
        {
            this.experienceRepository = experienceRepository;   
        }
        public void Delete(string id)
        {
            var entity = experienceRepository.Get(id);
            experienceRepository.Delete(entity);
        }

        public Experience Get(string id)
        {
            return experienceRepository.Get(id);
        }

        public IEnumerable<Experience> GetAll()
        {
            return experienceRepository.GetAll();
        }

        public void Insert(Experience entity)
        {
            experienceRepository.Insert(entity);
        }

        public void Update(Experience entity)
        {
            experienceRepository.Update(entity);
        }
    }
    public interface IExperienceService
    {
        IEnumerable<Experience> GetAll();
        Experience Get(string id);
        void Insert(Experience entity);
        void Update(Experience entity);
        void Delete(string id);
    }
}