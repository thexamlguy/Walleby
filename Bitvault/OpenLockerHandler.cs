using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class OpenLockerHandler(IConfigurationDescriptor<LockerConfiguration> descriptor,
    ISecurityKeyFactory securityKeyFactory,
    ILockerStorageFactory lockerStorageFactory) :
    IHandler<ActivateEventArgs<Locker>, bool>
{
    public async Task<bool> Handle(ActivateEventArgs<Locker> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is Locker locker && descriptor.Name is { Length: > 0 } name && locker.Password is { Length: > 0 } password)
        {
            LockerConfiguration configuration = descriptor.Value;
            if (configuration.Key?.Split(':') is { Length: >= 2 } keyPart)
            {
                byte[]? salt = Convert.FromBase64String(keyPart[0]);
                byte[]? encryptedKey = Convert.FromBase64String(keyPart[1]);

                if (securityKeyFactory.Create(Encoding.UTF8.GetBytes(password), encryptedKey, salt) is SecurityKey key)
                {
                    if (await lockerStorageFactory.Create(name, key))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}