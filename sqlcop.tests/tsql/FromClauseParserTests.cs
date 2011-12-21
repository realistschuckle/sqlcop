using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class FromClauseParserTests
	{
		[Test]
		public void Recognizes_Minimum_Tablesample_Modifier()
		{
			string input = "select * from t tablesample 17";
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

