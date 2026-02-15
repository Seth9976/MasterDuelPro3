using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a shortcut menu. Although <see cref="T:System.Windows.Forms.ContextMenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ContextMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.ContextMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004B RID: 75
	[DefaultEvent("Popup")]
	public class ContextMenu : Menu
	{
		/// <summary>Occurs before the shortcut menu is displayed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600024B RID: 587 RVA: 0x00009785 File Offset: 0x00007985
		// (remove) Token: 0x0600024C RID: 588 RVA: 0x00009798 File Offset: 0x00007998
		public event EventHandler Popup
		{
			add
			{
				base.Events.AddHandler(ContextMenu.PopupEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContextMenu.PopupEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with no menu items specified.</summary>
		// Token: 0x0600024D RID: 589 RVA: 0x000097AB File Offset: 0x000079AB
		public ContextMenu()
			: base(null)
		{
			this.tracker = new MenuTracker(this);
			this.right_to_left = RightToLeft.Inherit;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
		/// <param name="menuItems">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that represent the menu items to add to the shortcut menu. </param>
		// Token: 0x0600024E RID: 590 RVA: 0x000097C7 File Offset: 0x000079C7
		public ContextMenu(MenuItem[] menuItems)
			: base(menuItems)
		{
			this.tracker = new MenuTracker(this);
			this.right_to_left = RightToLeft.Inherit;
		}

		/// <summary>Gets the control that is displaying the shortcut menu.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control that is displaying the shortcut menu. If no control has displayed the shortcut menu, the property returns null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000097E3 File Offset: 0x000079E3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control SourceControl
		{
			get
			{
				return this.src_control;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Collapse" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000250 RID: 592 RVA: 0x000097EC File Offset: 0x000079EC
		protected internal virtual void OnCollapse(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ContextMenu.CollapseEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Popup" /> event </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000251 RID: 593 RVA: 0x0000981C File Offset: 0x00007A1C
		protected internal virtual void OnPopup(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ContextMenu.PopupEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Displays the shortcut menu at the specified position.</summary>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control with which this shortcut menu is associated. </param>
		/// <param name="pos">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates at which to display the menu. These coordinates are specified relative to the client coordinates of the control specified in the <paramref name="control" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="control" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The handle of the control does not exist or the control is not visible.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000252 RID: 594 RVA: 0x0000984A File Offset: 0x00007A4A
		public void Show(Control control, Point pos)
		{
			if (control == null)
			{
				throw new ArgumentException();
			}
			this.src_control = control;
			this.OnPopup(EventArgs.Empty);
			pos = control.PointToScreen(pos);
			MenuTracker.TrackPopupMenu(this, pos);
			this.OnCollapse(EventArgs.Empty);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009883 File Offset: 0x00007A83
		// Note: this type is marked as 'beforefieldinit'.
		static ContextMenu()
		{
			ContextMenu.PopupEvent = new object();
		}

		// Token: 0x0400018C RID: 396
		private RightToLeft right_to_left;

		// Token: 0x0400018D RID: 397
		private Control src_control;

		// Token: 0x0400018E RID: 398
		private static object CollapseEvent = new object();
	}
}
