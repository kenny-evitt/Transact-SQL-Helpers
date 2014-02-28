namespace Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NUnit.Framework;

    using TransactSqlHelpers;

    [TestFixture]
    public class ScriptsTester
    {
        [Test]
        public void TestScriptWithTwoBatches()
        {
            string script = @"IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'administrator')
DROP SCHEMA [administrator]
GO

CREATE SCHEMA [administrator] AUTHORIZATION [administrator]
GO";

            IEnumerable<string> scriptLines = Regex.Split(script, "\r\n|\r|\n");

            List<string> expectedBatches =
                new List<string>()
                {
                    @"IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'administrator')
DROP SCHEMA [administrator]
",
                    @"
CREATE SCHEMA [administrator] AUTHORIZATION [administrator]
" };

            Assert.IsTrue(
                expectedBatches.SequenceEqual(
                    (from Batch batch in Scripts.GetBatches(scriptLines)
                     select batch.Sql)));
        }
    }
}
