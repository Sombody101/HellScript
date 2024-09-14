![GitHub repo size](https://img.shields.io/github/repo-size/Sombody101/HellScript) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/Sombody101/HellScript)

# HellScript
HellScript is an amateur implementation of a custom bytecode completely written in C# (with the help of [.NET AOT](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=net7%2Cwindowshttps://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=net7%2Cwindows) compilation). It is still a WIP, and as such, will not work correctly.

Currently, there is only a [hell assembly](./test/test.hasm) language (or, "hasm") which is compiled to a raw binary. A second compiler will be made eventually which will compile
a custom high-level syntax to the hell assembly, then to the binary to be used with the HellScriptRuntime.

## Build instructions

The [makefile](./makefile) can realistically only be used with Linux, but it also contains implementations for Windows. Since .NET AOT can only be compiled for x64, there
are no options for compiling to x86.

To build both the runtime and compiler for your OS, use:

```bash
$ make # The argument `all` can also be used
```
This will branch off and run one of the following commands based on your detected OS
```bash
$ make win
$ make linux
```

To make only the runtime for your OS:

```bash
$ make runtime
```

For only the compiler:

```bash
$ make compiler
```

If, for some reason, you want to target an OS manually, you can add the OS as a suffix:
```bash
$ make <wanted app>-<os>
```