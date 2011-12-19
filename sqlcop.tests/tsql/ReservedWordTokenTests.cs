using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using sqlcop.tsql;

namespace sqlcop.tests
{
	[TestFixture]
	[Category("TSQL Lexer")]
	public class ReservedWordTokenTests
	{
		[Test]
		public void Identifies_Select_With_Case_Insensitivity()
		{
			_inputToken = "select";
			_expectedToken = Tokens.SELECT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_From_With_Case_Insensitivity()
		{
			_inputToken = "from";
			_expectedToken = Tokens.FROM;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_All_With_Case_Insensitivity()
		{
			_inputToken = "all";
			_expectedToken = Tokens.ALL;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Distinct_With_Case_Insensitivity()
		{
			_inputToken = "distinct";
			_expectedToken = Tokens.DISTINCT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Top_With_Case_Insensitivity()
		{
			_inputToken = "top";
			_expectedToken = Tokens.TOP;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Percent_With_Case_Insensitivity()
		{
			_inputToken = "percent";
			_expectedToken = Tokens.PERCENT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_With_With_Case_Insensitivity()
		{
			_inputToken = "with";
			_expectedToken = Tokens.WITH;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Ties_With_Case_Insensitivity()
		{
			_inputToken = "ties";
			_expectedToken = Tokens.TIES;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_scanner = new Scanner();
			_eofToken = Tokens.EOF;
		}
		
		private void EnsureLexerRecognizesInputTokenWithCaseInsensitivity()
		{
			int token = (int) _expectedToken;
			int eofToken = (int) _eofToken;
			IEnumerable<string> selects = BuildTokens();
			foreach(string sel in selects)
			{
				_scanner.SetSource(sel, 0);
				Assert.That(_scanner.yylex(), Is.EqualTo(token));
				Assert.That(_scanner.yylex(), Is.EqualTo(eofToken));
			}
		}
		
		private IEnumerable<string> BuildTokens()
		{
			List<string> generatedTokens = new List<string>();
			int upperBound = Convert.ToInt32(Math.Pow(2, _inputToken.Length));
			int currentTokenMask = 0;
			StringBuilder tokenBuilder = new StringBuilder(_inputToken);
			while(currentTokenMask < upperBound)
			{
				for(int j = 0, i = upperBound / 2; i > 0; i /= 2, j += 1)
				{
					char currentChar = _inputToken[j];
					if((i & currentTokenMask) > 1)
					{
						tokenBuilder[j] = Char.ToUpper(currentChar);
					}
					else
					{
						tokenBuilder[j] = currentChar;
					}
				}
				generatedTokens.Add(tokenBuilder.ToString());
				currentTokenMask += 1;
			}
			generatedTokens.Add(_inputToken.ToUpper());
			return generatedTokens;
		}
		
		private Tokens _eofToken;
		private string _inputToken;
		private Tokens _expectedToken;
		private Scanner _scanner;
	}
}

