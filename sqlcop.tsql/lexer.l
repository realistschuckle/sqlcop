%namespace sqlcop.tsql

SELECT            [sS][eE][lL][eE][cC][tT]
FROM              [fF][rR][oO][mM]
ALL               [aA][lL][lL]
DISTINCT          [dD][iI][sS][tT][iI][nN][cC][tT]
TOP               [tT][oO][pP]
PERCENT           [pP][eE][rR][cC][eE][nN][tT]
WITH              [wW][iI][tT][hH]
TIES              [tT][iI][eE][sS]

NAME              [a-zA-Z_][a-zA-Z0-9_@#$]*
BRACES_NAME       \[[a-zA-Z_][^\]]*\]
TEMP_TABLE_NAME   #{1,2}[a-zA-Z_][a-zA-Z0-9_@#$]*
INTEGER           -?[0-9]+

%%

{SELECT}            { return T(Tokens.SELECT); }
{FROM}              { return T(Tokens.FROM); }
{ALL}               { return T(Tokens.ALL); }
{DISTINCT}          { return T(Tokens.DISTINCT); }
{TOP}               { return T(Tokens.TOP); }
{PERCENT}           { return T(Tokens.PERCENT); }
{WITH}              { return T(Tokens.WITH); }
{TIES}              { return T(Tokens.TIES); }

{NAME}              { return T(Tokens.NAME); }
{BRACES_NAME}       { return T(Tokens.BRACES_NAME); }
{TEMP_TABLE_NAME}   { return T(Tokens.TEMP_TABLE_NAME); }
{INTEGER}           { return T(Tokens.INTEGER); }

"*"                 { return C(); }
"("                 { return C(); }
")"                 { return C(); }

%%

internal int T(Tokens token) {
	return (int) token;
}

internal int C() {
	return (int) yytext[0];
}
