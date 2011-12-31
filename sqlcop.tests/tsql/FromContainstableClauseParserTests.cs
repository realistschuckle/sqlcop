using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class FromContainstableClauseParserTests
	{
		[Test]
		public void Recognizes_Alias_With_As()
		{
			string input = "select * from containstable(tab, *, 'word') AS loo";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Alias_Without_As()
		{
			string input = "select * from containstable(tab, *, 'word') loo";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Containstable_With_Star()
		{
			string input = "select * from containstable(tab, *, 'word')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Containstable_With_Column()
		{
			string input = "select * from containstable(tab, col, 'word')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Containstable_With_Column_List()
		{
			string input = "select * from containstable(tab, (col1, col2), 'word')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Containstable_With_Language_Specifier()
		{
			string input = "select * from containstable(tab, *, 'word', LANGUAGE 'English')";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Containstable_With_Row_Limit()
		{
			string input = "select * from containstable(tab, *, 'word', 10)";
			_scanner.SetSource(input, 0);
			Assert.That(_parser.Parse());
		}
		
		[Test]
		public void Recognizes_Containstable_With_Language_Specifier_And_Row_Limit()
		{
			string input = "select * from containstable(tab, *, 'word', LANGUAGE 'English', 10)";
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

