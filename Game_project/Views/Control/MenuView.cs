using GameProject.Views.Text;

namespace GameProject.Views.Control
{
    internal class MenuView
    {
        private int index;

        private TextView _textView;

        private AreaView _areaView;

        public MenuView()
        {
            _textView = new TextView();
            _areaView = new AreaView();
        }

        private void SelectChoice(List<string> choices, ConsoleKey key)
        {
            int positionCursorY = 10;

            Console.ForegroundColor = ConsoleColor.Green;
            _textView.Center(choices[index], positionCursorY + index);

            if (key == ConsoleKey.DownArrow && index > 0) 
            {
                Console.ForegroundColor = ConsoleColor.White;
                _textView.Center(choices[index - 1], positionCursorY + index - 1);
            }
            else if (key == ConsoleKey.UpArrow && index < choices.Count - 1) 
            {
                Console.ForegroundColor = ConsoleColor.White;
                _textView.Center(choices[index + 1], positionCursorY + index + 1);
            }

            Console.ResetColor();
        }

        private void SelectPanel(List<string> choices, int panel)
        {
            switch (panel)
            {
                case 0:
                   _areaView.DrawMenuPanel(choices);
                    break;
                case 1:
                    _areaView.DrawSettingsPanel(choices);
                    break;
            }
        }
        public int MoveUpDownOnList(List<string> choices, int panel)
        {
            _areaView.DrawFrame();
            SelectPanel(choices, panel);
            index = 0;

            ConsoleKey key = ConsoleKey.DownArrow;
            int numberChoice = 0;

            do
            {
                if (numberChoice > 0)
                {
                    SelectChoice(choices, key);
                    key = Console.ReadKey().Key;
                    

                    if (index <= 0)
                        index = 0;
                    else if(index > choices.Count - 1)
                        index = choices.Count - 1;

                    if (index < choices.Count - 1 && key == ConsoleKey.DownArrow)
                        index++;
                    else if (index > 0 && key == ConsoleKey.UpArrow)
                        index--;
                }
                numberChoice++;
                

            } while (key != ConsoleKey.Enter);

            return index;
        }

        private void SelectChoiceTemplate<T>(List<T> choices, ConsoleKey key)
        {
            int positionCursorY = 10;

            Console.ForegroundColor = ConsoleColor.Green;
            _textView.Center(choices[index].ToString(), positionCursorY + index);

            if (key == ConsoleKey.DownArrow && index > 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                _textView.Center(choices[index - 1].ToString(), positionCursorY + index - 1);
            }
            else if (key == ConsoleKey.UpArrow && index < choices.Count - 1)
            {
                Console.ForegroundColor = ConsoleColor.White;
                _textView.Center(choices[index + 1].ToString(), positionCursorY + index + 1);
            }

            Console.ResetColor();
        }

        public int MoveUpDownOnListTemplate<T>(List<T> items)
        {
            ConsoleKey key = ConsoleKey.DownArrow;
            int numberChoice = 0;
            int positionCursorY = 10;
            index = 0;

            foreach (T item in items)
                _textView.Center(item.ToString(), positionCursorY++);

            do
            {
                if (numberChoice > 0)
                {
                    SelectChoiceTemplate(items, key);

                    key = Console.ReadKey().Key;

                    if (index <= 0)
                        index = 0;
                    else if (index > items.Count - 1)
                        index = items.Count - 1;

                    if (index < items.Count - 1 && key == ConsoleKey.DownArrow)
                        index++;
                    else if (index > 0 && key == ConsoleKey.UpArrow)
                        index--;
                }
                numberChoice++;

            } while (key != ConsoleKey.Enter);

            return index;
        }
    }
}
