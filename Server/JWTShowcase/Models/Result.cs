namespace JWTShowcase.Models;

using Microsoft.AspNetCore.Mvc;

public class Result
{
    private readonly List<string> _errors;

    internal Result(bool succeeded, List<string> errors)
    {
        Succeeded = succeeded;
        _errors = errors;
    }

    public bool Succeeded { get; }

    public List<string> Errors
        => Succeeded
            ? new List<string>()
            : _errors;

    public static Result Success 
        => new(true, new List<string>());

    public static Result Failure(string error) => error;

    public static Result Failure(IEnumerable<string> errors)
        => new Result(false, errors.ToList());

    public static implicit operator Result(string error)
        => Failure(new List<string> { error });

    public static implicit operator Result(List<string> errors)
        => Failure(errors.ToList());

    public static implicit operator Result(bool success)
        => success ? Success : Failure(new[] { "Unsuccessful operation." });

    public static implicit operator bool(Result result)
        => result.Succeeded;

    public static implicit operator ActionResult(Result result)
        => result.Succeeded ? new OkResult() : new BadRequestObjectResult(result.Errors);
}

public class Result<TData> : Result
{
    private readonly TData _data;

    private Result(bool succeeded, TData data, List<string> errors)
        : base(succeeded, errors)
        => _data = data;

    public TData Data
        => Succeeded
            ? _data
            : throw new InvalidOperationException(
                $"{nameof(Data)} is not available with a failed result. Use {Errors} instead.");

    public static Result<TData> SuccessWith(TData data) 
        => new(true, data, new List<string>());

    public new static Result<TData> Failure(IEnumerable<string> errors) 
        => new(false, default!, errors.ToList());

    public static implicit operator Result<TData>(string error)
        => Failure(new List<string> { error });

    public static implicit operator Result<TData>(List<string> errors)
        => Failure(errors);

    public static implicit operator Result<TData>(TData data)
        => SuccessWith(data);

    public static implicit operator bool(Result<TData> result)
        => result.Succeeded;

    public static implicit operator ActionResult<TData>(Result<TData> result) 
        => result.Succeeded ? result.Data : new BadRequestObjectResult(result.Errors);
}