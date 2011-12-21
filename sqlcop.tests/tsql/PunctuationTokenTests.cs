using System;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Lexer")]
	public class PunctuationTokenTests
	{
		[Test]
		public void Recognizes_Star()
		{
			_input = "*";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Open_Parenthesis()
		{
			_input = "(";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Close_Parenthesis()
		{
			_input = ")";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Comma()
		{
			_input = ",";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Dot()
		{
			_input = ".";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Equals()
		{
			_input = "=";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Plus()
		{
			_input = "+";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Minus()
		{
			_input = "-";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Divide()
		{
			_input = "/";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Modulo()
		{
			_input = "%";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Bitwise_And()
		{
			_input = "&";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Bitwise_Or()
		{
			_input = "|";
			EnsureLexerRecognizesInputToken();
		}
		
		[Test]
		public void Recognizes_Bitwise_Xor()
		{
			_input = "^";
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
			int token = (int) _input[0];
			int eofToken = (int) _eofToken;
			_scanner.SetSource(_input, 0);
			Assert.That(_scanner.yylex(), Is.EqualTo(token));
			Assert.That(_scanner.yytext, Is.EqualTo(_input));
			Assert.That(_scanner.yylex(), Is.EqualTo(eofToken));
		}

		private Tokens _eofToken;
		private string _input;
		private Scanner _scanner;
	}
}

