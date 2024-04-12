using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace razor_page_practice.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(
            ContosoUniversity.Data.SchoolContext context,
            IConfiguration configuration
        )
        {
            _context = context;
            Configuration = configuration;
        }

        public const string NameSortDesc = "name_desc";
        public const string EmptySort = "";
        public const string DateSortAsc = "Date";
        public const string DateSortDesc = "date_desc";

        public string NameSort { get; set; }
        public string DateSort { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public PaginatedList<Student> Students { get; set; } = default!;

        public async Task OnGetAsync(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? pageIndex
        )
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? NameSortDesc : EmptySort;
            DateSort = sortOrder == DateSortAsc ? DateSortDesc : DateSortAsc;
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Student> studentsIQ = from s in _context.Students select s;

            // studentsIQ = sortOrder switch
            // {
            //     "name_desc" => studentsIQ.OrderByDescending(x => x.LastName),
            //     "Date" => studentsIQ.OrderBy(s => s.EnrollmentDate),
            //     "date_desc" => studentsIQ.OrderByDescending(s => s.EnrollmentDate),
            //     _ => studentsIQ.OrderBy(s => s.LastName),
            // };

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(x => x.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(),
                pageIndex ?? 1,
                pageSize
            );
        }
    }
}
