using GameProject.Models.Characters;
using GameProject.Models.Menu;
using GameProject.Views.Control;
using GameProject.Views.Text;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace GameProject.Controllers
{
    internal class GameplayController
    {
        //zmienne ogólne
        Player _player;
        Enemy _enemy;
        int _modeGame;
        int _countdown;
        int _pointsAwarded;
        int _timeEnemyPosition;
        AreaView _areaView;
        CharactersView _charactersView;
        int[] tab;
        int _pointsBefore;

        //zmienne od wątków
        Thread threadTime;
        Thread threadShooting;
        Thread threadPoints;
        Thread threadEnemy;
        Thread threadPlayerMoveLeftRight;

        //mutexy
        Mutex mutex;

        //zmiena warunkowa
        bool condition = false;

        //sygnał
        AutoResetEvent signal = new AutoResetEvent(false);
        AutoResetEvent signalPoints = new AutoResetEvent(false);
        ManualResetEvent signalPositionEnemy = new ManualResetEvent(false);

        TaskCompletionSource<int> waitForPositionEnemy;
        public GameplayController(Player player, int modeGame)
        {
            _player = player;
            _modeGame = modeGame;
            switch (_modeGame)
            {
                case 0:
                    _pointsAwarded = 10;
                    break;
                case 1:
                    _pointsAwarded = 5;
                    break;
                case 2:
                    _pointsAwarded = 1;
                    break;
            }
            
            tab = new int[5] { 0, 0, 0, 0, 0 };
            _enemy = new Enemy();
            mutex = new Mutex();

            _charactersView = new CharactersView();
            _areaView = new AreaView(); 
        }

        private void SelectTimeForModeGame()
        {
            switch (_modeGame)
            {
                case 0:
                    _countdown = 180;
                    _timeEnemyPosition = 11500;
                    break;
                case 1:
                    _countdown = 120;
                    _timeEnemyPosition = 8500;
                    break;
                case 2:
                    _countdown = 60;
                    _timeEnemyPosition = 4500;
                    break;
            }
        }

        private void WaitForThreads()
        {
            switch (_modeGame)
            {
                case 0:
                    Thread.Sleep(180000);
                    break;
                case 1:
                    Thread.Sleep(120000);
                    break;
                case 2:
                    Thread.Sleep(60000);
                    break;
            }
        }

        private void TimeGame(CancellationToken cancellation, int countdown)
        {
            while (!cancellation.IsCancellationRequested && countdown > 0)
            {
                mutex.WaitOne();
                _areaView.SetTimeOnBar(_player, countdown);
                countdown--;
                mutex.ReleaseMutex();

                Thread.Sleep(1000);
            }
        }

        private void PointsGame(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                signalPositionEnemy.WaitOne();  
                signalPoints.WaitOne();
                mutex.WaitOne();
                int positionCartridge = _player.Position.X;
                if (positionCartridge == _enemy.Position.X && tab[0] == 0)
                {
                    _player = _areaView.SetPointsOnBar(_player, _pointsAwarded);
                    tab[0] = 1;
                }
                else if (positionCartridge == _enemy.Position.X - 1 && tab[1] == 0)
                {
                    _player = _areaView.SetPointsOnBar(_player, _pointsAwarded);
                    tab[1] = 1;
                }
                else if (positionCartridge == _enemy.Position.X - 2 && tab[2] == 0)
                {
                    _player = _areaView.SetPointsOnBar(_player, _pointsAwarded);
                    tab[2] = 1;
                }
                else if (positionCartridge == _enemy.Position.X + 1 && tab[3] == 0)
                {
                    _player = _areaView.SetPointsOnBar(_player, _pointsAwarded);
                    tab[3] = 1;
                }
                else if (positionCartridge == _enemy.Position.X + 2 && tab[4] == 0)
                {
                    _player = _areaView.SetPointsOnBar(_player, _pointsAwarded);
                    tab[4] = 1;
                }
                mutex.ReleaseMutex();
            }
        }

        private void GenerateEnemy(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                mutex.WaitOne();

                for (int i = 0; i < 5; i++)
                {
                    tab[i] = 0;
                }
                _enemy.ClearIconEnemy(_enemy.Position.X, _enemy.Position.Y);

                signalPositionEnemy.Reset();
                Random random = new Random();
                int randomNumber = random.Next(2, Console.WindowWidth - 10);

                _enemy.Position.X = randomNumber;

                _enemy.PrintIconEnemy(_enemy.Position.X, _enemy.Position.Y);
                signalPositionEnemy.Set();
                mutex.ReleaseMutex();

                Thread.Sleep(_timeEnemyPosition);
            }
        }

        private void PlayerMoveLeftRight(CancellationToken cancellation)
        {
            ConsoleKey key;

            _player.PrintIconPlayer(_player.Position.X, _player.Position.Y);

            while (!cancellation.IsCancellationRequested)
            {
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.LeftArrow)
                {
                    if (_player.Position.X > 2)
                        _player = _charactersView.MoveLeft(_player);     
                }
                if (key == ConsoleKey.RightArrow)
                {
                    if (_player.Position.X < Console.WindowWidth - 8)
                        _player = _charactersView.MoveRight(_player);
                }

                if (key == ConsoleKey.Spacebar)
                {
                    mutex.WaitOne();
                    signal.Set();
                    mutex.ReleaseMutex();
                }
            }
        }

        private void Shooting(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                signal.WaitOne();
                
                mutex.WaitOne();
                waitForPositionEnemy = new TaskCompletionSource<int>();
                _charactersView.ShootingAnimation(_player.Position.X, _player.Position.Y);
                signalPoints.Set();
                mutex.ReleaseMutex();
            }
        }
        public void Play()
        {
            TextView textView = new TextView();

            CancellationTokenSource threadCancellation = new CancellationTokenSource();
            CancellationToken cancellation = threadCancellation.Token;

            threadTime = new Thread(() => TimeGame(cancellation, _countdown));
            threadPlayerMoveLeftRight = new Thread(() => PlayerMoveLeftRight(cancellation));
            threadShooting = new Thread(() => Shooting(cancellation));
            threadEnemy = new Thread(() => GenerateEnemy(cancellation));
            threadPoints = new Thread(() => PointsGame(cancellation));

            _pointsBefore = _player.CurrentPoints;
            SelectTimeForModeGame();

            Console.Clear();

            _areaView.DrawGameplayPanel(_player);

            threadTime.Start();
            threadPlayerMoveLeftRight.Start();
            threadShooting.Start();
            threadEnemy.Start();
            threadPoints.Start();

            WaitForThreads();

            threadCancellation.Cancel();

            Console.Clear();

            
            textView.Center("YOU SCORED " + _player.CurrentPoints + " POINTS SO FAR",  12);
           
            if (_player.CurrentPoints > 0)
            {
                int numberLevel = CountLevel();
                _player.Level = _player.Level + numberLevel;
                if (_player.Level > 0)
                    textView.Center("YOU HAVE REACHED LEVEL " + _player.Level, 14);
            }

            Thread.Sleep(10000);
            Console.Clear();

            MainMenu mainMenu = MainMenu.GetInstance;
            mainMenu.UpdatePlayer(_player);
            mainMenu.Run();
        }

        private int CountLevel()
        {
            int counter = 0;
            int starter = _pointsBefore;
            int ender = _player.CurrentPoints;

            for (int i = starter; i < ender; i += 50)
                counter++;

            return counter - 1;
        }
    }
}
