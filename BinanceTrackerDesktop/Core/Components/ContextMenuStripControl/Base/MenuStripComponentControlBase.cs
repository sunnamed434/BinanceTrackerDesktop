using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.Control;
using BinanceTrackerDesktop.Core.Components.Expandable;
using BinanceTrackerDesktop.Core.Components.TextControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Base
{
    public class MenuStripComponentControlBase : TextComponentControl, IExpandableComponent<MenuStripComponentItemControl, byte>
    {
        public readonly ContextMenuStrip Strip;

        public IReadOnlyCollection<MenuStripComponentItemControl> AllComponents => Components;



        protected List<MenuStripComponentItemControl> Components = new List<MenuStripComponentItemControl>();



        public MenuStripComponentControlBase(ContextMenuStrip strip) : base(strip)
        {
            if (strip == null)
                throw new ArgumentNullException(nameof(strip));

            Strip = strip;
        }

        public MenuStripComponentControlBase()
        {

        }



        public virtual void AddComponent(MenuStripComponentItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Add(item.ToolStrip);
            Components.Add(item);
        }

        public void AddComponents(IEnumerable<MenuStripComponentItemControl> items)
        {
            if (!items.Any())
                throw new InvalidOperationException();

            foreach (MenuStripComponentItemControl item in items)
                AddComponent(item);
        }

        public virtual void RemoveComponent(MenuStripComponentItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Remove(item.ToolStrip);
            Components.Remove(item);
        }

        public MenuStripComponentItemControl GetComponentAt(byte id)
        {
            return Components.FirstOrDefault(c => c.Id == id);
        }



        protected virtual IEnumerable<MenuStripComponentItemControl> InitializeItems()
        {
            return new List<MenuStripComponentItemControl>();
        }
    }
}
