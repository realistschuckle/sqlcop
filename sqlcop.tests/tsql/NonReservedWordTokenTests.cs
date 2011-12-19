using System;
using System.Collections.Generic;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Lexer")]
	public class NonReservedWordTokenTests
	{
		[Test]
		public void Identifies_Name_Tokens()
		{
			_expectedToken = Tokens.NAME;
			_inputs = new string[] {
				"Table",
				"TABLE_UNDERSCORE",
				"TableNum123",
				"#TempTable",
				"##GlobalTempTable"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_scanner = new Scanner();
			_eofToken = Tokens.EOF;
		}
		
		private void EnsureLexerRecognizesInputToken()
		{
			int token = (int) _expectedToken;
			int eofToken = (int) _eofToken;
			foreach(string input in _inputs)
			{
				_scanner.SetSource(input, 0);
				Assert.That(_scanner.yylex(), Is.EqualTo(token));
				Assert.That(_scanner.yylex(), Is.EqualTo(eofToken));
			}
		}

		private Tokens _eofToken;
		private IEnumerable<string> _inputs;
		private Tokens _expectedToken;
		private Scanner _scanner;
	}
}

