using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellScriptRuntime.Exceptions;

public class InvalidScriptTypeException : Exception
{
    public InvalidScriptTypeException(string filePath)
        : base($"The file '{filePath}' is not a hellscript binary.") { }
}
