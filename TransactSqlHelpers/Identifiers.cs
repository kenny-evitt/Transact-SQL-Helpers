namespace TransactSqlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Encapsulates various helper methods pertaining to Transact-SQL database identifiers.
    /// </summary>
    public static class Identifiers
    {
        // Private fields

        private readonly static string _regularIdentifierRegexString = @"[\p{L}_][\p{L}\p{N}@$#_]{0,127}";
        private readonly static List<string> _reservedKeywords = new List<string> { "ADD", "ALL", "ALTER", "AND", "ANY", "AS", "ASC", "AUTHORIZATION", "BACKUP", "BEGIN", "BETWEEN", "BREAK", "BROWSE", "BULK", "BY", "CASCADE", "CASE", "CHECK", "CHECKPOINT", "CLOSE", "CLUSTERED", "COALESCE", "COLLATE", "COLUMN", "COMMIT", "COMPUTE", "CONSTRAINT", "CONTAINS", "CONTAINSTABLE", "CONTINUE", "CONVERT", "CREATE", "CROSS", "CURRENT", "CURRENT_DATE", "CURRENT_TIME", "CURRENT_TIMESTAMP", "CURRENT_USER", "CURSOR", "DATABASE", "DBCC", "DEALLOCATE", "DECLARE", "DEFAULT", "DELETE", "DENY", "DESC", "DISK", "DISTINCT", "DISTRIBUTED", "DOUBLE", "DROP", "DUMP", "ELSE", "END", "ERRLVL", "ESCAPE", "EXCEPT", "EXEC", "EXECUTE", "EXISTS", "EXIT", "EXTERNAL", "FETCH", "FILE", "FILLFACTOR", "FOR", "FOREIGN", "FREETEXT", "FREETEXTTABLE", "FROM", "FULL", "FUNCTION", "GOTO", "GRANT", "GROUP", "HAVING", "HOLDLOCK", "IDENTITY", "IDENTITY_INSERT", "IDENTITYCOL", "IF", "IN", "INDEX", "INNER", "INSERT", "INTERSECT", "INTO", "IS", "JOIN", "KEY", "KILL", "LEFT", "LIKE", "LINENO", "LOAD", "MERGE", "NATIONAL", "NOCHECK", "NONCLUSTERED", "NOT", "NULL", "NULLIF", "OF", "OFF", "OFFSETS", "ON", "OPEN", "OPENDATASOURCE", "OPENQUERY", "OPENROWSET", "OPENXML", "OPTION", "OR", "ORDER", "OUTER", "OVER", "PERCENT", "PIVOT", "PLAN", "PRECISION", "PRIMARY", "PRINT", "PROC", "PROCEDURE", "PUBLIC", "RAISERROR", "READ", "READTEXT", "RECONFIGURE", "REFERENCES", "REPLICATION", "RESTORE", "RESTRICT", "RETURN", "REVERT", "REVOKE", "RIGHT", "ROLLBACK", "ROWCOUNT", "ROWGUIDCOL", "RULE", "SAVE", "SCHEMA", "SECURITYAUDIT", "SELECT", "SESSION_USER", "SET", "SETUSER", "SHUTDOWN", "SOME", "STATISTICS", "SYSTEM_USER", "TABLE", "TABLESAMPLE", "TEXTSIZE", "THEN", "TO", "TOP", "TRAN", "TRANSACTION", "TRIGGER", "TRUNCATE", "TSEQUAL", "UNION", "UNIQUE", "UNPIVOT", "UPDATE", "UPDATETEXT", "USE", "USER", "VALUES", "VARYING", "VIEW", "WAITFOR", "WHEN", "WHERE", "WHILE", "WITH", "WRITETEXT", "ABSOLUTE", "ACTION", "ADA", "ADD", "ALL", "ALLOCATE", "ALTER", "AND", "ANY", "ARE", "AS", "ASC", "ASSERTION", "AT", "AUTHORIZATION", "AVG", "BEGIN", "BETWEEN", "BIT", "BIT_LENGTH", "BOTH", "BY", "CASCADE", "CASCADED", "CASE", "CAST", "CATALOG", "CHAR", "CHAR_LENGTH", "CHARACTER", "CHARACTER_LENGTH", "CHECK", "CLOSE", "COALESCE", "COLLATE", "COLLATION", "COLUMN", "COMMIT", "CONNECT", "CONNECTION", "CONSTRAINT", "CONSTRAINTS", "CONTINUE", "CONVERT", "CORRESPONDING", "COUNT", "CREATE", "CROSS", "CURRENT", "CURRENT_DATE", "CURRENT_TIME", "CURRENT_TIMESTAMP", "CURRENT_USER", "CURSOR", "DATE", "DAY", "DEALLOCATE", "DEC", "DECIMAL", "DECLARE", "DEFAULT", "DEFERRABLE", "DEFERRED", "DELETE", "DESC", "DESCRIBE", "DESCRIPTOR", "DIAGNOSTICS", "DISCONNECT", "DISTINCT", "DOMAIN", "DOUBLE", "DROP", "ELSE", "END", "END-EXEC", "ESCAPE", "EXCEPT", "EXCEPTION", "EXEC", "EXECUTE", "EXISTS", "EXTERNAL", "EXTRACT", "FALSE", "FETCH", "FIRST", "FLOAT", "FOR", "FOREIGN", "FORTRAN", "FOUND", "FROM", "FULL", "GET", "GLOBAL", "GO", "GOTO", "GRANT", "GROUP", "HAVING", "HOUR", "IDENTITY", "IMMEDIATE", "IN", "INCLUDE", "INDEX", "INDICATOR", "INITIALLY", "INNER", "INPUT", "INSENSITIVE", "INSERT", "INT", "INTEGER", "INTERSECT", "INTERVAL", "INTO", "IS", "ISOLATION", "JOIN", "KEY", "LANGUAGE", "LAST", "LEADING", "LEFT", "LEVEL", "LIKE", "LOCAL", "LOWER", "MATCH", "MAX", "MIN", "MINUTE", "MODULE", "MONTH", "NAMES", "NATIONAL", "NATURAL", "NCHAR", "NEXT", "NO", "NONE", "NOT", "NULL", "NULLIF", "NUMERIC", "OCTET_LENGTH", "OF", "ON", "ONLY", "OPEN", "OPTION", "OR", "ORDER", "OUTER", "OUTPUT", "OVERLAPS", "PAD", "PARTIAL", "PASCAL", "POSITION", "PRECISION", "PREPARE", "PRESERVE", "PRIMARY", "PRIOR", "PRIVILEGES", "PROCEDURE", "PUBLIC", "READ", "REAL", "REFERENCES", "RELATIVE", "RESTRICT", "REVOKE", "RIGHT", "ROLLBACK", "ROWS", "SCHEMA", "SCROLL", "SECOND", "SECTION", "SELECT", "SESSION", "SESSION_USER", "SET", "SIZE", "SMALLINT", "SOME", "SPACE", "SQL", "SQLCA", "SQLCODE", "SQLERROR", "SQLSTATE", "SQLWARNING", "SUBSTRING", "SUM", "SYSTEM_USER", "TABLE", "TEMPORARY", "THEN", "TIME", "TIMESTAMP", "TIMEZONE_HOUR", "TIMEZONE_MINUTE", "TO", "TRAILING", "TRANSACTION", "TRANSLATE", "TRANSLATION", "TRIM", "TRUE", "UNION", "UNIQUE", "UNKNOWN", "UPDATE", "UPPER", "USAGE", "USER", "USING", "VALUE", "VALUES", "VARCHAR", "VARYING", "VIEW", "WHEN", "WHENEVER", "WHERE", "WITH", "WORK", "WRITE", "YEAR", "ZONE", "ABSOLUTE", "ACTION", "ADMIN", "AFTER", "AGGREGATE", "ALIAS", "ALLOCATE", "ARE", "ARRAY", "ASENSITIVE", "ASSERTION", "ASYMMETRIC", "AT", "ATOMIC", "BEFORE", "BINARY", "BIT", "BLOB", "BOOLEAN", "BOTH", "BREADTH", "CALL", "CALLED", "CARDINALITY", "CASCADED", "CAST", "CATALOG", "CHAR", "CHARACTER", "CLASS", "CLOB", "COLLATION", "COLLECT", "COMPLETION", "CONDITION", "CONNECT", "CONNECTION", "CONSTRAINTS", "CONSTRUCTOR", "CORR", "CORRESPONDING", "COVAR_POP", "COVAR_SAMP", "CUBE", "CUME_DIST", "CURRENT_CATALOG", "CURRENT_DEFAULT_TRANSFORM_GROUP", "CURRENT_PATH", "CURRENT_ROLE", "CURRENT_SCHEMA", "CURRENT_TRANSFORM_GROUP_FOR_TYPE", "CYCLE", "DATA", "DATE", "DAY", "DEC", "DECIMAL", "DEFERRABLE", "DEFERRED", "DEPTH", "DEREF", "DESCRIBE", "DESCRIPTOR", "DESTROY", "DESTRUCTOR", "DETERMINISTIC", "DICTIONARY", "DIAGNOSTICS", "DISCONNECT", "DOMAIN", "DYNAMIC", "EACH", "ELEMENT", "END-EXEC", "EQUALS", "EVERY", "EXCEPTION", "FALSE", "FILTER", "FIRST", "FLOAT", "FOUND", "FREE", "FULLTEXTTABLE", "FUSION", "GENERAL", "GET", "GLOBAL", "GO", "GROUPING", "HOLD", "HOST", "HOUR", "IGNORE", "IMMEDIATE", "INDICATOR", "INITIALIZE", "INITIALLY", "INOUT", "INPUT", "INT", "INTEGER", "INTERSECTION", "INTERVAL", "ISOLATION", "ITERATE", "LANGUAGE", "LARGE", "LAST", "LATERAL", "LEADING", "LESS", "LEVEL", "LIKE_REGEX", "LIMIT", "LN", "LOCAL", "LOCALTIME", "LOCALTIMESTAMP", "LOCATOR", "MAP", "MATCH", "MEMBER", "METHOD", "MINUTE", "MOD", "MODIFIES", "MODIFY", "MODULE", "MONTH", "MULTISET", "NAMES", "NATURAL", "NCHAR", "NCLOB", "NEW", "NEXT", "NO", "NONE", "NORMALIZE", "NUMERIC", "OBJECT", "OCCURRENCES_REGEX", "OLD", "ONLY", "OPERATION", "ORDINALITY", "OUT", "OVERLAY", "OUTPUT", "PAD", "PARAMETER", "PARAMETERS", "PARTIAL", "PARTITION", "PATH", "POSTFIX", "PREFIX", "PREORDER", "PREPARE", "PERCENT_RANK", "PERCENTILE_CONT", "PERCENTILE_DISC", "POSITION_REGEX", "PRESERVE", "PRIOR", "PRIVILEGES", "RANGE", "READS", "REAL", "RECURSIVE", "REF", "REFERENCING", "REGR_AVGX", "REGR_AVGY", "REGR_COUNT", "REGR_INTERCEPT", "REGR_R2", "REGR_SLOPE", "REGR_SXX", "REGR_SXY", "REGR_SYY", "RELATIVE", "RELEASE", "RESULT", "RETURNS", "ROLE", "ROLLUP", "ROUTINE", "ROW", "ROWS", "SAVEPOINT", "SCROLL", "SCOPE", "SEARCH", "SECOND", "SECTION", "SENSITIVE", "SEQUENCE", "SESSION", "SETS", "SIMILAR", "SIZE", "SMALLINT", "SPACE", "SPECIFIC", "SPECIFICTYPE", "SQL", "SQLEXCEPTION", "SQLSTATE", "SQLWARNING", "START", "STATE", "STATEMENT", "STATIC", "STDDEV_POP", "STDDEV_SAMP", "STRUCTURE", "SUBMULTISET", "SUBSTRING_REGEX", "SYMMETRIC", "SYSTEM", "TEMPORARY", "TERMINATE", "THAN", "TIME", "TIMESTAMP", "TIMEZONE_HOUR", "TIMEZONE_MINUTE", "TRAILING", "TRANSLATE_REGEX", "TRANSLATION", "TREAT", "TRUE", "UESCAPE", "UNDER", "UNKNOWN", "UNNEST", "USAGE", "USING", "VALUE", "VAR_POP", "VAR_SAMP", "VARCHAR", "VARIABLE", "WHENEVER", "WIDTH_BUCKET", "WITHOUT", "WINDOW", "WITHIN", "WORK", "WRITE", "XMLAGG", "XMLATTRIBUTES", "XMLBINARY", "XMLCAST", "XMLCOMMENT", "XMLCONCAT", "XMLDOCUMENT", "XMLELEMENT", "XMLEXISTS", "XMLFOREST", "XMLITERATE", "XMLNAMESPACES", "XMLPARSE", "XMLPI", "XMLQUERY", "XMLSERIALIZE", "XMLTABLE", "XMLTEXT", "XMLVALIDATE", "YEAR", "ZONE" };

        private readonly static Regex _regularIdentifierRegex = new Regex(String.Format(@"^{0}$", _regularIdentifierRegexString));



        // Properties

        public static Regex RegularIdentifierRegex
        {
            get
            {
                return _regularIdentifierRegex;
            }
        }

        public static string RegularIdentifierRegexString
        {
            get
            {
                return _regularIdentifierRegexString;
            }
        }

        public static List<string> ReservedKeywords
        {
            get
            {
                return _reservedKeywords;
            }
        }



        // Public methods

        
        /// <summary>
        /// Determines whether an identifier complies with the rules for the format of regular 
        /// identifiers, and is not a reserved keyword (including ODBC and future reserved keywords)
        /// for SQL Server 2008 R2.
        /// </summary>
        /// <param name="identifier">Identifier to be tested</param>
        /// <returns>True if the supplied identifier is a regular identifier</returns>
        public static bool IsIdentifierRegular(string identifier)
        {
            if (_reservedKeywords.Contains(identifier.ToUpper()))
                return false;

            Match match = _regularIdentifierRegex.Match(identifier);

            return match.Success;
        }

        /// <summary>
        /// Produces a valid Transact-SQL identifier.
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <returns>Identifier, if a regular identifier; otherwise delimited identifier</returns>
        public static string ValidIdentifier(string identifier)
        {
            if (IsIdentifierRegular(identifier))
                return identifier;
            else
                return String.Format("[{0}]", identifier);
        }
    }
}
