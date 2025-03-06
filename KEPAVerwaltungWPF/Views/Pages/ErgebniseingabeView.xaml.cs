using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Helper;
using KEPAVerwaltungWPF.ViewModels;
using Size = System.Windows.Size;

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
                //_dragAdorner = new DragAdorner(dataGrid, visual, offsetX: 10, offsetY: 10);
                _dragAdorner = new DragAdorner(dataGrid, visual, -260, -160);
                _adornerLayer.Add(_dragAdorner);
            }
            
            DragDrop.DoDragDrop(dataGrid, dataGrid.SelectedItem, DragDropEffects.Move);
        }
    }

    // private void DataGrid_MouseMove(object sender, MouseEventArgs e)
    // {
    //     if (_dragAdorner != null)
    //     {
    //         var position = e.GetPosition(sender as DataGrid);
    //         _dragAdorner.UpdatePosition(position);
    //     }
    // }
    
    // private void StartDrag()
    // {
    //     var visual = new Rectangle
    //     {
    //         Width = 100,
    //         Height = 100,
    //         Fill = Brushes.LightBlue
    //     };
    //
    //     _adornerLayer = AdornerLayer.GetAdornerLayer(dataGrid);
    //     _dragAdorner = new DragAdorner(dataGrid, visual, offsetX: -10, offsetY: -10);
    //     _adornerLayer.Add(_dragAdorner);
    //
    //     dataGrid.MouseMove += DataGrid_MouseMove;
    // }
    //
    // private void StopDrag()
    // {
    //     if (_dragAdorner != null)
    //     {
    //         _adornerLayer.Remove(_dragAdorner);
    //         _dragAdorner = null;
    //         dataGrid.MouseMove -= DataGrid_MouseMove;
    //     }
    // }
    
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
        // if (e.Data.GetDataPresent(typeof(AktiveSpieler)))
        // {
        //     //var droppedData = e.Data.GetData(typeof(object));
        //     var droppedData = e.Data.GetData(typeof(AktiveSpieler));
        //     var targetDataGrid = sender as DataGrid;
        //
        //     if (droppedData != null && targetDataGrid != null)
        //     {
        //         // Notify ViewModel to handle the data transfer
        //         // var viewModel = DataContext as MeisterschaftenViewModel;
        //         // viewModel?.HandleDrop(droppedData, targetDataGrid.Name);
        //         
        //         ErgebniseingabeViewModel.HandleDrop(droppedData, targetDataGrid.Name);
        //     }
        // }
        
        var targetDataGrid = sender as DataGrid;
        switch (targetDataGrid.Name)
        {
            case "dgAktiveMitglieder":
                switch (ErgebniseingabeViewModel.SelectedSpiel)
                {
                    case "9er/Ratten":
                        var droppedDataNR = e.Data.GetData(typeof(NeunerRatten));
                
                        if (droppedDataNR != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataNR, targetDataGrid.Name);
                        }
                        break;
                    case "6-Tage-Rennen":
                        var droppedData6TR = e.Data.GetData(typeof(Spiel6TageRennen));
                
                        if (droppedData6TR != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedData6TR, targetDataGrid.Name);
                        }
                        break;
                    case "Pokal":
                        var droppedDataPokal = e.Data.GetData(typeof(SpielPokal));
                
                        if (droppedDataPokal != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataPokal, targetDataGrid.Name);
                        }
                        break;
                    case "Sargkegeln":
                        var droppedDataSarg = e.Data.GetData(typeof(SpielSargkegeln));
                
                        if (droppedDataSarg != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataSarg, targetDataGrid.Name);
                        }
                        break;
                    case "Meisterschaft":
                        var droppedDataMeister = e.Data.GetData(typeof(SpielMeisterschaft));
                
                        if (droppedDataMeister != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataMeister, targetDataGrid.Name);
                        }
                        break;
                    case "Blitztunier":
                        var droppedDataBlitz = e.Data.GetData(typeof(SpielBlitztunier));
                
                        if (droppedDataBlitz != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataBlitz, targetDataGrid.Name);
                        }
                        break;
                    case "Kombimeisterschaft":
                        var droppedDataKombi = e.Data.GetData(typeof(SpielKombimeisterschaft));
                
                        if (droppedDataKombi != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataKombi, targetDataGrid.Name);
                        }
                        break;
                }
                break;
            case "dgEingabe":
                var droppedDataAktiveSpieler = e.Data.GetData(typeof(AktiveSpieler));
                
                if (droppedDataAktiveSpieler != null && targetDataGrid != null)
                {
                    // Notify ViewModel to handle the data transfer
                    ErgebniseingabeViewModel.HandleDrop(droppedDataAktiveSpieler, targetDataGrid.Name);
                }
                break;
        }
    }
    
    // private void ChangeColumntypeOfColumnHinRueckrunde(int iColIndex)
    // {
    //     // Create a new DataGridTemplateColumn
    //     DataGridTemplateColumn comboBoxColumn = new DataGridTemplateColumn();
    //     comboBoxColumn.Header = "HinRueckrunde";
    //
    //     // Define the CellTemplate
    //     DataTemplate cellTemplate = new DataTemplate();
    //     FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
    //     textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("HinRueckrunde"));
    //     cellTemplate.VisualTree = textBlockFactory;
    //     comboBoxColumn.CellTemplate = cellTemplate;
    //
    //     // Define the CellEditingTemplate
    //     DataTemplate cellEditingTemplate = new DataTemplate();
    //     FrameworkElementFactory comboBoxFactory = new FrameworkElementFactory(typeof(ComboBox));
    //     
    //     //comboBoxFactory.SetBinding(ComboBox.ItemsSourceProperty, new Binding("CboHinRueckrundeItems"));
    //     Binding itemsSourceBinding = new Binding("CboHinRueckrundeItems")
    //     {
    //         RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(UserControl), 1)
    //     };
    //     comboBoxFactory.SetBinding(ComboBox.ItemsSourceProperty, itemsSourceBinding);
    //     
    //     comboBoxFactory.SetBinding(ComboBox.SelectedValueProperty, new Binding("HinRueckrunde"));
    //     comboBoxFactory.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
    //     comboBoxFactory.SetValue(ComboBox.SelectedValuePathProperty, "Key");
    //     cellEditingTemplate.VisualTree = comboBoxFactory;
    //     comboBoxColumn.CellEditingTemplate = cellEditingTemplate;
    //
    //     // Replace the existing column with the new ComboBox column
    //     DataGridColumn existingColumn = dgEingabe.Columns[iColIndex];
    //     dgEingabe.Columns.Remove(existingColumn);
    //     dgEingabe.Columns.Insert(iColIndex, comboBoxColumn);
    // }
    
    private void CboSpielauswahl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // dgEingabe.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        // dgEingabe.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        
        //Setzen der Datenquelle für das Eingabegrid
        switch (ErgebniseingabeViewModel.SelectedSpiel)
        {
            case "9er/Ratten":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.NeunerRatten;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "6-Tage-Rennen":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.Spiel6TageRennen;
                CboSpielnummer.Visibility = Visibility.Visible;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "Pokal":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielPokal;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "Sargkegeln":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielSargkegeln;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "Meisterschaft":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielMeisterschaft;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                break;
            case "Blitztunier":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielBlitztunier;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                break;
            case "Kombimeisterschaft":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielKombimeisterschaft;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                break;
        }

        //Formatieren des Datagrids
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
                    dgEingabe.Columns[i].MinWidth = 200;
                    break;
                case "Spieler1Name":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    dgEingabe.Columns[i].MinWidth = 200;
                    break;
                case "Spieler2Name":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    dgEingabe.Columns[i].MinWidth = 200;
                    break;
                case "Spielnr":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    dgEingabe.Columns[i].MinWidth = 80;
                    break;
                case "Platz":
                    dgEingabe.Columns[i].IsReadOnly = true;
                    dgEingabe.Columns[i].MinWidth = 80;
                    break;
                case "Neuner":
                    dgEingabe.Columns[i].MinWidth = 90;
                    break;
                case "Ratten":
                    dgEingabe.Columns[i].MinWidth = 90;
                    break;
                case "Runden":
                    dgEingabe.Columns[i].MinWidth = 80;
                    break;
                case "Punkte":
                    dgEingabe.Columns[i].MinWidth = 80;
                    break;
                case "HolzSpieler1":
                    dgEingabe.Columns[i].MinWidth = 120;
                    break;
                case "HolzSpieler2":
                    dgEingabe.Columns[i].MinWidth = 120;
                    break;
                case "HinRueckrunde":
                    //ChangeColumntypeOfColumnHinRueckrunde(i);
                    dgEingabe.Columns[i].IsReadOnly = true;
                    dgEingabe.Columns[i].MinWidth = 120;
                    break;
                case "PunkteSpieler1":
                    dgEingabe.Columns[i].MinWidth = 150;
                    break;
                case "PunkteSpieler2":
                    dgEingabe.Columns[i].MinWidth = 150;
                    break;
                case "Spieler1Punkte3bis8":
                    dgEingabe.Columns[i].MinWidth = 200;
                    break;
                case "Spieler2Punkte3bis8":
                    dgEingabe.Columns[i].MinWidth = 200;
                    break;
                case "Spieler1Punkte5Kugeln":
                    dgEingabe.Columns[i].MinWidth = 220;
                    break;
                case "Spieler2Punkte5Kugeln":
                    dgEingabe.Columns[i].MinWidth = 220;
                    break;
            }
        }
    }
}