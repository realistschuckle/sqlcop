%namespace sqlcop.tsql

SELECT            [sS][eE][lL][eE][cC][tT]
FROM              [fF][rR][oO][mM]
ALL               [aA][lL][lL]
DISTINCT          [dD][iI][sS][tT][iI][nN][cC][tT]
TOP               [tT][oO][pP]
PERCENT           [pP][eE][rR][cC][eE][nN][tT]
WITH              [wW][iI][tT][hH]
TIES              [tT][iI][eE][sS]
AS                [aA][sS]

NAME              [a-zA-Z_][a-zA-Z0-9_@#$]*
BRACES_NAME       \[[a-zA-Z_][^\]]*\]
TEMP_TABLE_NAME   #{1,2}[a-zA-Z_][a-zA-Z0-9_@#$]*

INTEGER           [0-9]+
DECIMAL           [0-9]*\.[0-9]+
FLOAT             ([0-9]*\.)?[0-9]+E-?[0-9]+
MONEY             \$([0-9,]*\.)?[0-9,]+
S_INTEGER         [+\-]?[0-9]+
S_DECIMAL         [+\-]?[0-9]*\.[0-9]+
S_FLOAT           [+\-]?([0-9]*\.)?[0-9]+E-?[0-9]+
S_MONEY           [+\-]?\$([0-9,]*\.)?[0-9,]+
STRING            '([^']|'')*'
BINARY            0x[0-9a-fA-F]*

%%

{SELECT}            { return T(Tokens.SELECT); }
{FROM}              { return T(Tokens.FROM); }
{ALL}               { return T(Tokens.ALL); }
{DISTINCT}          { return T(Tokens.DISTINCT); }
{TOP}               { return T(Tokens.TOP); }
{PERCENT}           { return T(Tokens.PERCENT); }
{WITH}              { return T(Tokens.WITH); }
{TIES}              { return T(Tokens.TIES); }
{AS}                { return T(Tokens.AS); }

{NAME}              { return T(Tokens.NAME); }
{BRACES_NAME}       { return T(Tokens.BRACES_NAME); }
{TEMP_TABLE_NAME}   { return T(Tokens.TEMP_TABLE_NAME); }

{INTEGER}           { return T(Tokens.INTEGER); }
{DECIMAL}           { return T(Tokens.DECIMAL); }
{FLOAT}             { return T(Tokens.FLOAT); }
{MONEY}             { return T(Tokens.MONEY); }
{S_INTEGER}         { return T(Tokens.S_INTEGER); }
{S_DECIMAL}         { return T(Tokens.S_DECIMAL); }
{S_FLOAT}           { return T(Tokens.S_FLOAT); }
{S_MONEY}           { return T(Tokens.S_MONEY); }
{STRING}            { return T(Tokens.STRING); }
{BINARY}            { return T(Tokens.BINARY); }

"*"                 |
"("                 |
")"                 |
","                 |
"."                 |
"="                 |
"+"                 |
"-"                 |
"/"                 |
"%"                 |
"&"                 |
"|"                 |
"^"                 { return C(); }

[ \t\n]             {  }

.                   { return T(Tokens.error); }

%%

internal int T(Tokens token) {
	return (int) token;
}

internal int C() {
	return (int) yytext[0];
}
