namespace HoveyTech.Core.Runtime
{
    public sealed class EmptyObject
    {
        private static EmptyObject _instance;
        public static EmptyObject Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EmptyObject();
                return _instance;
            }
        }
    }
}
