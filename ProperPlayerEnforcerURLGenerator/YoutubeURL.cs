using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperPlayerEnforcerURLGenerator
{
    using System.Text.RegularExpressions;
    public class YoutubeURL
    {
        private const string _pattern = @"(?:\w*://).*/(?:watch\?v=)?(\w*)";
        private string _inputURL;
        public string VideoID { get { return GetVideoID(); } }
        public string LongYTURL { get { return @"https://www.youtube.com/watch?v=" + VideoID; } }
        public string ShortYTURL { get { return @"http://youtu.be/" + VideoID; } }
        public string EnforcerURL{get{return @"http://www.interleave-vr.com/youtube-proper-player.php?v="+VideoID;}}
               

        public YoutubeURL(string inputURL)
        {
            _inputURL = inputURL;
        }

        private string GetVideoID()
        {
            Regex RX = new Regex(_pattern);
            Match m = RX.Match(_inputURL);
            if (m.Success && m.Groups[1].Success)
                return m.Groups[1].Value;
            else
                throw new ArgumentException("Invalid video URL.");
        }

        public static bool ValidateYTURL(string URLToCheck)
        {
            Regex RX = new Regex(_pattern);
            Match m = RX.Match(URLToCheck);
            if (m.Success && m.Groups[1].Success)
                return true;
            else
                return false;
        }
    }
}
