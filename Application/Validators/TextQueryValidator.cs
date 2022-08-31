using Application.Dto;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class TextQueryValidator : AbstractValidator<ItemQuery>
    {
        private int[] allowedPageSizes = { 2, 5, 10, 15 };
        private string[] allowedSortByColumnNames =
            {nameof(Text.Name), nameof(Text.Cost), nameof(Text.NumberOfSales),
            nameof(Text.Rating) };
        public TextQueryValidator()
        {
            RuleFor(a => a.pageNumber).GreaterThan(0);
            RuleFor(a => a.pageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });

            RuleFor(a => a.SortBy).Custom((value, context) =>
            {
                if (!allowedSortByColumnNames.Contains(value) && !string.IsNullOrEmpty(value))
                {
                    context.AddFailure("SortBy", $"Sort by must be empty or contains one of values: [{string.Join(",", allowedSortByColumnNames)}]");
                }
            });
        }
    }
}
