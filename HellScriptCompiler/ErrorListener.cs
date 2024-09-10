using Antlr4.Runtime;

namespace HellScriptCompiler;

internal class ErrorListener<T> : ConsoleErrorListener<T>
{
    public bool had_error;

    public override void SyntaxError(TextWriter output, IRecognizer recognizer, T offendingSymbol, int line,
        int col, string msg, RecognitionException e)
    {
        had_error = true;
        base.SyntaxError(output, recognizer, offendingSymbol, line, col, msg, e);
    }
}
