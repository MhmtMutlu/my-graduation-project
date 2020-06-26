using bitirme.data.Abstract;
using bitirme.entity;
using System.Linq;

namespace bitirme.data.Concrete.EfCore
{
    public class EfCoreNoteRepository : EfCoreGenericRepository<Note, DepartmentContext>, INoteRepository
    {
        public Note GetNoteDetails(int noteId)
        {
            using (var context = new DepartmentContext())
            {
                return context.Notes
                                .Where(i => i.NoteId == noteId)
                                .FirstOrDefault();
            }
        }
    }
}