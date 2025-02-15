using FluentValidation.Results;
using System.Text;

namespace theRightDirection.Library.FluentValidation;

public static partial class Extensions
{
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
}