namespace Tests
{
    using System;

    using NUnit.Framework;

    using TransactSqlHelpers;

    [TestFixture]
    public class IdentifiersTester
    {
        [TestCase("Description", true)]
        [TestCase("This should not be considered regular", false)]
        [TestCase("EntityId", true)]
        [TestCase("Jānis_Bērziņš", true)]
        public void TestIfIdentifierIsRegular(string identifier, bool expectedIsRegularValue)
        {
            Assert.AreEqual(expectedIsRegularValue, Identifiers.IsIdentifierRegular(identifier));
        }

        [TestCase("Description", "Description")]
        [TestCase("This should not be considered regular", "[This should not be considered regular]")]
        [TestCase("EntityId", "EntityId")]
        [TestCase("Jānis_Bērziņš", "Jānis_Bērziņš")]
        public void TestIfNonRegularIdentifierIsDelimited(string identifier, string expectedValidIdentifierForm)
        {
            Assert.AreEqual(expectedValidIdentifierForm, Identifiers.ValidIdentifier(identifier));
        }
    }
}
