using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DXD.WikimediaReports.Core
{
    public class ReportParameters
    {
        public ReportParameters(string title, string preamble, IList<ReportColumn> columns, string query, CultureInfo locale)
        {
            Title = title;
            Preamble = preamble;
            Columns = columns;
            Query = query;
            Locale = locale;
        }

        public string Title { get; }

        public string Preamble { get; }

        public IList<ReportColumn> Columns { get; }

        public string Query { get; }

        public CultureInfo Locale { get; }

        protected bool Equals(ReportParameters other)
        {
            return string.Equals(Title, other.Title) && string.Equals(Preamble, other.Preamble) &&
                   Columns.SequenceEqual(other.Columns) && string.Equals(Query, other.Query)
                   && Locale.Equals(other.Locale);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ReportParameters)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Title.GetHashCode();
                hashCode = (hashCode * 397) ^ Preamble.GetHashCode();
                hashCode = (hashCode * 397) ^ Columns.GetHashCode();
                hashCode = (hashCode * 397) ^ Query.GetHashCode();
                hashCode = (hashCode * 397) ^ Locale.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ReportParameters left, ReportParameters right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReportParameters left, ReportParameters right)
        {
            return !Equals(left, right);
        }
    }
}