pushd c:\dev\agdlr
msbuild
rmdir /S /Q %~dp0\bin
robocopy "bin\debug" %~dp0\bin Iron*.dll Microsoft*.dll Chiron.exe Chiron.exe.config
popd
