using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Helper;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class ErgebniseingabeView : UserControl
{
    private AdornerLayer _adornerLayer;
    private DragAdorner _dragAdorner;
    
    public ErgebniseingabeViewModel ErgebniseingabeViewModel { get; }

    public ErgebniseingabeView(ErgebniseingabeViewModel ergebniseingabeViewModel)
    {
        ErgebniseingabeViewModel = ergebniseingabeViewModel;
        InitializeComponent();
        DataContext = ErgebniseingabeViewModel;
        ErgebniseingabeViewModel.InitBaseViewModelDelegateAndEvents();
    }

    private UIElement CreateDragVisual(object item)
    {
        // Erstelle eine UI-Darstellung des Elements (zum Beispiel mithilfe eines DataTemplates)
        var contentPresenter = new ContentPresenter
        {
            Content = item,
            ContentTemplate = FindResource("DragVisualTemplate") as DataTemplate
        };

        contentPresenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        contentPresenter.Arrange(new Rect(contentPresenter.DesiredSize));

        return contentPresenter;
    }
    
    private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Initiate Drag-and-Drop
        var dataGrid = sender as DataGrid;
        var ht = dataGrid.InputHitTest(e.GetPosition(dataGrid));
        
        if (dataGrid?.SelectedItem != null)
        {
            // Erstelle ein visuelles Element für den Adorner
            var visual = CreateDragVisual(dataGrid.SelectedItem);

            // Adorner-Layer abrufen
            _adornerLayer = AdornerLayer.GetAdornerLayer(dataGrid);
            if (_adornerLayer != null)
            {
                _dragAdorner = new DragAdorner(dataGrid, visual);
                _adornerLayer.Add(_dragAdorner);
            }
            
            DragDrop.DoDragDrop(dataGrid, dataGrid.SelectedItem, DragDropEffects.Move);
        }
    }

    private void DataGrid_DragEnter(object sender, DragEventArgs e)
    {
        
        // Allow move effects
        e.Effects = DragDropEffects.Move;
    }

    private void DataGrid_DragOver(object sender, DragEventArgs e)
    {
        if (_dragAdorner != null)
        {
            // Adorner-Position aktualisieren
            var position = e.GetPosition(_adornerLayer);
            _dragAdorner.UpdatePosition(position);
        }
    }
    
    private void DataGrid_Drop(object sender, DragEventArgs e)
    {
        // Adorner entfernen
        if (_adornerLayer != null && _dragAdorner != null)
        {
            _adornerLayer.Remove(_dragAdorner);
            _adornerLayer = null;
            _dragAdorner = null;
        }
        
        // Handle the drop
        if (e.Data.GetDataPresent(typeof(AktiveSpieler)))
        {
            //var droppedData = e.Data.GetData(typeof(object));
            var droppedData = e.Data.GetData(typeof(AktiveSpieler));
            var targetDataGrid = sender as DataGrid;

            if (droppedData != null && targetDataGrid != null)
            {
                // Notify ViewModel to handle the data transfer
                // var viewModel = DataContext as MeisterschaftenViewModel;
                // viewModel?.HandleDrop(droppedData, targetDataGrid.Name);
                
                ErgebniseingabeViewModel.HandleDrop(droppedData, targetDataGrid.Name);
            }
        }
    }
    
    private void CboSpielauswahl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (ErgebniseingabeViewModel.SelectedSpiel)
        {
            case "9er/Ratten":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.NeunerRatten;
                break;
            case "6-Tage-Rennen":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.Spiel6TageRennen;
                break;
            default:
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.AktiveMitglieder;
                break;
        }

        for (int i = 0; i < dgEingabe.Columns.Count; i++)
        {
            switch (dgEingabe.Columns[i].Header)
            {
                case "ID":
                    dgEingabe.Columns[i].Visibility = Visibility.Hidden;
                    break;
                case "SpieltagID":
                    dgEingabe.Columns[i].Visibility = Visibility.Hidden;
                    break;
                case "SpielerID":
                    dgEingabe.Columns[i].Visibility = Visibility.Hidden;
                    break;
                case "Spieler1ID":
                    dgEingabe.Columns[i].Visibility = Visibility.Hidden;
                    break;
                case "Spieler2ID":
                    dgEingabe.Columns[i].Visibility = Visibility.Hidden;
                    break;
                case "Spielername":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    break;
                case "Spieler1Name":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    break;
                case "Spieler2Name":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    break;
                case "Spielnummer":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    break;
                case "Platz":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    break;
            }
        }
    }
}