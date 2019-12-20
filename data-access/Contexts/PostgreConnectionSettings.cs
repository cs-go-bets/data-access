namespace CSGOStats.Infrastructure.DataAccess.Contexts
{
    public class PostgreConnectionSettings
    {
        public string Host { get; }

        public string Database { get; }

        public string Username { get; }

        public string Password { get; }

        public PostgreConnectionSettings(string host, string database, string username, string password)
        {
            Host = host;
            Database = database;
            Username = username;
            Password = password;
        }

        public string GetConnectionString() =>
            $"Host={Host};Database={Database};Username={Username};Password={Password};";
    }
}