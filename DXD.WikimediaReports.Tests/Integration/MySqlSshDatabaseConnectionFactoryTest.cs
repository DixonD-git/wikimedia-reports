using DXD.WikimediaReports.Database;
using Shouldly;
using Xunit;

namespace DXD.WikimediaReports.Tests.Integration
{
    public class MySqlSshDatabaseConnectionFactoryTest
    {
        [Fact]
        public void CanOpenConnectionSuccessfully()
        {
            MySqlSshDatabaseConnectionFactory.OpenConnection().ShouldNotBeNull();
        }
    }
}