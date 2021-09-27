using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Errlake.Error.Model;

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

        public async Task SaveError(Exception ex, ErrorRequest errorRequest)
        {
            await Task.Run(() =>
            {
                var stackTrace = new StackTrace(ex, true);
                var errorFrame = stackTrace.GetFrame(0);
                var fileName = errorFrame.GetFileName();
                var ErrorLineNumber = errorFrame.GetFileLineNumber();
                var MethodName = errorFrame.GetMethod().Name;

                IsDuplicateError(ex, errorRequest);
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
                    RequestType = errorRequest.RequestType,
                    FilePath = fileName,
                    ErrorLineNo = ErrorLineNumber,
                    ErrorMethodName = MethodName,
                    LastErrorOccurredAt = DateTime.Now
                };
                var mainError = db.GetCollection<Model.Error>("MainError");
                mainError.Insert(error);
            });
        }

        private bool IsDuplicateError(Exception exception, ErrorRequest errorRequest)
        {
            var stackTrace = new StackTrace(exception, true);
            var errorFrame = stackTrace.GetFrame(0);
            var fileName = errorFrame.GetFileName();
            var ErrorLineNumber = errorFrame.GetFileLineNumber();
            var MethodName = errorFrame.GetMethod().Name;
            using var db = new LiteDB.LiteDatabase(DatabaseFile);
            var mainError = db.GetCollection<Model.Error>("MainError");
            //var result = mainError.Query

            return false;
        }
    }
}
