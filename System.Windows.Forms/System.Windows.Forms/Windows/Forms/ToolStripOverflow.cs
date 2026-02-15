using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Manages the overflow behavior of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E4 RID: 484
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ToolStripOverflow : ToolStripDropDown, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripOverflow" /> class derived from a base <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="parentItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> from which to derive this <see cref="T:System.Windows.Forms.ToolStripOverflow" /> instance. </param>
		// Token: 0x060014A5 RID: 5285 RVA: 0x00066FA3 File Offset: 0x000651A3
		public ToolStripOverflow(ToolStripItem parentItem)
		{
			base.OwnerItem = parentItem;
		}

		/// <summary>Gets all of the items on the <see cref="T:System.Windows.Forms.ToolStrip" />, whether they are currently being displayed or not.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> containing all of the items.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00066FB2 File Offset: 0x000651B2
		public override ToolStripItemCollection Items
		{
			get
			{
				return base.Items;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00066FBA File Offset: 0x000651BA
		public override LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new FlowLayout();
				}
				return base.LayoutEngine;
			}
		}

		/// <summary>Gets all of the items that are currently being displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> that includes all items on this <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00066FD5 File Offset: 0x000651D5
		protected internal override ToolStripItemCollection DisplayedItems
		{
			get
			{
				return base.DisplayedItems;
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		// Token: 0x060014A9 RID: 5289 RVA: 0x00066FDD File Offset: 0x000651DD
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return base.GetToolStripPreferredSize(constrainingSize);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x060014AA RID: 5290 RVA: 0x00066FE8 File Offset: 0x000651E8
		[MonoInternalNote("This should stack in rows of ~3, but for now 1 column will work.")]
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.SetDisplayedItems();
			int num = 0;
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Available && toolStripItem.GetPreferredSize(Size.Empty).Width > num)
				{
					num = toolStripItem.GetPreferredSize(Size.Empty).Width;
				}
			}
			int left = base.Padding.Left;
			num += base.Padding.Horizontal;
			int num2 = base.Padding.Top;
			foreach (object obj2 in this.DisplayedItems)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				if (toolStripItem2.Available)
				{
					num2 += toolStripItem2.Margin.Top;
					int num3;
					if (toolStripItem2 is ToolStripSeparator)
					{
						num3 = 7;
					}
					else
					{
						num3 = toolStripItem2.GetPreferredSize(Size.Empty).Height;
					}
					toolStripItem2.SetBounds(new Rectangle(left, num2, num, num3));
					num2 += toolStripItem2.Height + toolStripItem2.Margin.Bottom;
				}
			}
			base.Size = new Size(num + base.Padding.Horizontal, num2 + base.Padding.Bottom);
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x060014AB RID: 5291 RVA: 0x00067194 File Offset: 0x00065394
		protected override void SetDisplayedItems()
		{
			this.displayed_items.ClearInternal();
			if (base.OwnerItem != null && base.OwnerItem.Parent != null)
			{
				foreach (object obj in base.OwnerItem.Parent.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.Placement == ToolStripItemPlacement.Overflow && toolStripItem.Available && !(toolStripItem is ToolStripSeparator))
					{
						this.displayed_items.AddNoOwnerOrLayout(toolStripItem);
					}
				}
			}
			base.PerformLayout();
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0006723C File Offset: 0x0006543C
		internal ToolStrip ParentToolStrip
		{
			get
			{
				return base.OwnerItem.Parent;
			}
		}

		// Token: 0x04000C86 RID: 3206
		private LayoutEngine layout_engine;
	}
}
