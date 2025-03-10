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
            dataGridL.SelectedItem is AktiveSpieler selectedSpieler)
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
            
            DragDrop.DoDragDrop(dataGridL, selectedSpieler, DragDropEffects.Move);
        }
        
        // if (sender is DataGrid dataGridR && dataGridR.Name == "dgEingabe" &&
        //     dataGridR.SelectedItem!= null)
        // {
        //     // Erstelle ein visuelles Element für den Adorner
        //     var visual = CreateDragVisual(dataGridR.SelectedItem);
        //     
        //     // Adorner-Layer abrufen
        //     _adornerLayer = AdornerLayer.GetAdornerLayer(dataGridR);
        //     if (_adornerLayer != null)
        //     {
        //         //_dragAdorner = new DragAdorner(dataGrid, visual, offsetX: 10, offsetY: 10);
        //         _dragAdorner = new DragAdorner(dataGridR, visual, -260, -160);
        //         _adornerLayer.Add(_dragAdorner);
        //     }
        //     
        //     DragDrop.DoDragDrop(dataGridR, dataGridR.SelectedItem, DragDropEffects.Move);
        // }

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
                if ( columnHeader.EndsWith("name"))
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
    
    // private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    // {
    //     // Initiate Drag-and-Drop
    //     var dataGrid = sender as DataGrid;
    //     
    //     // // Check if any cell is in edit mode
    //     // if(dataGrid.Name == "dgEingabe")
    //     //     foreach (var column in dataGrid.Columns)
    //     //     {
    //     //         var cell = column.GetCellContent(dataGrid.SelectedItem)?.Parent as DataGridCell;
    //     //         if (cell != null && cell.IsEditing)
    //     //             return; // Do not start drag-and-drop if any cell is being edited
    //     //     }
    //     
    //     
    //     if (dataGrid?.SelectedItem != null)
    //     {
    //         // Erstelle ein visuelles Element für den Adorner
    //         var visual = CreateDragVisual(dataGrid.SelectedItem);
    //     
    //         // Adorner-Layer abrufen
    //         _adornerLayer = AdornerLayer.GetAdornerLayer(dataGrid);
    //         if (_adornerLayer != null)
    //         {
    //             //_dragAdorner = new DragAdorner(dataGrid, visual, offsetX: 10, offsetY: 10);
    //             _dragAdorner = new DragAdorner(dataGrid, visual, -260, -160);
    //             _adornerLayer.Add(_dragAdorner);
    //         }
    //         
    //         DragDrop.DoDragDrop(dataGrid, dataGrid.SelectedItem, DragDropEffects.Move);
    //     }
    //     
    //     
    //     // var dataGrid = sender as DataGrid;
    //     // if (dataGrid == null) return;
    //     //
    //     // // Get the clicked cell
    //     // var hit = VisualTreeHelper.HitTest(dataGrid, e.GetPosition(dataGrid));
    //     // if (hit == null) return;
    //     //
    //     // var cell = hit.VisualHit.GetParentOfType<DataGridCell>();
    //     // if (cell == null) return;
    //     //
    //     // // Check if the column header ends with "Name"
    //     // var columnHeader = cell.Column.Header as string;
    //     // if (columnHeader == null || !columnHeader.EndsWith("Name")) return;
    //     //
    //     // if (dataGrid.SelectedItem != null)
    //     // {
    //     //     var visual = CreateDragVisual(dataGrid.SelectedItem);
    //     //     _adornerLayer = AdornerLayer.GetAdornerLayer(dataGrid);
    //     //     if (_adornerLayer != null)
    //     //     {
    //     //         _dragAdorner = new DragAdorner(dataGrid, visual, -260, -160);
    //     //         _adornerLayer.Add(_dragAdorner);
    //     //     }
    //     //
    //     //     DragDrop.DoDragDrop(dataGrid, dataGrid.SelectedItem, DragDropEffects.Move);
    //     // }
    // }
    //
    // private void DataGrid_DragEnter(object sender, DragEventArgs e)
    // {
    //     
    //     // Allow move effects
    //     e.Effects = DragDropEffects.Move;
    // }
    //
    private void DataGrid_DragOver(object sender, DragEventArgs e)
    {
        if (_dragAdorner != null)
        {
            // Adorner-Position aktualisieren
            var position = e.GetPosition(_adornerLayer);
            _dragAdorner.UpdatePosition(position);
        }
    }
    
    //
    // private void DataGrid_Drop(object sender, DragEventArgs e)
    // {
    //     // Adorner entfernen
    //     if (_adornerLayer != null && _dragAdorner != null)
    //     {
    //         _adornerLayer.Remove(_dragAdorner);
    //         _adornerLayer = null;
    //         _dragAdorner = null;
    //     }
    //     
    //     // Handle the drop
    //     var targetDataGrid = sender as DataGrid;
    //     switch (targetDataGrid.Name)
    //     {
    //         case "dgAktiveMitglieder":
    //             switch (ErgebniseingabeViewModel.SelectedSpiel)
    //             {
    //                 case "9er/Ratten":
    //                     var droppedDataNR = e.Data.GetData(typeof(NeunerRatten));
    //             
    //                     if (droppedDataNR != null && targetDataGrid != null)
    //                     {
    //                         // Notify ViewModel to handle the data transfer
    //                         ErgebniseingabeViewModel.HandleDrop(droppedDataNR, targetDataGrid.Name);
    //                     }
    //                     break;
    //                 case "6-Tage-Rennen":
    //                     var droppedData6TR = e.Data.GetData(typeof(Spiel6TageRennen));
    //             
    //                     if (droppedData6TR != null && targetDataGrid != null)
    //                     {
    //                         // Notify ViewModel to handle the data transfer
    //                         ErgebniseingabeViewModel.HandleDrop(droppedData6TR, targetDataGrid.Name);
    //                     }
    //                     break;
    //                 case "Pokal":
    //                     var droppedDataPokal = e.Data.GetData(typeof(SpielPokal));
    //             
    //                     if (droppedDataPokal != null && targetDataGrid != null)
    //                     {
    //                         // Notify ViewModel to handle the data transfer
    //                         ErgebniseingabeViewModel.HandleDrop(droppedDataPokal, targetDataGrid.Name);
    //                     }
    //                     break;
    //                 case "Sargkegeln":
    //                     var droppedDataSarg = e.Data.GetData(typeof(SpielSargkegeln));
    //             
    //                     if (droppedDataSarg != null && targetDataGrid != null)
    //                     {
    //                         // Notify ViewModel to handle the data transfer
    //                         ErgebniseingabeViewModel.HandleDrop(droppedDataSarg, targetDataGrid.Name);
    //                     }
    //                     break;
    //                 case "Meisterschaft":
    //                     var droppedDataMeister = e.Data.GetData(typeof(SpielMeisterschaft));
    //             
    //                     if (droppedDataMeister != null && targetDataGrid != null)
    //                     {
    //                         // Notify ViewModel to handle the data transfer
    //                         ErgebniseingabeViewModel.HandleDrop(droppedDataMeister, targetDataGrid.Name);
    //                     }
    //                     break;
    //                 case "Blitztunier":
    //                     var droppedDataBlitz = e.Data.GetData(typeof(SpielBlitztunier));
    //             
    //                     if (droppedDataBlitz != null && targetDataGrid != null)
    //                     {
    //                         // Notify ViewModel to handle the data transfer
    //                         ErgebniseingabeViewModel.HandleDrop(droppedDataBlitz, targetDataGrid.Name);
    //                     }
    //                     break;
    //                 case "Kombimeisterschaft":
    //                     var droppedDataKombi = e.Data.GetData(typeof(SpielKombimeisterschaft));
    //             
    //                     if (droppedDataKombi != null && targetDataGrid != null)
    //                     {
    //                         // Notify ViewModel to handle the data transfer
    //                         ErgebniseingabeViewModel.HandleDrop(droppedDataKombi, targetDataGrid.Name);
    //                     }
    //                     break;
    //             }
    //             break;
    //         case "dgEingabe":
    //             var droppedDataAktiveSpieler = e.Data.GetData(typeof(AktiveSpieler));
    //             
    //             if (droppedDataAktiveSpieler != null && targetDataGrid != null)
    //             {
    //                 // Notify ViewModel to handle the data transfer
    //                 ErgebniseingabeViewModel.HandleDrop(droppedDataAktiveSpieler, targetDataGrid.Name);
    //             }
    //             break;
    //     }
    //     
    //     ErgebniseingabeViewModel.CheckMandatoryFields();
    // }

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

    private void CboSpielauswahl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //Setzen der Datenquelle für das Eingabegrid
        switch (ErgebniseingabeViewModel.SelectedSpiel)
        {
            case "9er/Ratten":
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                InitEingabeGrid9erRatten();
                break;
            case "6-Tage-Rennen":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.Spiel6TageRennen;
                CboSpielnummer.Visibility = Visibility.Visible;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                InitEingabeGrid6TageRennen();
                CboSpielauswahl.IsDropDownOpen = true;
                break;
            case "Pokal":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielPokal;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                InitEingabeGridPokal();
                break;
            case "Sargkegeln":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielSargkegeln;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Collapsed;
                InitEingabeGridSargkegeln();
                break;
            case "Meisterschaft":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielMeisterschaft;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                InitEingabeGridMeisterschaft();
                break;
            case "Blitztunier":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielBlitztunier;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                InitEingabeGridBlitztunier();
                break;
            case "Kombimeisterschaft":
                dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielKombimeisterschaft;
                CboSpielnummer.Visibility = Visibility.Collapsed;
                CboHinRueckrunde.Visibility = Visibility.Visible;
                InitEingabeGridKombimeisterschaft();
                break;
        }

        // dgEingabe.IsReadOnly = false;
        //
        // //Formatieren des Datagrids
        // for (int i = 0; i < dgEingabe.Columns.Count; i++)
        // {
        //     switch (dgEingabe.Columns[i].Header)
        //     {
        //         case "ID":
        //             dgEingabe.Columns[i].Visibility = Visibility.Hidden;
        //             break;
        //         case "SpieltagID":
        //             dgEingabe.Columns[i].Visibility = Visibility.Hidden;
        //             break;
        //         case "SpielerID":
        //             dgEingabe.Columns[i].Visibility = Visibility.Hidden;
        //             break;
        //         case "Spieler1ID":
        //             dgEingabe.Columns[i].Visibility = Visibility.Hidden;
        //             break;
        //         case "Spieler2ID":
        //             dgEingabe.Columns[i].Visibility = Visibility.Hidden;
        //             break;
        //         case "Spielername":
        //             // dgEingabe.Columns[i].IsReadOnly = true;
        //             dgEingabe.Columns[i].MinWidth = 200;
        //             break;
        //         case "Spieler1Name":
        //             // dgEingabe.Columns[i].IsReadOnly = true;
        //             dgEingabe.Columns[i].MinWidth = 200;
        //             break;
        //         case "Spieler2Name":
        //             // dgEingabe.Columns[i].IsReadOnly = true;
        //             dgEingabe.Columns[i].MinWidth = 200;
        //             break;
        //         case "Spielnr":
        //             // dgEingabe.Columns[i].IsReadOnly = true;
        //             dgEingabe.Columns[i].MinWidth = 80;
        //             break;
        //         case "Platz":
        //             // dgEingabe.Columns[i].IsReadOnly = true;
        //             dgEingabe.Columns[i].MinWidth = 80;
        //             break;
        //         case "Neuner":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             
        //             dgEingabe.Columns[i].MinWidth = 90;
        //             break;
        //         case "Ratten":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 90;
        //             break;
        //         case "Runden":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 80;
        //             break;
        //         case "Punkte":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 80;
        //             break;
        //         case "HolzSpieler1":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 120;
        //             break;
        //         case "HolzSpieler2":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 120;
        //             break;
        //         case "HinRueckrunde":
        //             //ChangeColumntypeOfColumnHinRueckrunde(i);
        //             // dgEingabe.Columns[i].IsReadOnly = true;
        //             dgEingabe.Columns[i].MinWidth = 120;
        //             break;
        //         case "PunkteSpieler1":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 150;
        //             break;
        //         case "PunkteSpieler2":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 150;
        //             break;
        //         case "Spieler1Punkte3bis8":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             // dgEingabe.Columns[i].MinWidth = 200;
        //             break;
        //         case "Spieler2Punkte3bis8":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 200;
        //             break;
        //         case "Spieler1Punkte5Kugeln":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 220;
        //             break;
        //         case "Spieler2Punkte5Kugeln":
        //             // dgEingabe.Columns[i].IsReadOnly = false;
        //             dgEingabe.Columns[i].MinWidth = 220;
        //             break;
        //     }
        // }

        ErgebniseingabeViewModel.CheckMandatoryFields();
    }

    private DataGridTextColumn CreateTextColumn(string header, string bindingPath, Visibility visibility,
        bool readonlyFlag, int minWidth)
    {
        return new DataGridTextColumn
        {
            Header = header,
            Binding = new Binding(bindingPath),
            Visibility = visibility,
            IsReadOnly = readonlyFlag,
            MinWidth = minWidth
        };
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

        dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielNeunerRatten;
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

        dgEingabe.ItemsSource = ErgebniseingabeViewModel.Spiel6TageRennen;
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

        dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielPokal;
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

        dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielSargkegeln;
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
        
        dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielMeisterschaft;
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
        
        dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielBlitztunier;
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
        
        dgEingabe.ItemsSource = ErgebniseingabeViewModel.SpielKombimeisterschaft;
    }
    
    private void CboSpielnummer_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox != null)
        {
            comboBox.IsDropDownOpen = true;
        }
    }
}