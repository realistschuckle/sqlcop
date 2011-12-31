using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class FromDerivedTableParserTests
	{
		[Test]
		public void Recognizes_Select_As_Row_Source()
		{
			string input = "SELECT * FROM (SELECT * FROM [table])";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Select_With_As_Alias()
		{
			string input = "SELECT t.col1 FROM (SELECT * FROM [table]) AS t";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Select_With_Alias()
		{
			string input = "SELECT t.col1 FROM (SELECT * FROM [table]) t";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Select_With_Column_Aliases()
		{
			string input = "SELECT col1 FROM (SELECT col2 FROM [table]) ([col1])";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Select_With_Column_Aliases_And_Table_Alias()
		{
			string input = "SELECT t.col1 FROM (SELECT col2 FROM [table]) t ([col1])";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Select_With_Column_Aliases_And_Table_Alias_With_As()
		{
			string input = "SELECT t.col1 FROM (SELECT col2 FROM [table]) as t ([col1])";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_scanner = new Scanner();
			_parser = new Parser(_scanner);
		}
		
		private Scanner _scanner;
		private Parser _parser;
	}
}

