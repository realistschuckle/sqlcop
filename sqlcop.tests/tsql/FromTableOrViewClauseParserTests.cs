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
		
		[Test]
		public void Recognizes_Tablesample_With_System_Modifier()
		{
			string input = "select * from t tablesample system 17";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Tablesample_With_Sample_Number_Percent_Modifier()
		{
			string input = "select * from t tablesample 17 percent";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Tablesample_With_Sample_Number_Rows_Modifier()
		{
			string input = "select * from t tablesample 17 rows";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Tablesample_With_Repeatable_Modifier()
		{
			string input = "select * from t tablesample 19 repeatable(99)";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Table_Hints()
		{
			string[] expands = new string[] {
				"NOEXPAND",
				""
			};
			string[] hints = new string[] {
				"INDEX(0)",
				"INDEX(some_index)",
				"INDEX(some_index, another_index)",
				"FASTFIRSTROW",
				"FORCESEEK",
				"FORCESEEK(some_index(some_column))",
				"FORCESEEK(some_index(some_column, another_column))",
				"FORCESCAN",
				"HOLDLOCK" ,
				"NOLOCK" ,
				"NOWAIT",
				"PAGLOCK", 
				"READCOMMITTED" ,
				"READCOMMITTEDLOCK" ,
				"READPAST" ,
				"READUNCOMMITTED" ,
				"REPEATABLEREAD" ,
				"ROWLOCK" ,
				"SERIALIZABLE" ,
				"TABLOCK" ,
				"TABLOCKX",
				"UPDLOCK",
				"XLOCK",
			};
			string format = "select * from t WITH ({0} {1})";
			foreach(string expand in expands)
			{
				foreach(string hint in hints)
				{
					string input = string.Format(format, expand, hint);
					_scanner.SetSource(input, 0);
					Assert.That(_parser.Parse(), "Failed on " + input);
				}
			}
		}
		
		[Test]
		public void Recognizes_Compound_Table_Hints()
		{
			string input = "select * from t WITH (NOLOCK, NOEXPAND NOWAIT)";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse(), "Failed on " + input);
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

