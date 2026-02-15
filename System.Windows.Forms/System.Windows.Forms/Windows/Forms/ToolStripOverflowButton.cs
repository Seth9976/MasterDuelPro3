using System;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Hosts a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that displays items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E5 RID: 485
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)]
	public class ToolStripOverflowButton : ToolStripDropDownButton
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x00067249 File Offset: 0x00065449
		internal ToolStripOverflowButton(ToolStrip ts)
		{
			base.InternalOwner = ts;
			base.Parent = ts;
			base.Visible = false;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has overflow items; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00067266 File Offset: 0x00065466
		public override bool HasDropDownItems
		{
			get
			{
				return this.drop_down != null && base.DropDown.DisplayedItems.Count > 0;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the space between controls.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00063F91 File Offset: 0x00062191
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 1, 0, 2);
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can fit.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014B0 RID: 5296 RVA: 0x00067285 File Offset: 0x00065485
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return new Size(16, base.Parent.Height);
		}

		/// <summary>Creates an empty <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that can be dropped down and to which events can be attached.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</returns>
		// Token: 0x060014B1 RID: 5297 RVA: 0x00067299 File Offset: 0x00065499
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripOverflow(this)
			{
				DefaultDropDownDirection = ToolStripDropDownDirection.BelowLeft,
				OwnerItem = this
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060014B2 RID: 5298 RVA: 0x000672AF File Offset: 0x000654AF
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				base.Owner.Renderer.DrawOverflowButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			}
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> representing the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</param>
		// Token: 0x060014B3 RID: 5299 RVA: 0x00066E6A File Offset: 0x0006506A
		protected internal override void SetBounds(Rectangle bounds)
		{
			base.SetBounds(bounds);
		}
	}
}
