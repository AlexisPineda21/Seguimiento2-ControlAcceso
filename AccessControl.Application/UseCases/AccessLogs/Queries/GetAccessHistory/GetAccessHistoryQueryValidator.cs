using FluentValidation;

namespace AccessControl.Application.UseCases.AccessLogs.Queries.GetAccessHistory
{
    public sealed class GetAccessHistoryQueryValidator : AbstractValidator<GetAccessHistoryQuery>
    {
        public GetAccessHistoryQueryValidator()
        {
            RuleFor(query => query.Result)
                .IsInEnum().WithMessage("El resultado debe ser Authorized o Rejected.")
                .When(query => query.Result.HasValue);

            RuleFor(query => query.ToDate)
                .GreaterThanOrEqualTo(query => query.FromDate)
                .WithMessage("La fecha final no puede ser anterior a la fecha inicial.")
                .When(query => query.FromDate.HasValue && query.ToDate.HasValue);
        }
    }
}
