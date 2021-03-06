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
		public void Identifies_Variable_Name_Tokens()
		{
			_expectedToken = Tokens.VARNAME;
			_inputs = new string[] {
				"@TABLE",
				"@TABLE_UNDERSCORE",
				"@TableNum123",
				"@table",
				"@_table",
				"@table$dollarsign",
				"@table@atsign",
				"@table#numbersign"
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
			_alternateToken = Tokens.S_INTEGER;
			_inputs = new string[] {
				"-1000",
				"-1",
				"0",
				"+1",
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
				"'Conan O''Brian'",
				"N''",
				"N'str'",
				"N'some space'",
				"N'Conan O''Brian'"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_Binary_Literals()
		{
			_expectedToken = Tokens.BINARY;
			_inputs = new string[] {
				"0xAE",
				"0x12Ef",
				"0x69048AEFDD010E",
				"0x"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_Decimal_Literals()
		{
			_expectedToken = Tokens.DECIMAL;
			_alternateToken = Tokens.S_DECIMAL;
			_inputs = new string[] {
				"1984.1204",
				"-2.0",
				"-.03",
				"+.03",
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_Float_Literals()
		{
			_expectedToken = Tokens.FLOAT;
			_alternateToken = Tokens.S_FLOAT;
			_inputs = new string[] {
				"-101.5E5",
				"0.5E-2",
				"-.2E-20",
				"+6E5",
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Identifies_Money_Literals()
		{
			_expectedToken = Tokens.MONEY;
			_alternateToken = Tokens.S_MONEY;
			_inputs = new string[] {
				"$12",
				"-$542023.14",
				"+$33,333.208"
			};
			EnsureLexerRecognizesInputToken();
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_scanner = new Scanner();
			_alternateToken = null;
			_eofToken = Tokens.EOF;
		}
		
		private void EnsureLexerRecognizesInputToken()
		{
			int token = (int) _expectedToken;
			int alternate = -100;
			if(_alternateToken.HasValue)
			{
				alternate = (int) _alternateToken.Value;
			}
			int eofToken = (int) _eofToken;
			foreach(string input in _inputs)
			{
				_scanner.SetSource(input, 0);
				string msg = "Failed on " + input;
				int lex = _scanner.yylex();
				bool isValid = lex == token || lex == alternate;
				Assert.That(isValid, msg);
				Assert.That(_scanner.yytext, Is.EqualTo(input));
				Assert.That(_scanner.yylex(), Is.EqualTo(eofToken));
			}
		}

		private Tokens _eofToken;
		private IEnumerable<string> _inputs;
		private Tokens _expectedToken;
		private Tokens? _alternateToken;
		private Scanner _scanner;
	}
}

