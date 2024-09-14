ifeq ($(OS),Windows_NT)
    detected_OS := win
else
    detected_OS := linux
endif

HS_RUNTIME := ./HellScriptRuntime/HellScriptRuntime.csproj
HS_COMPILER := ./HellScriptCompiler/HellScriptCompiler.csproj

BUILD_DIR := ./aot-publish

.PHONY: all

# Not literal "all" items, just both items for the current OS
all: $(detected_OS)

win:
	$(MAKE) runtime-win compiler-win

linux:
	$(MAKE) runtime-linux compiler-linux

# Runtime

runtime: runtime-$(detected_OS)

runtime-win:
	dotnet publish $(HS_RUNTIME) -r win-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly

runtime-linux:
	dotnet publish $(HS_RUNTIME) -r linux-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly

# Compiler

compiler: compiler-$(detected_OS)

compiler-win:
	dotnet publish $(HS_COMPILER) -r win-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly

compiler-linux:
	dotnet publish $(HS_COMPILER) -r linux-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly