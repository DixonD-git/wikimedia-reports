namespace DXD.WikimediaReports.Core
{
    public class ReportColumn
    {
        public ReportColumn(string name, string format)
        {
            Name = name;
            Format = format;
        }

        public string Name { get; }

        public string Format { get; }

        protected bool Equals(ReportColumn other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Format, other.Format);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ReportColumn)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Format.GetHashCode();
            }
        }

        public static bool operator ==(ReportColumn left, ReportColumn right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReportColumn left, ReportColumn right)
        {
            return !Equals(left, right);
        }
    }
}