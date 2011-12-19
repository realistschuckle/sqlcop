%namespace sqlcop.tsql

%token NAME TEMP_TABLE_NAME BRACES_NAME INTEGER
%token SELECT FROM ALL DISTINCT TOP PERCENT WITH TIES

%%

statement : select EOF
          ;

select : SELECT	repetition limit '*' FROM table_or_view_name
       ;

repetition : ALL
           | DISTINCT
           |
           ;

limit : TOP INTEGER limit_modifier
      | TOP '(' INTEGER ')' limit_modifier
      |
      ;

limit_modifier : PERCENT
               | WITH TIES
               | PERCENT WITH TIES
               |
               ;

table_or_view_name : NAME
                   | TEMP_TABLE_NAME
                   | BRACES_NAME
                   ;

%%

public Parser(Scanner lex) : base(lex) {}
