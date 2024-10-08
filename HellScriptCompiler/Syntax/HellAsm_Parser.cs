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

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class HellAsm_Parser : HellAsmParserBase {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		MetaProgram=1, Structure=2, CompilerArg=3, Local=4, Method=5, ArgCount=6, 
		DoubleQuote=7, Comma=8, Semi=9, Colon=10, OpenBrack=11, CloseBrack=12, 
		OpenParen=13, CloseParen=14, BoxOpen=15, BoxClose=16, Dollar=17, At=18, 
		Equals=19, Ampersand=20, LocalReference=21, StructReference=22, Identifier=23, 
		SingleLineComment=24, StringConstant=25, FastConstant=26, FloatingConstant=27, 
		IntegerConstant=28, Whitespace=29, Newline=30, UnexpectedCharacter=31;
	public const int
		RULE_program = 0, RULE_programLine = 1, RULE_line = 2, RULE_programMetadata = 3, 
		RULE_metadataSet = 4, RULE_methodDeclaration = 5, RULE_structDeclaration = 6, 
		RULE_fieldDeclaration = 7, RULE_fieldType = 8, RULE_definitionMetadata = 9, 
		RULE_label = 10, RULE_opcode = 11, RULE_argumentList = 12, RULE_argument = 13, 
		RULE_skipWhitespace = 14;
	public static readonly string[] ruleNames = {
		"program", "programLine", "line", "programMetadata", "metadataSet", "methodDeclaration", 
		"structDeclaration", "fieldDeclaration", "fieldType", "definitionMetadata", 
		"label", "opcode", "argumentList", "argument", "skipWhitespace"
	};

	private static readonly string[] _LiteralNames = {
		null, "'.program'", "'.struct'", "'.compilerdata'", "'.local'", "'.method'", 
		"'.args'", "'\"'", "','", "';'", "':'", "'{'", "'}'", "'('", "')'", "'['", 
		"']'", "'$'", "'@'", "'='", "'&'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "MetaProgram", "Structure", "CompilerArg", "Local", "Method", "ArgCount", 
		"DoubleQuote", "Comma", "Semi", "Colon", "OpenBrack", "CloseBrack", "OpenParen", 
		"CloseParen", "BoxOpen", "BoxClose", "Dollar", "At", "Equals", "Ampersand", 
		"LocalReference", "StructReference", "Identifier", "SingleLineComment", 
		"StringConstant", "FastConstant", "FloatingConstant", "IntegerConstant", 
		"Whitespace", "Newline", "UnexpectedCharacter"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "HellAsm_Parser.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static HellAsm_Parser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public HellAsm_Parser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public HellAsm_Parser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class ProgramContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ProgramMetadataContext programMetadata() {
			return GetRuleContext<ProgramMetadataContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(HellAsm_Parser.Eof, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ProgramLineContext[] programLine() {
			return GetRuleContexts<ProgramLineContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ProgramLineContext programLine(int i) {
			return GetRuleContext<ProgramLineContext>(i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_program; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgram(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgramContext program() {
		ProgramContext _localctx = new ProgramContext(Context, State);
		EnterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 30;
			programMetadata();
			State = 34;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & 8388644L) != 0)) {
				{
				{
				State = 31;
				programLine();
				}
				}
				State = 36;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 37;
			Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ProgramLineContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public LineContext line() {
			return GetRuleContext<LineContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public MethodDeclarationContext methodDeclaration() {
			return GetRuleContext<MethodDeclarationContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public StructDeclarationContext structDeclaration() {
			return GetRuleContext<StructDeclarationContext>(0);
		}
		public ProgramLineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_programLine; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgramLine(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgramLineContext programLine() {
		ProgramLineContext _localctx = new ProgramLineContext(Context, State);
		EnterRule(_localctx, 2, RULE_programLine);
		try {
			State = 42;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case Identifier:
				EnterOuterAlt(_localctx, 1);
				{
				State = 39;
				line();
				}
				break;
			case Method:
				EnterOuterAlt(_localctx, 2);
				{
				State = 40;
				methodDeclaration();
				}
				break;
			case Structure:
				EnterOuterAlt(_localctx, 3);
				{
				State = 41;
				structDeclaration();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class LineContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public OpcodeContext opcode() {
			return GetRuleContext<OpcodeContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public LabelContext label() {
			return GetRuleContext<LabelContext>(0);
		}
		public LineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_line; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLine(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public LineContext line() {
		LineContext _localctx = new LineContext(Context, State);
		EnterRule(_localctx, 4, RULE_line);
		try {
			State = 46;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,2,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 44;
				opcode();
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 45;
				label();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ProgramMetadataContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode MetaProgram() { return GetToken(HellAsm_Parser.MetaProgram, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OpenBrack() { return GetToken(HellAsm_Parser.OpenBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CloseBrack() { return GetToken(HellAsm_Parser.CloseBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public MetadataSetContext[] metadataSet() {
			return GetRuleContexts<MetadataSetContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public MetadataSetContext metadataSet(int i) {
			return GetRuleContext<MetadataSetContext>(i);
		}
		public ProgramMetadataContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_programMetadata; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgramMetadata(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgramMetadataContext programMetadata() {
		ProgramMetadataContext _localctx = new ProgramMetadataContext(Context, State);
		EnterRule(_localctx, 6, RULE_programMetadata);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 48;
			Match(MetaProgram);
			State = 49;
			Match(OpenBrack);
			State = 53;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==Identifier) {
				{
				{
				State = 50;
				metadataSet();
				}
				}
				State = 55;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 56;
			Match(CloseBrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MetadataSetContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Equals() { return GetToken(HellAsm_Parser.Equals, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ArgumentContext argument() {
			return GetRuleContext<ArgumentContext>(0);
		}
		public MetadataSetContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_metadataSet; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMetadataSet(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MetadataSetContext metadataSet() {
		MetadataSetContext _localctx = new MetadataSetContext(Context, State);
		EnterRule(_localctx, 8, RULE_metadataSet);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 58;
			Match(Identifier);
			State = 59;
			Match(Equals);
			State = 60;
			argument();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MethodDeclarationContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Method() { return GetToken(HellAsm_Parser.Method, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OpenParen() { return GetToken(HellAsm_Parser.OpenParen, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CloseParen() { return GetToken(HellAsm_Parser.CloseParen, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OpenBrack() { return GetToken(HellAsm_Parser.OpenBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CloseBrack() { return GetToken(HellAsm_Parser.CloseBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public FieldDeclarationContext[] fieldDeclaration() {
			return GetRuleContexts<FieldDeclarationContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public FieldDeclarationContext fieldDeclaration(int i) {
			return GetRuleContext<FieldDeclarationContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public DefinitionMetadataContext definitionMetadata() {
			return GetRuleContext<DefinitionMetadataContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public LineContext[] line() {
			return GetRuleContexts<LineContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public LineContext line(int i) {
			return GetRuleContext<LineContext>(i);
		}
		public MethodDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_methodDeclaration; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMethodDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MethodDeclarationContext methodDeclaration() {
		MethodDeclarationContext _localctx = new MethodDeclarationContext(Context, State);
		EnterRule(_localctx, 10, RULE_methodDeclaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 62;
			Match(Method);
			State = 63;
			Match(Identifier);
			State = 64;
			Match(OpenParen);
			State = 68;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==Local) {
				{
				{
				State = 65;
				fieldDeclaration();
				}
				}
				State = 70;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 71;
			Match(CloseParen);
			State = 73;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			if (_la==CompilerArg) {
				{
				State = 72;
				definitionMetadata();
				}
			}

			State = 75;
			Match(OpenBrack);
			State = 79;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==Identifier) {
				{
				{
				State = 76;
				line();
				}
				}
				State = 81;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 82;
			Match(CloseBrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StructDeclarationContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Structure() { return GetToken(HellAsm_Parser.Structure, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OpenBrack() { return GetToken(HellAsm_Parser.OpenBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CloseBrack() { return GetToken(HellAsm_Parser.CloseBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public FieldDeclarationContext[] fieldDeclaration() {
			return GetRuleContexts<FieldDeclarationContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public FieldDeclarationContext fieldDeclaration(int i) {
			return GetRuleContext<FieldDeclarationContext>(i);
		}
		public StructDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_structDeclaration; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStructDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StructDeclarationContext structDeclaration() {
		StructDeclarationContext _localctx = new StructDeclarationContext(Context, State);
		EnterRule(_localctx, 12, RULE_structDeclaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 84;
			Match(Structure);
			State = 85;
			Match(Identifier);
			State = 86;
			Match(OpenBrack);
			State = 90;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==Local) {
				{
				{
				State = 87;
				fieldDeclaration();
				}
				}
				State = 92;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 93;
			Match(CloseBrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FieldDeclarationContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Local() { return GetToken(HellAsm_Parser.Local, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public FieldTypeContext fieldType() {
			return GetRuleContext<FieldTypeContext>(0);
		}
		public FieldDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_fieldDeclaration; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFieldDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FieldDeclarationContext fieldDeclaration() {
		FieldDeclarationContext _localctx = new FieldDeclarationContext(Context, State);
		EnterRule(_localctx, 14, RULE_fieldDeclaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 95;
			Match(Local);
			State = 96;
			Match(Identifier);
			State = 98;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			if (_la==At) {
				{
				State = 97;
				fieldType();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FieldTypeContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode At() { return GetToken(HellAsm_Parser.At, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		public FieldTypeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_fieldType; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFieldType(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FieldTypeContext fieldType() {
		FieldTypeContext _localctx = new FieldTypeContext(Context, State);
		EnterRule(_localctx, 16, RULE_fieldType);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 100;
			Match(At);
			State = 101;
			Match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DefinitionMetadataContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CompilerArg() { return GetToken(HellAsm_Parser.CompilerArg, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OpenBrack() { return GetToken(HellAsm_Parser.OpenBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CloseBrack() { return GetToken(HellAsm_Parser.CloseBrack, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public MetadataSetContext[] metadataSet() {
			return GetRuleContexts<MetadataSetContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public MetadataSetContext metadataSet(int i) {
			return GetRuleContext<MetadataSetContext>(i);
		}
		public DefinitionMetadataContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_definitionMetadata; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDefinitionMetadata(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DefinitionMetadataContext definitionMetadata() {
		DefinitionMetadataContext _localctx = new DefinitionMetadataContext(Context, State);
		EnterRule(_localctx, 18, RULE_definitionMetadata);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 103;
			Match(CompilerArg);
			State = 104;
			Match(OpenBrack);
			State = 108;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==Identifier) {
				{
				{
				State = 105;
				metadataSet();
				}
				}
				State = 110;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 111;
			Match(CloseBrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class LabelContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Colon() { return GetToken(HellAsm_Parser.Colon, 0); }
		public LabelContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_label; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLabel(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public LabelContext label() {
		LabelContext _localctx = new LabelContext(Context, State);
		EnterRule(_localctx, 20, RULE_label);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 113;
			Match(Identifier);
			State = 114;
			Match(Colon);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class OpcodeContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ArgumentContext argument() {
			return GetRuleContext<ArgumentContext>(0);
		}
		public OpcodeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_opcode; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitOpcode(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public OpcodeContext opcode() {
		OpcodeContext _localctx = new OpcodeContext(Context, State);
		EnterRule(_localctx, 22, RULE_opcode);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 116;
			Match(Identifier);
			State = 118;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,10,Context) ) {
			case 1:
				{
				State = 117;
				argument();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ArgumentListContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] Identifier() { return GetTokens(HellAsm_Parser.Identifier); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier(int i) {
			return GetToken(HellAsm_Parser.Identifier, i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] Comma() { return GetTokens(HellAsm_Parser.Comma); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Comma(int i) {
			return GetToken(HellAsm_Parser.Comma, i);
		}
		public ArgumentListContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_argumentList; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitArgumentList(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ArgumentListContext argumentList() {
		ArgumentListContext _localctx = new ArgumentListContext(Context, State);
		EnterRule(_localctx, 24, RULE_argumentList);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 120;
			Match(Identifier);
			State = 125;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==Comma) {
				{
				{
				State = 121;
				Match(Comma);
				State = 122;
				Match(Identifier);
				}
				}
				State = 127;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ArgumentContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode LocalReference() { return GetToken(HellAsm_Parser.LocalReference, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode StructReference() { return GetToken(HellAsm_Parser.StructReference, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode StringConstant() { return GetToken(HellAsm_Parser.StringConstant, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode FastConstant() { return GetToken(HellAsm_Parser.FastConstant, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode FloatingConstant() { return GetToken(HellAsm_Parser.FloatingConstant, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IntegerConstant() { return GetToken(HellAsm_Parser.IntegerConstant, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(HellAsm_Parser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OpenParen() { return GetToken(HellAsm_Parser.OpenParen, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CloseParen() { return GetToken(HellAsm_Parser.CloseParen, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode BoxOpen() { return GetToken(HellAsm_Parser.BoxOpen, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode BoxClose() { return GetToken(HellAsm_Parser.BoxClose, 0); }
		public ArgumentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_argument; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitArgument(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ArgumentContext argument() {
		ArgumentContext _localctx = new ArgumentContext(Context, State);
		EnterRule(_localctx, 26, RULE_argument);
		try {
			State = 140;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case LocalReference:
				EnterOuterAlt(_localctx, 1);
				{
				State = 128;
				Match(LocalReference);
				}
				break;
			case StructReference:
				EnterOuterAlt(_localctx, 2);
				{
				State = 129;
				Match(StructReference);
				}
				break;
			case StringConstant:
				EnterOuterAlt(_localctx, 3);
				{
				State = 130;
				Match(StringConstant);
				}
				break;
			case FastConstant:
				EnterOuterAlt(_localctx, 4);
				{
				State = 131;
				Match(FastConstant);
				}
				break;
			case FloatingConstant:
				EnterOuterAlt(_localctx, 5);
				{
				State = 132;
				Match(FloatingConstant);
				}
				break;
			case IntegerConstant:
				EnterOuterAlt(_localctx, 6);
				{
				State = 133;
				Match(IntegerConstant);
				}
				break;
			case Identifier:
				EnterOuterAlt(_localctx, 7);
				{
				State = 134;
				Match(Identifier);
				State = 135;
				Match(OpenParen);
				State = 136;
				Match(CloseParen);
				}
				break;
			case BoxOpen:
				EnterOuterAlt(_localctx, 8);
				{
				State = 137;
				Match(BoxOpen);
				State = 138;
				Match(Identifier);
				State = 139;
				Match(BoxClose);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class SkipWhitespaceContext : ParserRuleContext {
		public SkipWhitespaceContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_skipWhitespace; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHellAsm_ParserVisitor<TResult> typedVisitor = visitor as IHellAsm_ParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSkipWhitespace(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SkipWhitespaceContext skipWhitespace() {
		SkipWhitespaceContext _localctx = new SkipWhitespaceContext(Context, State);
		EnterRule(_localctx, 28, RULE_skipWhitespace);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			skip();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,31,145,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,6,2,7,
		7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,7,14,
		1,0,1,0,5,0,33,8,0,10,0,12,0,36,9,0,1,0,1,0,1,1,1,1,1,1,3,1,43,8,1,1,2,
		1,2,3,2,47,8,2,1,3,1,3,1,3,5,3,52,8,3,10,3,12,3,55,9,3,1,3,1,3,1,4,1,4,
		1,4,1,4,1,5,1,5,1,5,1,5,5,5,67,8,5,10,5,12,5,70,9,5,1,5,1,5,3,5,74,8,5,
		1,5,1,5,5,5,78,8,5,10,5,12,5,81,9,5,1,5,1,5,1,6,1,6,1,6,1,6,5,6,89,8,6,
		10,6,12,6,92,9,6,1,6,1,6,1,7,1,7,1,7,3,7,99,8,7,1,8,1,8,1,8,1,9,1,9,1,
		9,5,9,107,8,9,10,9,12,9,110,9,9,1,9,1,9,1,10,1,10,1,10,1,11,1,11,3,11,
		119,8,11,1,12,1,12,1,12,5,12,124,8,12,10,12,12,12,127,9,12,1,13,1,13,1,
		13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,3,13,141,8,13,1,14,1,14,
		1,14,0,0,15,0,2,4,6,8,10,12,14,16,18,20,22,24,26,28,0,0,149,0,30,1,0,0,
		0,2,42,1,0,0,0,4,46,1,0,0,0,6,48,1,0,0,0,8,58,1,0,0,0,10,62,1,0,0,0,12,
		84,1,0,0,0,14,95,1,0,0,0,16,100,1,0,0,0,18,103,1,0,0,0,20,113,1,0,0,0,
		22,116,1,0,0,0,24,120,1,0,0,0,26,140,1,0,0,0,28,142,1,0,0,0,30,34,3,6,
		3,0,31,33,3,2,1,0,32,31,1,0,0,0,33,36,1,0,0,0,34,32,1,0,0,0,34,35,1,0,
		0,0,35,37,1,0,0,0,36,34,1,0,0,0,37,38,5,0,0,1,38,1,1,0,0,0,39,43,3,4,2,
		0,40,43,3,10,5,0,41,43,3,12,6,0,42,39,1,0,0,0,42,40,1,0,0,0,42,41,1,0,
		0,0,43,3,1,0,0,0,44,47,3,22,11,0,45,47,3,20,10,0,46,44,1,0,0,0,46,45,1,
		0,0,0,47,5,1,0,0,0,48,49,5,1,0,0,49,53,5,11,0,0,50,52,3,8,4,0,51,50,1,
		0,0,0,52,55,1,0,0,0,53,51,1,0,0,0,53,54,1,0,0,0,54,56,1,0,0,0,55,53,1,
		0,0,0,56,57,5,12,0,0,57,7,1,0,0,0,58,59,5,23,0,0,59,60,5,19,0,0,60,61,
		3,26,13,0,61,9,1,0,0,0,62,63,5,5,0,0,63,64,5,23,0,0,64,68,5,13,0,0,65,
		67,3,14,7,0,66,65,1,0,0,0,67,70,1,0,0,0,68,66,1,0,0,0,68,69,1,0,0,0,69,
		71,1,0,0,0,70,68,1,0,0,0,71,73,5,14,0,0,72,74,3,18,9,0,73,72,1,0,0,0,73,
		74,1,0,0,0,74,75,1,0,0,0,75,79,5,11,0,0,76,78,3,4,2,0,77,76,1,0,0,0,78,
		81,1,0,0,0,79,77,1,0,0,0,79,80,1,0,0,0,80,82,1,0,0,0,81,79,1,0,0,0,82,
		83,5,12,0,0,83,11,1,0,0,0,84,85,5,2,0,0,85,86,5,23,0,0,86,90,5,11,0,0,
		87,89,3,14,7,0,88,87,1,0,0,0,89,92,1,0,0,0,90,88,1,0,0,0,90,91,1,0,0,0,
		91,93,1,0,0,0,92,90,1,0,0,0,93,94,5,12,0,0,94,13,1,0,0,0,95,96,5,4,0,0,
		96,98,5,23,0,0,97,99,3,16,8,0,98,97,1,0,0,0,98,99,1,0,0,0,99,15,1,0,0,
		0,100,101,5,18,0,0,101,102,5,23,0,0,102,17,1,0,0,0,103,104,5,3,0,0,104,
		108,5,11,0,0,105,107,3,8,4,0,106,105,1,0,0,0,107,110,1,0,0,0,108,106,1,
		0,0,0,108,109,1,0,0,0,109,111,1,0,0,0,110,108,1,0,0,0,111,112,5,12,0,0,
		112,19,1,0,0,0,113,114,5,23,0,0,114,115,5,10,0,0,115,21,1,0,0,0,116,118,
		5,23,0,0,117,119,3,26,13,0,118,117,1,0,0,0,118,119,1,0,0,0,119,23,1,0,
		0,0,120,125,5,23,0,0,121,122,5,8,0,0,122,124,5,23,0,0,123,121,1,0,0,0,
		124,127,1,0,0,0,125,123,1,0,0,0,125,126,1,0,0,0,126,25,1,0,0,0,127,125,
		1,0,0,0,128,141,5,21,0,0,129,141,5,22,0,0,130,141,5,25,0,0,131,141,5,26,
		0,0,132,141,5,27,0,0,133,141,5,28,0,0,134,135,5,23,0,0,135,136,5,13,0,
		0,136,141,5,14,0,0,137,138,5,15,0,0,138,139,5,23,0,0,139,141,5,16,0,0,
		140,128,1,0,0,0,140,129,1,0,0,0,140,130,1,0,0,0,140,131,1,0,0,0,140,132,
		1,0,0,0,140,133,1,0,0,0,140,134,1,0,0,0,140,137,1,0,0,0,141,27,1,0,0,0,
		142,143,6,14,-1,0,143,29,1,0,0,0,13,34,42,46,53,68,73,79,90,98,108,118,
		125,140
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
