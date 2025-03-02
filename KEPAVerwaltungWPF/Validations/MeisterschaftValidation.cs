using FluentValidation;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.Validations;

public class MeisterschaftValidation : AbstractValidator<Meisterschaftsdaten>
{
    private readonly DBService _dbService;

    public MeisterschaftValidation(DBService dbService)
    {
        _dbService = dbService;
        _ = InitialRulesAsync();
    }

    private async Task InitialRulesAsync()
    {
        var meisterschaftstypenListe = await _dbService.GetMeisterschaftstypenAsync();
        var validIds = meisterschaftstypenListe.Select(mt => mt.ID).ToList();
        
        RuleFor(meisterschaft => meisterschaft.Bezeichnung)
            .NotEmpty().WithMessage("Bezeichnung darf nicht leer sein")
            .MaximumLength(50).WithMessage("Bezeichnung darf maximal 50 Zeichen lang sein");
        RuleFor(meisterschaft => meisterschaft.Beginn)
            .NotEmpty().WithMessage("Beginn darf nicht leer sein");
        RuleFor(meisterschaft => meisterschaft.Ende)
            .GreaterThan(gt => gt.Beginn).WithMessage("Ende muss nach Beginn liegen");
        
        // RuleFor(meisterschaft => meisterschaft.MeisterschaftstypID)
        //     .Must(id => meisterschaftstypenListe.Select(mt => mt.ID).Contains(id))
        //     .WithMessage("Ungültiger Meisterschaftstyp");
        RuleFor(meisterschaft => meisterschaft.MeisterschaftstypID)
            .Must(id => validIds.Contains(id))
            .WithMessage("Ungültiger Meisterschaftstyp");
    }
}