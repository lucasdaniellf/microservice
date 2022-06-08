namespace DataRepository
{
    public class DbContext
    {
        public DbContext(string connString)
        {
            ConnString = connString;
        }

        public string ConnString { get; set; } = null!;
    }
}
