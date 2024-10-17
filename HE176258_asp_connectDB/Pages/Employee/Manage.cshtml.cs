using HE176258_asp_connectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HE176258_asp_connectDB.Pages.Employee
{
    public class ManageModel : PageModel
    {
        [BindProperty]
        public Models.Employee Employee { get; set; }

        public Models.Employee EmployeesDis { get; set; }

        private prn221_lab2_v2Context _context;

        public ManageModel(prn221_lab2_v2Context context)
        {
            _context = context;
        }
        public List<Models.Employee> Employees { get; set; } = new List<Models.Employee>();


        [BindProperty]
        [Display(Name = "Choose file(s) to upload avatar")]
        public List<IFormFile> AvatarFiles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Employees = await _context.Employees.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatars");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var imageStrList = new List<string>();

            foreach (var formFile in AvatarFiles)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(uploadsFolder, formFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    imageStrList.Add(formFile.FileName);
                }
            }

            var imageStr = string.Join(";", imageStrList);

            var newEmp = new Models.Employee
            {
                Name = Employee.Name,
                Email = Employee.Email,
                Dob = Employee.Dob,
            };
            newEmp.Image = imageStr;
            EmployeesDis = newEmp;
            _context.Employees.Add(newEmp);
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}
