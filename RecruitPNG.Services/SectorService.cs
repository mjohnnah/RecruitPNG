using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitPNG.Services
{
   public class SectorService : ISectorService
    {
        private readonly IRepository<Sector> sectorRepository;
        public SectorService(IRepository<Sector> sectorRepository)
        {
            this.sectorRepository = sectorRepository;   
        }
        public void Delete(string id)
        {
            var entity = sectorRepository.Get(id);
            sectorRepository.Delete(entity);
        }

        public Sector Get(string id)
        {
            return sectorRepository.Get(id);
        }

        public IEnumerable<Sector> GetAll()
        {
            return sectorRepository.GetAll();
        }

        public void Insert(Sector entity)
        {
            sectorRepository.Insert(entity);
        }

        public void Update(Sector entity)
        {
            sectorRepository.Update(entity);
        }
    }
    public interface ISectorService
    {
        IEnumerable<Sector> GetAll();
        Sector Get(string id);
        void Insert(Sector entity);
        void Update(Sector entity);
        void Delete(string id);
    }
}
