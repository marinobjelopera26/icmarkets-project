namespace ICM.Crypto.SharedKernel.Mediator;

/// <summary>
/// Represents a command that does not generate any result.
/// </summary>
public interface ICommand;

/// <summary>
/// Represents a command that generates a result of <typeparamref name="TResult"/> type.
/// </summary>
/// <typeparam name="TResult">Result type.</typeparam>
public interface ICommand<TResult>;