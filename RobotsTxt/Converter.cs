using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HalloJoe.RobotsTxt
{
    public class ModelConverter : TypeConverter
    {
        /// <summary>
        /// Validate value
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return base.IsValid(context, value);
        }

        /// <summary>
        /// Allow converting to string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Allow converting from string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            IRobotsTxt robotsTxt = (IRobotsTxt)value;
            if (destinationType == typeof(string))
            {
                var sb = new StringBuilder();
                if (robotsTxt.Sitemaps != null && robotsTxt.Sitemaps.Any())
                { 
                    foreach (var sitemap in robotsTxt.Sitemaps)
                        sb.AppendLine($"sitemap: { sitemap }");
                    sb.AppendLine();
                }
                foreach (var useragent in robotsTxt.Useragents)
                {
                    sb.AppendLine($"user-agent: { useragent.Alias }");
                    foreach (var disallow in useragent.Disallow)
                        sb.AppendLine($"disallow: { disallow }");
                    foreach (var allow in useragent.Allow)
                        sb.AppendLine($"allow: { allow }");
                    sb.AppendLine();
                }
                return sb.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Convert from string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
                return base.ConvertFrom(context, culture, value);

            string s = (string)value;

            var result = new Model();

            // lines
            IEnumerable<string> lines = s.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // lines not empty or comment
            lines = lines
                .Select(line => line.Trim(new[] { '\r', ' ' }))
                .Where(line => !string.IsNullOrEmpty(line) && !line.StartsWith("#"));

            // sitemaplines
            var sitemaps = lines.Where(line => line.ToLower().StartsWith("sitemap")).Select(line => line.Substring(8).Trim());

            // lines without sitemaplines
            lines = lines.Except(sitemaps);

            // temp useragent
            Model.Useragent useragent = null;
            foreach (var line in lines)
            {

                // when hitting a user agent
                if (line.StartsWith("user-agent:", StringComparison.InvariantCultureIgnoreCase))
                {
                    // if useragent has value add it
                    if (useragent != null)
                        result.Useragents.Add(useragent);
                    // find alias for next useragent
                    var alias = line.Substring(11).Trim(new[] { ' ' });
                    if (string.IsNullOrEmpty(alias))
                        alias = "*";
                    // create new useragent
                    useragent = new Model.Useragent(alias);
                }
                else
                {
                    // if useragent exist(user-agent line has been read)
                    if (useragent != null)
                    {
                        // handle crawl-delay line
                        if (line.StartsWith("crawl-delay: ", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var val = line.Substring(12).Trim(new[] { ' ' });
                            if (!string.IsNullOrEmpty(val))
                                if (int.TryParse(val, out var i))
                                    useragent.CrawlDelay = i;
                        }
                        // handle disallow line
                        if (line.StartsWith("disallow: ", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var val = line.Substring(9).Trim(new[] { ' ' });
                            if (!string.IsNullOrEmpty(val))
                                useragent.Disallow.Add(val);
                        }
                        // handle allow line
                        else if (line.StartsWith("allow: ", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var val = line.Substring(6).Trim(new[] { ' ' });
                            if (!string.IsNullOrEmpty(val))
                                useragent.Allow.Add(val);
                        }
                    }
                }
            }
            // add last man standing
            if (useragent != null)
                result.Useragents.Add(useragent);

            // add sitemaps
            if (sitemaps.Any())
                result.Sitemaps.AddRange(sitemaps);

            return result;
        }

    }
}
