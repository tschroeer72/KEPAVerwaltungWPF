using System.Windows.Controls;

namespace KEPAVerwaltungWPF.UserControls;

public partial class AnimatedContentControl : UserControl
{
    public AnimatedContentControl()
    {
        InitializeComponent();
    }
    
    public void ShowPage(UserControl ucPage)
    {
        PagePlace.Content = null;
        PagePlace.Content = ucPage;

        MetroTabItem.IsSelected = false;
        MetroTabItem.IsSelected = true;
    }
}