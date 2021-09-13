using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errlake.Error.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Errlake.Areas.Errors.Pages
{
    public class IndexModel : PageModel
    {
        public List<Error.Model.Error> errors;
        private readonly IError error;
        public IndexModel(IError error)
        {
            this.error = error;
        }

        public async void OnGet()
        {
            errors = await error.GetErrors();
        }
    }
}
