using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DXD.WikimediaReports.Core
{
    public class ReportParametersLoader
    {
        public IList<ReportParameters> LoadFromFile(string path)
        {
            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<IList<ReportParameters>>(json);
        }
    }
}