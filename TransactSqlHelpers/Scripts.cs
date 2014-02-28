﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactSqlHelpers
{
    /// <summary>
    /// Encapsulates various helper methods pertaining to Transact-SQL script files, particularly
    /// script files typically used with SQL Server Management Studio and other SQL Server 
    /// utilities.
    /// </summary>
    public static class Scripts
    {
        /// <summary>
        /// Generates Transact-SQL batches, suitable for execution, from the lines of a 
        /// Transact-SQL script.
        /// </summary>
        /// <param name="scriptLines"></param>
        /// <returns></returns>
        public static IEnumerable<Batch> GetBatches(IEnumerable<string> scriptLines)
        {
            List<Batch> batches = new List<Batch>();

            StringBuilder nextBatchSql = new StringBuilder();

            foreach (string line in scriptLines)
            {
                if (line.Trim().StartsWith("GO", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    batches.Add(new Batch(nextBatchSql.ToString()));
                    nextBatchSql.Clear();
                }
                else
                {
                    nextBatchSql.AppendLine(line);
                }
            }

            return batches;
        }
    }
}
