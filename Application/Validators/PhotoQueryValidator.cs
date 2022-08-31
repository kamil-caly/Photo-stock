using Application.Dto;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class PhotoQueryValidator : AbstractValidator<ItemQuery>
    {
        private int[] allowedPageSizes = { 2, 5, 10, 15 };
        private string[] allowedSortByColumnNames =
            {nameof(Photo.Name), nameof(Photo.Cost), nameof(Photo.NumberOfSales),
            nameof(Photo.Rating) };
        public PhotoQueryValidator()
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
