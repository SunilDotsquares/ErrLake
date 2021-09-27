using System;
using System.Collections.Generic;
using System.Text;

namespace Errlake.Error.Model
{
    public class Error
    {
        public Guid Id { get; set; }
        public string ErrorCode { get; set; }
        public DateTime ErrorDate { get; set; }
        public int ErrorCount { get; set; }
        public string ErrorTitle { get; set; }
        public string StackeTrace { get; set; }

        public string RequestURI { get; set; }
        public string RequestHeader { get; set; }
        public string RequestQueryString { get; set; }
        public string RequestType { get; set; }
        public string RequestBody { get; set; }

        public string FilePath { get; set; }
        public string ErrorMethodName { get; set; }
        public int ErrorLineNo { get; set; }
        public DateTime LastErrorOccurredAt { get; set; }
    }
}
