## Overview of the Transact-SQL Helpers Library

Transact-SQL Helpers is a .NET (4) class library for working with the Microsoft SQL Server Transact-SQL language.

The following functionality has been implemented:

 - Test whether a database identifier is a 'regular identifier' [see [Identifiers](http://msdn.microsoft.com/en-us/library/ms175874%28v=sql.105%29.aspx) for SQL Server 2008 R2, from MSDN] and producing a 'valid' identifier (i.e. a delimited form if an identifier is not 'regular').
 - Parse T-SQL script text into distinct batches.
 - Replace non-breaking spaces in T-SQL script text.
