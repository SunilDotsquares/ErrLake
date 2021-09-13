using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Errlake.Error.Service
{
    public class ErrorService : Interface.IError
    {
        private const string DatabaseFile = "ErrorData.db";

        public async Task<List<Model.Error>> GetErrors()
        {
            using var db = new LiteDB.LiteDatabase(DatabaseFile);
            var mianError = db.GetCollection<Model.Error>("MainError");
            var data = mianError.Query().ToList();
            return data;

        }

        public async Task SaveError(Exception ex)
        {
            await Task.Run(() =>
             {
                 using var db = new LiteDB.LiteDatabase(DatabaseFile);
                 var error = new Model.Error
                 {
                     ErrorCode = ex.GetType().FullName,
                     ErrorDate = DateTime.Now,
                     ErrorTitle = ex.Message,
                     Id = Guid.NewGuid(),
                     StackeTrace = ex.StackTrace
                 };
                 var mianError = db.GetCollection<Model.Error>("MainError");
                 mianError.Insert(error);
             });
        }

        public async Task SaveError(Exception ex, Model.ErrorRequest errorRequest)
        {
            await Task.Run(() =>
            {
                using var db = new LiteDB.LiteDatabase(DatabaseFile);
                var error = new Model.Error
                {
                    ErrorCode = ex.GetType().FullName,
                    ErrorDate = DateTime.Now,
                    ErrorTitle = ex.Message,
                    Id = Guid.NewGuid(),
                    StackeTrace = ex.StackTrace,
                    RequestURI = errorRequest.RequestURI,
                    RequestBody = errorRequest.RequestBody,
                    RequestHeader = errorRequest.RequestHeader,
                    RequestQueryString = errorRequest.RequestQueryString,
                    RequestType = errorRequest.RequestType
                };
                var mianError = db.GetCollection<Model.Error>("MainError");
                mianError.Insert(error);
            });
        }
    }
}
