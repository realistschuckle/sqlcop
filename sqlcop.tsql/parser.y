%namespace sqlcop.tsql

%token VARNAME NAME TEMP_TABLE_NAME BRACES_NAME
%token INTEGER STRING BINARY DECIMAL FLOAT MONEY
%token S_INTEGER S_DECIMAL S_FLOAT S_MONEY

%token SELECT FROM ALL DISTINCT TOP PERCENT WITH TIES AS ROWS
%token AND ANY BETWEEN EXISTS IN LIKE NOT OR SOME EXCEPT
%token INTERSECT UNION INTO TABLESAMPLE SYSTEM REPEATABLE

%token ORDER ASC DESC UNIQUE

%token FASTFIRSTROW FORCESCAN FORCESEEK HOLDLOCK INDEX NOEXPAND
%token NOLOCK NOWAIT PAGLOCK READCOMMITTED READCOMMITTEDLOCK READPAST
%token READUNCOMMITTED REPEATABLEREAD ROWLOCK SERIALIZABLE TABLOCK
%token TABLOCKX UPDLOCK XLOCK

%token CONTAINSTABLE FREETEXTTABLE OPENDATASOURCE OPENQUERY OPENROWSET
%token OPENXML
%token WEIGHT LANGUAGE FORMSOF INFLECTIONAL THESAURUS NEAR ISABOUT
%token BULK FORMATFILE SINGLE_BLOB SINGLE_CLOB SINGLE_NCLOB CODEPAGE
%token ERRORFILE FIRSTROW LASTROW MAXERRORS ROWS_PER_BATCH

%%

statement : select EOF
          ;

select : select_clause
         into_clause
         from_clause
       ;
       
select_clause : SELECT repetition limit columns
              ;

from_clause : FROM row_source_list
            |
            ;

row_source_list : row_source
                | row_source_list ',' row_source
                ;

row_source : table_or_view_name alias tablesample_modifier table_hints
           | rowset_function alias
           ;

rowset_function : CONTAINSTABLE '(' table_or_view_name ',' containstable_columns ',' STRING language_opt ')'
                | FREETEXTTABLE '(' table_or_view_name ',' containstable_columns ',' STRING language_opt ')'
                | OPENDATASOURCE '(' STRING ',' STRING ')'
                | OPENQUERY '(' NAME ',' STRING ')'
                | openrowset openrowset_column_aliases_opt
                | openxml
                ;

openxml : OPENXML '(' VARNAME ',' STRING ')'
        | OPENXML '(' VARNAME ',' STRING ')' WITH '(' table_or_view_name ')'
        | OPENXML '(' VARNAME ',' STRING ')' WITH '(' openxml_schema_declaration_list ')'
        | OPENXML '(' VARNAME ',' STRING ',' INTEGER ')'
        | OPENXML '(' VARNAME ',' STRING ',' INTEGER ')' WITH '(' table_or_view_name ')'
        | OPENXML '(' VARNAME ',' STRING ',' INTEGER ')' WITH '(' openxml_schema_declaration_list ')'
        ;

openxml_schema_declaration_list : openxml_schema_declaration
                                | openxml_schema_declaration_list ',' openxml_schema_declaration
                                ;

openxml_schema_declaration : NAME data_type
                           | NAME data_type STRING
                           ;

openrowset_column_aliases_opt : '(' braced_name_list ')'
                              |
                              ;
                         

openrowset : OPENROWSET '(' STRING ',' STRING ',' STRING ')'
           | OPENROWSET '(' STRING ',' STRING ';' STRING ';' STRING ',' STRING ')'
           | OPENROWSET '(' STRING ',' STRING ',' local_table_or_view_name ')'
           | OPENROWSET '(' STRING ',' STRING ';' STRING ';' STRING ',' local_table_or_view_name ')'
           | OPENROWSET '(' BULK STRING ',' SINGLE_BLOB ')'
           | OPENROWSET '(' BULK STRING ',' SINGLE_CLOB ')'
           | OPENROWSET '(' BULK STRING ',' SINGLE_NCLOB ')'
           | OPENROWSET '(' BULK STRING ',' FORMATFILE '=' STRING bulk_options_list_opt ')'
           ;

bulk_options_list_opt : bulk_options_list
                      |
                      ;

bulk_options_list : bulk_options
                  | bulk_options_list bulk_options
                  ;

bulk_options : ',' CODEPAGE '=' STRING
             | ',' ERRORFILE '=' STRING
             | ',' FIRSTROW '=' INTEGER
             | ',' LASTROW '=' INTEGER
             | ',' MAXERRORS '=' INTEGER
             | ',' ROWS_PER_BATCH '=' INTEGER
             | ',' ORDER '(' braced_name_list ')'
             | ',' ORDER '(' braced_name_list UNIQUE ')'
             ;

containstable_columns : '*'
                      | NAME
                      | '(' name_list ')'
                      ;

language_opt : ',' LANGUAGE STRING
             | ',' LANGUAGE STRING ',' INTEGER
             | ',' INTEGER
             |
             ;

into_clause : INTO local_table_or_view_name
            |
            ;

tablesample_modifier : TABLESAMPLE INTEGER tablesample_count_modifier tablesample_repeatable_modifier
                     | TABLESAMPLE SYSTEM INTEGER tablesample_count_modifier tablesample_repeatable_modifier
                     |
                     ;

tablesample_count_modifier : PERCENT
                           | ROWS
                           |
                           ;

tablesample_repeatable_modifier : REPEATABLE '(' INTEGER ')'
                                |
                                ;

table_hints : WITH '(' table_hints_list ')'
            |
            ;

table_hints_list : table_hint_noexpand_opt
                 | table_hints_list ',' table_hint_noexpand_opt
                 ;

table_hint_noexpand_opt : NOEXPAND table_hint
                        | table_hint
                        ;

table_hint : FASTFIRSTROW
           | FORCESEEK
           | FORCESEEK '(' NAME '(' name_list ')' ')'
           | INDEX '(' INTEGER ')'
           | INDEX '(' name_list ')'
           | FORCESCAN
           | HOLDLOCK
           | NOLOCK
           | NOWAIT
           | PAGLOCK 
           | READCOMMITTED
           | READCOMMITTEDLOCK
           | READPAST
           | READUNCOMMITTED
           | REPEATABLEREAD
           | ROWLOCK
           | SERIALIZABLE
           | TABLOCK
           | TABLOCKX
           | UPDLOCK
           | XLOCK
           ;

name_list : NAME
          | name_list ',' NAME
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

braced_name_list : braced_name
                 | braced_name_list ',' braced_name
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
                | function_invocation
                | VARNAME
                | select_clause FROM table_or_view_name
                | '(' select ')'
                ;

function_invocation : NAME '(' method_arg_list ')'
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

data_type : NAME
          | NAME '(' INTEGER ')'
          | NAME '(' INTEGER ',' INTEGER ')'
          ;

%%

public Parser(Scanner lex) : base(lex) {}
