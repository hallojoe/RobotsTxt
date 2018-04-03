using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace HalloJoe.RobotsTxt
{
    public class Document : IDocument
    {

        private TypeConverter _typeConverter = TypeDescriptor.GetConverter(typeof(Model));
        private IRobotsTxt _content;
        private IGuard _guard;

        public IRobotsTxt Content { get => _content; }
        public string Source { get; private set; }

        public Document(IRobotsTxt model, IGuard guard)
        {
            _content = model;
            _guard = guard;
            Source = ToString();
        }

        public Document(IRobotsTxt model) : this(model, new Guard()) { }


        public Document(string robotsTxt, IGuard guard)
        {
            _content = FromString(robotsTxt);
            _guard = guard;
            Source = ToString();
        }

        public Document(string robotsTxt) : this(robotsTxt, new Guard()) { }



        public IDecision Allow(string alias, string path) =>
            _guard.Allow(alias, path, ref _content);

        public IDecision Allow(string path) => Allow("*", path);


        /// <summary>
        /// Robots.txt as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_typeConverter != null && _content != null)
                return (string)_typeConverter.ConvertTo(_content, typeof(string));
            return default(string);
        }

        /// <summary>
        /// Helper for creating a new instance of Model from string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private IRobotsTxt FromString(string s)
        {
            if (_typeConverter != null)
                return (IRobotsTxt)_typeConverter.ConvertFrom(s);
            return null;
        }

        /// <summary>
        /// Creates a new instance of Document from a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IDocument Parse(string s) => new Document(s);

    }
}
