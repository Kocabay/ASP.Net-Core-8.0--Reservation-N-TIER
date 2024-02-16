using DTOLayer.DTOs.AnouncementDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.AnouncementValidatonRules
{
    public class AnouncementUpdateValidator:AbstractValidator<AnouncementUpdateDto>

    {
        public AnouncementUpdateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Lütfen Başlığı Boş Geçmeyiniz.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Lütfen Duyuruyu Boş Geçmeyiniz.");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Lütfen Başlık En Az 5 Karakter Giriniz.");
            RuleFor(x => x.Content).MinimumLength(20).WithMessage("Lütfen İçerik En Az 20 Karakter Giriniz.");
            RuleFor(x => x.Title).MaximumLength(15).WithMessage("Lütfen Başlık En Fazla 15 Karakter Giriniz.");
            RuleFor(x => x.Content).MaximumLength(500).WithMessage("Lütfen İçerik En Fazla 500 Karakter Giriniz.");
        }
    }
}
