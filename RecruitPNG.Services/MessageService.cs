using RecruitPNG.Data;
using RecruitPNG.Models;
using System;
using System.Collections.Generic;

namespace RecruitPNG.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> messageRepository;
        public MessageService(IRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        public void Delete(string id)
        {
            var entity = messageRepository.Get(id);
            messageRepository.Delete(entity);
        }

        public Message Get(string id)
        {
            return messageRepository.Get(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return messageRepository.GetAll();
        }

        public IEnumerable<Message> GetAllByFrom(IList<string> from)
        {
            return messageRepository.GetMany(m => from.Contains(m.From), o => o.CreateDate, true);
        }

        public IEnumerable<Message> GetAllByTo(IList<string> to)
        {
            return messageRepository.GetMany(m => to.Contains(m.To), o=>o.CreateDate, true);
        }

        public void Insert(Message entity)
        {
            messageRepository.Insert(entity);
        }

        public void Update(Message entity)
        {
            messageRepository.Update(entity);
        }

        

    }

    public interface IMessageService
    {
        IEnumerable<Message> GetAll();
        IEnumerable<Message> GetAllByTo(IList<string> to);
        IEnumerable<Message> GetAllByFrom(IList<string> from);
        Message Get(string id);
        void Insert(Message entity);
        void Update(Message entity);
        void Delete(string id);
    }

}
