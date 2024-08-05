
namespace genegames.Services
{
    public class GameStateService
    {
        public GameState CurrentState { get; private set; } = GameState.NotStarted;
        private Timer _timer;
        public int _elapsedSeconds { get; private set; }
        public event Action OnTimerChanged;
        public event Action OnGameStateChanged;

        public string GameTimer => TimeSpan.FromSeconds(_elapsedSeconds).ToString(@"mm\:ss");

        public void StartNewGame()
        {
            CurrentState = GameState.Running;

            _timer = new Timer(UpdateTimer, null, 0, 1000);
            OnGameStateChanged?.Invoke();
        }

        public void NewGame()
        {
            CurrentState = GameState.NotStarted;
            _elapsedSeconds = 0;
            _timer?.Dispose();
            OnGameStateChanged?.Invoke();
            OnTimerChanged?.Invoke();
        }

        public void SetGameState(GameState newState)
        {
            CurrentState = newState;
            if(CurrentState == GameState.Running)
            {
                OnTimerChanged?.Invoke();
            }
            OnGameStateChanged?.Invoke();
        }


        public void PauseResumeGame()
        {
            if (CurrentState == GameState.Running)
            {
                CurrentState = GameState.Paused;
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            else if (CurrentState == GameState.Paused)
            {
                CurrentState = GameState.Running;
                _timer.Change(0, 1000);
            }
            OnTimerChanged?.Invoke();
            OnGameStateChanged?.Invoke();
        }

        private void UpdateTimer(object state)
        {
            if (CurrentState == GameState.Running)
            {
                _elapsedSeconds++;
                OnTimerChanged?.Invoke();
            }
        }
        public enum GameState
        {
            NotStarted,
            Running,
            Paused,
            Finished,
            GameOver
        }
    }

}
