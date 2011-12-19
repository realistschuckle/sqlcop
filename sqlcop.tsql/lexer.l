%namespace sqlcop.tsql

SELECT  [sS][eE][lL][eE][cC][tT]
FROM    [fF][rR][oO][mM]
NAMES   #?[a-zA-Z0-9_]+

%%

{SELECT}       { return Tok(Tokens.SELECT); }
{FROM}         { return Tok(Tokens.FROM); }
{NAMES}        { return Tok(Tokens.NAME); }
"*"            { return C(); }

%%

internal int Tok(Tokens token) {
	return (int) token;
}

internal int C() {
	return (int) yytext[0];
}
