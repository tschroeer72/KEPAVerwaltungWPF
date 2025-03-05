using KEPAVerwaltungWPF.DTOs;

namespace KEPAVerwaltungWPF.Services;

public class CommonService
{
    public event Action? OnChange;
    private AktiveMeisterschaft _aktiveMeisterschaft = new();

    public AktiveMeisterschaft AktiveMeisterschaft
    {
        get => _aktiveMeisterschaft;
        set
        {
            _aktiveMeisterschaft = value;
            OnChange?.Invoke();
        }
    }
}