using Toolkit.Foundation;

namespace Bitvault;

public class VaultStorageConnection(string connection)
{
    private readonly string connection = connection;

    public override string ToString() => connection;
}

public class VaultInitializer(IVaultKeyFactory keyVaultFactory,
    IContainer<VaultKey> vaultKeyContainer,
    IVaultStorage vaultStorage,
    IWritableConfiguration<VaultConfiguration> configuration) :
    IVaultInitializer
{
    public async Task<bool> Initialize(string name,
        string password)
    {
        VaultKey key = keyVaultFactory.Create(password);
        vaultKeyContainer.Set(key);

        if (await vaultStorage.CreateAsync(name, key))
        {
            configuration.Write(args => args.Key = $"{Convert.ToBase64String(key.Phrase)}:{Convert.ToBase64String(key.Public)}");
            return true;
        }

        return false;
    }
}
