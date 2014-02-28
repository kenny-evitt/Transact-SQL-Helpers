namespace TransactSqlHelpers
{
    public class Batch
    {
        private readonly string _sql;

        public string Sql
        {
            get
            {
                return _sql;
            }
        }

        public Batch(string sql)
        {
            _sql = sql;
        }
    }
}
