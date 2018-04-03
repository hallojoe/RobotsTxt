using HalloJoe.RobotsTxt;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestRobotsTxt
{
    /// <summary>
    /// Test case:
    /// - Creates a RobotsTxt.Document containg
    ///   directives/rules we want to test againts
    ///   It allows one disallow rule and one allow rule
    /// - Runs the path of given alias through IDocument.Allow 
    ///   and expose result IDecision
    /// </summary>
    public class TestCase
    {
        public IDocument Doc;
        public string Alias;
        public string Path;
        public IDecision Decision;
        public TestCase(IDocument doc, string alias, string path)
        {
            Doc = doc;
            Alias = alias;
            Path = path;
            Decision = Doc.Allow(Alias, Path);
        }
        public TestCase(string allowDirective, string disallowDirective, string alias, string path) :
            this(Document.Parse(GetTxt(allowDirective, disallowDirective)), alias, path)
        { }
        public TestCase(string allowDirective, string disallowDirective, string path) :
            this(allowDirective, disallowDirective, "*", path)
        { }

        public TestCase(string disallowDirective, string path) :
            this(null, disallowDirective, "*", path)
        { }

        public static string GetTxt(string allowDirective, string disallowDirective)
        {
            var sb = new StringBuilder()
                .AppendLine("user-agent: *");
            if (!string.IsNullOrEmpty(disallowDirective))
                sb.AppendLine($"disallow: {disallowDirective}");
            if (!string.IsNullOrEmpty(allowDirective))
                sb.AppendLine($"allow: {allowDirective}");
            return sb.ToString();
        }
    }
}
