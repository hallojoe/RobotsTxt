using System.Collections.Generic;
using System.ComponentModel;

namespace HalloJoe.RobotsTxt
{
    [TypeConverter(typeof(ModelConverter))]
    public class Model : IRobotsTxt
    {
        public List<string> Sitemaps { get; set; } = new List<string>();
        public List<IUseragent> Useragents { get; set; } = new List<IUseragent>();
        public class Useragent : IUseragent
        {
            public Useragent(string alias = "*")
            {
                Alias = alias;
            }

            public string Alias { get; set; }
            public int CrawlDelay { get; set; }
            public List<string> Disallow { get; set; } = new List<string>();
            public List<string> Allow { get; set; } = new List<string>();
        }
    }

    public class Decision : IDecision
    {
        public Decision(string alias, string path, string directive, bool allowed)
        {
            Alias = alias;
            Path = path;
            Directive = directive;
            Allowed = allowed;
        }
        public Decision()
        { }

        public string Alias { get; set; }
        public string Path { get; set; }
        public string Directive { get; set; }
        public bool Allowed { get; set; }
    }
}
