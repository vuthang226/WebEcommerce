using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.Common
{
    public class ServiceResult
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public WebCode ErrorCode { get; set; }
        public ServiceResult()
        {
            this.Success = true;
        }
        public void OnSuccess(object data, string message = "", WebCode errorCode = WebCode.Success)
        {
            this.Data = data;
            this.Success = true;
            this.Message = message;
            this.ErrorCode = errorCode;
        }
        public void OnError(object data = null, string message = "", WebCode errorCode = WebCode.Fail)
        {
            this.Data = data;
            this.Success = false;
            this.Message = message;
            this.ErrorCode = errorCode;
        }
        public void HandleException(object data = null, Exception ex = null, WebCode errorCode = WebCode.Exception)
        {
            this.Data = data;
            this.Success = false;
            this.Message = ex != null ? ex.ToString() : string.Empty;
            this.ErrorCode = errorCode;
        }
    }
}
