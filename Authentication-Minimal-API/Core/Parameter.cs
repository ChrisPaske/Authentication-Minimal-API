namespace Authentication_Minimal_API.Core;

/// <summary>
///     Methods for performing common operations on parameters
/// </summary>
public static class Parameter
{
    public static void NullCheck(object? param, string paramName)
    {
        if (param is null)
            throw new ArgumentNullException(paramName);
    }
}