using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HalloJoe.RobotsTxt
{
    public class Guard : IGuard
    {
        /// <summary>
        /// IGuard implementation
        /// see: https://developers.google.com/search/reference/robots_txt    
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="path"></param>
        /// <param name="userAgents"></param>
        /// <returns></returns>
        public IDecision Allow(string alias, string path, ref IRobotsTxt model)
        {
            // allow is undefined by default
            bool? allowed = null;

            // return undefined when no user-agents are present
            if (model?.Useragents == null || !model.Useragents.Any() || string.IsNullOrEmpty(path))
                return new Decision(alias, path, "RobotsTxt or Useragent is null", true);

            // set alias to all if empty
            if (string.IsNullOrEmpty(alias))
                alias = "*";

            // find useragent
            var userAgent = model.Useragents
                .FirstOrDefault(ua => ua.Alias.Equals(alias, StringComparison.InvariantCultureIgnoreCase));

            // if no user agent then return
            if (userAgent == null)
                return new Decision(alias, path, $"Useragent {userAgent.Alias} not found", true) ;

            // todo: disallow anything if no diallow directives are present but a user agent is

            // check if any disallow patterns match
            var match = userAgent.Disallow
                .FirstOrDefault(disallow => Regex.IsMatch(path, MakePattern(disallow)));

            // if match is not empty then disallow
            if (!string.IsNullOrEmpty(match))
                allowed = false;

            // return if allowed
            if(allowed.HasValue && allowed.Value)
                return new Decision(alias, path, string.Empty, true); 
            else if (allowed.HasValue && !allowed.Value)
            {
                // check if it is allowed by directive
                var allowMatch = userAgent.Allow
                    .FirstOrDefault(allow => Regex.IsMatch(path, MakePattern(allow)));
                if (string.IsNullOrEmpty(allowMatch))
                    return new Decision(alias, path, match, false);
                else
                    return new Decision(alias, path, allowMatch, true);
            }
            return new Decision(alias, path, "No matches", true);
        }

        /// <summary>
        /// Converts a wildcard string that will
        /// now allow trailing wildcard, to regex
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string MakePattern(string s)
        {
            // return if no value
            if (string.IsNullOrEmpty(s)) return s;
            // register line ending and trim
            var applyEndOf = s.Trim().EndsWith("$"); // apply end of line later?
            s = s.Trim(new[] { ' ', '*', '$' }); // disallow trailing white, asterisk and dollar
            // return if no value
            if (string.IsNullOrEmpty(s)) return s;
            // escape pattern
            s = Regex.Escape(s);
            // patternize wildcard
            s = s.Replace("\\*", ".*");
            s = s.Replace("\\.", ".");
            // apply ending on open ended rules
            if (!s.EndsWith("/") && !applyEndOf)
                s = s + "(?:[^$]+)?$"; // allow querystring and end of line
            if (applyEndOf)
                s = s + "$";
            return s;
        }




    }
}
