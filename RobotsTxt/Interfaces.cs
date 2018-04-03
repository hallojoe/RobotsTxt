using System;
using System.Collections.Generic;

namespace HalloJoe.RobotsTxt
{
    /// <summary>
    /// Interesting values of a robots.txt
    /// </summary>
    public interface IRobotsTxt
    {
        /// <summary>
        /// Sitemap locations
        /// </summary>
        List<string> Sitemaps { get; set; }
        /// <summary>
        /// User-agents
        /// </summary>
        List<IUseragent> Useragents { get; set; }
    }

    /// <summary>
    /// Directives for a single useragent alias 
    /// </summary>
    public interface IUseragent
    {
        /// <summary>
        /// User-agent alias
        /// </summary>
        string Alias { get; set; }
        /// <summary>
        /// Crawl-delay in seconds
        /// </summary>
        int CrawlDelay { get; set; }
        /// <summary>
        /// Disallow directives
        /// </summary>
        List<string> Disallow { get; set; }
        /// <summary>
        /// Allow directives
        /// </summary>
        List<string> Allow { get; set; }
    }

    /// <summary>
    /// Thing to determaine wheather or not 
    /// a url is allowed or disallowed
    /// </summary>
    public interface IGuard
    {
        /// <summary>
        /// Is allowed or disallowed
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="url"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        IDecision Allow(string alias, string path, ref IRobotsTxt model);
    }

    /// <summary>
    /// An IGuard decision
    /// </summary>
    public interface IDecision
    {
        /// <summary>
        /// Alias
        /// </summary>
        string Alias { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// Directive/Rule
        /// </summary>
        string Directive { get; set; }
        /// <summary>
        /// Allowed or disallowed
        /// </summary>
        bool Allowed { get; set; }
    }

    /// <summary>
    /// A robots.txt document
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Object representation of robots.txt
        /// </summary>
        IRobotsTxt Content { get; }
        /// <summary>
        /// IGuard.Allow proxy
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        IDecision Allow(string alias, string url);

    }

}
