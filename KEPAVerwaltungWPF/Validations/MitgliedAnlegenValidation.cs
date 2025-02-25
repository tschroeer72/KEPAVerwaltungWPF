using FluentValidation;
using KEPAVerwaltungWPF.DTOs;

namespace KEPAVerwaltungWPF.Validations;

public class MitgliedAnlegenValidation : AbstractValidator<MitgliedDetails>
{
    public MitgliedAnlegenValidation()
    {
        RuleFor(mitglied => mitglied.Anrede).MaximumLength(50).WithMessage("Anrede darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.Vorname)
            .NotEmpty().WithMessage("Vorname darf nicht leer sein")
            .MaximumLength(50).WithMessage("Vorname darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.Nachname)
            .NotEmpty().WithMessage("Nachname darf nicht leer sein")
            .MaximumLength(50).WithMessage("Nachname darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.Spitzname).MaximumLength(50).WithMessage("Spitzname darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.Straße).MaximumLength(50).WithMessage("Straße darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.Ort).MaximumLength(50).WithMessage("Ort darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.PLZ)
            .MinimumLength(5).WithMessage("PLZ muss 5 Zeichen lang sein")
            .MaximumLength(5).WithMessage("PLZ muss 5 Zeichen lang sein");
        RuleFor(mitglied => mitglied.EMail)
            .MaximumLength(100).WithMessage("EMail darf maximal 100 Zeichen lang sein")
            .EmailAddress().WithMessage("EMail muss eine gültige EMail-Adresse sein");
        RuleFor(mitglied => mitglied.Fax).MaximumLength(50).WithMessage("Fax darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.TelefonFirma).MaximumLength(50).WithMessage("TelefonFirma darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.TelefonMobil).MaximumLength(50).WithMessage("TelefonMobil darf maximal 50 Zeichen lang sein");
        RuleFor(mitglied => mitglied.TelefonPrivat).MaximumLength(50).WithMessage("TelefonPrivat darf maximal 50 Zeichen lang sein");
        
        RuleFor(mitglied => mitglied.Geburtsdatum)
            .NotEmpty().WithMessage("Geburtsdatum darf nicht leer sein")
            .LessThan(lt => lt.MitgliedSeit).WithMessage("Geburtsdatum muss vor MitgliedSeit liegen");
        RuleFor(mitglied => mitglied.MitgliedSeit)
            .NotNull().WithMessage("MitgliedSeit darf nicht leer sein")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("MitgliedSeit darf nicht in der Zukunft liegen");
    }
}