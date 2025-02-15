using FluentAssertions;
using FluentValidation;
using theRightDirection.Library.FluentValidation;

namespace theRightDirection.Tests.Library.FluentValidation;

public class ExtensionsTest
{
    [Fact]
    public void CombineErrorMessages()
    {
        var sut = new TestObject();
        sut.Name = string.Empty;
        var validator = new TestObjectValidator();
        var results = validator.Validate(sut);
        var result = results.CombineErrorsToString();
        result.Should().Be("te kort\r\nniet leeg");
    }
}

internal class TestObject
{
    public string Name { get; set; }
}

internal class TestObjectValidator : AbstractValidator<TestObject>
{
    public TestObjectValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("te kort");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("niet leeg");
    }
}
