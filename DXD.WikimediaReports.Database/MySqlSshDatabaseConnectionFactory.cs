using System.Configuration;
using System.Data;

namespace DXD.WikimediaReports.Database
{
    public static class MySqlSshDatabaseConnectionFactory
    {
        private const string SshHostSettingName = "SSH.Host";
        private const string SshPortSettingName = "SSH.Port";
        private const string SshUsernameSettingName = "SSH.Username";
        private const string SshPrivateKeyPathSettingName = "SSH.PrivateKey.Path";
        private const string SshPrivatePassPhraseSettingName = "SSH.PrivateKey.PassPhrase";

        private const string MySqlHostSettingName = "MySql.Host";
        private const string MySqlPortSettingName = "MySql.Port";
        private const string MySqlUsernameSettingName = "MySql.Username";
        private const string MySqlPasswordSettingName = "MySql.Password";
        private const string MySqlDatabaseSettingName = "MySql.Database";

        public static IDbConnection OpenConnection()
        {
            var sshHost = GetSetting(SshHostSettingName);
            var sssPort = int.Parse(GetSetting(SshPortSettingName));
            var sshUsername = GetSetting(SshUsernameSettingName);
            var sshPrivateKeyPath = GetSetting(SshPrivateKeyPathSettingName);
            var sshPrivateKeyPassPhrase = GetSetting(SshPrivatePassPhraseSettingName);

            var mySqlHost = GetSetting(MySqlHostSettingName);
            var mySqlPort = int.Parse(GetSetting(MySqlPortSettingName));
            var mySqlUsername = GetSetting(MySqlUsernameSettingName);
            var mySqlPassword = GetSetting(MySqlPasswordSettingName);
            var mySqlDatabase = GetSetting(MySqlDatabaseSettingName);

            var connection = new MySqlSshDatabaseConnection(sshHost, sssPort, sshUsername, sshPrivateKeyPath,
                sshPrivateKeyPassPhrase, mySqlHost, mySqlPort, mySqlUsername, mySqlPassword, mySqlDatabase);
            connection.Open();

            return connection;
        }

        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}