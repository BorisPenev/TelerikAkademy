using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CinemaReserve.WpfClient.Helpers
{
    public class RegexValidationRule : ValidationRule
    {
        private string _pattern = String.Empty;
        private Regex _regex;
        private string _mess = String.Empty;

        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                _regex = new Regex(_pattern, RegexOptions.IgnoreCase);
            }
        }

        public RegexValidationRule()
        {
        }
        public string Mess
        {
            get
            {
                return _mess;
            }
            set
            {
                if (value != null)
                {
                    _mess = value;
                }
                else
                {
                    _mess = value;
                }
            }
        }

        public override ValidationResult Validate(object value, CultureInfo ultureInfo)
        {

            if ("".Equals(value.ToString()))
            {
                return new ValidationResult(true, null);
            }

            if (value == null || !_regex.Match(value.ToString()).Success)
            {
                return new ValidationResult(false, Mess);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
