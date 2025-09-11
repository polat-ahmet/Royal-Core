using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Command
{
    public class CommandFactory : ICommandFactory
    {
        private readonly FeatureContext _context;

        public CommandFactory(FeatureContext context)
        {
            _context = context;
        }

        public TCommand CreateCommandVoid<TCommand>() where TCommand : ICommandVoid, new()
        {
            var command = new TCommand();
            command.SetContext(_context);
            command.ResolveDependencies();
            return command;
        }
    }
}