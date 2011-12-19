using System;
using NUnit.Framework;
using sqlcop.engine;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Parser")]
	public class TSqlParserMetaTests
	{
		[Test]
		public void Implements_IParseSql()
		{
			Type type = typeof(TSqlParser);
			Type iface = typeof(IParseSql);
			Assert.That(iface.IsAssignableFrom(type));
		}
		
		[Test]
		public void Parse_Raises_ArgumentNullException_For_Null_Input()
		{
			TestDelegate td = () => _parser.Parse(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("input"));
		}
		
		[Test]
		public void Parse_Raises_InvalidSqlException_For_Empty_Input()
		{
			string input = "";
			TestDelegate td = () => _parser.Parse(input);
			InvalidSqlException ex;
			ex = Assert.Throws<InvalidSqlException>(td);
			Assert.That(ex.Input, Is.EqualTo(input));
			Assert.That(ex.Row, Is.EqualTo(0));
			Assert.That(ex.Column, Is.EqualTo(0));
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_parser = new TSqlParser();
		}
		
		private TSqlParser _parser;
	}
}

