using BinanceTrackerDesktop.Themes.Forms.Design;

namespace BinanceTrackerDesktop.Expandables;

public class ExpandableDesignable<TAddGetRemove, TSearchArgument> : Expandable<TAddGetRemove, TSearchArgument>
{
    public readonly DesignableForm DesignableForm;



    public ExpandableDesignable() => DesignableForm = new DesignableForm();
}
