using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    public partial class Page
    {
        private bool GameOver;
        private Line WinningLine;
        private int NumMoves;

        public string strBoard { get; set; }

        static readonly string[] patterns = new[]{"(?<1>)(?<2>-)(?<3>OO......)|(?<1>)(?<2>-)(?<3>..O..O..)|(?<1>)(?<2>-)(?<3>...O...O)|(?<1>O)(?<2>-)(?<3>O......)|(?<1>.)(?<2>-)(?<3>..O..O.)", 
            "(?<1>)(?<2>-)(?<3>XX......)|(?<1>)(?<2>-)(?<3>..X..X..)|(?<1>)(?<2>-)(?<3>...X...X)|(?<1>X)(?<2>-)(?<3>X......)|(?<1>.)(?<2>-)(?<3>..X..X.)", 
            "(?<1>-)(?<2>-)(?<3>X-O-X--)|(?<1>)(?<2>-)(?<3>X-XO.-..)|(?<1>)(?<2>-)(?<3>XXO.-..)|(?<1>)(?<2>-)(?<3>-X-O.X..)|(?<1>)(?<2>-)(?<3>...X...-)|(?<1>.)(?<2>-)(?<3>..X...-)", 
            "(?<1>....)(?<2>-)(?<3>....)"};

        public Page()
        {
            InitializeComponent();
            strBoard = "---------";
        }

        private static string RotateString(string s)
        {
            string temp = string.Empty;

            for (int k = 1; k <= 3; k++) for (int j = 1; j <= 3; j++) temp += s[(j * 3) - k].ToString();

            return temp;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MakeUserMove(sender);
        }

        public void MakeUserMove(object sender) {
            if (GameOver) return;

            int position = (3 * int.Parse(((Rectangle)sender).Name[2].ToString())) + int.Parse(((Rectangle)sender).Name[1].ToString());

            if (strBoard[position] != '-') return;

            strBoard = strBoard.Substring(0, position) + "X" + strBoard.Substring(position + 1);

            DrawBoard();
            NumMoves++;

            if (IsThereAWinner()) { return; }

            MakeAiMove();
            DrawBoard();
            NumMoves++;

            if (IsThereAWinner()) { return; }

            WhosMove.Text = "Play!";

            if (NumMoves >= 9) {
                WhosMove.Text = "Cats game!";
                GameOver = true;
            }
        }

        private void DrawBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                if (strBoard[i] == 'X') ((Image) LayoutRoot.FindName("Cross" + i % 3 + i / 3)).Visibility = Visibility.Visible;
                else if (strBoard[i] == 'O')((Image)LayoutRoot.FindName("Ellipse" + i % 3 + i / 3)).Visibility = Visibility.Visible;
            }
            
        }

        private void MakeAiMove()
        {
            WhosMove.Text = "It is O's move";

            bool blnFound = false;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Regex.IsMatch(strBoard, patterns[j]) && blnFound == false)
                    {
                        strBoard = Regex.Replace(strBoard, patterns[j], "${1}O${3}");
                        blnFound = true;                        
                    }
                    strBoard = RotateString(strBoard);
                }
            }

            if (!blnFound) strBoard = Regex.Replace(strBoard, "(?<1>.*?)(?<2>-)(?<3>.*)", "${1}O${3}");
        }

        private bool IsThereAWinner()
        {
            if (Regex.IsMatch(strBoard, @"^(?<char>[XO])\k<char>\k<char>|^(?<char>[XO])...\k<char>...\k<char>|^(?<char>[XO])..\k<char>..\k<char>|" +
                @"^...(?<char>[XO])\k<char>\k<char>|^..(?<char>[XO]).\k<char>.\k<char>|^.(?<char>[XO])..\k<char>..\k<char>|" +
                @"^..(?<char>[XO])..\k<char>..\k<char>|^......(?<char>[XO])\k<char>\k<char>"))
            {
                DrawWinningLine();
                return true;
            }

            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset() {
            foreach (var x in LayoutRoot.Children)
            {
                if (x.GetType().ToString() == "System.Windows.Controls.Image" && x.GetValue(NameProperty).ToString() != "XamlBoard")
                {
                    x.Visibility = Visibility.Collapsed;
                }
            }

            foreach (var x in LayoutRoot.Children)
            {
                if (x.GetType().ToString() == "System.Windows.Shapes.Line")
                {
                    LayoutRoot.Children.Remove(x);
                    break;
                }
            }

            WhosMove.Text = "Play!";
            GameOver = false;
            NumMoves = 0;
            strBoard = "---------";
        }

        private void DrawWinningLine()
        {
            UserControl p = (UserControl)LayoutRoot.Parent;

            string temp = string.Empty;

            int NearPoint = (int) p.Width/6;
            int MidPoint = (int) p.Width/2;
            int FarPoint = ((int) p.Width/6)*5;

            const string s = @"<Line " +
                             "xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' " +
                             "xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' " +
                             "x:name='WinningLine' " +
                             "Stroke='Black' " +
                             "Grid.RowSpan='3' " +
                             "Grid.ColumnSpan='3' " +
                             "StrokeThickness='10' " +
                             "X1='[X1]' Y1='[Y1]' " +
                             "X2='[X2]' Y2='[Y2]' " +
                             "StrokeStartLineCap='Round' StrokeEndLineCap='Round' />";

            if (Regex.IsMatch(strBoard, @"^(?<char>[XO])..\k<char>..\k<char>"))
            {
                temp = s.Replace("[X1]", NearPoint.ToString());
                temp = temp.Replace("[X2]", NearPoint.ToString());
                temp = temp.Replace("[Y1]", NearPoint.ToString());
                temp = temp.Replace("[Y2]", FarPoint.ToString());
            }
            else if (Regex.IsMatch(strBoard, @"^(?<char>[XO])...\k<char>...\k<char>"))
            {
                temp = s.Replace("[X1]", NearPoint.ToString());
                temp = temp.Replace("[X2]", FarPoint.ToString());
                temp = temp.Replace("[Y1]", NearPoint.ToString());
                temp = temp.Replace("[Y2]", FarPoint.ToString());
            }
            else if (Regex.IsMatch(strBoard, @"^(?<char>[XO])\k<char>\k<char>"))
            {
                temp = s.Replace("[X1]", NearPoint.ToString());
                temp = temp.Replace("[X2]", FarPoint.ToString());
                temp = temp.Replace("[Y1]", NearPoint.ToString());
                temp = temp.Replace("[Y2]", NearPoint.ToString());
            }
            else if (Regex.IsMatch(strBoard, @"^.(?<char>[XO])..\k<char>..\k<char>"))
            {
                temp = s.Replace("[X1]", MidPoint.ToString());
                temp = temp.Replace("[X2]", MidPoint.ToString());
                temp = temp.Replace("[Y1]", NearPoint.ToString());
                temp = temp.Replace("[Y2]", FarPoint.ToString());
            }
            else if (Regex.IsMatch(strBoard, @"^..(?<char>[XO])..\k<char>..\k<char>"))
            {
                temp = s.Replace("[X1]", FarPoint.ToString());
                temp = temp.Replace("[X2]", FarPoint.ToString());
                temp = temp.Replace("[Y1]", NearPoint.ToString());
                temp = temp.Replace("[Y2]", FarPoint.ToString());
            }
            else if (Regex.IsMatch(strBoard, @"^...(?<char>[XO])\k<char>\k<char>"))
            {
                temp = s.Replace("[X1]", NearPoint.ToString());
                temp = temp.Replace("[X2]", FarPoint.ToString());
                temp = temp.Replace("[Y1]", MidPoint.ToString());
                temp = temp.Replace("[Y2]", MidPoint.ToString());
            }
            else if (Regex.IsMatch(strBoard, @"^......(?<char>[XO])\k<char>\k<char>"))
            {
                temp = s.Replace("[X1]", NearPoint.ToString());
                temp = temp.Replace("[X2]", FarPoint.ToString());
                temp = temp.Replace("[Y1]", FarPoint.ToString());
                temp = temp.Replace("[Y2]", FarPoint.ToString());
            }
            else if (Regex.IsMatch(strBoard, @"^..(?<char>[XO]).\k<char>.\k<char>"))
            {
                temp = s.Replace("[X1]", FarPoint.ToString());
                temp = temp.Replace("[X2]", NearPoint.ToString());
                temp = temp.Replace("[Y1]", NearPoint.ToString());
                temp = temp.Replace("[Y2]", FarPoint.ToString());
            }
            if (temp != string.Empty)
            {
                GameOver = true;
                strBoard = "---------";               
                WinningLine = (Line)XamlReader.Load(temp);
                LayoutRoot.Children.Add(WinningLine);

                WhosMove.Text = "Game Over!";
            }
        }
    }
}
