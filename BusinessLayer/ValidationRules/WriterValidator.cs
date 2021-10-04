using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator:AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı soyadı kısmı boş geçilemez..");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail Adresi Boş Geçilemez..");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Boş Geçilemez..");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın..");
            RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("Lütfen en fazla 50 karakterlik veri girişi yapın..");
            RuleFor(x => x.WriterPassword).Matches(@"[A-Z]+").WithMessage("Şifreniz en az 1 büyük karakter içermelidir..");
            RuleFor(x => x.WriterPassword).Matches(@"[a-z]").WithMessage("Şifreniz en az 1 küçük harf içermelidir...");
            RuleFor(x => x.WriterPassword).Matches(@"[0-9]").WithMessage("Şifrenizde en az 1 tane sayı içermelidir..");


        }
    }
}
