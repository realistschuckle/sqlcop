using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class FromOpenxmlClauseParserTests
	{
		[Test]
		public void Recognizes_Idoc_Rowpattern()
		{
			_input = "openxml(@idoc, '/some/path')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Aliased_Form_With_As()
		{
			_input = "openxml(@idoc, '/some/path') AS xoo";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Aliased_Form_Without_As()
		{
			_input = "openxml(@idoc, '/some/path') xoo";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Idoc_Rowpattern_Flags()
		{
			_input = "openxml(@idoc, '/some/path', 8)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_With_Tablename_Schema_Modifier()
		{
			_input = "openxml(@idoc, '/some/path') WITH ([some].stupid.table_or_view)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_With_Tablename_ColumnName_ColumnType()
		{
			_input = "openxml(@idoc, '/some/path') WITH (col varchar)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_With_Tablename_ColumnName_ColumnType_ColumnPath()
		{
			_input = "openxml(@idoc, '/some/path') WITH (col varchar 'path')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_With_Tablename_ColumnName_Sized_ColumnType()
		{
			_input = "openxml(@idoc, '/some/path') WITH (col varchar(33))";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_With_Tablename_ColumnName_Sized_ColumnType_ColumnPath()
		{
			_input = "openxml(@idoc, '/some/path') WITH (col varchar(33) 'path')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_With_Homogenous_Compound_Column_List()
		{
			_input = "openxml(@idoc, '/some/path') WITH (col varchar(33) 'path', col byte)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Flags_With_Tablename_Schema_Modifier()
		{
			_input = "openxml(@idoc, '/some/path', 8) WITH ([some].stupid.table_or_view)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Flags_With_Tablename_ColumnName_ColumnType()
		{
			_input = "openxml(@idoc, '/some/path', 8) WITH (col varchar)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Flags_With_Tablename_ColumnName_ColumnType_ColumnPath()
		{
			_input = "openxml(@idoc, '/some/path', 8) WITH (col varchar 'path')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Flags_With_Tablename_ColumnName_Sized_ColumnType()
		{
			_input = "openxml(@idoc, '/some/path', 8) WITH (col varchar(33))";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Flags_With_Tablename_ColumnName_Sized_ColumnType_ColumnPath()
		{
			_input = "openxml(@idoc, '/some/path', 8) WITH (col varchar(33) 'path')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Flags_With_Homogenous_Compound_Column_List()
		{
			_input = "openxml(@idoc, '/some/path', 2) WITH (col decimal(33, 27) 'path', col byte)";
			CheckSelectStatement();
		}
		
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

