%namespace sqlcop.tsql

ALL               [aA][lL][lL]
AND               [aA][nN][dD]
ANY               [aA][nN][yY]
AS                [aA][sS]
BETWEEN           [bB][eE][tT][wW][eE][eE][nN]
DISTINCT          [dD][iI][sS][tT][iI][nN][cC][tT]
EXCEPT            [eE][xX][cC][eE][pP][tT]
EXISTS            [eE][xX][iI][sS][tT][sS]
FROM              [fF][rR][oO][mM]
IN                [iI][nN]
INTERSECT         [iI][nN][tT][eE][rR][sS][eE][cC][tT]
INTO              [iI][nN][tT][oO]
LIKE              [lL][iI][kK][eE]
NOT               [nN][oO][tT]
OR                [oO][rR]
PERCENT           [pP][eE][rR][cC][eE][nN][tT]
REPEATABLE        [rR][eE][pP][eE][aA][tT][aA][bB][lL][eE]
ROWS              [rR][oO][wW][sS]
SELECT            [sS][eE][lL][eE][cC][tT]
SOME              [sS][oO][mM][eE]
SYSTEM            [sS][yY][sS][tT][eE][mM]
TABLESAMPLE       [tT][aA][bB][lL][eE][sS][aA][mM][pP][lL][eE]
TIES              [tT][iI][eE][sS]
TOP               [tT][oO][pP]
UNION             [uU][nN][iI][oO][nN]
WITH              [wW][iI][tT][hH]

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
{AND}               { return T(Tokens.AND); }
{ANY}               { return T(Tokens.ANY); }
{BETWEEN}           { return T(Tokens.BETWEEN); }
{EXISTS}            { return T(Tokens.EXISTS); }
{IN}                { return T(Tokens.IN); }
{LIKE}              { return T(Tokens.LIKE); }
{NOT}               { return T(Tokens.NOT); }
{OR}                { return T(Tokens.OR); }
{SOME}              { return T(Tokens.SOME); }
{EXCEPT}            { return T(Tokens.EXCEPT); }
{INTERSECT}         { return T(Tokens.INTERSECT); }
{UNION}             { return T(Tokens.UNION); }
{INTO}              { return T(Tokens.INTO); }
{TABLESAMPLE}       { return T(Tokens.TABLESAMPLE); }
{SYSTEM}            { return T(Tokens.SYSTEM); }
{REPEATABLE}        { return T(Tokens.REPEATABLE); }
{ROWS}              { return T(Tokens.ROWS); }

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
"^"                 |
"~"                 |
">"                 |
"<"                 |
"!"                 { return C(); }

[ \t\n]             {  }

.                   { return T(Tokens.error); }

%%

internal int T(Tokens token) {
	return (int) token;
}

internal int C() {
	return (int) yytext[0];
}
