pushd c:\dev\eggs
call eggxap.bat
copy eggs.xap %~dp0TicTacToe.Web\ClientBin\eggs.xap
popd
