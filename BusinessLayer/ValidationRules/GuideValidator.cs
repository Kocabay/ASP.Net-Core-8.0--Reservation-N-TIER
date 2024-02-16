using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class GuideValidator : AbstractValidator<Guide>
    {
        public GuideValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen Rehber Adını Giriniz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Lütfen Rehber Açıklaması Giriniz.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Lütfen Rehber Görselini Giriniz.");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Lütfen 30 Karakterden fazla girmeyiniz..");
        }
    }
}
