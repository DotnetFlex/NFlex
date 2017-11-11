using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace NFlex
{
    public sealed class HttpClient
    {
        #region 字段
        //地址栏参数
        private Dictionary<string, string> _queryParams = new Dictionary<string, string>();
        //表单参数
        private Dictionary<string, string> _formParams = new Dictionary<string, string>();
        //文件参数
        private Dictionary<string, FileInfo> _fileParams = new Dictionary<string, FileInfo>();
        //Json参数
        private string _jsonParams = "";
        //Cookie
        private List<CookieData> _cookieList = new List<CookieData>();
        //最后访问地址
        private string _lastRequestUrl = "";
        //请求头信息
        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        #endregion

        #region 属性
        /// <summary>
        /// Cookies
        /// </summary>
        public CookieContainer Cookie { get; set; } = new CookieContainer();

        public Uri BaseUri { get; set; }

        public IWebProxy Proxy { get; set; }
        public Encoding Encoding { get; set; }

        public bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// 输出头信息
        /// </summary>
        public WebHeaderCollection ResponseHeaders { get; private set; } = new WebHeaderCollection();
        //internal string Method { get; set; }
        //internal string RequestUrl { get; set; }

        #endregion

        #region 构造
        public HttpClient()
        {
            Encoding = Encoding.UTF8;
            Proxy = WebRequest.GetSystemWebProxy();
            AllowAutoRedirect = true;

            InitRequestData();
        }

        public HttpClient(string baseUrl):this()
        {
            BaseUri = new Uri(baseUrl);
        }

        public HttpClient(CookieContainer cookie):this()
        {
            Cookie = cookie;
        }
        #endregion

        /// <summary>
        /// 设置允许并发的最大连接数
        /// </summary>
        /// <param name="limit"></param>
        public static void SetConnectionLimit(int limit)
        {
            ServicePointManager.DefaultConnectionLimit = limit;
        }


        #region 请求内容管理

        #region 请求头
        /// <summary>
        /// 添加请求头
        /// </summary>
        /// <param name="header">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public HttpClient AddHeader(string header, string value)
        {
            if (_headers.ContainsKey(header))
                _headers[header] = value;
            else
                _headers.Add(header, value);
            return this;
        }
        #endregion

        #region Cookie
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookie">cookie</param>
        public HttpClient AddCookie(Cookie cookie)
        {
            Cookie.Add(cookie);
            return this;
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="domain">所属域</param>
        /// <param name="path">源服务器URL子集</param>
        /// <param name="expiresDate">到期时间</param>
        public HttpClient AddCookie(string name, string value, string domain = null, string path = "/", DateTime? expiresDate = null)
        {
            _cookieList.Add(new CookieData
            {
                Name = name,
                Value = value,
                Domain = domain,
                Path = path,
                ExpiresDate = expiresDate
            });
            return this;
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">到期时间</param>
        public HttpClient AddCookie(string name, string value, DateTime expiresDate)
        {
            return AddCookie(name, value, null, null, expiresDate);
        }
        #endregion

        #region 请求数据
        /// <summary>
        /// 添加 Url 请求参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public HttpClient AddQuery(string key, object value)
        {
            AddParams(key, value, _queryParams);
            return this;
        }

        /// <summary>
        /// 添加 Form 表单参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public HttpClient AddForm(string key, object value)
        {
            AddParams(key, value, _formParams);
            return this;
        }

        /// <summary>
        /// 添加上传文件
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="fileName">文件名称</param>
        public HttpClient AddFile(string key, string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists) throw new FileNotFoundException(fileName);
            if (_fileParams.ContainsKey(key))
                _fileParams[key] = file;
            else
                _fileParams.Add(key, file);
            return this;
        }

        /// <summary>
        /// 添加 Json 请求体
        /// </summary>
        /// <param name="json">参数对象</param>
        public HttpClient SetJson(object json)
        {
            _jsonParams = json.ToJson();
            return this;
        }

        /// <summary>
        /// 添加 Json 请求体
        /// </summary>
        /// <param name="json">Json字符串</param>
        public HttpClient SetJson(string json)
        {
            _jsonParams = json;
            return this;
        }

        private void AddParams(string key, object value, Dictionary<string, string> dic)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(key));
            if (dic.ContainsKey(key))
                dic[key] = value.ToString();
            else
                dic.Add(key, value.ToString());
        }
        #endregion

        #endregion

        #region 发送请求并返回结果
        public HttpResult Get(string url)
        {
            return SendRequest(url, HttpMethod.Get);
        }

        public HttpResult Post(string url)
        {
            return SendRequest(url, HttpMethod.Post);
        }

        public HttpResult Delete(string url)
        {
            return SendRequest(url, HttpMethod.Delete);
        }

        public HttpResult Put(string url)
        {
            return SendRequest(url, HttpMethod.Put);
        }

        public HttpResult Options(string url)
        {
            return SendRequest(url, HttpMethod.Options);
        }

        public HttpResult SendRequest(string url, HttpMethod method)
        {
            var request = CreateRequest(url, method.ToString());
            var requestData = GetRequestData(request);
            if (requestData != null && requestData.Length > 0)
            {
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(requestData, 0, requestData.Length);
                }
            }
            using (HttpWebResponse response = GetResponse(request))
            {
                Encoding encoding = GetEncoding(response.CharacterSet);
                using (Stream stream = GetResponseStream(response))
                {
                    using (MemoryStream mstream = new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int count = stream.Read(buffer, 0, buffer.Length);
                        while (count > 0)
                        {
                            mstream.Write(buffer, 0, count);
                            count = stream.Read(buffer, 0, buffer.Length);
                        }
                        var data = mstream.ToArray();
                        return new HttpResult(data, encoding, response);
                    }
                }
            }
        }
        #endregion

        #region 私有方法
        private HttpWebRequest CreateRequest(string url, string method)
        {
            if (BaseUri != null)
                url = new Uri(BaseUri, url).ToString();
            if (url.IndexOf("http:") != 0 && url.IndexOf("https:") != 0)
            {
                url = "http://" + url.TrimStart('/');
            }

            HttpWebRequest request = null;
            AppendUrlData(ref url);

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request.Proxy = Proxy;
            SetRequestHeader(request);
            SetCookies(request);
            request.Method = method;
            _lastRequestUrl = url;
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = false;
            return request;
        }

        private void SetRequestHeader(HttpWebRequest request)
        {
            request.Headers.Clear();
            foreach (var kv in _headers)
            {
                var key = kv.Key;
                var value = kv.Value;

                if (key.ToLower() == "accept") { request.Accept = value; continue; }
                if (key.ToLower() == "connection") { request.Connection = value; continue; }
                if (key.ToLower() == "content-length") { request.ContentLength = value.To<long>(); continue; }
                if (key.ToLower() == "expect") { request.Expect = value; continue; }
                if (key.ToLower() == "date") { request.Date = value.To<DateTime>(); continue; }
                if (key.ToLower() == "host") { request.Host = value; continue; }
                if (key.ToLower() == "if-modified-since") { request.IfModifiedSince = value.To<DateTime>(); continue; }
                if (key.ToLower() == "range") { request.AddRange(value.Split(',')[0].To<long>(), value.Split(',')[1].To<long>()); continue; }
                if (key.ToLower() == "referer") { request.Referer = value; continue; }
                if (key.ToLower() == "transfer-encoding") { request.TransferEncoding = value; continue; }
                if (key.ToLower() == "user-agent") { request.UserAgent = value; continue; }

                request.Headers.Add(key, value);
            }
            request.CookieContainer = Cookie;
        }

        private void SetCookies(HttpWebRequest request)
        {
            foreach(var c in _cookieList)
            {
                Cookie.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain ?? request.RequestUri.Host) {
                    Expires = c.ExpiresDate??DateTime.Now.AddYears(1)
                });
            }
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        private byte[] GetRequestData(HttpWebRequest request)
        {
            byte[] requestData = null;
            if (!string.IsNullOrEmpty(_jsonParams))
            {
                requestData = GetJsonData(request);
            }
            else
            {
                if (_fileParams.Any())
                {
                    var boundary = string.Format("----{0}", DateTime.Now.Ticks.ToString("x"));
                    request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                    requestData = GetFormDataWithFile(request, boundary);
                }
                else if (_formParams.Any())
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    requestData = GetFormData(request);
                }
            }
            InitRequestData();
            return requestData;
        }

        private void InitRequestData()
        {
            _fileParams.Clear();
            _formParams.Clear();
            _queryParams.Clear();
            _headers.Clear();
            _jsonParams = "";

            AddHeader("Accept", "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*")
                .AddHeader("Accept-Langauge", "zh-CN")
                .AddHeader("Accept-Encoding", "gzip, deflate")
                .AddHeader("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)")
                ;
        }

        private void AppendUrlData(ref string url)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kv in _queryParams)
            {
                sb.Append(kv.Key).Append("=").Append(kv.Value.ToString()).Append("&");
            }
            var _params = sb.ToString().TrimEnd('&');

            if (!string.IsNullOrWhiteSpace(_params))
            {
                url = url.TrimEnd('&').TrimEnd('?');
                if (url.IndexOf("?") != -1)
                    url = url + "&" + _params;
                else
                    url = url + "?" + _params;
            }
        }

        private byte[] GetFormData(HttpWebRequest request)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kv in _formParams)
            {
                sb.Append(kv.Key).Append("=").Append(kv.Value.ToString()).Append("&");
            }
            var postData = Encoding.GetBytes(sb.ToString().TrimEnd('&'));
            return postData;
        }

        private byte[] GetFormDataWithFile(HttpWebRequest request, string boundary)
        {
            string beginBoundary = string.Format("--{0}\r\n", boundary),
                endBoundary = string.Format("\r\n--{0}--\r\n", boundary);
            byte[] beginBoundaryBytes = Encoding.GetBytes(beginBoundary),
                endBoundaryBytes = Encoding.GetBytes(endBoundary);

            using (MemoryStream memory = new MemoryStream())
            {
                memory.Write(beginBoundaryBytes, 0, beginBoundaryBytes.Length);
                //组装Post参数
                foreach (var kv in _formParams)
                {
                    string kvTemp = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n{2}", kv.Key, kv.Value.ToString(), beginBoundary);
                    var kvTempBytes = Encoding.GetBytes(kvTemp);
                    memory.Write(kvTempBytes, 0, kvTempBytes.Length);
                }
                //组装上传的文件参数
                foreach (var kv in _fileParams)
                {
                    string kvTemp = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", kv.Key, kv.Value.Name);
                    var kvTempBytes = Encoding.GetBytes(kvTemp);
                    memory.Write(kvTempBytes, 0, kvTempBytes.Length);


                    using (Stream stream = kv.Value.OpenRead())
                    {
                        byte[] buffer = new byte[1024];
                        int size = stream.Read(buffer, 0, buffer.Length);
                        while (size > 0)
                        {
                            memory.Write(buffer, 0, buffer.Length);
                            size = stream.Read(buffer, 0, buffer.Length);
                        }
                    }
                    memory.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                }

                var postData = memory.ToArray();

                return postData;
            }
        }

        private byte[] GetJsonData(HttpWebRequest request)
        {
            request.ContentType = "application/json";
            var postData = Encoding.GetBytes(_jsonParams);

            return postData;
        }

        private Encoding GetEncoding(string charset)
        {
            if (Encoding != null) return Encoding;
            if (!string.IsNullOrWhiteSpace(charset))
                return Encoding.GetEncoding(charset);
            return Encoding.Default;
        }

        private HttpWebResponse GetResponse(HttpWebRequest request)
        {
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            ResponseHeaders = response.Headers;

            if (AllowAutoRedirect && (response.StatusCode == HttpStatusCode.Redirect || response.StatusCode == HttpStatusCode.Found || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.MovedPermanently))
            {
                response.Close();
                var location = ResponseHeaders[HttpResponseHeader.Location];
                HttpWebRequest req = CreateRequest(location, "GET");
                req.Referer = _lastRequestUrl;
                response = GetResponse(req);
            }

            return response;
        }

        /// <summary>
        /// 获取合适的页面输出流
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private Stream GetResponseStream(HttpWebResponse response)
        {
            Stream responseStream = response.GetResponseStream();
            Stream stream = null;
            if (response.ContentEncoding.ToLower() == "gzip")
                stream = new GZipStream(responseStream, CompressionMode.Decompress);
            else if (response.ContentEncoding.ToLower() == "deflate")
                stream = new DeflateStream(responseStream, CompressionMode.Decompress);
            else
                stream = responseStream;

            return stream;
        }
        #endregion

        #region 内部类
        private class CookieData
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Domain { get; set; }
            public string Path { get; set; }
            public DateTime? ExpiresDate { get; set; }
        }
        #endregion
    }


    /// <summary>
    /// Http请求返回结果对象
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// 返回的字节内容
        /// </summary>
        public byte[] ResponseData { get; set; }

        /// <summary>
        /// 输出编码
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 服务器输出的头信息
        /// </summary>
        public WebHeaderCollection ResponseHeader { get; set; }

        /// <summary>
        /// 服务器向客户端新增的 Cookie
        /// </summary>
        public CookieCollection ResponseCookie { get; set; }

        /// <summary>
        /// 服务器输出的内容类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 返回内容是否来自缓存
        /// </summary>
        public bool IsFromCache { get; set; }

        /// <summary>
        /// 请求内容最后修改日期
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// 服务器返回的状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        internal HttpResult(byte[] data, Encoding encoding, HttpWebResponse response)
        {
            ResponseData = data;
            Encoding = encoding;
            ResponseHeader = response.Headers;
            ResponseCookie = response.Cookies;
            ContentType = response.ContentType;
            IsFromCache = response.IsFromCache;
            LastModified = response.LastModified;
            StatusCode = response.StatusCode;
        }

        /// <summary>
        /// 将请求结果转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ResponseData.ToString(Encoding);
        }

        /// <summary>
        /// 将请求结果转换为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            return ResponseData;
        }

        /// <summary>
        /// 将请求结果转换为数据流
        /// </summary>
        /// <returns></returns>
        public Stream ToStream()
        {
            return ResponseData.ToStream();
        }

        /// <summary>
        /// 将请求结果转换为文件并保存
        /// </summary>
        /// <param name="fileName"></param>
        public void ToFile(string fileName)
        {
            ResponseData.ToFile(fileName);
        }

        /// <summary>
        /// 将请求结果转换为图片对象
        /// </summary>
        /// <returns></returns>
        public Image ToImage()
        {
            return ResponseData.ToImage();
        }

        /// <summary>
        /// 将请求的Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T JsonTo<T>()
        {
            return ResponseData.JsonTo<T>(Encoding);
        }

        /// <summary>
        /// 将请求的xml字符串转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T XmlTo<T>()
        {
            return ResponseData.XmlTo<T>(Encoding);
        }

        public void Dispose()
        {
            ResponseData = null;
        }
    }

    public enum HttpMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Options,
        Head,
        Trace
    }
}
