using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Data;

namespace DXD.WikimediaReports.Database
{
    public class MySqlSshDatabaseConnection : IDbConnection
    {
        private const string LocalHost = "127.0.0.1";
        private const int LocalPort = 3306;

        private readonly SshClient _client;
        private readonly IDbConnection _connection;

        private ForwardedPort _forwardedPort;

        private readonly string _mySqlHost;
        private readonly int _mySqlPort;

        public MySqlSshDatabaseConnection(string host, int port, string username, string privateKeyPath,
            string privateKeyPassPhrase, string mySqlHost, int mySqlPort, string mySqlUsername,
            string mySqlPassword, string mySqlDatabase)
        {
            _mySqlHost = mySqlHost;
            _mySqlPort = mySqlPort;

            var privateKeyFile = new PrivateKeyFile(privateKeyPath, privateKeyPassPhrase);
            _client = new SshClient(host, port, username, privateKeyFile);

            var databaseConnectionString = BuildConnectionString(LocalHost, LocalPort, mySqlUsername, mySqlPassword,
                mySqlDatabase);
            _connection = new MySqlConnection(databaseConnectionString);
        }

        private static string BuildConnectionString(string host, int port, string username, string password,
            string database)
        {
            return $"Server={host};Port={port};Uid={username};Pwd={password};Database={database};";
        }

        public void Dispose()
        {
            _connection.Dispose();
            _client.Dispose();
        }

        public void Open()
        {
            _client.Connect();

            _forwardedPort = new ForwardedPortLocal(LocalHost, LocalPort, _mySqlHost, (uint)_mySqlPort);
            _client.AddForwardedPort(_forwardedPort);
            _forwardedPort.Start();

            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
            _forwardedPort?.Stop();
            _client.Disconnect();
        }

        public IDbTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _connection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        public IDbCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public string ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }

        public int ConnectionTimeout => _connection.ConnectionTimeout;

        public string Database => _connection.Database;

        public ConnectionState State => _connection.State;
    }
}