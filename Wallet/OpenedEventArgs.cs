namespace Wallet;

public record OpenedEventArgs<TValue>(TValue? Value = default);