using System;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020001EF RID: 495
	internal class ToolStripSplitStackLayout : LayoutEngine
	{
		// Token: 0x0600150B RID: 5387 RVA: 0x00069904 File Offset: 0x00067B04
		public override bool Layout(object container, LayoutEventArgs args)
		{
			if (!(container is ToolStrip))
			{
				ToolStripContentPanel toolStripContentPanel = (ToolStripContentPanel)container;
				int num = toolStripContentPanel.DisplayRectangle.Left;
				int top = toolStripContentPanel.DisplayRectangle.Top;
				foreach (object obj in toolStripContentPanel.Controls)
				{
					ToolStrip toolStrip = (ToolStrip)obj;
					Rectangle rectangle = default(Rectangle);
					num += toolStrip.Margin.Left;
					rectangle.Location = new Point(num, top + toolStrip.Margin.Top);
					rectangle.Height = toolStripContentPanel.DisplayRectangle.Height - toolStrip.Margin.Vertical;
					rectangle.Width = toolStrip.GetToolStripPreferredSize(new Size(0, rectangle.Height)).Width;
					toolStrip.Width = rectangle.Width + 12;
					num += rectangle.Width + toolStrip.Margin.Right;
				}
				return false;
			}
			ToolStrip toolStrip2 = (ToolStrip)container;
			if (toolStrip2.Items == null)
			{
				return false;
			}
			Rectangle displayRectangle = toolStrip2.DisplayRectangle;
			if (toolStrip2.Orientation == Orientation.Horizontal && toolStrip2.LayoutStyle != ToolStripLayoutStyle.Table)
			{
				this.LayoutHorizontalToolStrip(toolStrip2, displayRectangle);
			}
			else
			{
				this.LayoutVerticalToolStrip(toolStrip2, displayRectangle);
			}
			return false;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00069A84 File Offset: 0x00067C84
		private void LayoutHorizontalToolStrip(ToolStrip ts, Rectangle bounds)
		{
			ToolStripItemOverflow[] array = new ToolStripItemOverflow[ts.Items.Count];
			ToolStripItemPlacement[] array2 = new ToolStripItemPlacement[ts.Items.Count];
			Size size = new Size(0, bounds.Height);
			int[] array3 = new int[ts.Items.Count];
			int i = 0;
			int num = bounds.Width;
			int num2 = 0;
			bool flag = ts.CanOverflow & !(ts is MenuStrip) & !(ts is StatusStrip);
			bool flag2 = false;
			foreach (object obj in ts.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				array[num2] = toolStripItem.Overflow;
				array2[num2] = ((toolStripItem.Overflow == ToolStripItemOverflow.Always) ? ToolStripItemPlacement.Overflow : ToolStripItemPlacement.Main);
				array3[num2] = toolStripItem.GetPreferredSize(size).Width + toolStripItem.Margin.Horizontal;
				if (!toolStripItem.Available)
				{
					array2[num2] = ToolStripItemPlacement.None;
				}
				i += ((array2[num2] == ToolStripItemPlacement.Main) ? array3[num2] : 0);
				if (array2[num2] == ToolStripItemPlacement.Overflow)
				{
					flag2 = true;
				}
				num2++;
			}
			if (flag2)
			{
				ts.OverflowButton.Visible = true;
				ts.OverflowButton.SetBounds(new Rectangle(ts.Width - 16, 0, 16, ts.Height));
				num -= ts.OverflowButton.Width;
			}
			else
			{
				ts.OverflowButton.Visible = false;
			}
			while (i > num)
			{
				if (flag && !ts.OverflowButton.Visible)
				{
					ts.OverflowButton.Visible = true;
					ts.OverflowButton.SetBounds(new Rectangle(ts.Width - 16, 0, 16, ts.Height));
					num -= ts.OverflowButton.Width;
				}
				bool flag3 = false;
				for (int j = array3.Length - 1; j >= 0; j--)
				{
					if (array[j] == ToolStripItemOverflow.AsNeeded && array2[j] == ToolStripItemPlacement.Main)
					{
						array2[j] = ToolStripItemPlacement.Overflow;
						i -= array3[j];
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					for (int k = array3.Length - 1; k >= 0; k--)
					{
						if (array[k] == ToolStripItemOverflow.Never && array2[k] == ToolStripItemPlacement.Main)
						{
							array2[k] = ToolStripItemPlacement.None;
							i -= array3[k];
							flag3 = true;
							break;
						}
					}
				}
				if (!flag3)
				{
					break;
				}
			}
			num2 = 0;
			Point point = new Point(ts.DisplayRectangle.Left, ts.DisplayRectangle.Top);
			Point point2 = new Point(ts.DisplayRectangle.Right, ts.DisplayRectangle.Top);
			int height = ts.DisplayRectangle.Height;
			foreach (object obj2 in ts.Items)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				toolStripItem2.SetPlacement(array2[num2]);
				if (array2[num2] == ToolStripItemPlacement.Main)
				{
					if (toolStripItem2.Alignment == ToolStripItemAlignment.Left)
					{
						toolStripItem2.SetBounds(new Rectangle(point.X + toolStripItem2.Margin.Left, point.Y + toolStripItem2.Margin.Top, array3[num2] - toolStripItem2.Margin.Horizontal, height - toolStripItem2.Margin.Vertical));
						point.X += array3[num2];
					}
					else
					{
						toolStripItem2.SetBounds(new Rectangle(point2.X - toolStripItem2.Margin.Right - toolStripItem2.Width, point2.Y + toolStripItem2.Margin.Top, array3[num2] - toolStripItem2.Margin.Horizontal, height - toolStripItem2.Margin.Vertical));
						point2.X -= array3[num2];
					}
				}
				num2++;
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00069ED0 File Offset: 0x000680D0
		private void LayoutVerticalToolStrip(ToolStrip ts, Rectangle bounds)
		{
			if (!ts.Visible)
			{
				return;
			}
			ToolStripItemOverflow[] array = new ToolStripItemOverflow[ts.Items.Count];
			ToolStripItemPlacement[] array2 = new ToolStripItemPlacement[ts.Items.Count];
			Size size = new Size(bounds.Width, 0);
			int[] array3 = new int[ts.Items.Count];
			int[] array4 = new int[ts.Items.Count];
			int i = 0;
			int num = bounds.Height;
			int num2 = 0;
			bool flag = ts.CanOverflow & !(ts is MenuStrip) & !(ts is StatusStrip);
			foreach (object obj in ts.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				array[num2] = toolStripItem.Overflow;
				array2[num2] = ((toolStripItem.Overflow == ToolStripItemOverflow.Always) ? ToolStripItemPlacement.Overflow : ToolStripItemPlacement.Main);
				Size preferredSize = toolStripItem.GetPreferredSize(size);
				array3[num2] = preferredSize.Height + toolStripItem.Margin.Vertical;
				array4[num2] = preferredSize.Width + toolStripItem.Margin.Horizontal;
				if (!toolStripItem.Available)
				{
					array2[num2] = ToolStripItemPlacement.None;
				}
				i += ((array2[num2] == ToolStripItemPlacement.Main) ? array3[num2] : 0);
				num2++;
			}
			ts.OverflowButton.Visible = false;
			while (i > num)
			{
				if (flag && !ts.OverflowButton.Visible)
				{
					ts.OverflowButton.Visible = true;
					ts.OverflowButton.SetBounds(new Rectangle(0, ts.Height - 16, ts.Width, 16));
					num -= ts.OverflowButton.Height;
				}
				bool flag2 = false;
				for (int j = array3.Length - 1; j >= 0; j--)
				{
					if (array[j] == ToolStripItemOverflow.AsNeeded && array2[j] == ToolStripItemPlacement.Main)
					{
						array2[j] = ToolStripItemPlacement.Overflow;
						i -= array3[j];
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					for (int k = array3.Length - 1; k >= 0; k--)
					{
						if (array[k] == ToolStripItemOverflow.Never && array2[k] == ToolStripItemPlacement.Main)
						{
							array2[k] = ToolStripItemPlacement.None;
							i -= array3[k];
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					break;
				}
			}
			num2 = 0;
			Point point = new Point(ts.DisplayRectangle.Left, ts.DisplayRectangle.Top);
			Point point2 = new Point(ts.DisplayRectangle.Left, ts.DisplayRectangle.Bottom);
			int num3 = ts.DisplayRectangle.Width;
			foreach (object obj2 in ts.Items)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				toolStripItem2.SetPlacement(array2[num2]);
				if (ts.LayoutStyle == ToolStripLayoutStyle.Table)
				{
					num3 = array4[num2];
				}
				if (array2[num2] == ToolStripItemPlacement.Main)
				{
					if (toolStripItem2.Alignment == ToolStripItemAlignment.Left)
					{
						toolStripItem2.SetBounds(new Rectangle(point.X + toolStripItem2.Margin.Left, point.Y + toolStripItem2.Margin.Top, num3 - toolStripItem2.Margin.Horizontal, array3[num2] - toolStripItem2.Margin.Vertical));
						point.Y += array3[num2];
					}
					else
					{
						toolStripItem2.SetBounds(new Rectangle(point2.X + toolStripItem2.Margin.Left, point2.Y - toolStripItem2.Margin.Bottom - toolStripItem2.Height, num3 - toolStripItem2.Margin.Horizontal, array3[num2] - toolStripItem2.Margin.Vertical));
						point.Y += array3[num2];
					}
				}
				num2++;
			}
		}
	}
}
