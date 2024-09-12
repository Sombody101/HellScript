ifeq ($(OS),Windows_NT)
    detected_OS := win
else
    detected_OS := linux
endif

HS_RUNTIME := ./HellScriptRuntime/HellScriptRuntime.csproj
HS_COMPILER := ./HellScriptCompiler/HellScriptCompiler.csproj

BUILD_DIR := ./aot-publish

.PHONY: build

build: build-$(detected_OS)

build-win:
	$(MAKE) build-runtime-win build-compiler-win

build-linux:
	$(MAKE) build-runtime-linux build-compiler-linux

# Runtime

build-runtime: build-runtime-$(detected_OS)

build-runtime-win:
	dotnet publish $(HS_RUNTIME) -r win-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly

build-runtime-linux:
	dotnet publish $(HS_RUNTIME) -r linux-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly

# Compiler

build-compiler: build-compiler-$(detected_OS)

build-compiler-win:
	dotnet publish $(HS_COMPILER) -r win-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly

build-compiler-linux:
	dotnet publish $(HS_COMPILER) -r linux-x64 -c Release -o $(BUILD_DIR) --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly