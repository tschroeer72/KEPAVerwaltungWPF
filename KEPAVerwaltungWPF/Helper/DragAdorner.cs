using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace KEPAVerwaltungWPF.Helper;

public class DragAdorner : Adorner
{
    private readonly UIElement _child;
    private Point _currentPosition;

    public DragAdorner(UIElement adornedElement, UIElement child) 
        : base(adornedElement)
    {
        _child = child ?? throw new ArgumentNullException(nameof(child));

        AddVisualChild(_child);
        AddLogicalChild(_child);
    }

    public void UpdatePosition(Point currentPosition)
    {
        _currentPosition = currentPosition;
        InvalidateVisual();
    }

    protected override int VisualChildrenCount => 1;

    protected override Visual GetVisualChild(int index)
    {
        if (index != 0)
            throw new ArgumentOutOfRangeException(nameof(index));
        return _child;
    }

    protected override Size MeasureOverride(Size constraint)
    {
        _child.Measure(constraint);
        return _child.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        _child.Arrange(new Rect(_currentPosition, _child.DesiredSize));
        return new Size(_child.DesiredSize.Width, _child.DesiredSize.Height);
    }
}