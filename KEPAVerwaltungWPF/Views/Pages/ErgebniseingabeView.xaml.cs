using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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

    private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is DataGrid dataGridL && dataGridL.Name == "dgAktiveMitglieder" &&
            dataGridL.SelectedItem != null)
        {
            // // Erstelle ein visuelles Element für den Adorner
            // var visual = CreateDragVisual(dataGridL.SelectedItem);
            //
            // // Adorner-Layer abrufen
            // _adornerLayer = AdornerLayer.GetAdornerLayer(dataGridL);
            // if (_adornerLayer != null)
            // {
            //     //_dragAdorner = new DragAdorner(dataGrid, visual, offsetX: 10, offsetY: 10);
            //     _dragAdorner = new DragAdorner(dataGridL, visual, -260, -160);
            //     _adornerLayer.Add(_dragAdorner);
            // }
            //
            // DragDrop.DoDragDrop(dataGridL, selectedSpieler, DragDropEffects.Move);
            
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null && !(dep is DataGridCell))
                dep = VisualTreeHelper.GetParent(dep);
            
            DataGridCell cell = dep as DataGridCell;
            if (cell != null)
            {
                DataGridRow row = DataGridRow.GetRowContainingElement(cell);
                DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;
                //int col = dataGrid?.Columns.IndexOf(cell.Column) ?? -1;
                var columnHeader = cell.Column.Header as string;
                //if (cell != null && col == 1)
                if ( columnHeader.ToLower().EndsWith("name"))
                {
                    // Erstelle ein visuelles Element für den Adorner
                    var visual = CreateDragVisual(dataGridL.SelectedItem);

                    // Adorner-Layer abrufen
                    _adornerLayer = AdornerLayer.GetAdornerLayer(dataGridL);
                    if (_adornerLayer != null)
                    {
                        //_dragAdorner = new DragAdorner(dataGrid, visual, offsetX: 10, offsetY: 10);
                        _dragAdorner = new DragAdorner(dataGridL, visual, -260, -160);
                        _adornerLayer.Add(_dragAdorner);
                    }

                    DragDrop.DoDragDrop(cell, cell.DataContext, DragDropEffects.Move);
                }
            }
        }
        
        if (sender is DataGrid dataGridR && dataGridR.Name == "dgEingabe" && dataGridR.SelectedItem != null)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null && !(dep is DataGridCell))
                dep = VisualTreeHelper.GetParent(dep);
            
            DataGridCell cell = dep as DataGridCell;
            if (cell != null)
            {
                DataGridRow row = DataGridRow.GetRowContainingElement(cell);
                DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;
                //int col = dataGrid?.Columns.IndexOf(cell.Column) ?? -1;
                var columnHeader = cell.Column.Header as string;
                //if (cell != null && col == 1)
                if ( columnHeader.ToLower().EndsWith("name"))
                {
                    // Erstelle ein visuelles Element für den Adorner
                    var visual = CreateDragVisual(dataGridR.SelectedItem);

                    // Adorner-Layer abrufen
                    _adornerLayer = AdornerLayer.GetAdornerLayer(dataGridR);
                    if (_adornerLayer != null)
                    {
                        //_dragAdorner = new DragAdorner(dataGrid, visual, offsetX: 10, offsetY: 10);
                        _dragAdorner = new DragAdorner(dataGridR, visual, -260, -160);
                        _adornerLayer.Add(_dragAdorner);
                    }

                    DragDrop.DoDragDrop(cell, cell.DataContext, DragDropEffects.Move);
                }
            }
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
                        var droppedData6TR = e.Data.GetData(typeof(SechsTageRennen));

                        if (droppedData6TR != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedData6TR, targetDataGrid.Name);
                        }

                        break;
                    case "Pokal":
                        var droppedDataPokal = e.Data.GetData(typeof(Pokal));

                        if (droppedDataPokal != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataPokal, targetDataGrid.Name);
                        }

                        break;
                    case "Sargkegeln":
                        var droppedDataSarg = e.Data.GetData(typeof(Sargkegeln));

                        if (droppedDataSarg != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataSarg, targetDataGrid.Name);
                        }

                        break;
                    case "Meisterschaft":
                        var droppedDataMeister = e.Data.GetData(typeof(Meisterschaft));

                        if (droppedDataMeister != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataMeister, targetDataGrid.Name);
                        }

                        break;
                    case "Blitztunier":
                        var droppedDataBlitz = e.Data.GetData(typeof(Blitztunier));

                        if (droppedDataBlitz != null && targetDataGrid != null)
                        {
                            // Notify ViewModel to handle the data transfer
                            ErgebniseingabeViewModel.HandleDrop(droppedDataBlitz, targetDataGrid.Name);
                        }

                        break;
                    case "Kombimeisterschaft":
                        var droppedDataKombi = e.Data.GetData(typeof(Kombimeisterschaft));

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

        ErgebniseingabeViewModel.CheckMandatoryFields();
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
    
    private void DataGrid_DragOver(object sender, DragEventArgs e)
    {
        if (_dragAdorner != null)
        {
            // Adorner-Position aktualisieren
            var position = e.GetPosition(_adornerLayer);
            _dragAdorner.UpdatePosition(position);
        }
    }

    private async void CboSpielauswahl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ErgebniseingabeViewModel.LstIDsToDelete.Clear();
            
        //Setzen der Datenquelle für das Eingabegrid
        switch (ErgebniseingabeViewModel.SelectedSpiel)
        {
            case "9er/Ratten":
                InitEingabeGrid9erRatten();
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "6-Tage-Rennen":
                InitEingabeGrid6TageRennen();
                //dgEingabe.ItemsSource = ErgebniseingabeViewModel.Spiel6TageRennen;
                ErgebniseingabeViewModel.SelectedSpielNummer = "1";
                CboSpielnummer.Visibility = Visibility.Visible;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "Pokal":
                InitEingabeGridPokal();
                //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielPokal;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "Sargkegeln":
                InitEingabeGridSargkegeln();
                //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielSargkegeln;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                break;
            case "Meisterschaft":
                InitEingabeGridMeisterschaft();
                //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielMeisterschaft;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                break;
            case "Blitztunier":
                InitEingabeGridBlitztunier();
                //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielBlitztunier;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                break;
            case "Kombimeisterschaft":
                InitEingabeGridKombimeisterschaft();
                //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielKombimeisterschaft;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                break;
        }

        ErgebniseingabeViewModel.CheckMandatoryFields();
        await ErgebniseingabeViewModel.RefreshOutputAsync();
    }

    private DataGridTextColumn CreateTextColumn(string header, string bindingPath, Visibility visibility,
        bool readonlyFlag, int minWidth)
    {
        return new DataGridTextColumn
        {
            Header = header,
            Binding = new Binding
            {
                Path = new PropertyPath(bindingPath),
                Mode = BindingMode.TwoWay
            },
            Visibility = visibility,
            IsReadOnly = readonlyFlag,
            MinWidth = minWidth
        };
    }

    private DataGridColumn CreateColumnHinRueckrunde()
    {
        // Create a new DataGridTemplateColumn
        DataGridTemplateColumn comboBoxColumn = new DataGridTemplateColumn();
        comboBoxColumn.Header = "HinRueckrunde";
        comboBoxColumn.MinWidth = 150;
    
        // Define the CellTemplate
        DataTemplate cellTemplate = new DataTemplate();
        FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
        textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("HinRueckrunde"));
        cellTemplate.VisualTree = textBlockFactory;
        comboBoxColumn.CellTemplate = cellTemplate;
    
        // Define the CellEditingTemplate
        DataTemplate cellEditingTemplate = new DataTemplate();
        FrameworkElementFactory comboBoxFactory = new FrameworkElementFactory(typeof(ComboBox));
        
        Binding itemsSourceBinding = new Binding("DataContext.GridCboHinRueckrundeItems")
        {
            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGrid), 1)
        };
        comboBoxFactory.SetBinding(ComboBox.ItemsSourceProperty, itemsSourceBinding);
        
        comboBoxFactory.SetBinding(ComboBox.SelectedValueProperty, new Binding("HinRueckrunde"));
        comboBoxFactory.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
        comboBoxFactory.SetValue(ComboBox.SelectedValuePathProperty, "Key");
        cellEditingTemplate.VisualTree = comboBoxFactory;
        comboBoxColumn.CellEditingTemplate = cellEditingTemplate;
    
        return comboBoxColumn;
    }

    private void InitEingabeGrid9erRatten()
    {
        dgEingabe.ItemsSource = null;
        dgEingabe.Columns.Clear();
        dgEingabe.IsReadOnly = false;
        dgEingabe.AutoGenerateColumns = false;
        dgEingabe.Columns.Add(CreateTextColumn("ID", "ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpieltagID", "SpieltagID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpielerID", "SpielerID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spielername", "Spielername", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Neuner", "Neuner", Visibility.Visible, false, 90));
        dgEingabe.Columns.Add(CreateTextColumn("Ratten", "Ratten", Visibility.Visible, false, 90));

        //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielNeunerRatten;
        dgEingabe.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("SpielNeunerRatten"));
    }
    
    private void InitEingabeGrid6TageRennen ()
    {
        dgEingabe.ItemsSource = null;
        dgEingabe.Columns.Clear();
        dgEingabe.IsReadOnly = false;
        dgEingabe.AutoGenerateColumns = false;
        dgEingabe.Columns.Add(CreateTextColumn("ID", "ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpieltagID", "SpieltagID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1ID", "Spieler1ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1Name", "Spieler1Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2ID", "Spieler2ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2Name", "Spieler2Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Runden", "Runden", Visibility.Visible, false, 90));
        dgEingabe.Columns.Add(CreateTextColumn("Punkte", "Punkte", Visibility.Visible, false, 90));
        dgEingabe.Columns.Add(CreateTextColumn("Spielnr", "Spielnr", Visibility.Visible, true, 90));
        dgEingabe.Columns.Add(CreateTextColumn("Platz", "Platz", Visibility.Collapsed, false, 90));

        //dgEingabe.ItemsSource = ErgebniseingabeViewModel.Spiel6TageRennen;
        dgEingabe.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Spiel6TageRennen"));
    }

    private void InitEingabeGridPokal()
    {
        dgEingabe.ItemsSource = null;
        dgEingabe.Columns.Clear();
        dgEingabe.IsReadOnly = false;
        dgEingabe.AutoGenerateColumns = false;
        dgEingabe.Columns.Add(CreateTextColumn("ID", "ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpieltagID", "SpieltagID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpielerID", "SpielerID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spielername", "Spielername", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Platzierung", "Platzierung", Visibility.Visible, false, 90));

        //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielPokal;
        dgEingabe.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("SpielPokal"));
    }
    
    private void InitEingabeGridSargkegeln()
    {
        dgEingabe.ItemsSource = null;
        dgEingabe.Columns.Clear();
        dgEingabe.IsReadOnly = false;
        dgEingabe.AutoGenerateColumns = false;
        dgEingabe.Columns.Add(CreateTextColumn("ID", "ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpieltagID", "SpieltagID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpielerID", "SpielerID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spielername", "Spielername", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Platzierung", "Platzierung", Visibility.Visible, false, 90));

        //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielSargkegeln;
        dgEingabe.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("SpielSargkegeln"));
    }
    
    private void InitEingabeGridMeisterschaft()
    {
        dgEingabe.ItemsSource = null;
        dgEingabe.Columns.Clear();
        dgEingabe.IsReadOnly = false;
        dgEingabe.AutoGenerateColumns = false;
        dgEingabe.Columns.Add(CreateTextColumn("ID", "ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpieltagID", "SpieltagID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1ID", "Spieler1ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1Name", "Spieler1Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2ID", "Spieler2ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2Name", "Spieler2Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("HolzSpieler1", "HolzSpieler1", Visibility.Visible, false, 150));
        dgEingabe.Columns.Add(CreateTextColumn("HolzSpieler2", "HolzSpieler2", Visibility.Visible, false, 150));
        //dgEingabe.Columns.Add(CreateTextColumn("HinRueckrunde", "HinRueckrunde", Visibility.Visible, true, 90));
        dgEingabe.Columns.Add(CreateColumnHinRueckrunde());
        
        //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielMeisterschaft;
        dgEingabe.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("SpielMeisterschaft"));
    }
    
    private void InitEingabeGridBlitztunier()
    {
        dgEingabe.ItemsSource = null;
        dgEingabe.Columns.Clear();
        dgEingabe.IsReadOnly = false;
        dgEingabe.AutoGenerateColumns = false;
        dgEingabe.Columns.Add(CreateTextColumn("ID", "ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpieltagID", "SpieltagID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1ID", "Spieler1ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1Name", "Spieler1Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2ID", "Spieler2ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2Name", "Spieler2Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("PunkteSpieler1", "PunkteSpieler1", Visibility.Visible, false, 150));
        dgEingabe.Columns.Add(CreateTextColumn("PunkteSpieler2", "PunkteSpieler2", Visibility.Visible, false, 150));
        //dgEingabe.Columns.Add(CreateTextColumn("HinRueckrunde", "HinRueckrunde", Visibility.Visible, true, 90));
        dgEingabe.Columns.Add(CreateColumnHinRueckrunde());
        
        //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielBlitztunier;
        dgEingabe.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("SpielBlitztunier"));
    }
    
    private void InitEingabeGridKombimeisterschaft()
    {
        dgEingabe.ItemsSource = null;
        dgEingabe.Columns.Clear();
        dgEingabe.IsReadOnly = false;
        dgEingabe.AutoGenerateColumns = false;
        dgEingabe.Columns.Add(CreateTextColumn("ID", "ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("SpieltagID", "SpieltagID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1ID", "Spieler1ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1Name", "Spieler1Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2ID", "Spieler2ID", Visibility.Collapsed, false, 0));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2Name", "Spieler2Name", Visibility.Visible, true, 200));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1Punkte3bis8", "Spieler1Punkte3bis8", Visibility.Visible, false, 220));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler1Punkte5Kugeln", "Spieler1Punkte5Kugeln", Visibility.Visible, false, 220));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2Punkte3bis8", "Spieler2Punkte3bis8", Visibility.Visible, false, 220));
        dgEingabe.Columns.Add(CreateTextColumn("Spieler2Punkte5Kugeln", "Spieler2Punkte5Kugeln", Visibility.Visible, false, 220));
        //dgEingabe.Columns.Add(CreateTextColumn("HinRueckrunde", "HinRueckrunde", Visibility.Visible, true, 90));
        dgEingabe.Columns.Add(CreateColumnHinRueckrunde());
        
        //dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielKombimeisterschaft;
        dgEingabe.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("SpielKombimeisterschaft"));
    }

    private void DgEingabe_OnPreparingCellForEdit(object? sender, DataGridPreparingCellForEditEventArgs e)
    {
        if (e.EditingElement is TextBox textBox)
        {
            textBox.SelectAll();
            ErgebniseingabeViewModel.DataChanged = true;
        }
    }
}