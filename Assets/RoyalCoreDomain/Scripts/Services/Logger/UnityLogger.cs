namespace RoyalCoreDomain.Scripts.Services.Logger
{
    public sealed class UnityLogger : ILogger
    {
        public void Debug(string m)
        {
            UnityEngine.Debug.Log(m);
        }

        public void Info(string m)
        {
            UnityEngine.Debug.Log($"<color=#8BC34A>[INFO]</color> {m}");
        }

        public void Warn(string m)
        {
            UnityEngine.Debug.LogWarning(m);
        }

        public void Error(string m)
        {
            UnityEngine.Debug.LogError(m);
        }
    }
}