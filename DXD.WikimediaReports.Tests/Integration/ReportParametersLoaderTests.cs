using DXD.WikimediaReports.Core;
using Ploeh.AutoFixture.Xunit2;
using Shouldly;
using System.Globalization;
using Xunit;

namespace DXD.WikimediaReports.Tests.Integration
{
    public class ReportParametersLoaderTests
    {
        [Theory]
        [InlineAutoData("TestData/reports.json")]
        public void CanLoadPredefinedFile(string fileName, ReportParametersLoader loader)
        {
            var reportParameters = new ReportParameters("Сторінки описів файлів без відповідного файлу",
                "Сторінки описів файлів без відповідного файлу; дані станом на <onlyinclude>{0}</onlyinclude>.",
                new[] { new ReportColumn("Файл", "[[:Файл:{0}|{0}]]") }, @"SELECT
          pg1.page_title
        FROM page AS pg1
        WHERE pg1.page_title NOT IN (
            SELECT
                img_name
            FROM image
            WHERE img_name = pg1.page_title)
        AND pg1.page_title NOT IN (
            SELECT
                img_name
            FROM commonswiki_p.image
            WHERE img_name = pg1.page_title)
        AND pg1.page_namespace = 6
        AND pg1.page_is_redirect = 0
        ORDER BY pg1.page_title;", CultureInfo.GetCultureInfo("uk-ua"));

            var loadedReportParameters = loader.LoadFromFile(fileName);

            loadedReportParameters.ShouldBe(new[] { reportParameters });
        }
    }
}