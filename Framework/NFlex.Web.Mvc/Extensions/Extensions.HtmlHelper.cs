namespace System.Web.Mvc
{
    public static partial class Extensions
    {
        public static MvcHtmlString Script(this HtmlHelper helper,string fileName,string version)
        {
            var html=string.Format(@"<script type=""text/javascript"" src=""{0}?v={1}""></script>",fileName,version);
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString Css(this HtmlHelper helper,string fileName,string version)
        {
            var html = string.Format(@"<link type=""text/css"" rel=""stylesheet"" href=""{0}?v={1}""></script>", fileName, version);
            return new MvcHtmlString(html);
        }
    }
}
