%namespace sqlcop.tsql

%token NAME TEMP_TABLE_NAME BRACES_NAME
%token INTEGER STRING BINARY DECIMAL FLOAT MONEY
%token S_INTEGER S_DECIMAL S_FLOAT S_MONEY

%token SELECT FROM ALL DISTINCT TOP PERCENT WITH TIES AS
%token AND ANY BETWEEN EXISTS IN LIKE NOT OR SOME EXCEPT
%token INTERSECT UNION

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


expression_atom : literal
                | braced_name
                | braced_name '.' braced_name
                | braced_name '.' '*'
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
