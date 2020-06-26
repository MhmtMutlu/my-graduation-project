using System.Collections.Generic;
using bitirme.business.Abstract;
using bitirme.data.Abstract;
using bitirme.entity;

namespace bitirme.business.Concrete
{
    public class NoteManager : INoteService
    {
        INoteRepository _noteRepository;
        public NoteManager(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public bool Create(Note entity)
        {
            if (Validation(entity))
            {
                _noteRepository.Create(entity);
                return true;
            }
            return false;
        }
        public string ErrorMessage { get; set; }

        public void Delete(Note entity)
        {
            _noteRepository.Delete(entity);
        }

        public List<Note> GetAll()
        {
            return _noteRepository.GetAll();
        }

        public Note GetById(int id)
        {
            return _noteRepository.GetById(id);
        }

        public Note GetNoteDetails(int noteId)
        {
            return _noteRepository.GetNoteDetails(noteId);
        }

        public void Update(Note entity)
        {
            _noteRepository.Update(entity);
        }

        public bool Validation(Note entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Title))
            {
                ErrorMessage += "Başlık girmelisiniz.\n";
                isValid = false;
            }

            return isValid;
        }
    }
}