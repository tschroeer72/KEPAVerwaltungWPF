using FluentValidation;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.Validations;

public class MeisterschaftAktualisierenValidation : AbstractValidator<Meisterschaftsdaten>
{
    private readonly DBService _dbService;

    public MeisterschaftAktualisierenValidation(DBService dbService)
    {
        _dbService = dbService;
        _ = InitialRulesAsync();
    }
    
    private async Task InitialRulesAsync()
    {
        var meisterschaftstypenListe = await _dbService.GetMeisterschaftstypenAsync();
        
        RuleFor(meisterschaft => meisterschaft.Bezeichnung)
            .NotEmpty().WithMessage("Bezeichnung darf nicht leer sein")
            .MaximumLength(50).WithMessage("Bezeichnung darf maximal 50 Zeichen lang sein");
        RuleFor(meisterschaft => meisterschaft.Beginn)
            .NotEmpty().WithMessage("Beginn darf nicht leer sein");
        RuleFor(meisterschaft => meisterschaft.Ende)
            .GreaterThan(gt => gt.Beginn).WithMessage("Ende muss nach Beginn liegen");
        
        RuleFor(meisterschaft => meisterschaft.MeisterschaftstypID)
            .Must(id => meisterschaftstypenListe.Select(mt => mt.ID).Contains(id))
            .WithMessage("Ungültiger Meisterschaftstyp");
    }
}