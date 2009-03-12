pushd c:\dev\eggs
call eggxap.bat
copy eggs.xap %~dp0Calculator.Web\ClientBin\
popd
