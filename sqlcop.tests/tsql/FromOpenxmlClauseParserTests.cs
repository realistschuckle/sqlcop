using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class FromOpenxmlClauseParserTests
	{
		[SetUp]
		public void RunBeforeEachTest()
		{
			_scanner = new Scanner();
			_parser = new Parser(_scanner);
		}
		
		private void CheckSelectStatement()
		{
			_scanner.SetSource("select * from " + _input, 0);
			Assert.That(_parser.Parse(), "Failed on: " + _input);
		}
		
		private string _input;
		private Scanner _scanner;
		private Parser _parser;
	}
}

