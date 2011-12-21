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
		public void Recognizes_Select_Star_From_Table()
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
		public void Recognizes_Repetitions_Modifiers_With_Star()
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
		public void Recognizes_Limits_Modifiers_With_Star()
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
		public void Recognizes_Repetitions_And_Limit_Modifiers_With_Star()
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
		
		[Test]
		public void Recognizes_Column_In_Select_List()
		{
			string input = "SELECT Column FROM Table";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Column_List_In_Select_List()
		{
			string input = "SELECT Column1, Column2, Column3 FROM Table";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Table_With_Alias()
		{
			string[] tableNames = new string[] {
				"TABLE tab",
				"[bracketd with space] alias",
				"#table#numbersign sumpin"
			};
			foreach(string tableName in tableNames)
			{
				string source = string.Format("SELECT * FROM {0}", tableName);
				_scanner.SetSource(source, 0);
				Assert.That(_parser.Parse());
			}
		}
		
		[Test]
		public void Recognizes_Table_With_Alias_Declared_With_As()
		{
			string[] tableNames = new string[] {
				"TABLE AS tab",
				"[bracketd with space] as alias",
				"#table#numbersign As sumpin"
			};
			foreach(string tableName in tableNames)
			{
				string source = string.Format("SELECT * FROM {0}", tableName);
				_scanner.SetSource(source, 0);
				Assert.That(_parser.Parse());
			}
		}
		
		[Test]
		public void Recognizes_Column_With_Alias_In_Select_List()
		{
			string input = "SELECT Column column_alias FROM Table";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Column_List_With_Alias_In_Select_List()
		{
			string input = "SELECT Column1 col, Column2, Column3 alias\n" +
				           "FROM Table";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Column_With_Single_Prefix()
		{
			string input = "SELECT p.Column FROM Table p";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Column_List_With_Prefixes_In_Select_List()
		{
			string input = "SELECT t.Col1, Col2, t.Col3 FROM Table t";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Prefixed_Table_Name()
		{
			string input = "SELECT Column FROM odb.schema.Table";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Prefixed_Star_In_Column_List()
		{
			string input = "SELECT t.*, Col FROM Table t";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Assigned_Column()
		{
			string[] literals = new string[] {
				"'string'",
				"0x12Ef",
				"1894",
				"99.45",
				"99.45E3",
				"$12,345.67"
			};
			string format = "SELECT Col = {0} FROM Table";
			foreach(string literal in literals)
			{
				string source = string.Format(format, literal);
				_scanner.SetSource(source, 0);
				Assert.That(_parser.Parse(), "Failed on const " + literal);
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

