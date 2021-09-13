using Errlake.Error.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Errlake.Error.Interface
{
    public interface IError
    {
        Task SaveError(Exception ex);
        Task SaveError(Exception ex, ErrorRequest errorRequest);
        Task<List<Model.Error>> GetErrors();    
    }
}
