using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.business.Abstract
{
    public interface INoteService: IValidator<Note>
    {
        Note GetById(int id);
        List<Note> GetAll();
        Note GetNoteDetails(int noteId);
        bool Create(Note entity);
        void Update(Note entity);
        void Delete(Note entity);
    }
}