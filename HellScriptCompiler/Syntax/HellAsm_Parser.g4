// $antlr-format alignTrailingComments true, columnLimit 150, minEmptyLines 1
// $antlr-format maxEmptyLinesToKeep 1, reflowComments false, useTab false
// $antlr-format allowShortRulesOnASingleLine false, allowShortBlocksOnASingleLine true
// $antlr-format alignSemicolons hanging, alignColons hanging

parser grammar HellAsm_Parser;

options {
    tokenVocab = HellAsm_Lexer;
    superClass = HellAsmParserBase;
}

program
    : programMetadata programLine* EOF
    ;

programLine
    : line
    | methodDeclaration
    ;

programMetadata
    : MetaProgram '{' metadataSet* '}'
    ;

metadataSet
    : Identifier '=' argument
    ;

methodDeclaration
    : Method Identifier '(' ArgCount IntegerConstant ')' definitionMetadata? '{' line* '}'
    ;

structDeclaration
    : Structure Identifier '{' '}'
    ;

definitionMetadata
    : CompilerArg '{' metadataSet* '}'
    ;

line
    : opcode
    | label
    ;

label
    : Identifier ':'
    ;

opcode
    : Identifier (argument | methodReference)?
    ;

argumentList
    : Identifier (',' Identifier)*
    ;

argument
    : StringConstant
    | FastConstant
    | FloatingConstant
    | IntegerConstant
    | '[' Identifier ']'
    ;

methodReference
    : Identifier '(' /*argumentList?*/ ')'
    ;

skipWhitespace
    : {skip();}
    ;