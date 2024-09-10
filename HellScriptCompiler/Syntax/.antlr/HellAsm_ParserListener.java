// Generated from d:/HellScript/HellScriptCompiler/Syntax/HellAsm_Parser.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link HellAsm_Parser}.
 */
public interface HellAsm_ParserListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link HellAsm_Parser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(HellAsm_Parser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link HellAsm_Parser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(HellAsm_Parser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link HellAsm_Parser#programMetadata}.
	 * @param ctx the parse tree
	 */
	void enterProgramMetadata(HellAsm_Parser.ProgramMetadataContext ctx);
	/**
	 * Exit a parse tree produced by {@link HellAsm_Parser#programMetadata}.
	 * @param ctx the parse tree
	 */
	void exitProgramMetadata(HellAsm_Parser.ProgramMetadataContext ctx);
	/**
	 * Enter a parse tree produced by {@link HellAsm_Parser#methodDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterMethodDeclaration(HellAsm_Parser.MethodDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link HellAsm_Parser#methodDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitMethodDeclaration(HellAsm_Parser.MethodDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link HellAsm_Parser#structDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterStructDeclaration(HellAsm_Parser.StructDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link HellAsm_Parser#structDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitStructDeclaration(HellAsm_Parser.StructDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link HellAsm_Parser#opcode}.
	 * @param ctx the parse tree
	 */
	void enterOpcode(HellAsm_Parser.OpcodeContext ctx);
	/**
	 * Exit a parse tree produced by {@link HellAsm_Parser#opcode}.
	 * @param ctx the parse tree
	 */
	void exitOpcode(HellAsm_Parser.OpcodeContext ctx);
	/**
	 * Enter a parse tree produced by {@link HellAsm_Parser#argument}.
	 * @param ctx the parse tree
	 */
	void enterArgument(HellAsm_Parser.ArgumentContext ctx);
	/**
	 * Exit a parse tree produced by {@link HellAsm_Parser#argument}.
	 * @param ctx the parse tree
	 */
	void exitArgument(HellAsm_Parser.ArgumentContext ctx);
	/**
	 * Enter a parse tree produced by {@link HellAsm_Parser#label}.
	 * @param ctx the parse tree
	 */
	void enterLabel(HellAsm_Parser.LabelContext ctx);
	/**
	 * Exit a parse tree produced by {@link HellAsm_Parser#label}.
	 * @param ctx the parse tree
	 */
	void exitLabel(HellAsm_Parser.LabelContext ctx);
}