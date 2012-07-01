words = [
'CROSS',
'JOIN',
'OUTER',
'APPLY',
'INNER',
'LEFT',
'RIGHT',
'FULL',
]

test_template = """
[Test]
public void Identifies_%s_With_Case_Sensitivity()
{
	_inputToken = "%s";
	_expectedToken = Tokens.%s;
	EnsureLexerRecognizesInputTokenWithCaseInsensitivity();
}
"""

for word in words:
  print test_template % (word.capitalize(), word.lower(), word)

for word in words:
  re = ""
  for c in word:
    re += '[' + c.lower() + c + ']'
  print '%-18s%s' % (word, re)

for word in words:
  print '%-20s{ return T(Tokens.%s); }' % ('{' + word + '}', word)

