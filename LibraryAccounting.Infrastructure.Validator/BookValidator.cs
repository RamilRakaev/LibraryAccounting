using Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Infrastructure.Validator
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Id).NotNull();
            RuleFor(b => b.Title).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
