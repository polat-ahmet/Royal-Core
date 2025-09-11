using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services;

namespace RoyalCoreDomain.Scripts.Services.Logger
{
    public interface ILogger : IService
    {
        void Debug(string m);
        void Info(string m);
        void Warn(string m);
        void Error(string m);
    }
}