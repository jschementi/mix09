using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Microsoft.Scripting.Hosting;
using IronPython.Hosting;

namespace CalculatorTestApp
{
    public partial class Page : UserControl
    {
        private PythonEngine _engine;

        public Page() {
            InitializeComponent();

            _engine = new PythonEngine();
            _engine.Execute(code.Text.ToString());
        }
    }

    public class PythonEngine {
        private ScriptEngine _engine;

        public PythonEngine() {
            _engine = Python.CreateEngine();
        }

        public object Execute(string code) {
            return _engine.Execute(code);
        }
    }
}
