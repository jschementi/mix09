REM Use following code for private builds
pushd c:\dev\agdlr
msbuild /p:Configuration=Debug
rmdir /S /Q %~dp0bin
robocopy "bin\debug" %~dp0bin Iron*.dll Microsoft*.dll Chiron.exe Chiron.exe.config

REM Use following code for release builds
REM pushd c:\dev\releases\agdlr-0.5.0.sl3b
REM rmdir /S /Q %~dp0bin
REM robocopy "bin" %~dp0bin Iron*.dll Microsoft*.dll Chiron*

popd

pushd %~dp0bin

mkdir IronRuby
mkdir IronPython
mkdir Microsoft.Scripting

copy IronRuby* IronRuby\
copy IronPython* IronPython\
copy Microsoft.Scripting* Microsoft.Scripting\

Chiron.exe /d:IronRuby /x:IronRuby-0.5.0.slvx
Chiron.exe /d:IronPython /x:IronPython-0.5.0.slvx
Chiron.exe /d:Microsoft.Scripting /x:Microsoft.Scripting-0.5.0.slvx

rmdir /S /Q IronRuby\
rmdir /S /Q IronPython\
rmdir /S /Q Microsoft.Scripting\

popd

ruby %~dp0upload.rb
