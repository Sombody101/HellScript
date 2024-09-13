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
    | structDeclaration
    ;

line
    : opcode
    | label
    ;

programMetadata
    : MetaProgram '{' metadataSet* '}'
    ;

metadataSet
    : Identifier '=' argument
    ;

methodDeclaration
    : Method Identifier '(' fieldDeclaration* ')' definitionMetadata? '{' line* '}'
    ;

structDeclaration
    : Structure Identifier '{' fieldDeclaration* '}'
    ;

fieldDeclaration
    : Local Identifier fieldType?
    ;

fieldType
    : At Identifier
    ;

definitionMetadata
    : CompilerArg '{' metadataSet* '}'
    ;

label
    : Identifier ':'
    ;

opcode
    : Identifier (argument)?
    ;

argumentList
    : Identifier (',' Identifier)*
    ;

argument
    : LocalReference
    | StructReference
    | StringConstant
    | FastConstant
    | FloatingConstant
    | IntegerConstant
    | Identifier '(' /*argumentList?*/ ')'
    | '[' Identifier ']'
    ;

skipWhitespace
    : {skip();}
    ;