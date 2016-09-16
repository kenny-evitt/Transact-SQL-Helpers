namespace TransactSqlHelpers
{
    using Microsoft.SqlServer.Management.SqlParser.Parser;

    public class TokenInfo
    {
        // Public properties

        public int Start { get; set; }
        public int End { get; set; }
        public string Sql { get; set; }
        public Tokens Token { get; set; }
        public bool IsExecAutoParamHelp { get; set; }
        public bool IsPairMatch { get; set; }


        // Public operators

        public static bool operator ==(TokenInfo token1, TokenInfo token2)
        {
            if (System.Object.ReferenceEquals(token1, token2))
                return true;

            if (((object)token1 == null) || ((object)token2 == null))
                return false;

            return
                token1.Start == token2.Start
                && token1.End == token2.End
                && token1.Sql == token2.Sql
                && token1.Token == token2.Token
                && token1.IsExecAutoParamHelp == token2.IsExecAutoParamHelp
                && token1.IsPairMatch == token2.IsPairMatch;
        }

        public static bool operator !=(TokenInfo item1, TokenInfo item2)
        {
            return !(item1 == item2);
        }


        // Public methods

        public override bool Equals(object obj)
        {
            return Equals(obj as TokenInfo);
        }

        public bool Equals(TokenInfo other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return
                this.Start.GetHashCode()
                ^ this.End.GetHashCode()
                ^ this.Sql.GetHashCode()
                ^ this.Token.GetHashCode()
                ^ this.IsExecAutoParamHelp.GetHashCode()
                ^ this.IsPairMatch.GetHashCode();
        }
    }
}
