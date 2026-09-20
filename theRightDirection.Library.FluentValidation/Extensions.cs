using FluentValidation.Results;
using System.Text;

namespace theRightDirection.Library.FluentValidation;

public static partial class Extensions
{
    public static IEnumerable<string> GetErrorMessages(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(x => x.ErrorMessage);
    }
    /// <summary>
    /// small helper method which combines all error messages with the stringbuilder and does appendline, helping
    /// with formatting the error messages in log files
    /// </summary>
    public static string CombineErrorsToString(this ValidationResult validationResult)
    {
        var sb = new StringBuilder();
        validationResult.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));
        return sb.ToString().Trim();
    }

    public static string GetErrorMessage(this List<ValidationFailure> errors)
    {
        var message = new StringBuilder();
        errors.ForEach(x => message.AppendLine(x.ErrorMessage));
        return message.ToString();
    }

    public static string GetErrorMessageFromSetOfResults(this List<ValidationResult> results)
    {
        var sb = new StringBuilder();
        var errorMessages = results.Select(x => x.Errors.GetErrorMessage());
        sb.AppendJoin(";", errorMessages);
        return sb.ToString();
    }

    public static string GetErrorMessageFromSetOfResults(this ValidationResult[] results)
    {
        return GetErrorMessageFromSetOfResults(results.ToList());
    }

    public static bool IsValidFromSetOfResults(this List<ValidationResult> results)
    {
        var isValid = results.Select(x => x.IsValid).ToList();
        return isValid.TrueForAll(x => x);
    }

    public static bool IsValidFromSetOfResults(this ValidationResult[] results)
    {
        return IsValidFromSetOfResults(results.ToList());
    }
}