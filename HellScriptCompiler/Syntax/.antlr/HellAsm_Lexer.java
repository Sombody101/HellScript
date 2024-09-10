// Generated from d:/HellScript/HellScriptCompiler/Syntax/HellAsm_Lexer.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue", "this-escape"})
public class HellAsm_Lexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		DoubleQuote=1, Semi=2, Colon=3, OpenBrack=4, CloseBrack=5, OpenParen=6, 
		CloseParen=7, Equals=8, Program=9, Structure=10, Method=11, ArgCount=12, 
		StringLiteral=13, SingleLineComment=14, DoubleLiteral=15, NumericLiteral=16, 
		Identifier=17, WhiteSpaces=18;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"DoubleQuote", "Semi", "Colon", "OpenBrack", "CloseBrack", "OpenParen", 
			"CloseParen", "Equals", "Program", "Structure", "Method", "ArgCount", 
			"StringLiteral", "SingleLineComment", "DoubleLiteral", "NumericLiteral", 
			"Identifier", "WhiteSpaces"
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


	public HellAsm_Lexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "HellAsm_Lexer.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\u0004\u0000\u0012~\u0006\uffff\uffff\u0002\u0000\u0007\u0000\u0002\u0001"+
		"\u0007\u0001\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004"+
		"\u0007\u0004\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007"+
		"\u0007\u0007\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b"+
		"\u0007\u000b\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002"+
		"\u000f\u0007\u000f\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011\u0001"+
		"\u0000\u0001\u0000\u0001\u0001\u0001\u0001\u0001\u0002\u0001\u0002\u0001"+
		"\u0003\u0001\u0003\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001"+
		"\u0006\u0001\u0006\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0001\b\u0001"+
		"\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\t\u0001\t\u0001\t\u0001"+
		"\t\u0001\t\u0001\t\u0001\t\u0001\t\u0001\n\u0001\n\u0001\n\u0001\n\u0001"+
		"\n\u0001\n\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001\u000b\u0001\u000b"+
		"\u0001\u000b\u0001\u000b\u0001\f\u0001\f\u0001\f\u0001\f\u0001\r\u0001"+
		"\r\u0005\r[\b\r\n\r\f\r^\t\r\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001"+
		"\u000e\u0001\u000e\u0001\u000f\u0001\u000f\u0004\u000fh\b\u000f\u000b"+
		"\u000f\f\u000fi\u0001\u000f\u0004\u000fm\b\u000f\u000b\u000f\f\u000fn"+
		"\u0003\u000fq\b\u000f\u0001\u0010\u0004\u0010t\b\u0010\u000b\u0010\f\u0010"+
		"u\u0001\u0011\u0004\u0011y\b\u0011\u000b\u0011\f\u0011z\u0001\u0011\u0001"+
		"\u0011\u0000\u0000\u0012\u0001\u0001\u0003\u0002\u0005\u0003\u0007\u0004"+
		"\t\u0005\u000b\u0006\r\u0007\u000f\b\u0011\t\u0013\n\u0015\u000b\u0017"+
		"\f\u0019\r\u001b\u000e\u001d\u000f\u001f\u0010!\u0011#\u0012\u0001\u0000"+
		"\u0005\u0003\u0000\n\n\r\r\u2028\u2029\u0001\u000009\u0002\u000009__\u0002"+
		"\u0000,,..\u0004\u0000\t\t\u000b\f  \u00a0\u00a0\u0083\u0000\u0001\u0001"+
		"\u0000\u0000\u0000\u0000\u0003\u0001\u0000\u0000\u0000\u0000\u0005\u0001"+
		"\u0000\u0000\u0000\u0000\u0007\u0001\u0000\u0000\u0000\u0000\t\u0001\u0000"+
		"\u0000\u0000\u0000\u000b\u0001\u0000\u0000\u0000\u0000\r\u0001\u0000\u0000"+
		"\u0000\u0000\u000f\u0001\u0000\u0000\u0000\u0000\u0011\u0001\u0000\u0000"+
		"\u0000\u0000\u0013\u0001\u0000\u0000\u0000\u0000\u0015\u0001\u0000\u0000"+
		"\u0000\u0000\u0017\u0001\u0000\u0000\u0000\u0000\u0019\u0001\u0000\u0000"+
		"\u0000\u0000\u001b\u0001\u0000\u0000\u0000\u0000\u001d\u0001\u0000\u0000"+
		"\u0000\u0000\u001f\u0001\u0000\u0000\u0000\u0000!\u0001\u0000\u0000\u0000"+
		"\u0000#\u0001\u0000\u0000\u0000\u0001%\u0001\u0000\u0000\u0000\u0003\'"+
		"\u0001\u0000\u0000\u0000\u0005)\u0001\u0000\u0000\u0000\u0007+\u0001\u0000"+
		"\u0000\u0000\t-\u0001\u0000\u0000\u0000\u000b/\u0001\u0000\u0000\u0000"+
		"\r1\u0001\u0000\u0000\u0000\u000f3\u0001\u0000\u0000\u0000\u00115\u0001"+
		"\u0000\u0000\u0000\u0013>\u0001\u0000\u0000\u0000\u0015F\u0001\u0000\u0000"+
		"\u0000\u0017N\u0001\u0000\u0000\u0000\u0019T\u0001\u0000\u0000\u0000\u001b"+
		"X\u0001\u0000\u0000\u0000\u001da\u0001\u0000\u0000\u0000\u001fp\u0001"+
		"\u0000\u0000\u0000!s\u0001\u0000\u0000\u0000#x\u0001\u0000\u0000\u0000"+
		"%&\u0005\"\u0000\u0000&\u0002\u0001\u0000\u0000\u0000\'(\u0005;\u0000"+
		"\u0000(\u0004\u0001\u0000\u0000\u0000)*\u0005:\u0000\u0000*\u0006\u0001"+
		"\u0000\u0000\u0000+,\u0005{\u0000\u0000,\b\u0001\u0000\u0000\u0000-.\u0005"+
		"}\u0000\u0000.\n\u0001\u0000\u0000\u0000/0\u0005(\u0000\u00000\f\u0001"+
		"\u0000\u0000\u000012\u0005)\u0000\u00002\u000e\u0001\u0000\u0000\u0000"+
		"34\u0005=\u0000\u00004\u0010\u0001\u0000\u0000\u000056\u0005.\u0000\u0000"+
		"67\u0005p\u0000\u000078\u0005r\u0000\u000089\u0005o\u0000\u00009:\u0005"+
		"g\u0000\u0000:;\u0005r\u0000\u0000;<\u0005a\u0000\u0000<=\u0005m\u0000"+
		"\u0000=\u0012\u0001\u0000\u0000\u0000>?\u0005.\u0000\u0000?@\u0005s\u0000"+
		"\u0000@A\u0005t\u0000\u0000AB\u0005r\u0000\u0000BC\u0005u\u0000\u0000"+
		"CD\u0005c\u0000\u0000DE\u0005t\u0000\u0000E\u0014\u0001\u0000\u0000\u0000"+
		"FG\u0005.\u0000\u0000GH\u0005m\u0000\u0000HI\u0005e\u0000\u0000IJ\u0005"+
		"t\u0000\u0000JK\u0005h\u0000\u0000KL\u0005o\u0000\u0000LM\u0005d\u0000"+
		"\u0000M\u0016\u0001\u0000\u0000\u0000NO\u0005.\u0000\u0000OP\u0005a\u0000"+
		"\u0000PQ\u0005r\u0000\u0000QR\u0005g\u0000\u0000RS\u0005s\u0000\u0000"+
		"S\u0018\u0001\u0000\u0000\u0000TU\u0005\"\u0000\u0000UV\t\u0000\u0000"+
		"\u0000VW\u0005\"\u0000\u0000W\u001a\u0001\u0000\u0000\u0000X\\\u0005;"+
		"\u0000\u0000Y[\b\u0000\u0000\u0000ZY\u0001\u0000\u0000\u0000[^\u0001\u0000"+
		"\u0000\u0000\\Z\u0001\u0000\u0000\u0000\\]\u0001\u0000\u0000\u0000]_\u0001"+
		"\u0000\u0000\u0000^\\\u0001\u0000\u0000\u0000_`\u0006\r\u0000\u0000`\u001c"+
		"\u0001\u0000\u0000\u0000ab\u0003\u001f\u000f\u0000bc\u0005.\u0000\u0000"+
		"cd\u0003\u001f\u000f\u0000d\u001e\u0001\u0000\u0000\u0000eg\u0007\u0001"+
		"\u0000\u0000fh\u0007\u0002\u0000\u0000gf\u0001\u0000\u0000\u0000hi\u0001"+
		"\u0000\u0000\u0000ig\u0001\u0000\u0000\u0000ij\u0001\u0000\u0000\u0000"+
		"jq\u0001\u0000\u0000\u0000km\u0007\u0001\u0000\u0000lk\u0001\u0000\u0000"+
		"\u0000mn\u0001\u0000\u0000\u0000nl\u0001\u0000\u0000\u0000no\u0001\u0000"+
		"\u0000\u0000oq\u0001\u0000\u0000\u0000pe\u0001\u0000\u0000\u0000pl\u0001"+
		"\u0000\u0000\u0000q \u0001\u0000\u0000\u0000rt\b\u0003\u0000\u0000sr\u0001"+
		"\u0000\u0000\u0000tu\u0001\u0000\u0000\u0000us\u0001\u0000\u0000\u0000"+
		"uv\u0001\u0000\u0000\u0000v\"\u0001\u0000\u0000\u0000wy\u0007\u0004\u0000"+
		"\u0000xw\u0001\u0000\u0000\u0000yz\u0001\u0000\u0000\u0000zx\u0001\u0000"+
		"\u0000\u0000z{\u0001\u0000\u0000\u0000{|\u0001\u0000\u0000\u0000|}\u0006"+
		"\u0011\u0000\u0000}$\u0001\u0000\u0000\u0000\u0007\u0000\\inpuz\u0001"+
		"\u0000\u0001\u0000";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}