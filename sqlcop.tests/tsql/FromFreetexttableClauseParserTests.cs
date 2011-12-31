using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class FromFreetexttableClauseParserTests
	{
		[Test]
		public void Recognizes_Freetexttable_With_Star()
		{
			string input = "select * from freetexttable(tab, *, 'word')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Freetexttable_With_Column()
		{
			string input = "select * from freetexttable(tab, col, 'word')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Freetexttable_With_Column_List()
		{
			string input = "select * from freetexttable(tab, (col1, col2), 'word')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Freetexttable_With_Language_Specifier()
		{
			string input = "select * from freetexttable(tab, *, 'word', LANGUAGE 'English')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Freetexttable_With_Row_Limit()
		{
			string input = "select * from freetexttable(tab, *, 'word', 10)";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Freetexttable_With_Language_Specifier_And_Row_Limit()
		{
			string input = "select * from freetexttable(tab, *, 'word', LANGUAGE 'English', 10)";
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

