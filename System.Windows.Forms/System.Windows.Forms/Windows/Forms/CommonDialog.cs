using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Specifies the base class used for displaying dialog boxes on the screen.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000048 RID: 72
	[ToolboxItemFilter("System.Windows.Forms")]
	public abstract class CommonDialog : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CommonDialog" /> class.</summary>
		// Token: 0x06000215 RID: 533 RVA: 0x00008B3A File Offset: 0x00006D3A
		public CommonDialog()
		{
		}

		/// <summary>When overridden in a derived class, resets the properties of a common dialog box to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000216 RID: 534
		public abstract void Reset();

		/// <summary>Runs a common dialog box with the specified owner.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel" />.</returns>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000217 RID: 535 RVA: 0x00008B44 File Offset: 0x00006D44
		public DialogResult ShowDialog(IWin32Window owner)
		{
			if (this.form != null)
			{
				if (this.RunDialog(this.form.Handle))
				{
					this.form.ShowDialog(owner);
				}
				return this.form.DialogResult;
			}
			if (this.RunDialog((owner == null) ? IntPtr.Zero : owner.Handle))
			{
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CommonDialog.HelpRequest" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.HelpEventArgs" /> that provides the event data. </param>
		// Token: 0x06000218 RID: 536 RVA: 0x00008BA0 File Offset: 0x00006DA0
		protected virtual void OnHelpRequest(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CommonDialog.HelpRequestEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>When overridden in a derived class, specifies a common dialog box.</summary>
		/// <returns>true if the dialog box was successfully run; otherwise, false.</returns>
		/// <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
		// Token: 0x06000219 RID: 537
		protected abstract bool RunDialog(IntPtr hwndOwner);

		/// <summary>Occurs when the user clicks the Help button on a common dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600021A RID: 538 RVA: 0x00008BCE File Offset: 0x00006DCE
		// (remove) Token: 0x0600021B RID: 539 RVA: 0x00008BE1 File Offset: 0x00006DE1
		public event EventHandler HelpRequest
		{
			add
			{
				base.Events.AddHandler(CommonDialog.HelpRequestEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CommonDialog.HelpRequestEvent, value);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008BF4 File Offset: 0x00006DF4
		// Note: this type is marked as 'beforefieldinit'.
		static CommonDialog()
		{
			CommonDialog.HelpRequestEvent = new object();
		}

		// Token: 0x0400017D RID: 381
		internal CommonDialog.DialogForm form;

		// Token: 0x02000049 RID: 73
		internal class DialogForm : Form
		{
			// Token: 0x0600021D RID: 541 RVA: 0x00008C00 File Offset: 0x00006E00
			internal DialogForm(CommonDialog owner)
			{
				this.owner = owner;
				base.ControlBox = true;
				base.MinimizeBox = false;
				base.MaximizeBox = false;
				base.ShowInTaskbar = false;
				base.FormBorderStyle = FormBorderStyle.Sizable;
				base.StartPosition = FormStartPosition.CenterScreen;
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x0600021E RID: 542 RVA: 0x00008C39 File Offset: 0x00006E39
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style |= -2134376448;
					return createParams;
				}
			}

			// Token: 0x0400017F RID: 383
			protected CommonDialog owner;
		}
	}
}
