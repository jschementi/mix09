pushd c:\dev\agdlr

msbuild
rmdir /S /Q %~dp0bin
robocopy "bin\debug" %~dp0bin Iron*.dll Microsoft*.dll Chiron.exe Chiron.exe.config

pushd %~dp0bin

mkdir IronRuby
mkdir IronPython
mkdir Microsoft.Scripting

copy IronRuby* IronRuby\
copy IronPython* IronPython\
copy Microsoft.Scripting* Microsoft.Scripting\

Chiron.exe /d:IronRuby /x:IronRuby.slvx
Chiron.exe /d:IronPython /x:IronPython.slvx
Chiron.exe /d:Microsoft.Scripting /x:Microsoft.Scripting.slvx

rmdir /S /Q IronRuby\
rmdir /S /Q IronPython\
rmdir /S /Q Microsoft.Scripting\

popd
popd

ruby %~dp0upload.rb
