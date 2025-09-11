namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Command
{
    public interface ICommandFactory
    {
        TCommand CreateCommandVoid<TCommand>() where TCommand : ICommandVoid, new();
    }
}