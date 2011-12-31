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
		
		[Test]
		public void Identifies_As_With_Case_Insensitivity()
		{
			_inputToken = "as";
			_expectedToken = Tokens.AS;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_And_With_Case_Insensitivity()
		{
			_inputToken = "and";
			_expectedToken = Tokens.AND;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Any_With_Case_Insensitivity()
		{
			_inputToken = "any";
			_expectedToken = Tokens.ANY;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Between_With_Case_Insensitivity()
		{
			_inputToken = "between";
			_expectedToken = Tokens.BETWEEN;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Exists_With_Case_Insensitivity()
		{
			_inputToken = "exists";
			_expectedToken = Tokens.EXISTS;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_In_With_Case_Insensitivity()
		{
			_inputToken = "in";
			_expectedToken = Tokens.IN;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Like_With_Case_Insensitivity()
		{
			_inputToken = "like";
			_expectedToken = Tokens.LIKE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Not_With_Case_Insensitivity()
		{
			_inputToken = "Not";
			_expectedToken = Tokens.NOT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Or_With_Case_Insensitivity()
		{
			_inputToken = "or";
			_expectedToken = Tokens.OR;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Some_With_Case_Insensitivity()
		{
			_inputToken = "some";
			_expectedToken = Tokens.SOME;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Except_With_Case_Insensitivity()
		{
			_inputToken = "except";
			_expectedToken = Tokens.EXCEPT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Intersect_With_Case_Insensitivity()
		{
			_inputToken = "intersect";
			_expectedToken = Tokens.INTERSECT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Union_With_Case_Insensitivity()
		{
			_inputToken = "union";
			_expectedToken = Tokens.UNION;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Into_With_Case_Insensitivity()
		{
			_inputToken = "into";
			_expectedToken = Tokens.INTO;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Tablesample_With_Case_Insensitivity()
		{
			_inputToken = "tablesample";
			_expectedToken = Tokens.TABLESAMPLE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_System_With_Case_Insensitivity()
		{
			_inputToken = "system";
			_expectedToken = Tokens.SYSTEM;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Repeatable_With_Case_Insensitivity()
		{
			_inputToken = "repeatable";
			_expectedToken = Tokens.REPEATABLE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Rows_With_Case_Insensitivity()
		{
			_inputToken = "rows";
			_expectedToken = Tokens.ROWS;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}

		[Test]
		public void Identifies_Fastfirstrow_With_Case_Sensitivity()
		{
			_inputToken = "fastfirstrow";
			_expectedToken = Tokens.FASTFIRSTROW;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Forcescan_With_Case_Sensitivity()
		{
			_inputToken = "forcescan";
			_expectedToken = Tokens.FORCESCAN;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Forceseek_With_Case_Sensitivity()
		{
			_inputToken = "forceseek";
			_expectedToken = Tokens.FORCESEEK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Holdlock_With_Case_Sensitivity()
		{
			_inputToken = "holdlock";
			_expectedToken = Tokens.HOLDLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Index_With_Case_Sensitivity()
		{
			_inputToken = "index";
			_expectedToken = Tokens.INDEX;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Noexpand_With_Case_Sensitivity()
		{
			_inputToken = "noexpand";
			_expectedToken = Tokens.NOEXPAND;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Nolock_With_Case_Sensitivity()
		{
			_inputToken = "nolock";
			_expectedToken = Tokens.NOLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Nowait_With_Case_Sensitivity()
		{
			_inputToken = "nowait";
			_expectedToken = Tokens.NOWAIT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Paglock_With_Case_Sensitivity()
		{
			_inputToken = "paglock";
			_expectedToken = Tokens.PAGLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Readcommitted_With_Case_Sensitivity()
		{
			_inputToken = "readcommitted";
			_expectedToken = Tokens.READCOMMITTED;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Readcommittedlock_With_Case_Sensitivity()
		{
			_inputToken = "readcommittedlock";
			_expectedToken = Tokens.READCOMMITTEDLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Readpast_With_Case_Sensitivity()
		{
			_inputToken = "readpast";
			_expectedToken = Tokens.READPAST;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Readuncommitted_With_Case_Sensitivity()
		{
			_inputToken = "readuncommitted";
			_expectedToken = Tokens.READUNCOMMITTED;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Repeatableread_With_Case_Sensitivity()
		{
			_inputToken = "repeatableread";
			_expectedToken = Tokens.REPEATABLEREAD;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Rowlock_With_Case_Sensitivity()
		{
			_inputToken = "rowlock";
			_expectedToken = Tokens.ROWLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Serializable_With_Case_Sensitivity()
		{
			_inputToken = "serializable";
			_expectedToken = Tokens.SERIALIZABLE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Tablock_With_Case_Sensitivity()
		{
			_inputToken = "tablock";
			_expectedToken = Tokens.TABLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Tablockx_With_Case_Sensitivity()
		{
			_inputToken = "tablockx";
			_expectedToken = Tokens.TABLOCKX;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Updlock_With_Case_Sensitivity()
		{
			_inputToken = "updlock";
			_expectedToken = Tokens.UPDLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Xlock_With_Case_Sensitivity()
		{
			_inputToken = "xlock";
			_expectedToken = Tokens.XLOCK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Containstable_With_Case_Sensitivity()
		{
			_inputToken = "containstable";
			_expectedToken = Tokens.CONTAINSTABLE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}

		[Test]
		public void Identifies_Language_With_Case_Sensitivity()
		{
			_inputToken = "language";
			_expectedToken = Tokens.LANGUAGE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Formsof_With_Case_Sensitivity()
		{
			_inputToken = "formsof";
			_expectedToken = Tokens.FORMSOF;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Inflectional_With_Case_Sensitivity()
		{
			_inputToken = "inflectional";
			_expectedToken = Tokens.INFLECTIONAL;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Thesaurus_With_Case_Sensitivity()
		{
			_inputToken = "thesaurus";
			_expectedToken = Tokens.THESAURUS;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Near_With_Case_Sensitivity()
		{
			_inputToken = "near";
			_expectedToken = Tokens.NEAR;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Isabout_With_Case_Sensitivity()
		{
			_inputToken = "isabout";
			_expectedToken = Tokens.ISABOUT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Weight_With_Case_Sensitivity()
		{
			_inputToken = "weight";
			_expectedToken = Tokens.WEIGHT;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}

		[Test]
		public void Identifies_Freetexttable_With_Case_Sensitivity()
		{
			_inputToken = "freetexttable";
			_expectedToken = Tokens.FREETEXTTABLE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		[Test]
		public void Identifies_Opendatasource_With_Case_Sensitivity()
		{
			_inputToken = "opendatasource";
			_expectedToken = Tokens.OPENDATASOURCE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}

		[Test]
		public void Identifies_Openquery_With_Case_Sensitivity()
		{
			_inputToken = "openquery";
			_expectedToken = Tokens.OPENQUERY;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}

		[Test]
		public void Identifies_Openrowset_With_Case_Sensitivity()
		{
			_inputToken = "openrowset";
			_expectedToken = Tokens.OPENROWSET;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Bulk_With_Case_Sensitivity()
		{
			_inputToken = "bulk";
			_expectedToken = Tokens.BULK;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Formatfile_With_Case_Sensitivity()
		{
			_inputToken = "formatfile";
			_expectedToken = Tokens.FORMATFILE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Single_blob_With_Case_Sensitivity()
		{
			_inputToken = "single_blob";
			_expectedToken = Tokens.SINGLE_BLOB;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Single_clob_With_Case_Sensitivity()
		{
			_inputToken = "single_clob";
			_expectedToken = Tokens.SINGLE_CLOB;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Single_nclob_With_Case_Sensitivity()
		{
			_inputToken = "single_nclob";
			_expectedToken = Tokens.SINGLE_NCLOB;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Codepage_With_Case_Sensitivity()
		{
			_inputToken = "codepage";
			_expectedToken = Tokens.CODEPAGE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Errorfile_With_Case_Sensitivity()
		{
			_inputToken = "errorfile";
			_expectedToken = Tokens.ERRORFILE;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Firstrow_With_Case_Sensitivity()
		{
			_inputToken = "firstrow";
			_expectedToken = Tokens.FIRSTROW;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Lastrow_With_Case_Sensitivity()
		{
			_inputToken = "lastrow";
			_expectedToken = Tokens.LASTROW;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Maxerrors_With_Case_Sensitivity()
		{
			_inputToken = "maxerrors";
			_expectedToken = Tokens.MAXERRORS;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Rows_per_batch_With_Case_Sensitivity()
		{
			_inputToken = "rows_per_batch";
			_expectedToken = Tokens.ROWS_PER_BATCH;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Order_With_Case_Sensitivity()
		{
			_inputToken = "order";
			_expectedToken = Tokens.ORDER;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Asc_With_Case_Sensitivity()
		{
			_inputToken = "asc";
			_expectedToken = Tokens.ASC;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Desc_With_Case_Sensitivity()
		{
			_inputToken = "desc";
			_expectedToken = Tokens.DESC;
			EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
		}
		
		
		[Test]
		public void Identifies_Unique_With_Case_Sensitivity()
		{
			_inputToken = "unique";
			_expectedToken = Tokens.UNIQUE;
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

