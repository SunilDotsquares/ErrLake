using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Errlake.Error.Model
{
    public class ErrorRequest
    {
        private HttpRequest request;
        public string RequestURI { get; set; }
        public string RequestHeader { get; set; }
        public string RequestQueryString { get; set; }
        public string RequestType { get; set; }
        public string RequestBody { get; set; }
        public ErrorRequest(HttpRequest request)
        {
            this.request = request;
            InitRequest(request);
        }

        private void InitRequest(HttpRequest request)
        {
            this.RequestURI = request.Path;
            this.RequestHeader = JsonSerializer.Serialize(request.Headers.Values);
            this.RequestQueryString = request.QueryString.ToString();
            this.RequestType = request.Method;
            if (RequestType.ToLower() != HttpMethod.Get.ToString().ToLower())
            {
                var reader = new StreamReader(request.Body);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                this.RequestBody = reader.ReadToEnd();
            }
        }
    }
}
