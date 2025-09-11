using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Input.RoyalFeatureTemplate.Scripts.Ports;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Input.RoyalFeatureTemplate.Scripts.Commands
{
    public class InputChangedCommandData
    {
        public readonly IJoystickInputPort _joystickInputPort;

        public InputChangedCommandData(IJoystickInputPort joystickInputPort)
        {
            _joystickInputPort = joystickInputPort;
        }
    }
}