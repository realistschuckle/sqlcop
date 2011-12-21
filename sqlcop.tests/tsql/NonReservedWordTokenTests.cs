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
		public void Identifies_Plain_Name_Tokens()
		{
			_expectedToken = Tokens.NAME;
			_inputs = new string[] {
				"TABLE",
				"TABLE_UNDERSCORE",
				"TableNum123",
				"table",
				"_table",
				"table$dollarsign",
				"table@atsign",
				"table#numbersign"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_Bracketed_Name_Tokens()
		{
			_expectedToken = Tokens.BRACES_NAME;
			_inputs = new string[] {
				"[TABLE]",
				"[TABLE_UNDERSCORE]",
				"[TableNum123]",
				"[table]",
				"[_table]",
				"[table$dollarsign]",
				"[table@atsign]",
				"[table#numbersign]",
				"[bracketd with space]"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_Temp_Table_Name_Tokens()
		{
			_expectedToken = Tokens.TEMP_TABLE_NAME;
			_inputs = new string[] {
				"#TABLE",
				"#TABLE_UNDERSCORE",
				"#TableNum123",
				"#table",
				"#_table",
				"#table$dollarsign",
				"#table@atsign",
				"#table#numbersign",
				"##TABLE",
				"##TABLE_UNDERSCORE",
				"##TableNum123",
				"##table",
				"##_table",
				"##table$dollarsign",
				"##table@atsign",
				"##table#numbersign"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_Integer_Literals()
		{
			_expectedToken = Tokens.INTEGER;
			_inputs = new string[] {
				"-1000",
				"-1",
				"0",
				"1",
				"1000"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_String_Literals()
		{
			_expectedToken = Tokens.STRING;
			_inputs = new string[] {
				"''",
				"'str'",
				"'some space'",
				"'Conan O''Brian'"
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
				string msg = "Failed on " + input;
				Assert.That(_scanner.yylex(), Is.EqualTo(token), msg);
				Assert.That(_scanner.yytext, Is.EqualTo(input));
				Assert.That(_scanner.yylex(), Is.EqualTo(eofToken));
			}
		}

		private Tokens _eofToken;
		private IEnumerable<string> _inputs;
		private Tokens _expectedToken;
		private Scanner _scanner;
	}
}

