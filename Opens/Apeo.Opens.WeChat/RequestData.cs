using System.Web;

namespace NFlex.Opens.Weixin
{
    internal class RequestData
    {
        public string Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Echostr { get; set; }
        public string OpenID { get; set; }

        public string ReceiveBody { get; set; }

        public HttpResponse Response { get; set; }


        public T ConvertBodyTo<T>() where T : PushMessage.PushObject
        {
            if (string.IsNullOrEmpty(ReceiveBody)) return null;
            return Xml.ToObject<T>(ReceiveBody);
        }
    }
}
