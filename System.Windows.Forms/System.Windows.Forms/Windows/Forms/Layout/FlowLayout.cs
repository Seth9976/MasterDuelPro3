using System;
using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x02000393 RID: 915
	internal class FlowLayout : LayoutEngine
	{
		// Token: 0x06001DA8 RID: 7592 RVA: 0x00091F78 File Offset: 0x00090178
		public override bool Layout(object container, LayoutEventArgs args)
		{
			if (container is ToolStripPanel)
			{
				return false;
			}
			if (container is ToolStrip)
			{
				return this.LayoutToolStrip((ToolStrip)container);
			}
			Control control = container as Control;
			FlowLayoutSettings layoutSettings;
			if (control is FlowLayoutPanel)
			{
				layoutSettings = (control as FlowLayoutPanel).LayoutSettings;
			}
			else
			{
				layoutSettings = FlowLayout.default_settings;
			}
			if (control.Controls.Count == 0)
			{
				return false;
			}
			Rectangle displayRectangle = control.DisplayRectangle;
			Point location;
			switch (layoutSettings.FlowDirection)
			{
			case FlowDirection.RightToLeft:
				location = new Point(displayRectangle.Right, displayRectangle.Top);
				goto IL_00AF;
			case FlowDirection.BottomUp:
				location = new Point(displayRectangle.Left, displayRectangle.Bottom);
				goto IL_00AF;
			}
			location = displayRectangle.Location;
			IL_00AF:
			bool flag = false;
			List<Control> list = new List<Control>();
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				if (control2.Visible)
				{
					if (control2.AutoSize)
					{
						Size preferredSize = control2.GetPreferredSize(control2.Size);
						control2.SetBoundsInternal(control2.Left, control2.Top, preferredSize.Width, preferredSize.Height, BoundsSpecified.None);
					}
					switch (layoutSettings.FlowDirection)
					{
					case FlowDirection.LeftToRight:
						goto IL_022E;
					case FlowDirection.TopDown:
						if (layoutSettings.WrapContents && (displayRectangle.Height + displayRectangle.Top - location.Y < control2.Height + control2.Margin.Top + control2.Margin.Bottom || flag))
						{
							location.X = this.FinishColumn(list);
							location.Y = displayRectangle.Top;
							flag = false;
							list.Clear();
						}
						location.Offset(0, control2.Margin.Top);
						control2.SetBoundsInternal(location.X + control2.Margin.Left, location.Y, control2.Width, control2.Height, BoundsSpecified.None);
						location.Y += control2.Height + control2.Margin.Bottom;
						break;
					case FlowDirection.RightToLeft:
						if (layoutSettings.WrapContents && (location.X < control2.Width + control2.Margin.Left + control2.Margin.Right || flag))
						{
							location.Y = this.FinishRow(list);
							location.X = displayRectangle.Right;
							flag = false;
							list.Clear();
						}
						location.Offset(control2.Margin.Right * -1, 0);
						control2.SetBoundsInternal(location.X - control2.Width, location.Y + control2.Margin.Top, control2.Width, control2.Height, BoundsSpecified.None);
						location.X -= control2.Width + control2.Margin.Left;
						break;
					case FlowDirection.BottomUp:
						if (layoutSettings.WrapContents && (location.Y < control2.Height + control2.Margin.Top + control2.Margin.Bottom || flag))
						{
							location.X = this.FinishColumn(list);
							location.Y = displayRectangle.Bottom;
							flag = false;
							list.Clear();
						}
						location.Offset(0, control2.Margin.Bottom * -1);
						control2.SetBoundsInternal(location.X + control2.Margin.Left, location.Y - control2.Height, control2.Width, control2.Height, BoundsSpecified.None);
						location.Y -= control2.Height + control2.Margin.Top;
						break;
					default:
						goto IL_022E;
					}
					IL_04F5:
					list.Add(control2);
					if (layoutSettings.GetFlowBreak(control2))
					{
						flag = true;
						continue;
					}
					continue;
					IL_022E:
					if (layoutSettings.WrapContents && !(control is ToolStripPanel) && (displayRectangle.Width + displayRectangle.Left - location.X < control2.Width + control2.Margin.Left + control2.Margin.Right || flag))
					{
						location.Y = this.FinishRow(list);
						location.X = displayRectangle.Left;
						flag = false;
						list.Clear();
					}
					location.Offset(control2.Margin.Left, 0);
					control2.SetBoundsInternal(location.X, location.Y + control2.Margin.Top, control2.Width, control2.Height, BoundsSpecified.None);
					location.X += control2.Width + control2.Margin.Right;
					goto IL_04F5;
				}
			}
			if (layoutSettings.FlowDirection == FlowDirection.LeftToRight || layoutSettings.FlowDirection == FlowDirection.RightToLeft)
			{
				this.FinishRow(list);
			}
			else
			{
				this.FinishColumn(list);
			}
			return false;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000924F8 File Offset: 0x000906F8
		private int FinishRow(List<Control> row)
		{
			if (row.Count == 0)
			{
				return 0;
			}
			int num = int.MaxValue;
			int num2 = 0;
			bool flag = true;
			bool flag2 = true;
			foreach (Control control in row)
			{
				if (control.Dock != DockStyle.Fill && ((control.Anchor & AnchorStyles.Top) != AnchorStyles.Top || (control.Anchor & AnchorStyles.Bottom) != AnchorStyles.Bottom))
				{
					flag = false;
				}
				if (control.AutoSize)
				{
					flag2 = false;
				}
			}
			foreach (Control control2 in row)
			{
				if (control2.Bottom + control2.Margin.Bottom > num2 && control2.Dock != DockStyle.Fill && ((control2.Anchor & AnchorStyles.Top) != AnchorStyles.Top || (control2.Anchor & AnchorStyles.Bottom) != AnchorStyles.Bottom || control2.AutoSize))
				{
					num2 = control2.Bottom + control2.Margin.Bottom;
				}
				if (control2.Top - control2.Margin.Top < num)
				{
					num = control2.Top - control2.Margin.Top;
				}
			}
			if (num2 == 0)
			{
				foreach (Control control3 in row)
				{
					if (control3.Bottom + control3.Margin.Bottom > num2 && control3.Dock != DockStyle.Fill && control3.AutoSize)
					{
						num2 = control3.Bottom + control3.Margin.Bottom;
					}
				}
			}
			if (num2 == 0)
			{
				foreach (Control control4 in row)
				{
					if (control4.Bottom + control4.Margin.Bottom > num2 && control4.Dock == DockStyle.Fill)
					{
						num2 = control4.Bottom + control4.Margin.Bottom;
					}
				}
			}
			foreach (Control control5 in row)
			{
				if (flag && flag2)
				{
					control5.SetBoundsInternal(control5.Left, control5.Top, control5.Width, 0, BoundsSpecified.None);
				}
				else if (control5.Dock == DockStyle.Fill || ((control5.Anchor & AnchorStyles.Top) == AnchorStyles.Top && (control5.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom))
				{
					control5.SetBoundsInternal(control5.Left, control5.Top, control5.Width, num2 - control5.Top - control5.Margin.Bottom, BoundsSpecified.None);
				}
				else if (control5.Dock == DockStyle.Bottom || (control5.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
				{
					control5.SetBoundsInternal(control5.Left, num2 - control5.Margin.Bottom - control5.Height, control5.Width, control5.Height, BoundsSpecified.None);
				}
				else if (control5.Dock != DockStyle.Top && (control5.Anchor & AnchorStyles.Top) != AnchorStyles.Top)
				{
					control5.SetBoundsInternal(control5.Left, (num2 - num) / 2 - control5.Height / 2 + (int)Math.Floor((double)(control5.Margin.Top - control5.Margin.Bottom) / 2.0) + num, control5.Width, control5.Height, BoundsSpecified.None);
				}
			}
			if (num2 == 0)
			{
				return num;
			}
			return num2;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00092920 File Offset: 0x00090B20
		private int FinishColumn(List<Control> col)
		{
			if (col.Count == 0)
			{
				return 0;
			}
			int num = int.MaxValue;
			int num2 = 0;
			bool flag = true;
			bool flag2 = true;
			foreach (Control control in col)
			{
				if (control.Dock != DockStyle.Fill && ((control.Anchor & AnchorStyles.Left) != AnchorStyles.Left || (control.Anchor & AnchorStyles.Right) != AnchorStyles.Right))
				{
					flag = false;
				}
				if (control.AutoSize)
				{
					flag2 = false;
				}
			}
			foreach (Control control2 in col)
			{
				if (control2.Right + control2.Margin.Right > num2 && control2.Dock != DockStyle.Fill && ((control2.Anchor & AnchorStyles.Left) != AnchorStyles.Left || (control2.Anchor & AnchorStyles.Right) != AnchorStyles.Right || control2.AutoSize))
				{
					num2 = control2.Right + control2.Margin.Right;
				}
				if (control2.Left - control2.Margin.Left < num)
				{
					num = control2.Left - control2.Margin.Left;
				}
			}
			if (num2 == 0)
			{
				foreach (Control control3 in col)
				{
					if (control3.Right + control3.Margin.Right > num2 && control3.Dock != DockStyle.Fill && control3.AutoSize)
					{
						num2 = control3.Right + control3.Margin.Right;
					}
				}
			}
			if (num2 == 0)
			{
				foreach (Control control4 in col)
				{
					if (control4.Right + control4.Margin.Right > num2 && control4.Dock == DockStyle.Fill)
					{
						num2 = control4.Right + control4.Margin.Right;
					}
				}
			}
			foreach (Control control5 in col)
			{
				if (flag && flag2)
				{
					control5.SetBoundsInternal(control5.Left, control5.Top, 0, control5.Height, BoundsSpecified.None);
				}
				else if (control5.Dock == DockStyle.Fill || ((control5.Anchor & AnchorStyles.Left) == AnchorStyles.Left && (control5.Anchor & AnchorStyles.Right) == AnchorStyles.Right))
				{
					control5.SetBoundsInternal(control5.Left, control5.Top, num2 - control5.Left - control5.Margin.Right, control5.Height, BoundsSpecified.None);
				}
				else if (control5.Dock == DockStyle.Right || (control5.Anchor & AnchorStyles.Right) == AnchorStyles.Right)
				{
					control5.SetBoundsInternal(num2 - control5.Margin.Right - control5.Width, control5.Top, control5.Width, control5.Height, BoundsSpecified.None);
				}
				else if (control5.Dock != DockStyle.Left && (control5.Anchor & AnchorStyles.Left) != AnchorStyles.Left)
				{
					control5.SetBoundsInternal((num2 - num) / 2 - control5.Width / 2 + (int)Math.Floor((double)(control5.Margin.Left - control5.Margin.Right) / 2.0) + num, control5.Top, control5.Width, control5.Height, BoundsSpecified.None);
				}
			}
			if (num2 == 0)
			{
				return num;
			}
			return num2;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00092D48 File Offset: 0x00090F48
		private bool LayoutToolStrip(ToolStrip parent)
		{
			FlowLayoutSettings flowLayoutSettings = (FlowLayoutSettings)parent.LayoutSettings;
			if (parent.Items.Count == 0)
			{
				return false;
			}
			foreach (object obj in parent.Items)
			{
				((ToolStripItem)obj).SetPlacement(ToolStripItemPlacement.Main);
			}
			Rectangle displayRectangle = parent.DisplayRectangle;
			Point location;
			switch (flowLayoutSettings.FlowDirection)
			{
			case FlowDirection.RightToLeft:
				location = new Point(displayRectangle.Right, displayRectangle.Top);
				goto IL_00BA;
			case FlowDirection.BottomUp:
				location = new Point(displayRectangle.Left, displayRectangle.Bottom);
				goto IL_00BA;
			}
			location = displayRectangle.Location;
			IL_00BA:
			bool flag = false;
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object obj2 in parent.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj2;
				if (toolStripItem.Available)
				{
					if (toolStripItem.AutoSize)
					{
						toolStripItem.SetBounds(new Rectangle(toolStripItem.Location, toolStripItem.GetPreferredSize(toolStripItem.Size)));
					}
					switch (flowLayoutSettings.FlowDirection)
					{
					case FlowDirection.LeftToRight:
						goto IL_0219;
					case FlowDirection.TopDown:
						if (flowLayoutSettings.WrapContents && (displayRectangle.Height - location.Y < toolStripItem.Height + toolStripItem.Margin.Top + toolStripItem.Margin.Bottom || flag))
						{
							location.X = this.FinishColumn(list);
							location.Y = displayRectangle.Top;
							flag = false;
							list.Clear();
						}
						location.Offset(0, toolStripItem.Margin.Top);
						toolStripItem.Location = new Point(location.X + toolStripItem.Margin.Left, location.Y);
						location.Y += toolStripItem.Height + toolStripItem.Margin.Bottom;
						break;
					case FlowDirection.RightToLeft:
						if (flowLayoutSettings.WrapContents && (location.X < toolStripItem.Width + toolStripItem.Margin.Left + toolStripItem.Margin.Right || flag))
						{
							location.Y = this.FinishRow(list);
							location.X = displayRectangle.Right;
							flag = false;
							list.Clear();
						}
						location.Offset(toolStripItem.Margin.Right * -1, 0);
						toolStripItem.Location = new Point(location.X - toolStripItem.Width, location.Y + toolStripItem.Margin.Top);
						location.X -= toolStripItem.Width + toolStripItem.Margin.Left;
						break;
					case FlowDirection.BottomUp:
						if (flowLayoutSettings.WrapContents && (location.Y < toolStripItem.Height + toolStripItem.Margin.Top + toolStripItem.Margin.Bottom || flag))
						{
							location.X = this.FinishColumn(list);
							location.Y = displayRectangle.Bottom;
							flag = false;
							list.Clear();
						}
						location.Offset(0, toolStripItem.Margin.Bottom * -1);
						toolStripItem.Location = new Point(location.X + toolStripItem.Margin.Left, location.Y - toolStripItem.Height);
						location.Y -= toolStripItem.Height + toolStripItem.Margin.Top;
						break;
					default:
						goto IL_0219;
					}
					IL_04A4:
					list.Add(toolStripItem);
					if (flowLayoutSettings.GetFlowBreak(toolStripItem))
					{
						flag = true;
						continue;
					}
					continue;
					IL_0219:
					if (flowLayoutSettings.WrapContents && (displayRectangle.Width - location.X < toolStripItem.Width + toolStripItem.Margin.Left + toolStripItem.Margin.Right || flag))
					{
						location.Y = this.FinishRow(list);
						location.X = displayRectangle.Left;
						flag = false;
						list.Clear();
					}
					location.Offset(toolStripItem.Margin.Left, 0);
					toolStripItem.Location = new Point(location.X, location.Y + toolStripItem.Margin.Top);
					location.X += toolStripItem.Width + toolStripItem.Margin.Right;
					goto IL_04A4;
				}
			}
			int num = 0;
			if (flowLayoutSettings.FlowDirection == FlowDirection.LeftToRight || flowLayoutSettings.FlowDirection == FlowDirection.RightToLeft)
			{
				num = this.FinishRow(list);
			}
			else
			{
				this.FinishColumn(list);
			}
			if (num > 0)
			{
				parent.SetBoundsInternal(parent.Left, parent.Top, parent.Width, num + parent.Padding.Bottom, BoundsSpecified.None);
			}
			return false;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000932C0 File Offset: 0x000914C0
		private int FinishRow(List<ToolStripItem> row)
		{
			if (row.Count == 0)
			{
				return 0;
			}
			int num = int.MaxValue;
			int num2 = 0;
			bool flag = true;
			bool flag2 = true;
			foreach (ToolStripItem toolStripItem in row)
			{
				if (toolStripItem.Dock != DockStyle.Fill && ((toolStripItem.Anchor & AnchorStyles.Top) != AnchorStyles.Top || (toolStripItem.Anchor & AnchorStyles.Bottom) != AnchorStyles.Bottom))
				{
					flag = false;
				}
				if (toolStripItem.AutoSize)
				{
					flag2 = false;
				}
			}
			foreach (ToolStripItem toolStripItem2 in row)
			{
				if (toolStripItem2.Bottom + toolStripItem2.Margin.Bottom > num2 && toolStripItem2.Dock != DockStyle.Fill && ((toolStripItem2.Anchor & AnchorStyles.Top) != AnchorStyles.Top || (toolStripItem2.Anchor & AnchorStyles.Bottom) != AnchorStyles.Bottom || toolStripItem2.AutoSize))
				{
					num2 = toolStripItem2.Bottom + toolStripItem2.Margin.Bottom;
				}
				if (toolStripItem2.Top - toolStripItem2.Margin.Top < num)
				{
					num = toolStripItem2.Top - toolStripItem2.Margin.Top;
				}
			}
			if (num2 == 0)
			{
				foreach (ToolStripItem toolStripItem3 in row)
				{
					if (toolStripItem3.Bottom + toolStripItem3.Margin.Bottom > num2 && toolStripItem3.Dock != DockStyle.Fill && toolStripItem3.AutoSize)
					{
						num2 = toolStripItem3.Bottom + toolStripItem3.Margin.Bottom;
					}
				}
			}
			if (num2 == 0)
			{
				foreach (ToolStripItem toolStripItem4 in row)
				{
					if (toolStripItem4.Bottom + toolStripItem4.Margin.Bottom > num2 && toolStripItem4.Dock == DockStyle.Fill)
					{
						num2 = toolStripItem4.Bottom + toolStripItem4.Margin.Bottom;
					}
				}
			}
			foreach (ToolStripItem toolStripItem5 in row)
			{
				if (flag && flag2)
				{
					toolStripItem5.Height = 0;
				}
				else if (toolStripItem5.Dock == DockStyle.Fill || ((toolStripItem5.Anchor & AnchorStyles.Top) == AnchorStyles.Top && (toolStripItem5.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom))
				{
					toolStripItem5.Height = num2 - toolStripItem5.Top - toolStripItem5.Margin.Bottom;
				}
				else if (toolStripItem5.Dock == DockStyle.Bottom || (toolStripItem5.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
				{
					toolStripItem5.Top = num2 - toolStripItem5.Margin.Bottom - toolStripItem5.Height;
				}
				else if (toolStripItem5.Dock != DockStyle.Top && (toolStripItem5.Anchor & AnchorStyles.Top) != AnchorStyles.Top)
				{
					toolStripItem5.Top = (num2 - num) / 2 - toolStripItem5.Height / 2 + (int)Math.Floor((double)(toolStripItem5.Margin.Top - toolStripItem5.Margin.Bottom) / 2.0) + num;
				}
			}
			if (num2 == 0)
			{
				return num;
			}
			return num2;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00093690 File Offset: 0x00091890
		private int FinishColumn(List<ToolStripItem> col)
		{
			if (col.Count == 0)
			{
				return 0;
			}
			int num = int.MaxValue;
			int num2 = 0;
			bool flag = true;
			bool flag2 = true;
			foreach (ToolStripItem toolStripItem in col)
			{
				if (toolStripItem.Dock != DockStyle.Fill && ((toolStripItem.Anchor & AnchorStyles.Left) != AnchorStyles.Left || (toolStripItem.Anchor & AnchorStyles.Right) != AnchorStyles.Right))
				{
					flag = false;
				}
				if (toolStripItem.AutoSize)
				{
					flag2 = false;
				}
			}
			foreach (ToolStripItem toolStripItem2 in col)
			{
				if (toolStripItem2.Right + toolStripItem2.Margin.Right > num2 && toolStripItem2.Dock != DockStyle.Fill && ((toolStripItem2.Anchor & AnchorStyles.Left) != AnchorStyles.Left || (toolStripItem2.Anchor & AnchorStyles.Right) != AnchorStyles.Right || toolStripItem2.AutoSize))
				{
					num2 = toolStripItem2.Right + toolStripItem2.Margin.Right;
				}
				if (toolStripItem2.Left - toolStripItem2.Margin.Left < num)
				{
					num = toolStripItem2.Left - toolStripItem2.Margin.Left;
				}
			}
			if (num2 == 0)
			{
				foreach (ToolStripItem toolStripItem3 in col)
				{
					if (toolStripItem3.Right + toolStripItem3.Margin.Right > num2 && toolStripItem3.Dock != DockStyle.Fill && toolStripItem3.AutoSize)
					{
						num2 = toolStripItem3.Right + toolStripItem3.Margin.Right;
					}
				}
			}
			if (num2 == 0)
			{
				foreach (ToolStripItem toolStripItem4 in col)
				{
					if (toolStripItem4.Right + toolStripItem4.Margin.Right > num2 && toolStripItem4.Dock == DockStyle.Fill)
					{
						num2 = toolStripItem4.Right + toolStripItem4.Margin.Right;
					}
				}
			}
			foreach (ToolStripItem toolStripItem5 in col)
			{
				if (flag && flag2)
				{
					toolStripItem5.Width = 0;
				}
				else if (toolStripItem5.Dock == DockStyle.Fill || ((toolStripItem5.Anchor & AnchorStyles.Left) == AnchorStyles.Left && (toolStripItem5.Anchor & AnchorStyles.Right) == AnchorStyles.Right))
				{
					toolStripItem5.Width = num2 - toolStripItem5.Left - toolStripItem5.Margin.Right;
				}
				else if (toolStripItem5.Dock == DockStyle.Right || (toolStripItem5.Anchor & AnchorStyles.Right) == AnchorStyles.Right)
				{
					toolStripItem5.Left = num2 - toolStripItem5.Margin.Right - toolStripItem5.Width;
				}
				else if (toolStripItem5.Dock != DockStyle.Left && (toolStripItem5.Anchor & AnchorStyles.Left) != AnchorStyles.Left)
				{
					toolStripItem5.Left = (num2 - num) / 2 - toolStripItem5.Width / 2 + (int)Math.Floor((double)(toolStripItem5.Margin.Left - toolStripItem5.Margin.Right) / 2.0) + num;
				}
			}
			if (num2 == 0)
			{
				return num;
			}
			return num2;
		}

		// Token: 0x04001CD8 RID: 7384
		private static FlowLayoutSettings default_settings = new FlowLayoutSettings();
	}
}
