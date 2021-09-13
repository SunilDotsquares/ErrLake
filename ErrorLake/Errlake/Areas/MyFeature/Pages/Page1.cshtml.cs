using Errlake.Error.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errlake.MyFeature.Pages
{
    public class Page1Model : PageModel
    {
        public List<Error.Model.Error> errors;
        private readonly IError error;
        public Page1Model(IError error)
        {
            this.error = error;

        }
        public async void OnGet()
        {
            errors = await error.GetErrors();
        }
    }
}
