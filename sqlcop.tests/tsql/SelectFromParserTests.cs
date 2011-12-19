using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class SelectFromParserTests
	{
		[Test]
		public void Parser_Recognizes_Select_Star_From_Table()
		{
			string[] tableNames = new string[] {
				"TABLE",
				"[bracketd with space]",
				"#table#numbersign"
			};
			foreach(string tableName in tableNames)
			{
				string source = string.Format("SELECT * FROM {0}", tableName);
				_scanner.SetSource(source, 0);
				Assert.That(_parser.Parse());
			}
		}
		
		[Test]
		public void Parser_Recognizes_Repetitions_Modifiers_With_Star()
		{
			string[] repetitions = new string[] {
				"ALL",
				"DISTINCT"
			};
			string format = "SELECT {0} * FROM Foo";
			foreach(string rep in repetitions)
			{
				string source = string.Format(format, rep);
				_scanner.SetSource(source, 0);
				Assert.That(_parser.Parse());
			}
		}
		
		[Test]
		public void Parser_Recognizes_Limits_Modifiers_With_Star()
		{
			string[] limits = new string[] {
				"TOP 20"
				,"TOP (20)"
				,"TOP 20 PERCENT"
				,"TOP (20) PERCENT"
				,"TOP 20 WITH TIES"
				,"TOP (20) WITH TIES"
				,"TOP 20 PERCENT WITH TIES"
				,"TOP (20) PERCENT WITH TIES"
			};
			string format = "SELECT {0} * FROM Foo";
			foreach(string limit in limits)
			{
				string source = string.Format(format, limit);
				_scanner.SetSource(source, 0);
				Assert.That(_parser.Parse(), "Failed on " + source);
			}
		}
		
		[Test]
		public void Parser_Recognizes_Repetitions_And_Limit_Modifiers_With_Star()
		{
			string[] repetitions = new string[] {
				"ALL TOP 20 PERCENT WITH TIES",
				"DISTINCT TOP(30) WITH TIES"
			};
			string format = "SELECT {0} * FROM Foo";
			foreach(string rep in repetitions)
			{
				string source = string.Format(format, rep);
				_scanner.SetSource(source, 0);
				Assert.That(_parser.Parse());
			}
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

