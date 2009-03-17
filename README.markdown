Silverlight Dynamic Lanugage Demos for MIX 09
=============================================

TicTacToe
---------
Testing C# Silverlight application with IronRuby

To run, open TicTacToe\TicTacToe.sln in Visual Studio. 
TicTacToe.Web is a ASP.NET MVC project, so install ASP.NET MVC here:
http://aspnet.codeplex.com/Wiki/View.aspx?title=MVC&referringTitle=Home

Make sure TicTacToe.Web is the start project, and TicTacToeTestPage.aspx 
is the start page.

To run tests, add "?test" to the end of the URL.

Calculator
----------
A calculator with IronPython functions

To run, open Calculator\Calculator.sln in Visual Studio. 
TicTacToe.Web is a ASP.NET MVC project, so install ASP.NET MVC here:
http://aspnet.codeplex.com/Wiki/View.aspx?title=MVC&referringTitle=Home

Make sure Calculator.Web is the start project, and CalculatorTestAppTestPage.aspx
is the start page.

When you add a valid python function in the right textbox, buttons will be 
created above to run the functions, with the first argument being the current
calculator value.

To run tests, add "?test" to the end of the URL.

AgDLR
-----
Making Silverlight applications in DLR languages

See http://github.com/jschementi/agdlr for more information

Eggs
----
Testing Silverlight applications with IronRuby

TicTacToe and Calculator use Eggs to run tests. See
http://github.com/jschementi/eggs for more information.

Silverlight Extensions
----------------------
Silverlight 3 supports extension for platform components, such as controls
the DLR, etc. Run extension-example\server.bat to see how extensions work.
