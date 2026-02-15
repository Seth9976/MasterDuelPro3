using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents the menu structure of a form. Although <see cref="T:System.Windows.Forms.MenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MainMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.MainMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000127 RID: 295
	[ToolboxItemFilter("System.Windows.Forms.MainMenu", ToolboxItemFilterType.Allow)]
	public class MainMenu : Menu
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class without any specified menu items.</summary>
		// Token: 0x06000B7F RID: 2943 RVA: 0x00031039 File Offset: 0x0002F239
		public MainMenu()
			: base(null)
		{
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.MainMenu" /> that is a duplicate of the current <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the cloned menu.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B80 RID: 2944 RVA: 0x00031049 File Offset: 0x0002F249
		public virtual MainMenu CloneMenu()
		{
			MainMenu mainMenu = new MainMenu();
			mainMenu.CloneMenu(this);
			return mainMenu;
		}

		/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000B81 RID: 2945 RVA: 0x00031057 File Offset: 0x0002F257
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Form" /> that contains this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that is the container for this control. Returns null if the <see cref="T:System.Windows.Forms.MainMenu" /> is not currently hosted on a form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B82 RID: 2946 RVA: 0x00031060 File Offset: 0x0002F260
		public Form GetForm()
		{
			return this.form;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MainMenu" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B83 RID: 2947 RVA: 0x00031068 File Offset: 0x0002F268
		public override string ToString()
		{
			return base.ToString() + ", GetForm: " + this.form;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MainMenu.Collapse" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000B84 RID: 2948 RVA: 0x00031080 File Offset: 0x0002F280
		protected internal virtual void OnCollapse(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MainMenu.CollapseEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000310B0 File Offset: 0x0002F2B0
		internal void Draw()
		{
			Message message = Message.Create(this.Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref message, this.Wnd.window.Handle, false);
			this.Draw(paintEventArgs, base.Rect);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00031108 File Offset: 0x0002F308
		internal void Draw(Rectangle rect)
		{
			if (this.Wnd.IsHandleCreated)
			{
				Point menuOrigin = XplatUI.GetMenuOrigin(this.Wnd.window.Handle);
				Message message = Message.Create(this.Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
				PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref message, this.Wnd.window.Handle, false);
				paintEventArgs.Graphics.SetClip(new Rectangle(rect.X + menuOrigin.X, rect.Y + menuOrigin.Y, rect.Width, rect.Height));
				this.Draw(paintEventArgs, base.Rect);
				XplatUI.PaintEventEnd(ref message, this.Wnd.window.Handle, false);
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000311D8 File Offset: 0x0002F3D8
		internal void Draw(PaintEventArgs pe, Rectangle rect)
		{
			if (!this.Wnd.IsHandleCreated)
			{
				return;
			}
			base.X = rect.X;
			base.Y = rect.Y;
			base.Height = base.Rect.Height;
			ThemeEngine.Current.DrawMenuBar(pe.Graphics, this, rect);
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[MainMenu.PaintEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, pe);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00031254 File Offset: 0x0002F454
		internal override void InvalidateItem(MenuItem item)
		{
			this.Draw(item.bounds);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00031262 File Offset: 0x0002F462
		internal void SetForm(Form form)
		{
			this.form = form;
			this.Wnd = form;
			if (this.tracker == null)
			{
				this.tracker = new MenuTracker(this);
				this.tracker.GrabControl = form;
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00031294 File Offset: 0x0002F494
		internal override void OnMenuChanged(EventArgs e)
		{
			base.OnMenuChanged(EventArgs.Empty);
			if (this.form == null)
			{
				return;
			}
			Rectangle rect = base.Rect;
			base.Height = 0;
			if (!this.Wnd.IsHandleCreated)
			{
				return;
			}
			Message message = Message.Create(this.Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref message, this.Wnd.window.Handle, false);
			paintEventArgs.Graphics.SetClip(rect);
			this.Draw(paintEventArgs, rect);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00031320 File Offset: 0x0002F520
		internal void OnMouseDown(object window, MouseEventArgs args)
		{
			this.tracker.OnMouseDown(args);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00031330 File Offset: 0x0002F530
		internal void OnMouseMove(object window, MouseEventArgs e)
		{
			MouseEventArgs mouseEventArgs = new MouseEventArgs(e.Button, e.Clicks, Control.MousePosition.X, Control.MousePosition.Y, e.Delta);
			this.tracker.OnMotion(mouseEventArgs);
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000B8D RID: 2957 RVA: 0x0003137B File Offset: 0x0002F57B
		// (remove) Token: 0x06000B8E RID: 2958 RVA: 0x0003138E File Offset: 0x0002F58E
		internal event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(MainMenu.PaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MainMenu.PaintEvent, value);
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000313A1 File Offset: 0x0002F5A1
		// Note: this type is marked as 'beforefieldinit'.
		static MainMenu()
		{
			MainMenu.PaintEvent = new object();
		}

		// Token: 0x04000779 RID: 1913
		private RightToLeft right_to_left = RightToLeft.Inherit;

		// Token: 0x0400077A RID: 1914
		private Form form;

		// Token: 0x0400077B RID: 1915
		private static object CollapseEvent = new object();
	}
}
