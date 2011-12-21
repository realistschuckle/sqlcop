%namespace sqlcop.tsql

%token NAME TEMP_TABLE_NAME BRACES_NAME
%token INTEGER STRING BINARY DECIMAL FLOAT MONEY
%token SELECT FROM ALL DISTINCT TOP PERCENT WITH TIES AS

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

column_list : prefixed_name alias
            | column_list ',' prefixed_name alias
            ;

prefixed_name : braced_name '.' braced_name
              | braced_name '.' '*'
              | braced_name
              | braced_name '=' expression
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

table_or_view_name : braced_name '.' braced_name '.' braced_name
                   | braced_name '.' braced_name
                   | braced_name
                   | TEMP_TABLE_NAME
                   ;

braced_name : NAME
            | BRACES_NAME
            ;

alias : NAME
      | AS NAME
      |
      ;

expression : literal
           | braced_name
           | braced_name '.' braced_name
           ;

literal : INTEGER
        | STRING
        | BINARY
        | DECIMAL
        | FLOAT
        | MONEY
        ;

%%

public Parser(Scanner lex) : base(lex) {}
