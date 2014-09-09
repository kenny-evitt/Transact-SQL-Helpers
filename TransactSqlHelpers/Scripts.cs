namespace TransactSqlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Microsoft.SqlServer.Management.SqlParser.Parser;

    /// <summary>
    /// Encapsulates various helper methods pertaining to Transact-SQL script files, particularly
    /// script files typically used with SQL Server Management Studio and other SQL Server 
    /// utilities.
    /// </summary>
    public static class Scripts
    {
        /// <summary>
        /// Generates Transact-SQL batches, suitable for execution, from the text of a 
        /// Transact-SQL script.
        /// </summary>
        /// <param name="scriptSql">Transact-SQL script text</param>
        /// <returns>Collection of Batch objects for each batch in the supplied script</returns>
        public static IEnumerable<Batch> GetBatches(string scriptSql)
        {
            ParseOptions parseOptions = new ParseOptions();
            Scanner scanner = new Scanner(parseOptions);

            int state = 0,
                start,
                end,
                lastTokenEnd = -1,
                token;

            bool isPairMatch, isExecAutoParamHelp;

            List<Batch> batches = new List<Batch>();
            StringBuilder nextBatchSql = new StringBuilder();

            scanner.SetSource(scriptSql, 0);

            while ((token = scanner.GetNext(ref state, out start, out end, out isPairMatch, out isExecAutoParamHelp)) != (int)Tokens.EOF)
            {
                if ((Tokens)token == Tokens.LEX_BATCH_SEPERATOR)
                {
                    nextBatchSql.Append(scriptSql.Substring(lastTokenEnd + 1, start - lastTokenEnd + 1 - 1 - 1));
                    batches.Add(new Batch(nextBatchSql.ToString()));
                    nextBatchSql.Clear();
                }
                else
                {
                    nextBatchSql.Append(scriptSql.Substring(lastTokenEnd + 1, end - lastTokenEnd + 1 - 1));
                }

                lastTokenEnd = end;
            }

            if (!String.IsNullOrWhiteSpace(nextBatchSql.ToString()))
                batches.Add(new Batch(nextBatchSql.ToString()));

            return batches;
        }

        public static string ReplaceNonBreakingSpaces(string scriptSql)
        {
            ParseOptions parseOptions = new ParseOptions();
            Scanner scanner = new Scanner(parseOptions);

            int state = 0,
                start,
                end,
                lastTokenEnd = -1,
                token;

            bool isPairMatch, isExecAutoParamHelp;

            StringBuilder newScriptSql = new StringBuilder();

            scanner.SetSource(scriptSql, 0);

            while ((token = scanner.GetNext(ref state, out start, out end, out isPairMatch, out isExecAutoParamHelp)) != (int)Tokens.EOF)
            {
                if (lastTokenEnd > -1)
                    newScriptSql.Append(scriptSql.Substring(lastTokenEnd + 1, start - lastTokenEnd - 1));

                string tokenSubstring = scriptSql.Substring(start, end - start + 1);

                if ((Tokens)token != Tokens.TOKEN_STRING)
                {
                    newScriptSql.Append(tokenSubstring.Replace('\xA0', '\x20'));
                }
                else
                {
                    newScriptSql.Append(tokenSubstring);
                }

                lastTokenEnd = end;
            }

            if (lastTokenEnd + 1 < scriptSql.Length)
                newScriptSql.Append(scriptSql.Substring(lastTokenEnd + 1));

            return newScriptSql.ToString();
        }
    }
}
