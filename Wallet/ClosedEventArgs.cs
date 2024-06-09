namespace Wallet;

public record ClosedEventArgs<TValue>(TValue? Value = default);