namespace TransactSqlHelpers
{
    using Microsoft.SqlServer.Management.SqlParser.Parser;

    public class TokenInfo
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Sql { get; set; }
        public Tokens Token { get; set; }
        public bool IsExecAutoParamHelp { get; set; }
        public bool IsPairMatch { get; set; }
    }
}
