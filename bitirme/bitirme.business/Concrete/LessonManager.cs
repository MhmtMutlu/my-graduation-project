using System.Collections.Generic;
using bitirme.business.Abstract;
using bitirme.data.Abstract;
using bitirme.entity;

namespace bitirme.business.Concrete
{
    public class LessonManager : ILessonService
    {
        private ILessonRepository _lessonRepository;
        public LessonManager(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public void Create(Lesson entity)
        {
            _lessonRepository.Create(entity);
        }

        public void Delete(Lesson entity)
        {
            _lessonRepository.Delete(entity);
        }

        public List<Lesson> GetAll()
        {
            return _lessonRepository.GetAll();
        }

        public Lesson GetById(int id)
        {
            return _lessonRepository.GetById(id);
        }

        public Lesson GetByIdWitNotes(int lessonId)
        {
            return _lessonRepository.GetByIdWitNotes(lessonId);
        }

        public void Update(Lesson entity)
        {
            _lessonRepository.Update(entity);
        }
    }
}