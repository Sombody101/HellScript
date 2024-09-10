using Antlr4.Runtime;
using static HellAsm_Parser;

/// <summary>
/// All parser methods that used in grammar (p, prev, notLineTerminator, etc.)
/// should start with lower case char similar to parser rules.
/// </summary>
public abstract class HellAsmParserBase : Parser
{
    private readonly Stack<string> _tagNames = new();
    public HellAsmParserBase(ITokenStream input)
        : base(input)
    {
    }

    public HellAsmParserBase(ITokenStream input, TextWriter output, TextWriter errorOutput) : this(input)
    {
    }

    /// <summary>
    /// Short form for prev(String str)
    /// </summary>
    protected bool p(string str)
    {
        return prev(str);
    }

    /// <summary>
    /// Whether the previous token value equals to str
    /// </summary>
    protected bool prev(string str)
    {
        return ((ITokenStream)InputStream).LT(-1).Text.Equals(str);
    }

    // Short form for next(String str)
    protected bool n(string str)
    {
        return next(str);
    }

    // Whether the next token value equals to @param str
    protected bool next(string str)
    {
        return ((ITokenStream)InputStream).LT(1).Text.Equals(str);
    }

    protected bool notLineTerminator()
    {
        return !lineTerminatorAhead();
    }

    protected bool notOpenBraceAndNotFunction()
    {
        int nextTokenType = ((ITokenStream)InputStream).LT(1).Type;
        return nextTokenType is not OpenBrack and not Method;
    }

    protected bool closeBrace()
    {
        return ((ITokenStream)InputStream).LT(1).Type == CloseBrack;
    }

    /// <summary>
    /// Returns true if on the current index of the parser's
    /// token stream a token exists on the Hidden channel which
    /// either is a line terminator, or is a multi line comment that
    /// contains a line terminator.
    /// </summary>
    protected bool lineTerminatorAhead()
    {
        // Get the token ahead of the current index.
        int possibleIndexEosToken = CurrentToken.TokenIndex - 1;
        if (possibleIndexEosToken < 0)
        {
            return false;
        }

        IToken ahead = ((ITokenStream)InputStream).Get(possibleIndexEosToken);

        if (ahead.Channel != Lexer.Hidden)
        {
            // We're only interested in tokens on the Hidden channel.
            return false;
        }

        if (ahead.Type == Newline)
        {
            // There is definitely a line terminator ahead.
            return true;
        }

        if (ahead.Type == Whitespace)
        {
            // Get the token ahead of the current whitespaces.
            possibleIndexEosToken = CurrentToken.TokenIndex - 2;
            if (possibleIndexEosToken < 0)
            {
                return false;
            }

            ahead = ((ITokenStream)InputStream).Get(possibleIndexEosToken);
        }

        // Get the token's text and type.
        string text = ahead.Text;
        int type = ahead.Type;

        // Check if the token is, or contains a line terminator.
        return text.Contains('\r') 
            || text.Contains('\n') 
            || (type == Newline);
    }

    protected void skip() { }
}
