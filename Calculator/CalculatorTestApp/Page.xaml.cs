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
using System.Windows.Browser;
using System.IO;

namespace CalculatorTestApp
{
    public partial class Page : UserControl
    {
        private PythonEngine _engine;

        public Page() {
            InitializeComponent();

            _engine = new PythonEngine();

            Functions.KeyUp += new KeyEventHandler(Functions_KeyUp);

            // Pre-define a simple user-defined function
            LoadFunctions_Click(SaveFunctions, null);

            LoadFunctions.Click += new RoutedEventHandler(LoadFunctions_Click);
            SaveFunctions.Click += new RoutedEventHandler(SaveFunctions_Click);
        }

        void SaveFunctions_Click(object sender, RoutedEventArgs e) {
            SaveFunctions.Content = "Saving ...";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://localhost:35348/Script/save", UriKind.Absolute)); 
            request.Method = "POST"; 
            request.ContentType = "application/x-www-form-urlencoded"; 
            request.BeginGetRequestStream(new AsyncCallback(RequestReady), request);
        }

        void RequestReady(IAsyncResult asyncResult) 
        { 
            HttpWebRequest request = asyncResult.AsyncState as HttpWebRequest; 
            Stream stream = request.EndGetRequestStream(asyncResult); 

            this.Dispatcher.BeginInvoke(delegate() 
            { 
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(string.Format("code={0}", Functions.Text));
                writer.Flush();
                writer.Close();
                request.BeginGetResponse(new AsyncCallback(ResponseReady), request); 
            }); 
        }

        void ResponseReady(IAsyncResult asyncResult) 
        {           
            HttpWebRequest request = asyncResult.AsyncState as HttpWebRequest;
            try {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult);
                this.Dispatcher.BeginInvoke(delegate() {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    string result = reader.ReadToEnd();

                    HtmlPage.Window.Alert("Saved!");
                });
            } catch (Exception e) {
                this.Dispatcher.BeginInvoke(delegate() {
                    HtmlPage.Window.Alert("Error, sorry!");
                });
            }
            SaveFunctions.Content = "Save";
        }

        void LoadFunctions_Click(object sender, RoutedEventArgs e) {
            Functions.Text = "Loading ...";
            WebClient wc = new WebClient(); 
            string sRequest = "/Script";
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            wc.DownloadStringAsync(new Uri(sRequest, UriKind.Relative));
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
            Functions.IsEnabled = false;
            Functions.Text = e.Result;
            Functions_KeyUp(Functions, null);
            Functions.IsEnabled = true;
        }

        // Execute the script code in the Functions buffer, and add buttons to the UI
        // to call the function if it doesn't exist.
        void Functions_KeyUp(object sender, KeyEventArgs e) {
            try {
                object result = _engine.Execute(Functions.Text.ToString());
                FunctionDefinitions.Children.Clear();
                foreach(var method in _engine.ListOfMethods()) {
                    if (!method.StartsWith("__")) {
                        var b = new Button();
                        b.Tag = method;
                        b.Content = method + "(x)";
                        b.Click += new RoutedEventHandler(RunCustomFunction);
                        FunctionDefinitions.Children.Add(b);
                    }
                }
            } catch (Exception _e) {
                // Ignore exception
            }
        }

        // Call a user-defined function
        void RunCustomFunction(object sender, RoutedEventArgs e) {
            try {
                object result = _engine.CallMethod((string)((Button)sender).Tag, (object)Decimal.Parse(Calculator.CurrentNumber.ToString()));
                Calculator.CurrentNumber = new NumberProject.Number(result.ToString());
                Calculator.OnValueChanged();
            } catch (Exception _e) {
                HtmlPage.Window.Alert("The user-defined function returned None");
            }
        }
    }

    public class PythonEngine {
        private ScriptEngine _engine;
        private ScriptScope _scope;

        public PythonEngine() {
            var setup = Microsoft.Scripting.Silverlight.DynamicApplication.CreateRuntimeSetup();
            setup.DebugMode = true;
            var runtime = new ScriptRuntime(setup);
            _engine = Python.GetEngine(runtime);
            _scope = null;
        }

        public object Execute(string code) {
            _scope = _engine.CreateScope();
            return _engine.Execute(code, _scope);
        }

        public ScriptScope Scope { get { return _scope; } }

        public IList<string> ListOfMethods() {
            return _engine.Operations.GetMemberNames(_scope);
        }

        public object CallMethod(string methodName, object argument) {
            return _engine.Operations.InvokeMember(_scope, methodName, new object[] { argument });
        }
    }
}
