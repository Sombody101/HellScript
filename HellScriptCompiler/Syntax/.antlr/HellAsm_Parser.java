// Generated from d:/HellScript/HellScriptCompiler/Syntax/HellAsm_Parser.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class HellAsm_Parser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		DoubleQuote=1, Semi=2, Colon=3, OpenBrack=4, CloseBrack=5, OpenParen=6, 
		CloseParen=7, Equals=8, Program=9, Structure=10, Method=11, ArgCount=12, 
		StringLiteral=13, SingleLineComment=14, DoubleLiteral=15, NumericLiteral=16, 
		Identifier=17, WhiteSpaces=18;
	public static final int
		RULE_program = 0, RULE_programMetadata = 1, RULE_methodDeclaration = 2, 
		RULE_structDeclaration = 3, RULE_opcode = 4, RULE_argument = 5, RULE_label = 6;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "programMetadata", "methodDeclaration", "structDeclaration", 
			"opcode", "argument", "label"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'\"'", "';'", "':'", "'{'", "'}'", "'('", "')'", "'='", "'.program'", 
			"'.struct'", "'.method'", "'.args'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "DoubleQuote", "Semi", "Colon", "OpenBrack", "CloseBrack", "OpenParen", 
			"CloseParen", "Equals", "Program", "Structure", "Method", "ArgCount", 
			"StringLiteral", "SingleLineComment", "DoubleLiteral", "NumericLiteral", 
			"Identifier", "WhiteSpaces"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "HellAsm_Parser.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public HellAsm_Parser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ProgramContext extends ParserRuleContext {
		public ProgramMetadataContext programMetadata() {
			return getRuleContext(ProgramMetadataContext.class,0);
		}
		public List<OpcodeContext> opcode() {
			return getRuleContexts(OpcodeContext.class);
		}
		public OpcodeContext opcode(int i) {
			return getRuleContext(OpcodeContext.class,i);
		}
		public List<MethodDeclarationContext> methodDeclaration() {
			return getRuleContexts(MethodDeclarationContext.class);
		}
		public MethodDeclarationContext methodDeclaration(int i) {
			return getRuleContext(MethodDeclarationContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).enterProgram(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).exitProgram(this);
		}
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(15);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Program) {
				{
				setState(14);
				programMetadata();
				}
			}

			setState(21);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Method || _la==Identifier) {
				{
				setState(19);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case Identifier:
					{
					setState(17);
					opcode();
					}
					break;
				case Method:
					{
					setState(18);
					methodDeclaration();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(23);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ProgramMetadataContext extends ParserRuleContext {
		public TerminalNode Program() { return getToken(HellAsm_Parser.Program, 0); }
		public TerminalNode OpenBrack() { return getToken(HellAsm_Parser.OpenBrack, 0); }
		public TerminalNode Identifier() { return getToken(HellAsm_Parser.Identifier, 0); }
		public TerminalNode Equals() { return getToken(HellAsm_Parser.Equals, 0); }
		public ArgumentContext argument() {
			return getRuleContext(ArgumentContext.class,0);
		}
		public TerminalNode CloseBrack() { return getToken(HellAsm_Parser.CloseBrack, 0); }
		public ProgramMetadataContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_programMetadata; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).enterProgramMetadata(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).exitProgramMetadata(this);
		}
	}

	public final ProgramMetadataContext programMetadata() throws RecognitionException {
		ProgramMetadataContext _localctx = new ProgramMetadataContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_programMetadata);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(24);
			match(Program);
			setState(25);
			match(OpenBrack);
			setState(26);
			match(Identifier);
			setState(27);
			match(Equals);
			setState(28);
			argument();
			setState(29);
			match(CloseBrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class MethodDeclarationContext extends ParserRuleContext {
		public TerminalNode Method() { return getToken(HellAsm_Parser.Method, 0); }
		public TerminalNode Identifier() { return getToken(HellAsm_Parser.Identifier, 0); }
		public TerminalNode OpenParen() { return getToken(HellAsm_Parser.OpenParen, 0); }
		public TerminalNode ArgCount() { return getToken(HellAsm_Parser.ArgCount, 0); }
		public TerminalNode NumericLiteral() { return getToken(HellAsm_Parser.NumericLiteral, 0); }
		public TerminalNode CloseParen() { return getToken(HellAsm_Parser.CloseParen, 0); }
		public TerminalNode OpenBrack() { return getToken(HellAsm_Parser.OpenBrack, 0); }
		public TerminalNode CloseBrack() { return getToken(HellAsm_Parser.CloseBrack, 0); }
		public List<OpcodeContext> opcode() {
			return getRuleContexts(OpcodeContext.class);
		}
		public OpcodeContext opcode(int i) {
			return getRuleContext(OpcodeContext.class,i);
		}
		public List<LabelContext> label() {
			return getRuleContexts(LabelContext.class);
		}
		public LabelContext label(int i) {
			return getRuleContext(LabelContext.class,i);
		}
		public MethodDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_methodDeclaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).enterMethodDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).exitMethodDeclaration(this);
		}
	}

	public final MethodDeclarationContext methodDeclaration() throws RecognitionException {
		MethodDeclarationContext _localctx = new MethodDeclarationContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_methodDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(31);
			match(Method);
			setState(32);
			match(Identifier);
			setState(33);
			match(OpenParen);
			setState(34);
			match(ArgCount);
			setState(35);
			match(NumericLiteral);
			setState(36);
			match(CloseParen);
			setState(37);
			match(OpenBrack);
			setState(42);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Identifier) {
				{
				setState(40);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
				case 1:
					{
					setState(38);
					opcode();
					}
					break;
				case 2:
					{
					setState(39);
					label();
					}
					break;
				}
				}
				setState(44);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(45);
			match(CloseBrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StructDeclarationContext extends ParserRuleContext {
		public TerminalNode Structure() { return getToken(HellAsm_Parser.Structure, 0); }
		public TerminalNode Identifier() { return getToken(HellAsm_Parser.Identifier, 0); }
		public TerminalNode OpenBrack() { return getToken(HellAsm_Parser.OpenBrack, 0); }
		public TerminalNode CloseBrack() { return getToken(HellAsm_Parser.CloseBrack, 0); }
		public StructDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_structDeclaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).enterStructDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).exitStructDeclaration(this);
		}
	}

	public final StructDeclarationContext structDeclaration() throws RecognitionException {
		StructDeclarationContext _localctx = new StructDeclarationContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_structDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(47);
			match(Structure);
			setState(48);
			match(Identifier);
			setState(49);
			match(OpenBrack);
			setState(50);
			match(CloseBrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class OpcodeContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(HellAsm_Parser.Identifier, 0); }
		public ArgumentContext argument() {
			return getRuleContext(ArgumentContext.class,0);
		}
		public OpcodeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_opcode; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).enterOpcode(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).exitOpcode(this);
		}
	}

	public final OpcodeContext opcode() throws RecognitionException {
		OpcodeContext _localctx = new OpcodeContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_opcode);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(52);
			match(Identifier);
			setState(53);
			argument();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ArgumentContext extends ParserRuleContext {
		public TerminalNode StringLiteral() { return getToken(HellAsm_Parser.StringLiteral, 0); }
		public TerminalNode DoubleLiteral() { return getToken(HellAsm_Parser.DoubleLiteral, 0); }
		public TerminalNode NumericLiteral() { return getToken(HellAsm_Parser.NumericLiteral, 0); }
		public ArgumentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_argument; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).enterArgument(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).exitArgument(this);
		}
	}

	public final ArgumentContext argument() throws RecognitionException {
		ArgumentContext _localctx = new ArgumentContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_argument);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(55);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 106496L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class LabelContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(HellAsm_Parser.Identifier, 0); }
		public TerminalNode Colon() { return getToken(HellAsm_Parser.Colon, 0); }
		public LabelContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_label; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).enterLabel(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof HellAsm_ParserListener ) ((HellAsm_ParserListener)listener).exitLabel(this);
		}
	}

	public final LabelContext label() throws RecognitionException {
		LabelContext _localctx = new LabelContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_label);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(57);
			match(Identifier);
			setState(58);
			match(Colon);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\u0004\u0001\u0012=\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0001\u0000\u0003\u0000\u0010"+
		"\b\u0000\u0001\u0000\u0001\u0000\u0005\u0000\u0014\b\u0000\n\u0000\f\u0000"+
		"\u0017\t\u0000\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001"+
		"\u0001\u0001\u0001\u0001\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002"+
		"\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0005\u0002"+
		")\b\u0002\n\u0002\f\u0002,\t\u0002\u0001\u0002\u0001\u0002\u0001\u0003"+
		"\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0004\u0001\u0004"+
		"\u0001\u0004\u0001\u0005\u0001\u0005\u0001\u0006\u0001\u0006\u0001\u0006"+
		"\u0001\u0006\u0000\u0000\u0007\u0000\u0002\u0004\u0006\b\n\f\u0000\u0001"+
		"\u0002\u0000\r\r\u000f\u0010:\u0000\u000f\u0001\u0000\u0000\u0000\u0002"+
		"\u0018\u0001\u0000\u0000\u0000\u0004\u001f\u0001\u0000\u0000\u0000\u0006"+
		"/\u0001\u0000\u0000\u0000\b4\u0001\u0000\u0000\u0000\n7\u0001\u0000\u0000"+
		"\u0000\f9\u0001\u0000\u0000\u0000\u000e\u0010\u0003\u0002\u0001\u0000"+
		"\u000f\u000e\u0001\u0000\u0000\u0000\u000f\u0010\u0001\u0000\u0000\u0000"+
		"\u0010\u0015\u0001\u0000\u0000\u0000\u0011\u0014\u0003\b\u0004\u0000\u0012"+
		"\u0014\u0003\u0004\u0002\u0000\u0013\u0011\u0001\u0000\u0000\u0000\u0013"+
		"\u0012\u0001\u0000\u0000\u0000\u0014\u0017\u0001\u0000\u0000\u0000\u0015"+
		"\u0013\u0001\u0000\u0000\u0000\u0015\u0016\u0001\u0000\u0000\u0000\u0016"+
		"\u0001\u0001\u0000\u0000\u0000\u0017\u0015\u0001\u0000\u0000\u0000\u0018"+
		"\u0019\u0005\t\u0000\u0000\u0019\u001a\u0005\u0004\u0000\u0000\u001a\u001b"+
		"\u0005\u0011\u0000\u0000\u001b\u001c\u0005\b\u0000\u0000\u001c\u001d\u0003"+
		"\n\u0005\u0000\u001d\u001e\u0005\u0005\u0000\u0000\u001e\u0003\u0001\u0000"+
		"\u0000\u0000\u001f \u0005\u000b\u0000\u0000 !\u0005\u0011\u0000\u0000"+
		"!\"\u0005\u0006\u0000\u0000\"#\u0005\f\u0000\u0000#$\u0005\u0010\u0000"+
		"\u0000$%\u0005\u0007\u0000\u0000%*\u0005\u0004\u0000\u0000&)\u0003\b\u0004"+
		"\u0000\')\u0003\f\u0006\u0000(&\u0001\u0000\u0000\u0000(\'\u0001\u0000"+
		"\u0000\u0000),\u0001\u0000\u0000\u0000*(\u0001\u0000\u0000\u0000*+\u0001"+
		"\u0000\u0000\u0000+-\u0001\u0000\u0000\u0000,*\u0001\u0000\u0000\u0000"+
		"-.\u0005\u0005\u0000\u0000.\u0005\u0001\u0000\u0000\u0000/0\u0005\n\u0000"+
		"\u000001\u0005\u0011\u0000\u000012\u0005\u0004\u0000\u000023\u0005\u0005"+
		"\u0000\u00003\u0007\u0001\u0000\u0000\u000045\u0005\u0011\u0000\u0000"+
		"56\u0003\n\u0005\u00006\t\u0001\u0000\u0000\u000078\u0007\u0000\u0000"+
		"\u00008\u000b\u0001\u0000\u0000\u00009:\u0005\u0011\u0000\u0000:;\u0005"+
		"\u0003\u0000\u0000;\r\u0001\u0000\u0000\u0000\u0005\u000f\u0013\u0015"+
		"(*";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}