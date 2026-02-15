using System;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x02000392 RID: 914
	internal class DefaultLayout : LayoutEngine
	{
		// Token: 0x06001DA0 RID: 7584 RVA: 0x000917C0 File Offset: 0x0008F9C0
		private void LayoutDockedChildren(Control parent, Control[] controls)
		{
			Rectangle displayRectangle = parent.DisplayRectangle;
			MdiClient mdiClient = null;
			for (int i = controls.Length - 1; i >= 0; i--)
			{
				Control control = controls[i];
				Size size = control.Size;
				if (control.AutoSize)
				{
					size = this.GetPreferredControlSize(control);
				}
				if (control.VisibleInternal && control.ControlLayoutType != Control.LayoutType.Anchor)
				{
					if (control is MdiClient)
					{
						mdiClient = (MdiClient)control;
					}
					else
					{
						switch (control.Dock)
						{
						case DockStyle.Top:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Y, displayRectangle.Width, size.Height, BoundsSpecified.None);
							displayRectangle.Y += control.Height;
							displayRectangle.Height -= control.Height;
							break;
						case DockStyle.Bottom:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Bottom - size.Height, displayRectangle.Width, size.Height, BoundsSpecified.None);
							displayRectangle.Height -= control.Height;
							break;
						case DockStyle.Left:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Y, size.Width, displayRectangle.Height, BoundsSpecified.None);
							displayRectangle.X += control.Width;
							displayRectangle.Width -= control.Width;
							break;
						case DockStyle.Right:
							control.SetBoundsInternal(displayRectangle.Right - size.Width, displayRectangle.Y, size.Width, displayRectangle.Height, BoundsSpecified.None);
							displayRectangle.Width -= control.Width;
							break;
						case DockStyle.Fill:
							control.SetBoundsInternal(displayRectangle.Left, displayRectangle.Top, displayRectangle.Width, displayRectangle.Height, BoundsSpecified.None);
							break;
						}
					}
				}
			}
			if (mdiClient != null)
			{
				mdiClient.SetBoundsInternal(displayRectangle.Left, displayRectangle.Top, displayRectangle.Width, displayRectangle.Height, BoundsSpecified.None);
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000919CC File Offset: 0x0008FBCC
		private void LayoutAnchoredChildren(Control parent, Control[] controls)
		{
			Rectangle clientRectangle = parent.ClientRectangle;
			foreach (Control control in controls)
			{
				if (control.VisibleInternal && control.ControlLayoutType != Control.LayoutType.Dock)
				{
					AnchorStyles anchor = control.Anchor;
					int num = control.Left;
					int num2 = control.Top;
					int num3 = control.Width;
					int num4 = control.Height;
					if ((anchor & AnchorStyles.Right) != AnchorStyles.None)
					{
						if ((anchor & AnchorStyles.Left) != AnchorStyles.None)
						{
							num3 = clientRectangle.Width - control.dist_right - num;
						}
						else
						{
							num = clientRectangle.Width - control.dist_right - num3;
						}
					}
					else if ((anchor & AnchorStyles.Left) == AnchorStyles.None)
					{
						num += (clientRectangle.Width - (num + num3 + control.dist_right)) / 2;
						control.dist_right = clientRectangle.Width - (num + num3);
					}
					if ((anchor & AnchorStyles.Bottom) != AnchorStyles.None)
					{
						if ((anchor & AnchorStyles.Top) != AnchorStyles.None)
						{
							num4 = clientRectangle.Height - control.dist_bottom - num2;
						}
						else
						{
							num2 = clientRectangle.Height - control.dist_bottom - num4;
						}
					}
					else if ((anchor & AnchorStyles.Top) == AnchorStyles.None)
					{
						num2 += (clientRectangle.Height - (num2 + num4 + control.dist_bottom)) / 2;
						control.dist_bottom = clientRectangle.Height - (num2 + num4);
					}
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num4 < 0)
					{
						num4 = 0;
					}
					control.SetBoundsInternal(num, num2, num3, num4, BoundsSpecified.None);
				}
			}
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00091B2C File Offset: 0x0008FD2C
		private void LayoutAutoSizedChildren(Control parent, Control[] controls)
		{
			foreach (Control control in controls)
			{
				if (control.VisibleInternal && control.ControlLayoutType != Control.LayoutType.Dock && control.AutoSize)
				{
					AnchorStyles anchor = control.Anchor;
					int left = control.Left;
					int top = control.Top;
					Size preferredControlSize = this.GetPreferredControlSize(control);
					if ((anchor & AnchorStyles.Left) != AnchorStyles.None || (anchor & AnchorStyles.Right) == AnchorStyles.None)
					{
						control.dist_right += control.Width - preferredControlSize.Width;
					}
					if ((anchor & AnchorStyles.Top) != AnchorStyles.None || (anchor & AnchorStyles.Bottom) == AnchorStyles.None)
					{
						control.dist_bottom += control.Height - preferredControlSize.Height;
					}
					control.SetBoundsInternal(left, top, preferredControlSize.Width, preferredControlSize.Height, BoundsSpecified.None);
				}
			}
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00091BF8 File Offset: 0x0008FDF8
		private void LayoutAutoSizeContainer(Control container)
		{
			if (!container.VisibleInternal || container.ControlLayoutType == Control.LayoutType.Dock || !container.AutoSize)
			{
				return;
			}
			int left = container.Left;
			int top = container.Top;
			Size preferredSize = container.PreferredSize;
			int num;
			int num2;
			if (container.GetAutoSizeMode() == AutoSizeMode.GrowAndShrink)
			{
				num = preferredSize.Width;
				num2 = preferredSize.Height;
			}
			else
			{
				num = container.ExplicitBounds.Width;
				num2 = container.ExplicitBounds.Height;
				if (preferredSize.Width > num)
				{
					num = preferredSize.Width;
				}
				if (preferredSize.Height > num2)
				{
					num2 = preferredSize.Height;
				}
			}
			if (num < container.MinimumSize.Width)
			{
				num = container.MinimumSize.Width;
			}
			if (num2 < container.MinimumSize.Height)
			{
				num2 = container.MinimumSize.Height;
			}
			if (container.MaximumSize.Width != 0 && num > container.MaximumSize.Width)
			{
				num = container.MaximumSize.Width;
			}
			if (container.MaximumSize.Height != 0 && num2 > container.MaximumSize.Height)
			{
				num2 = container.MaximumSize.Height;
			}
			container.SetBoundsInternal(left, top, num, num2, BoundsSpecified.None);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00091D48 File Offset: 0x0008FF48
		public override bool Layout(object container, LayoutEventArgs args)
		{
			Control control = container as Control;
			Control[] allControls = control.Controls.GetAllControls();
			this.LayoutDockedChildren(control, allControls);
			this.LayoutAnchoredChildren(control, allControls);
			this.LayoutAutoSizedChildren(control, allControls);
			if (control is Form)
			{
				this.LayoutAutoSizeContainer(control);
			}
			return false;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00091D90 File Offset: 0x0008FF90
		private Size GetPreferredControlSize(Control child)
		{
			Size preferredSize = child.PreferredSize;
			int num;
			int num2;
			if (child.GetAutoSizeMode() == AutoSizeMode.GrowAndShrink || (child.Dock != DockStyle.None && !(child is Button) && !(child is FlowLayoutPanel)))
			{
				num = preferredSize.Width;
				num2 = preferredSize.Height;
			}
			else
			{
				num = child.ExplicitBounds.Width;
				num2 = child.ExplicitBounds.Height;
				if (preferredSize.Width > num)
				{
					num = preferredSize.Width;
				}
				if (preferredSize.Height > num2)
				{
					num2 = preferredSize.Height;
				}
			}
			if (child.AutoSize && child is FlowLayoutPanel && child.Dock != DockStyle.None)
			{
				DockStyle dock = child.Dock;
				if (dock - DockStyle.Top > 1)
				{
					if (dock - DockStyle.Left <= 1 && preferredSize.Width < child.ExplicitBounds.Width && preferredSize.Height < child.Parent.PaddingClientRectangle.Height)
					{
						num = preferredSize.Width;
					}
				}
				else if (preferredSize.Height < child.ExplicitBounds.Height && preferredSize.Width < child.Parent.PaddingClientRectangle.Width)
				{
					num2 = preferredSize.Height;
				}
			}
			if (num < child.MinimumSize.Width)
			{
				num = child.MinimumSize.Width;
			}
			if (num2 < child.MinimumSize.Height)
			{
				num2 = child.MinimumSize.Height;
			}
			if (child.MaximumSize.Width != 0 && num > child.MaximumSize.Width)
			{
				num = child.MaximumSize.Width;
			}
			if (child.MaximumSize.Height != 0 && num2 > child.MaximumSize.Height)
			{
				num2 = child.MaximumSize.Height;
			}
			return new Size(num, num2);
		}
	}
}
