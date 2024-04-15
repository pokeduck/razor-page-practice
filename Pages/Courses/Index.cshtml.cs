using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<CourseViewModel> CourseVMs { get; set; } = default!;

        public async Task OnGetAsync()
        {
            CourseVMs = await _context
                .Courses.Select(p => new CourseViewModel
                {
                    CourseID = p.CourseID,
                    Title = p.Title ?? "",
                    Credits = p.Credits,
                    DepartmentName = p.Department.Name
                })
                .ToListAsync();
        }
    }
}
