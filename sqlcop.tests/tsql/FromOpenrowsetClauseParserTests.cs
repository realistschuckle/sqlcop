using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class FromOpenrowsetClauseParserTests
	{
		[Test]
		public void Recognizes_ProviderName_ProviderString_Query()
		{
			_input = "openrowset('provider_name', 'provider_string', 'query')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_ProviderName_Credentials_Query()
		{
			_input = "openrowset('provider_name', 'datasource';'user';'pwd', 'query')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_ProviderName_ProviderString_Object()
		{
			_input = "openrowset('provider_name', 'provider_string', [some].stupid.table_or_view)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_ProviderName_Credentials_Object()
		{
			_input = "openrowset('provider_name', 'datasource';'user';'pwd', [some].stupid.table_or_view)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Singleblob()
		{
			_input = "openrowset(BULK 'data_file', sinGLE_blob)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Singleclob()
		{
			_input = "openrowset(bulk 'data_file', single_CLOB)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Singlenclob()
		{
			_input = "openrowset(bulk 'data_file', single_nclob)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Codepage()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', codepage='ACP')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Errorfile()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', errorfile='file_name')";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Firstrow()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', firstrow=17)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Lastrow()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', lastrow=13)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Maxerrors()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', maxerrors=1)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_RowsPerBatch()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', rows_per_batch=200)";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Order_Of_Single_Column()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', order (col))";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Order_Of_Multiple_Columns()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', order (col1, col2, col3))";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Order_Of_Columns_And_Unique()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', order (col1, col2, col3 unique))";
			CheckSelectStatement();
		}
		
		[Test]
		public void Recognizes_Bulk_Formatfile_With_Order_Of_Columns_And_Unique_And_Lastrow()
		{
			_input = "openrowset(bulk 'data_file', formatfile='file_path', order (col1, col2, col3 unique), lastrow=99)";
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

