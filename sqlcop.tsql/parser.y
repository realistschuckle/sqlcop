%namespace sqlcop.tsql

%token NAME
%token SELECT FROM

%%

statement : select EOF
          ;

select : SELECT	'*' FROM table_or_view_name
       ;

table_or_view_name : NAME
                   ;

%%

public Parser(Scanner lex) : base(lex) {}
