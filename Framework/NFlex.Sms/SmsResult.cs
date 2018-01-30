namespace NFlex.Sms
{
    public class SmsResult : SmsResult<object>
    {
        public SmsResult(bool isSuccess, string code, string msg, object body = null)
            :base(isSuccess,code,msg,body)
        {
        }
    }

    public class SmsResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

        public T ResultBody { get; set; }

        public SmsResult(bool isSuccess,string code,string msg,T body=default(T))
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = msg;
            ResultBody = body;
        }
    }
}
