using bitirme.entity;

namespace bitirme.data.Abstract
{
    public interface INoteRepository:IRepository<Note>
    {
        Note GetNoteDetails(int noteId);
    }
}