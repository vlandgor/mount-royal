using Utilities;

namespace Core.Bootstrapper
{
    public class Bootstrapper : Singleton<Bootstrapper>
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void StartNewGame()
        {
            
        }
        
        public void LoadGame()
        {
            
        }
    }
}
