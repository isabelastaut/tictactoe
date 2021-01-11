using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Personal.TicTacToe
{
    public partial class MainWindow : Window
    {
        #region Private Members

        private MarkType[] Results;

        private bool Player1Turn;

        private bool GameEnded;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            NewGame(); 
        }
        #endregion

        private void NewGame() /// creates a new game and clears all existing values
        {
            Results = new MarkType[9];

            for (var i = 0; 1 < Results.Length; i++) { Results[i] = MarkType.Free; }

            Player1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.LightSalmon;
            });

            GameEnded = false;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (GameEnded) /// starts a new game after one finishes
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);
            if (Results[index] != MarkType.Free) { return; }
            
            Results[index] = Player1Turn ? MarkType.Cross: MarkType.Nought;
            button.Content = Player1Turn ? "X" : "O";
            if (!Player1Turn) { button.Foreground = Brushes.PaleVioletRed; }

            Player1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            #region Horizontal wins

            if (Results[0] != MarkType.Free && (Results[0] & Results[1] & Results[2]) == Results[0])
            {
                GameEnded = true;
                Button00.Background = Button10.Background = Button20.Background = Brushes.Gainsboro;
            }

            else if (Results[3] != MarkType.Free && (Results[3] & Results[4] & Results[5]) == Results[3])
            {
                GameEnded = true;
                Button01.Background = Button11.Background = Button21.Background = Brushes.Gainsboro;
            }

            else if (Results[6] != MarkType.Free && (Results[6] & Results[7] & Results[8]) == Results[6])
            {
                GameEnded = true;
                Button02.Background = Button12.Background = Button22.Background = Brushes.Gainsboro;
            }
            #endregion

            #region Vertical wins

            else if (Results[0] != MarkType.Free && (Results[0] & Results[3] & Results[6]) == Results[0])
            {
                GameEnded = true;
                Button00.Background = Button01.Background = Button02.Background = Brushes.Gainsboro;
            }

            else if (Results[1] != MarkType.Free && (Results[1] & Results[4] & Results[7]) == Results[1])
            {
                GameEnded = true;
                Button10.Background = Button11.Background = Button12.Background = Brushes.Gainsboro;
            }

            else if (Results[2] != MarkType.Free && (Results[2] & Results[5] & Results[8]) == Results[2])
            {
                GameEnded = true;
                Button20.Background = Button21.Background = Button22.Background = Brushes.Gainsboro;
            }
            #endregion

            #region Diagonal wins

            else if (Results[0] != MarkType.Free && (Results[0] & Results[4] & Results[8]) == Results[0])
            {
                GameEnded = true;
                Button00.Background = Button11.Background = Button22.Background = Brushes.Gainsboro;
            }

            else if (Results[2] != MarkType.Free && (Results[2] & Results[4] & Results[6]) == Results[0])
            {
                GameEnded = true;
                Button20.Background = Button11.Background = Button02.Background = Brushes.Gainsboro;
            }
            #endregion

            #region No winners

            else if (!Results.Any(f => f == MarkType.Free))
            {
                GameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Black;
                });
            }
            #endregion
        }
    }
}