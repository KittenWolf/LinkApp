using LinkApp.API.Utils;

namespace LinkApp.API.Services
{
    public class LinkShorterService
    {
        public string Short(string link)
        {
            var bytes = BaseEncode.Encode(link);
            var shortLink = BaseEncode.Decode(bytes);

            return shortLink;
        }
    }
}
