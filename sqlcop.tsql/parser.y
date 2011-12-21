%namespace sqlcop.tsql

%token NAME TEMP_TABLE_NAME BRACES_NAME
%token INTEGER STRING BINARY DECIMAL FLOAT MONEY
%token S_INTEGER S_DECIMAL S_FLOAT S_MONEY

%token SELECT FROM ALL DISTINCT TOP PERCENT WITH TIES AS
%token AND ANY BETWEEN EXISTS IN LIKE NOT OR SOME EXCEPT
%token INTERSECT UNION INTO

%%

statement : select EOF
          ;

select : select_clause
         into_clause
         aliased_from_clause
       ;
       
select_clause : SELECT repetition limit columns
              ;

from_clause : FROM table_or_view_name
            ;

aliased_from_clause : from_clause alias
                    ;

into_clause : INTO local_table_or_view_name
            |
            ;

repetition : ALL
           | DISTINCT
           |
           ;

columns : '*'
        | column_list
        ;

column_list : expression alias
            | column_list ',' expression alias
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

table_or_view_name : external_table_or_view_name
                   | local_table_or_view_name
                   ;

external_table_or_view_name : braced_name '.' braced_name '.' braced_name '.' braced_name
                            ;

local_table_or_view_name : braced_name '.' braced_name '.' braced_name
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

expression : expression '+' expression_atom
           | expression '-' expression_atom
           | expression '/' expression_atom
           | expression '*' expression_atom
           | expression '&' expression_atom
           | expression '|' expression_atom
           | expression '~' expression_atom
           | expression '<' expression_atom
           | expression '>' expression_atom
           | expression '=' expression_atom
           | expression signed_literal
           | expression_atom
           ;

method_arg_list : expression
                | method_arg_list ',' expression
                |
                ;

expression_atom : literal
                | braced_name
                | braced_name '.' braced_name
                | braced_name '.' '*'
                | NAME '(' method_arg_list ')'
                | select_clause from_clause
                | '(' select ')'
                ;

literal : INTEGER
        | STRING
        | BINARY
        | DECIMAL
        | FLOAT
        | MONEY
        | signed_literal
        ;

signed_literal : S_INTEGER
               | S_DECIMAL
               | S_FLOAT
               | S_MONEY
               ;

%%

public Parser(Scanner lex) : base(lex) {}
