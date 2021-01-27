namespace Hahn.ApplicationProcess.Application.Utility
{
   
    public class BusinessResponse<T>
    {


        public BusinessResponse()
        {

        }
        public BusinessResponse(BusinessResponse<T> response)
        {
            if (response != null)
            {
                Message = response?.Message;
                IsSuccessful = response.IsSuccessful;
                ResponseObject = response.ResponseObject;
            }

        }

        public BusinessResponse(string message, bool isSuccessful)
        {
            Message = message;
            IsSuccessful = isSuccessful;
        }



        public BusinessResponse(bool isSuccessful, string message, T responseObject)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            ResponseObject = responseObject;
        }

        public string UniqueIdentifier { get; set; }
        public string StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public T ResponseObject { get; set; }


    }

    
}