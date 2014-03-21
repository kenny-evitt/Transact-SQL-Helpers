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
        public void TestGetBatchesForScriptWithTwoBatches()
        {
            string script = @"IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'administrator')
DROP SCHEMA [administrator]
GO

CREATE SCHEMA [administrator] AUTHORIZATION [administrator]
GO";

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
                    (from Batch batch in Scripts.GetBatches(script)
                     select batch.Sql)));
        }

        [Test]
        public void TestGetBatchesForScriptWithGotoStatement()
        {
            string script = @"IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'administrator')
DROP SCHEMA [administrator]
GO

GOTO DummyLabel
GO

CREATE SCHEMA [administrator] AUTHORIZATION [administrator]
GO

DummyLabel:
    SELECT 'Blah blah blah';
GO";

            List<string> expectedBatches =
                new List<string>()
                {
                    @"IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'administrator')
DROP SCHEMA [administrator]
",
                    @"

GOTO DummyLabel
",
                    @"

CREATE SCHEMA [administrator] AUTHORIZATION [administrator]
",
                    @"

DummyLabel:
    SELECT 'Blah blah blah';
" };

            Assert.IsTrue(
                expectedBatches.SequenceEqual(
                    (from Batch batch in Scripts.GetBatches(script)
                     select batch.Sql)));
        }

        [Test]
        public void TestGetBatchesForScriptWithCommentedGoStatements()
        {
            string script = @"IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'administrator')
DROP SCHEMA [administrator]
GO

GOTO DummyLabel
GO

-- The following commented GO statements should be included in a batch with the following uncommented T-SQL code; right?
-- GO
/* GO */
/*
GO
*/
/*
/*
GO
*/
*/

-- /* GO
SELECT 'Blah blah blah';
-- */


CREATE SCHEMA [administrator] AUTHORIZATION [administrator]
GO

-- /* GO
SELECT 'Blah blah blah';
GO
-- */";

            List<string> expectedBatches =
                new List<string>()
                {
                    @"IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'administrator')
DROP SCHEMA [administrator]
",
                    @"

GOTO DummyLabel
",
                    @"

-- The following commented GO statements should be included in a batch with the following uncommented T-SQL code; right?
-- GO
/* GO */
/*
GO
*/
/*
/*
GO
*/
*/

-- /* GO
SELECT 'Blah blah blah';
-- */


CREATE SCHEMA [administrator] AUTHORIZATION [administrator]
",
                    @"

-- /* GO
SELECT 'Blah blah blah';
",
                    @"
-- */"
                };

            Assert.IsTrue(
                expectedBatches.SequenceEqual(
                    (from Batch batch in Scripts.GetBatches(script)
                     select batch.Sql)));
        }
    }
}
