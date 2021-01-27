using System;
namespace Hahn.ApplicationProcess.Application.Utility
{
    public class RestResponse
    {
        public bool IsSuccessStatusCode;
        public string StatusCode;
        public string reasonPhrase;

        public RestResponse(bool isSuccessStatusCode, string StatusCode, string reasonPhrase, string result)
        {
            this.IsSuccessStatusCode = isSuccessStatusCode;
            this.StatusCode = StatusCode;
            this.reasonPhrase = reasonPhrase;
            Result = result;
        }

        public RestResponse(bool isSuccessStatusCode, string StatusCode, string reasonPhrase, byte[] result)
        {
            this.IsSuccessStatusCode = isSuccessStatusCode;
            this.StatusCode = StatusCode;
            this.reasonPhrase = reasonPhrase;
            ResultByteAray = result;
        }
        public string Result { get; set; }
        public byte[] ResultByteAray { get; set; }


    }
}