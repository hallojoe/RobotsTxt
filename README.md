# RobotsTxt
A tool for accessing and querying the contents of a robots.txt file. It is also useful for constructing robots.txt files. 

## Install 
`PM> Install-Package HalloJoe.RobotsTxt -Version 0.1.0`

## Get started

Load some robots.txt:

    string robotsTxt = "...";

Instatiate an document:

    IDocument document = new Document(robotsTxt);

Or instantiate with Parse:

    IDocument document = Document.Parse(robotsTxt);

If you need to get a string back: 

    string robotsTxt = document.ToString();

Once a Document is loaded, you can query the directives:

    string path = "/path";
    string alias = "*"

    bool canAccess = document.Allow(alias, path);

If you need to build a robots.txt then create a document and start adding to its contents:

    IDocument document = new Document("");
    document.Content.Sitemaps.Add("https://demo.local/sitemap.xml");
    var anyUserAgent = new Model.Useragent("*")
    {
        CrawlDelay = 1,
        Disallow = new List<string>() { "/disallowed" },
        Allow = new List<string>() { "/disallowed/allow.html" }
    };
    var evilUserAgent = new Model.Useragent("evil-bot")
    {
        Disallow = new List<string>() { "/", }
    };
    document.Content.Useragents.Add(anyUserAgent);
    document.Content.Useragents.Add(evilUserAgent);

    // get as string
    string robotsTxt = document.ToString();

    
