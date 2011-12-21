%namespace sqlcop.tsql

%token NAME TEMP_TABLE_NAME BRACES_NAME INTEGER
%token SELECT FROM ALL DISTINCT TOP PERCENT WITH TIES

%%

statement : select EOF
          ;

select : SELECT	repetition limit columns FROM table_or_view_name alias
       ;

repetition : ALL
           | DISTINCT
           |
           ;

columns : '*'
        | column_list
        ;

column_list : NAME
            | column_list ',' NAME
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

alias : NAME
      |
      ;

%%

public Parser(Scanner lex) : base(lex) {}
