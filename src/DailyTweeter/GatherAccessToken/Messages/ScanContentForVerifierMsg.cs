using SoftLattice.Common;

namespace DailyTweeter.GatherAccessToken
{
    [Message]
    public class ScanContentForVerifierMsg
    {
        private readonly string _content;

        public ScanContentForVerifierMsg(string content)
        {
            _content = content;
        }

        public string Content
        {
            get { return _content; }
        }
    }
}