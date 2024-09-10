![GitHub repo size](https://img.shields.io/github/repo-size/Sombody101/HellScript)

# HellScript
HellScript is an amateur implementation of a custom bytecode completely written in C# (with the help of [.NET AOT](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=net7%2Cwindowshttps://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=net7%2Cwindows) compilation). It is still a WIP, and as such, will not work correctly.

Currently, there is only a [hell assembly](./test/test.hasm) language (or, "hasm") which is compiled to a raw binary. A second compiler will be made eventually which will compile
a custom high-level syntax to the hell assembly, then from there to the binary to be use with the HellScriptRuntime.