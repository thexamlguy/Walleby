namespace Bitvault;

public record VaultKey(byte[] Salt, byte[] Public, byte[] Private);
