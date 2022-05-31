using BinanceTrackerDesktop.Validators.String.Exceptions;

namespace BinanceTrackerDesktop.Validators.String;

public sealed class StringValidator : IStringValidator
{
    private readonly string content;



    private bool success;



    public StringValidator(string content)
    {
        this.content = content;
        success = true;
    }



    public bool IsSuccess => success == true;

    public bool IsFailed => IsSuccess == false;



    public IStringValidator MinCharacters(int count)
    {
        if (content.Length < count)
        {
            success = false;
        }

        return this;
    }

    public IStringValidator MaxCharacters(int count)
    {
        if (content.Length > count)
        {
            success = false;
        }

        return this;
    }

    public IStringValidator ContentNotNullOrWhiteSpace()
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            success = false;
        }

        return this;
    }

    public IStringValidator ThrowIfFailed(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        ThrowIfFailed(type.Name);

        return this;
    }

    public IStringValidator ThrowIfFailed(string message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        return IsFailed 
            ? ThrowIfFailed(new FailedStringValidationException(message)) 
            : this;
    }

    public IStringValidator ThrowIfFailed(Exception exception)
    {
        if (exception == null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        return IsFailed 
            ? throw exception 
            : (IStringValidator)this;
    }
}
