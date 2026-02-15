using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EB RID: 491
	public abstract class ToolStripRenderer
	{
		/// <summary>Creates a gray-scale copy of a given image.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that is a copy of the given image, but with a gray-scale color matrix.</returns>
		/// <param name="normalImage">The image to be copied. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014D1 RID: 5329 RVA: 0x00068B9C File Offset: 0x00066D9C
		public static Image CreateDisabledImage(Image normalImage)
		{
			if (normalImage == null)
			{
				return null;
			}
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(ToolStripRenderer.grayscale_matrix);
			Bitmap bitmap = new Bitmap(normalImage.Width, normalImage.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.DrawImage(normalImage, new Rectangle(0, 0, normalImage.Width, normalImage.Height), 0, 0, normalImage.Width, normalImage.Height, GraphicsUnit.Pixel, imageAttributes);
			}
			return bitmap;
		}

		/// <summary>Draws an arrow on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains data to draw the arrow.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014D2 RID: 5330 RVA: 0x00068C20 File Offset: 0x00066E20
		public void DrawArrow(ToolStripArrowRenderEventArgs e)
		{
			this.OnRenderArrow(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripButton" />.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains data to draw the button's background.</param>
		// Token: 0x060014D3 RID: 5331 RVA: 0x00068C29 File Offset: 0x00066E29
		public void DrawButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderButtonBackground(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the data to draw the drop-down button's background.</param>
		// Token: 0x060014D4 RID: 5332 RVA: 0x00068C32 File Offset: 0x00066E32
		public void DrawDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderDropDownButtonBackground(e);
		}

		/// <summary>Draws a move handle on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the data to draw the move handle.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014D5 RID: 5333 RVA: 0x00068C3B File Offset: 0x00066E3B
		public void DrawGrip(ToolStripGripRenderEventArgs e)
		{
			this.OnRenderGrip(e);
		}

		/// <summary>Draws the space around an image on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the data to draw the space around the image.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014D6 RID: 5334 RVA: 0x00068C44 File Offset: 0x00066E44
		public void DrawImageMargin(ToolStripRenderEventArgs e)
		{
			this.OnRenderImageMargin(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the data to draw the background of the item.</param>
		// Token: 0x060014D7 RID: 5335 RVA: 0x00068C4D File Offset: 0x00066E4D
		public void DrawItemBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderItemBackground(e);
		}

		/// <summary>Draws an image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> that indicates the item is in a selected state.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the data to draw the selected image.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014D8 RID: 5336 RVA: 0x00068C56 File Offset: 0x00066E56
		public void DrawItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			this.OnRenderItemCheck(e);
		}

		/// <summary>Draws an image on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the data to draw the image.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014D9 RID: 5337 RVA: 0x00068C5F File Offset: 0x00066E5F
		public void DrawItemImage(ToolStripItemImageRenderEventArgs e)
		{
			this.OnRenderItemImage(e);
		}

		/// <summary>Draws text on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the data to draw the text.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014DA RID: 5338 RVA: 0x00068C68 File Offset: 0x00066E68
		public void DrawItemText(ToolStripItemTextRenderEventArgs e)
		{
			this.OnRenderItemText(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the data to draw the background for the menu item.</param>
		// Token: 0x060014DB RID: 5339 RVA: 0x00068C71 File Offset: 0x00066E71
		public void DrawMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderMenuItemBackground(e);
		}

		/// <summary>Draws the background for an overflow button.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014DC RID: 5340 RVA: 0x00068C7A File Offset: 0x00066E7A
		public void DrawOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderOverflowButtonBackground(e);
		}

		/// <summary>Draws a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the data to draw the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014DD RID: 5341 RVA: 0x00068C83 File Offset: 0x00066E83
		public void DrawSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			this.OnRenderSeparator(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the data to draw the background for the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x060014DE RID: 5342 RVA: 0x00068C8C File Offset: 0x00066E8C
		public void DrawToolStripBackground(ToolStripRenderEventArgs e)
		{
			this.OnRenderToolStripBackground(e);
		}

		/// <summary>Draws the border for a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the data to draw the border for the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x060014DF RID: 5343 RVA: 0x00068C95 File Offset: 0x00066E95
		public void DrawToolStripBorder(ToolStripRenderEventArgs e)
		{
			this.OnRenderToolStripBorder(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderArrow" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014E0 RID: 5344 RVA: 0x00068CA0 File Offset: 0x00066EA0
		protected virtual void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			ArrowDirection direction = e.Direction;
			if (direction <= ArrowDirection.Up)
			{
				if (direction != ArrowDirection.Left && direction != ArrowDirection.Up)
				{
				}
			}
			else
			{
				if (direction != ArrowDirection.Right)
				{
					if (direction != ArrowDirection.Down)
					{
						goto IL_0113;
					}
					using (Pen pen = new Pen(e.ArrowColor))
					{
						int num = e.ArrowRectangle.Left + e.ArrowRectangle.Width / 2 - 3;
						int num2 = e.ArrowRectangle.Top + e.ArrowRectangle.Height / 2 - 2;
						ToolStripRenderer.DrawDownArrow(e.Graphics, pen, num, num2);
						goto IL_0113;
					}
				}
				using (Pen pen2 = new Pen(e.ArrowColor))
				{
					int num3 = e.ArrowRectangle.Left + e.ArrowRectangle.Width / 2 - 3;
					int num4 = e.ArrowRectangle.Top + e.ArrowRectangle.Height / 2 - 4;
					ToolStripRenderer.DrawRightArrow(e.Graphics, pen2, num3, num4);
				}
			}
			IL_0113:
			ToolStripArrowRenderEventHandler toolStripArrowRenderEventHandler = (ToolStripArrowRenderEventHandler)this.Events[ToolStripRenderer.RenderArrowEvent];
			if (toolStripArrowRenderEventHandler != null)
			{
				toolStripArrowRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014E1 RID: 5345 RVA: 0x00068E00 File Offset: 0x00067000
		protected virtual void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderButtonBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderDropDownButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014E2 RID: 5346 RVA: 0x00068E30 File Offset: 0x00067030
		protected virtual void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderDropDownButtonBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014E3 RID: 5347 RVA: 0x00068E60 File Offset: 0x00067060
		protected virtual void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			ToolStripGripRenderEventHandler toolStripGripRenderEventHandler = (ToolStripGripRenderEventHandler)this.Events[ToolStripRenderer.RenderGripEvent];
			if (toolStripGripRenderEventHandler != null)
			{
				toolStripGripRenderEventHandler(this, e);
			}
		}

		/// <summary>Draws the item background.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014E4 RID: 5348 RVA: 0x00068E90 File Offset: 0x00067090
		protected virtual void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			ToolStripRenderEventHandler toolStripRenderEventHandler = (ToolStripRenderEventHandler)this.Events[ToolStripRenderer.RenderImageMarginEvent];
			if (toolStripRenderEventHandler != null)
			{
				toolStripRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ToolStripSystemRenderer.OnRenderItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs)" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014E5 RID: 5349 RVA: 0x00068EC0 File Offset: 0x000670C0
		protected virtual void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (e.Item.BackColor != Control.DefaultBackColor)
			{
				Rectangle rectangle = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(e.Item.BackColor), rectangle);
			}
			if (e.Item.BackgroundImage != null)
			{
				Rectangle rectangle2 = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
				this.DrawBackground(e.Graphics, rectangle2, e.Item.BackgroundImage, e.Item.BackgroundImageLayout);
			}
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderItemBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemCheck" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014E6 RID: 5350 RVA: 0x00068F9C File Offset: 0x0006719C
		protected virtual void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			ToolStripItemImageRenderEventHandler toolStripItemImageRenderEventHandler = (ToolStripItemImageRenderEventHandler)this.Events[ToolStripRenderer.RenderItemCheckEvent];
			if (toolStripItemImageRenderEventHandler != null)
			{
				toolStripItemImageRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemImage" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014E7 RID: 5351 RVA: 0x00068FCC File Offset: 0x000671CC
		protected virtual void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			bool flag = false;
			Image image = e.Image;
			if (e.Item.RightToLeft == RightToLeft.Yes && e.Item.RightToLeftAutoMirrorImage)
			{
				image = ToolStripRenderer.CreateMirrorImage(image);
				flag = true;
			}
			if (e.Item.ImageTransparentColor != Color.Empty)
			{
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorKey(e.Item.ImageTransparentColor, e.Item.ImageTransparentColor);
				e.Graphics.DrawImage(image, e.ImageRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
				imageAttributes.Dispose();
			}
			else
			{
				e.Graphics.DrawImage(image, e.ImageRectangle);
			}
			if (flag)
			{
				image.Dispose();
			}
			ToolStripItemImageRenderEventHandler toolStripItemImageRenderEventHandler = (ToolStripItemImageRenderEventHandler)this.Events[ToolStripRenderer.RenderItemImageEvent];
			if (toolStripItemImageRenderEventHandler != null)
			{
				toolStripItemImageRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemText" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014E8 RID: 5352 RVA: 0x000690A8 File Offset: 0x000672A8
		protected virtual void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			if (e.TextDirection == ToolStripTextDirection.Vertical90)
			{
				GraphicsState graphicsState = e.Graphics.Save();
				PointF pointF = new PointF(e.Graphics.Transform.OffsetX, e.Graphics.Transform.OffsetY);
				e.Graphics.ResetTransform();
				e.Graphics.RotateTransform(90f);
				RectangleF rectangleF = new RectangleF((float)((e.Item.Height - e.TextRectangle.Height) / 2), ((float)e.TextRectangle.Width + pointF.X) * -1f - 18f, (float)e.TextRectangle.Height, (float)e.TextRectangle.Width);
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				e.Graphics.DrawString(e.Text, e.TextFont, ThemeEngine.Current.ResPool.GetSolidBrush(e.TextColor), rectangleF, stringFormat);
				e.Graphics.Restore(graphicsState);
			}
			else if (e.TextDirection == ToolStripTextDirection.Vertical270)
			{
				GraphicsState graphicsState2 = e.Graphics.Save();
				PointF pointF2 = new PointF(e.Graphics.Transform.OffsetX, e.Graphics.Transform.OffsetY);
				e.Graphics.ResetTransform();
				e.Graphics.RotateTransform(270f);
				RectangleF rectangleF2 = new RectangleF((float)(-(float)e.TextRectangle.Height - (e.Item.Height - e.TextRectangle.Height) / 2), (float)e.TextRectangle.Width + pointF2.X + 4f, (float)e.TextRectangle.Height, (float)e.TextRectangle.Width);
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.Alignment = StringAlignment.Center;
				e.Graphics.DrawString(e.Text, e.TextFont, ThemeEngine.Current.ResPool.GetSolidBrush(e.TextColor), rectangleF2, stringFormat2);
				e.Graphics.Restore(graphicsState2);
			}
			else
			{
				TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle, e.TextColor, e.TextFormat);
			}
			ToolStripItemTextRenderEventHandler toolStripItemTextRenderEventHandler = (ToolStripItemTextRenderEventHandler)this.Events[ToolStripRenderer.RenderItemTextEvent];
			if (toolStripItemTextRenderEventHandler != null)
			{
				toolStripItemTextRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderMenuItemBackground" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014E9 RID: 5353 RVA: 0x00069330 File Offset: 0x00067530
		protected virtual void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderMenuItemBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderOverflowButtonBackground" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014EA RID: 5354 RVA: 0x00069360 File Offset: 0x00067560
		protected virtual void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderOverflowButtonBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderSeparator" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014EB RID: 5355 RVA: 0x00069390 File Offset: 0x00067590
		protected virtual void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			ToolStripSeparatorRenderEventHandler toolStripSeparatorRenderEventHandler = (ToolStripSeparatorRenderEventHandler)this.Events[ToolStripRenderer.RenderSeparatorEvent];
			if (toolStripSeparatorRenderEventHandler != null)
			{
				toolStripSeparatorRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBackground" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014EC RID: 5356 RVA: 0x000693C0 File Offset: 0x000675C0
		protected virtual void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			ToolStripRenderEventHandler toolStripRenderEventHandler = (ToolStripRenderEventHandler)this.Events[ToolStripRenderer.RenderToolStripBackgroundEvent];
			if (toolStripRenderEventHandler != null)
			{
				toolStripRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBorder" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014ED RID: 5357 RVA: 0x000693F0 File Offset: 0x000675F0
		protected virtual void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			ToolStripRenderEventHandler toolStripRenderEventHandler = (ToolStripRenderEventHandler)this.Events[ToolStripRenderer.RenderToolStripBorderEvent];
			if (toolStripRenderEventHandler != null)
			{
				toolStripRenderEventHandler(this, e);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0006941E File Offset: 0x0006761E
		private EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00069439 File Offset: 0x00067639
		internal static Image CreateMirrorImage(Image normalImage)
		{
			if (normalImage == null)
			{
				return null;
			}
			Bitmap bitmap = new Bitmap(normalImage);
			bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
			return bitmap;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00069450 File Offset: 0x00067650
		private void DrawBackground(Graphics g, Rectangle bounds, Image image, ImageLayout layout)
		{
			if ((layout == ImageLayout.Center || layout == ImageLayout.Tile) && image.Size.Width >= bounds.Size.Width && image.Size.Height >= bounds.Size.Height)
			{
				layout = ImageLayout.None;
			}
			switch (layout)
			{
			case ImageLayout.None:
				g.DrawImageUnscaledAndClipped(image, bounds);
				return;
			case ImageLayout.Tile:
			{
				int i = 0;
				for (int j = 0; j < bounds.Height; j += image.Height)
				{
					while (i < bounds.Width)
					{
						g.DrawImageUnscaledAndClipped(image, bounds);
						i += image.Width;
					}
					i = 0;
				}
				return;
			}
			case ImageLayout.Center:
			{
				Rectangle rectangle = new Rectangle((bounds.Size.Width - image.Size.Width) / 2, (bounds.Size.Height - image.Size.Height) / 2, image.Width, image.Height);
				g.DrawImageUnscaledAndClipped(image, rectangle);
				return;
			}
			case ImageLayout.Stretch:
				g.DrawImage(image, bounds);
				return;
			case ImageLayout.Zoom:
			{
				if ((float)image.Height / (float)image.Width < (float)bounds.Height / (float)bounds.Width)
				{
					Rectangle rectangle2 = new Rectangle(0, 0, bounds.Width, (int)((float)bounds.Width * ((float)image.Height / (float)image.Width)));
					rectangle2.Y = (bounds.Height - rectangle2.Height) / 2;
					g.DrawImage(image, rectangle2);
					return;
				}
				Rectangle rectangle3 = new Rectangle(0, 0, (int)((float)bounds.Height * ((float)image.Width / (float)image.Height)), bounds.Height);
				rectangle3.X = (bounds.Width - rectangle3.Width) / 2;
				g.DrawImage(image, rectangle3);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00069620 File Offset: 0x00067820
		internal static void DrawRightArrow(Graphics g, Pen p, int x, int y)
		{
			g.DrawLine(p, x, y, x, y + 6);
			g.DrawLine(p, x + 1, y + 1, x + 1, y + 5);
			g.DrawLine(p, x + 2, y + 2, x + 2, y + 4);
			g.DrawLine(p, x + 2, y + 3, x + 3, y + 3);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00069673 File Offset: 0x00067873
		internal static void DrawDownArrow(Graphics g, Pen p, int x, int y)
		{
			g.DrawLine(p, x + 1, y, x + 5, y);
			g.DrawLine(p, x + 2, y + 1, x + 4, y + 1);
			g.DrawLine(p, x + 3, y + 1, x + 3, y + 2);
		}

		// Token: 0x04000C94 RID: 3220
		private static ColorMatrix grayscale_matrix = new ColorMatrix(new float[][]
		{
			new float[] { 0.22f, 0.22f, 0.22f, 0f, 0f },
			new float[] { 0.27f, 0.27f, 0.27f, 0f, 0f },
			new float[] { 0.04f, 0.04f, 0.04f, 0f, 0f },
			new float[] { 0.365f, 0.365f, 0.365f, 0.7f, 0f },
			new float[] { 0f, 0f, 0f, 0f, 1f }
		});

		// Token: 0x04000C95 RID: 3221
		private EventHandlerList events;

		// Token: 0x04000C96 RID: 3222
		private static object RenderArrowEvent = new object();

		// Token: 0x04000C97 RID: 3223
		private static object RenderButtonBackgroundEvent = new object();

		// Token: 0x04000C98 RID: 3224
		private static object RenderDropDownButtonBackgroundEvent = new object();

		// Token: 0x04000C99 RID: 3225
		private static object RenderGripEvent = new object();

		// Token: 0x04000C9A RID: 3226
		private static object RenderImageMarginEvent = new object();

		// Token: 0x04000C9B RID: 3227
		private static object RenderItemBackgroundEvent = new object();

		// Token: 0x04000C9C RID: 3228
		private static object RenderItemCheckEvent = new object();

		// Token: 0x04000C9D RID: 3229
		private static object RenderItemImageEvent = new object();

		// Token: 0x04000C9E RID: 3230
		private static object RenderItemTextEvent = new object();

		// Token: 0x04000C9F RID: 3231
		private static object RenderLabelBackgroundEvent = new object();

		// Token: 0x04000CA0 RID: 3232
		private static object RenderMenuItemBackgroundEvent = new object();

		// Token: 0x04000CA1 RID: 3233
		private static object RenderOverflowButtonBackgroundEvent = new object();

		// Token: 0x04000CA2 RID: 3234
		private static object RenderSeparatorEvent = new object();

		// Token: 0x04000CA3 RID: 3235
		private static object RenderSplitButtonBackgroundEvent = new object();

		// Token: 0x04000CA4 RID: 3236
		private static object RenderStatusStripSizingGripEvent = new object();

		// Token: 0x04000CA5 RID: 3237
		private static object RenderToolStripBackgroundEvent = new object();

		// Token: 0x04000CA6 RID: 3238
		private static object RenderToolStripBorderEvent = new object();

		// Token: 0x04000CA7 RID: 3239
		private static object RenderToolStripContentPanelBackgroundEvent = new object();

		// Token: 0x04000CA8 RID: 3240
		private static object RenderToolStripPanelBackgroundEvent = new object();

		// Token: 0x04000CA9 RID: 3241
		private static object RenderToolStripStatusLabelBackgroundEvent = new object();
	}
}
