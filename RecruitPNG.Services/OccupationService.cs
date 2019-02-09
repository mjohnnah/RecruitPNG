using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
    public class OccupationService : IOccupationService
    {
        private readonly IRepository<Occupation> occupationRepository;
        public OccupationService(IRepository<Occupation> occupationRepository)
        {
            this.occupationRepository = occupationRepository;
        }

        public void Delete(string id)
        {
            var entity = occupationRepository.Get(id);
            occupationRepository.Delete(entity);
        }

        public Occupation Get(string id)
        {
            return occupationRepository.Get(id);
        }

        public IEnumerable<Occupation> GetAll()
        {
            return occupationRepository.GetAll();
        }

        public void Insert(Occupation entity)
        {
            occupationRepository.Insert(entity);
        }

        public void Update(Occupation entity)
        {
            occupationRepository.Update(entity);
        }
    }

    public interface IOccupationService
    {
        IEnumerable<Occupation> GetAll();
        Occupation Get(string id);
        void Insert(Occupation entity);
        void Update(Occupation entity);
        void Delete(string id);
    }
}
