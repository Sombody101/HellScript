// $antlr-format alignTrailingComments true, columnLimit 150, minEmptyLines 1
// $antlr-format maxEmptyLinesToKeep 1, reflowComments false, useTab false
// $antlr-format allowShortRulesOnASingleLine false, allowShortBlocksOnASingleLine true
// $antlr-format alignSemicolons hanging, alignColons hanging

lexer grammar HellAsm_Lexer;

channels {
    ERROR
}

options {
    superClass = HellAsmLexerBase;
}

MetaProgram
    : '.program'
    ;

Structure
    : '.struct'
    ;

CompilerArg
    : '.compilerdata'
    ;

Local
    : '.local'
    ;

Method
    : '.method'
    ;

ArgCount
    : '.args'
    ;

DoubleQuote
    : '"'
    ;

Comma
    : ','
    ;

Semi
    : ';'
    ;

Colon
    : ':'
    ;

OpenBrack
    : '{'
    ;

CloseBrack
    : '}'
    ;

OpenParen
    : '('
    ;

CloseParen
    : ')'
    ;

BoxOpen
    : '['
    ;

BoxClose
    : ']'
    ;

Equals
    : '='
    ;

Identifier
    : IdentifierStart IdentifierPart*
    ;

SingleLineComment
    : ';' ~[\r\n\u2028\u2029]* -> channel(HIDDEN)
    ;

StringConstant
    : EncodingPrefix? '"' SCharSequence? '"'
    ;

FastConstant
    : '[' (IntegerConstant | FloatingConstant) ']'
    ;

FloatingConstant
    : '-'? (IntStart IntPart) '.' (IntStart IntPart)
    ;

IntegerConstant
    : '-'? IntStart IntPart
    | '-'? IntStart
    ;

fragment IntStart
    : [0-9]
    ;

fragment IntPart
    : [0-9_]+
    ;

Whitespace
    : [\t\u000B\u000C\u0020\u00A0]+ -> channel(HIDDEN)
    ;

Newline
    : ('\r' '\n'? | '\n') -> channel(HIDDEN)
    ;

UnexpectedCharacter
    : . -> channel(ERROR)
    ;

fragment EncodingPrefix
    : 'u8'
    | 'u'
    | 'U'
    | 'L'
    ;

fragment SCharSequence
    : SChar+
    ;

fragment SChar
    : ~["\\\r\n]
    | EscapeSequence
    | '\\\n'   // Added line
    | '\\\r\n' // Added line
    ;

fragment IdentifierStart
    : [a-zA-Z_`<>]
    ;

fragment IdentifierPart
    : [a-zA-Z0-9_`<>.]
    ;

/* Escapes */

fragment EscapeSequence
    : SimpleEscapeSequence
    | OctalEscapeSequence
    | HexadecimalEscapeSequence
    | UniversalCharacterName
    ;

fragment SimpleEscapeSequence
    : '\\' ['"?abfnrtv\\]
    ;

fragment OctalEscapeSequence
    : '\\' OctalDigit OctalDigit? OctalDigit?
    ;

fragment UniversalCharacterName
    : '\\u' HexQuad
    | '\\U' HexQuad HexQuad
    ;

fragment HexQuad
    : HexadecimalDigit HexadecimalDigit HexadecimalDigit HexadecimalDigit
    ;

fragment HexadecimalEscapeSequence
    : '\\x' HexadecimalDigit+
    ;

fragment HexadecimalDigit
    : [0-9a-fA-F]
    ;

fragment OctalConstant
    : '0' OctalDigit*
    ;

fragment OctalDigit
    : [0-7]
    ;