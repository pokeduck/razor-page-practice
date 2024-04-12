using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages
{
    public class AboutModel : PageModel
    {
        private readonly SchoolContext _context;
        public IList<EnrollmentDateGroup> Students { get; set; }

        public AboutModel(SchoolContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dategGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dategGroup.Key,
                    StudentCount = dategGroup.Count()
                };

            Students = await data.AsNoTracking().ToListAsync();
        }
    }
}
