//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from d:/HellScriptProject/HellScript.cs/HellScriptCompiler/Syntax/HellAsm_Parser.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="HellAsm_Parser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IHellAsm_ParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] HellAsm_Parser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.programLine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgramLine([NotNull] HellAsm_Parser.ProgramLineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.programMetadata"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgramMetadata([NotNull] HellAsm_Parser.ProgramMetadataContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.metadataSet"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMetadataSet([NotNull] HellAsm_Parser.MetadataSetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.methodDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodDeclaration([NotNull] HellAsm_Parser.MethodDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.structDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructDeclaration([NotNull] HellAsm_Parser.StructDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.definitionMetadata"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefinitionMetadata([NotNull] HellAsm_Parser.DefinitionMetadataContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] HellAsm_Parser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLabel([NotNull] HellAsm_Parser.LabelContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.opcode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpcode([NotNull] HellAsm_Parser.OpcodeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.argumentList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgumentList([NotNull] HellAsm_Parser.ArgumentListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgument([NotNull] HellAsm_Parser.ArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.methodReference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodReference([NotNull] HellAsm_Parser.MethodReferenceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="HellAsm_Parser.skipWhitespace"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSkipWhitespace([NotNull] HellAsm_Parser.SkipWhitespaceContext context);
}
