using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides focus-management functionality for controls that can function as a container for other controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004A RID: 74
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class ContainerControl : ScrollableControl, IContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContainerControl" /> class.</summary>
		// Token: 0x0600021F RID: 543 RVA: 0x00008C54 File Offset: 0x00006E54
		public ContainerControl()
		{
			this.active_control = null;
			this.unvalidated_control = null;
			base.ControlRemoved += this.OnControlRemoved;
			this.auto_scale_dimensions = SizeF.Empty;
			this.auto_scale_mode = AutoScaleMode.Inherit;
		}

		/// <summary>Gets or sets the active control on the container control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the <see cref="T:System.Windows.Forms.ContainerControl" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.Control" /> assigned could not be activated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008CA7 File Offset: 0x00006EA7
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00008CB0 File Offset: 0x00006EB0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control ActiveControl
		{
			get
			{
				return this.active_control;
			}
			set
			{
				if (value == null || (this.active_control == value && this.active_control.Focused))
				{
					return;
				}
				if (!base.Contains(value))
				{
					throw new ArgumentException("Cannot activate invisible or disabled control.");
				}
				Form form = base.FindForm();
				Control mostDeeplyNestedActiveControl = this.GetMostDeeplyNestedActiveControl((form == null) ? this : form);
				Control commonContainer = this.GetCommonContainer(mostDeeplyNestedActiveControl, value);
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				Control control = mostDeeplyNestedActiveControl;
				bool flag = true;
				Control control2 = commonContainer;
				this.active_control = value;
				while (control != commonContainer && control != null)
				{
					if (control == value)
					{
						control2 = value;
						flag = false;
						break;
					}
					control.FireLeave();
					if (control is ContainerControl)
					{
						((ContainerControl)control).active_control = null;
					}
					if (control.CausesValidation)
					{
						arrayList2.Add(control);
					}
					control = control.Parent;
				}
				Control control3 = null;
				bool flag2;
				if (value == control2)
				{
					flag2 = false;
				}
				else
				{
					flag2 = true;
					control = value;
					while (control != control2 && control != null)
					{
						if (control.CausesValidation)
						{
							flag2 = false;
						}
						control3 = control;
						control = control.Parent;
					}
				}
				Control control4 = this.PerformValidation((form == null) ? this : form, flag2, arrayList2, control3);
				if (control4 != null)
				{
					value = (this.active_control = control4);
					flag = true;
				}
				if (flag)
				{
					control = value;
					while (control != control2 && control != null)
					{
						arrayList.Add(control);
						control = control.Parent;
					}
					if (control2 != null && control == control2 && !(control2 is ContainerControl))
					{
						arrayList.Add(control);
					}
					for (int i = arrayList.Count - 1; i >= 0; i--)
					{
						control = (Control)arrayList[i];
						control.FireEnter();
					}
				}
				control = this;
				Control control5 = this;
				while (control != null)
				{
					if (control.Parent is ContainerControl)
					{
						((ContainerControl)control.Parent).active_control = control5;
						control5 = control.Parent;
					}
					control = control.Parent;
				}
				if (this is Form)
				{
					this.CheckAcceptButton();
				}
				base.ScrollControlIntoView(this.active_control);
				control = this;
				control5 = this;
				while (control != null)
				{
					if (control.Parent is ContainerControl)
					{
						control5 = control.Parent;
					}
					control = control.Parent;
				}
				if (control5.InternalContainsFocus)
				{
					this.SendControlFocus(this.active_control);
				}
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008EE0 File Offset: 0x000070E0
		private Control PerformValidation(ContainerControl top_container, bool postpone_validation, ArrayList validation_chain, Control topmost_under_root)
		{
			this.validation_failed = false;
			if (postpone_validation)
			{
				this.AddValidationChain(top_container, validation_chain);
				return null;
			}
			if (top_container.pending_validation_chain != null)
			{
				int num = top_container.pending_validation_chain.Count - 1;
				if (topmost_under_root == top_container.pending_validation_chain[num])
				{
					top_container.pending_validation_chain.RemoveAt(num);
				}
				this.AddValidationChain(top_container, validation_chain);
				validation_chain = top_container.pending_validation_chain;
				top_container.pending_validation_chain = null;
			}
			for (int i = 0; i < validation_chain.Count; i++)
			{
				if (!this.ValidateControl((Control)validation_chain[i]))
				{
					this.validation_failed = true;
					return (Control)validation_chain[i];
				}
			}
			return null;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008F84 File Offset: 0x00007184
		private void AddValidationChain(ContainerControl top_container, ArrayList validation_chain)
		{
			if (validation_chain.Count == 0)
			{
				return;
			}
			if (top_container.pending_validation_chain == null || top_container.pending_validation_chain.Count == 0)
			{
				top_container.pending_validation_chain = validation_chain;
				return;
			}
			foreach (object obj in validation_chain)
			{
				Control control = (Control)obj;
				if (!top_container.pending_validation_chain.Contains(control))
				{
					top_container.pending_validation_chain.Add(control);
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009014 File Offset: 0x00007214
		private bool ValidateControl(Control c)
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			c.FireValidating(cancelEventArgs);
			if (cancelEventArgs.Cancel)
			{
				return false;
			}
			c.FireValidated();
			return true;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009040 File Offset: 0x00007240
		private Control GetMostDeeplyNestedActiveControl(ContainerControl container)
		{
			Control control = container.ActiveControl;
			while (control is ContainerControl && ((ContainerControl)control).ActiveControl != null)
			{
				control = ((ContainerControl)control).ActiveControl;
			}
			return control;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009078 File Offset: 0x00007278
		private Control GetCommonContainer(Control active_control, Control value)
		{
			for (Control control = active_control; control != null; control = control.Parent)
			{
				for (Control control2 = value.Parent; control2 != null; control2 = control2.Parent)
				{
					if (control2 == control)
					{
						return control2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000090AF File Offset: 0x000072AF
		internal void SendControlFocus(Control c)
		{
			if (c != null && c.IsHandleCreated)
			{
				XplatUI.SetFocus(c.window.Handle);
			}
		}

		/// <summary>Gets the scaling factor between the current and design-time automatic scaling dimensions. </summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the scaling ratio between the current and design-time scaling automatic scaling dimensions.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000090CC File Offset: 0x000072CC
		protected SizeF AutoScaleFactor
		{
			get
			{
				if (this.auto_scale_dimensions.IsEmpty)
				{
					return new SizeF(1f, 1f);
				}
				return new SizeF(this.CurrentAutoScaleDimensions.Width / this.auto_scale_dimensions.Width, this.CurrentAutoScaleDimensions.Height / this.auto_scale_dimensions.Height);
			}
		}

		/// <summary>Gets or sets the automatic scaling mode of the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoScaleMode" /> that represents the current scaling mode. The default is <see cref="F:System.Windows.Forms.AutoScaleMode.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An <see cref="T:System.Windows.Forms.AutoScaleMode" /> value that is not valid was used to set this property.</exception>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000912F File Offset: 0x0000732F
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00009138 File Offset: 0x00007338
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AutoScaleMode AutoScaleMode
		{
			get
			{
				return this.auto_scale_mode;
			}
			set
			{
				if (this is Form)
				{
					(this as Form).AutoScale = false;
				}
				if (this.auto_scale_mode != value)
				{
					this.auto_scale_mode = value;
					if (this.auto_scale_mode_set)
					{
						this.auto_scale_dimensions = SizeF.Empty;
					}
					this.auto_scale_mode_set = true;
					this.PerformAutoScale();
				}
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009189 File Offset: 0x00007389
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000091A4 File Offset: 0x000073A4
		[Browsable(false)]
		public override BindingContext BindingContext
		{
			get
			{
				if (base.BindingContext == null)
				{
					base.BindingContext = new BindingContext();
				}
				return base.BindingContext;
			}
			set
			{
				base.BindingContext = value;
			}
		}

		/// <summary>Gets the current run-time dimensions of the screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the current dots per inch (DPI) or <see cref="T:System.Drawing.Font" /> size of the screen.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A Win32 device context could not be created for the current screen.</exception>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000091B0 File Offset: 0x000073B0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SizeF CurrentAutoScaleDimensions
		{
			get
			{
				AutoScaleMode autoScaleMode = this.auto_scale_mode;
				if (autoScaleMode == AutoScaleMode.Font)
				{
					Size size = TextRenderer.MeasureText("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890", this.Font);
					return new SizeF((float)((int)Math.Round((double)((float)size.Width / 62f))), (float)size.Height);
				}
				if (autoScaleMode == AutoScaleMode.Dpi)
				{
					return TextRenderer.GetDpi();
				}
				return this.auto_scale_dimensions;
			}
		}

		/// <summary>Gets the form that the container control is assigned to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the container control is assigned to. This property will return null if the control is hosted inside of Internet Explorer or in another hosting context where there is no parent form. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009210 File Offset: 0x00007410
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Form ParentForm
		{
			get
			{
				for (Control control = base.Parent; control != null; control = control.Parent)
				{
					if (control is Form)
					{
						return (Form)control;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009240 File Offset: 0x00007440
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009248 File Offset: 0x00007448
		internal void PerformAutoScale(bool called_by_scale)
		{
			if (this.AutoScaleMode == AutoScaleMode.Inherit && !called_by_scale)
			{
				return;
			}
			if (this.layout_suspended > 0 && !called_by_scale)
			{
				this.auto_scale_pending = true;
				return;
			}
			this.auto_scale_pending = false;
			SizeF sizeF = this.AutoScaleFactor;
			if (this.AutoScaleMode == AutoScaleMode.Inherit)
			{
				ContainerControl containerControl = base.FindContainer(base.Parent);
				if (containerControl != null)
				{
					sizeF = containerControl.AutoScaleFactor;
				}
			}
			if (sizeF != new SizeF(1f, 1f))
			{
				this.is_auto_scaling = true;
				base.SuspendLayout();
				base.Scale(sizeF);
				base.ResumeLayout(false);
				this.is_auto_scaling = false;
			}
			this.auto_scale_dimensions = this.CurrentAutoScaleDimensions;
		}

		/// <summary>Performs scaling of the container control and its children.</summary>
		// Token: 0x06000231 RID: 561 RVA: 0x000092E9 File Offset: 0x000074E9
		public void PerformAutoScale()
		{
			this.PerformAutoScale(false);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000092F2 File Offset: 0x000074F2
		internal void PerformDelayedAutoScale()
		{
			if (this.auto_scale_pending)
			{
				this.PerformAutoScale();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00009302 File Offset: 0x00007502
		internal bool IsAutoScaling
		{
			get
			{
				return this.is_auto_scaling;
			}
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data. </summary>
		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		// Token: 0x06000234 RID: 564 RVA: 0x0000930A File Offset: 0x0000750A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ValidateChildren()
		{
			return this.ValidateChildren(ValidationConstraints.Selectable);
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data. </summary>
		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		// Token: 0x06000235 RID: 565 RVA: 0x00009314 File Offset: 0x00007514
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			bool flag = (validationConstraints & ValidationConstraints.ImmediateChildren) != ValidationConstraints.ImmediateChildren;
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (!this.ValidateNestedControls(control, validationConstraints, flag))
				{
					return false;
				}
			}
			return true;
		}

		/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
		// Token: 0x06000236 RID: 566 RVA: 0x00009388 File Offset: 0x00007588
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars(displayScrollbars);
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000237 RID: 567 RVA: 0x000046A0 File Offset: 0x000028A0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009391 File Offset: 0x00007591
		private void OnControlRemoved(object sender, ControlEventArgs e)
		{
			if (e.Control == this.unvalidated_control)
			{
				this.unvalidated_control = null;
			}
			if (e.Control == this.active_control)
			{
				this.unvalidated_control = null;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000093BD File Offset: 0x000075BD
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.OnBindingContextChanged(EventArgs.Empty);
		}

		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x0600023A RID: 570 RVA: 0x000093D0 File Offset: 0x000075D0
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return ToolStripManager.ProcessCmdKey(ref msg, keyData) || base.ProcessCmdKey(ref msg, keyData);
		}

		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x0600023B RID: 571 RVA: 0x000093E5 File Offset: 0x000075E5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override bool ProcessDialogChar(char charCode)
		{
			return (base.GetTopLevel() && this.ProcessMnemonic(charCode)) || base.ProcessDialogChar(charCode);
		}

		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x0600023C RID: 572 RVA: 0x00009404 File Offset: 0x00007604
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			bool flag = true;
			if (keys != Keys.Tab)
			{
				switch (keys)
				{
				case Keys.Left:
					flag = false;
					break;
				case Keys.Up:
					flag = false;
					break;
				case Keys.Right:
				case Keys.Down:
					break;
				default:
					goto IL_0065;
				}
				if (base.SelectNextControl(this.active_control, flag, false, false, true))
				{
					return true;
				}
			}
			else if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None && this.ProcessTabKey((Control.ModifierKeys & Keys.Shift) == Keys.None))
			{
				return true;
			}
			IL_0065:
			return base.ProcessDialogKey(keyData);
		}

		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x0600023D RID: 573 RVA: 0x00009480 File Offset: 0x00007680
		protected override bool ProcessMnemonic(char charCode)
		{
			bool flag = false;
			Control nextControl = this.active_control;
			for (;;)
			{
				nextControl = base.GetNextControl(nextControl, true);
				if (nextControl != null)
				{
					if (nextControl.ProcessControlMnemonic(charCode))
					{
						break;
					}
				}
				else
				{
					if (flag)
					{
						return false;
					}
					flag = true;
				}
				if (nextControl == this.active_control)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Selects the next available control and makes it the active control.</summary>
		/// <returns>true if a control is selected; otherwise, false.</returns>
		/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, false. </param>
		// Token: 0x0600023E RID: 574 RVA: 0x000094BC File Offset: 0x000076BC
		protected virtual bool ProcessTabKey(bool forward)
		{
			return base.SelectNextControl(this.active_control, forward, true, true, false);
		}

		/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
		// Token: 0x0600023F RID: 575 RVA: 0x000094D0 File Offset: 0x000076D0
		protected override void Select(bool directed, bool forward)
		{
			if (base.Parent != null)
			{
				IContainerControl containerControl = base.Parent.GetContainerControl();
				if (containerControl != null)
				{
					containerControl.ActiveControl = this;
				}
			}
			if (directed && this.auto_select_child)
			{
				base.SelectNextControl(null, forward, true, true, false);
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000240 RID: 576 RVA: 0x00009514 File Offset: 0x00007714
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_SETFOCUS)
			{
				base.WndProc(ref m);
				return;
			}
			if (this.active_control != null)
			{
				base.Select(this.active_control);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009554 File Offset: 0x00007754
		internal void ChildControlRemoved(Control control)
		{
			ContainerControl containerControl = base.FindForm();
			if (containerControl == null)
			{
				containerControl = this;
			}
			ArrayList arrayList = containerControl.pending_validation_chain;
			if (arrayList != null)
			{
				this.RemoveChildrenFromValidation(arrayList, control);
				if (arrayList.Count == 0)
				{
					containerControl.pending_validation_chain = null;
				}
			}
			if (control == this.active_control || control.Contains(this.active_control))
			{
				base.SelectNextControl(this, true, true, true, true);
				if (control == this.active_control || control.Contains(this.active_control))
				{
					this.active_control = null;
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000095D0 File Offset: 0x000077D0
		private bool RemoveChildrenFromValidation(ArrayList validation_chain, Control c)
		{
			if (this.RemoveFromValidationChain(validation_chain, c))
			{
				return true;
			}
			foreach (object obj in c.Controls)
			{
				Control control = (Control)obj;
				if (this.RemoveChildrenFromValidation(validation_chain, control))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009640 File Offset: 0x00007840
		private bool RemoveFromValidationChain(ArrayList validation_chain, Control c)
		{
			int num = validation_chain.IndexOf(c);
			if (num > -1)
			{
				this.pending_validation_chain.RemoveAt(num--);
				return true;
			}
			return false;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void CheckAcceptButton()
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000966C File Offset: 0x0000786C
		private bool ValidateNestedControls(Control c, ValidationConstraints constraints, bool recurse)
		{
			bool flag = true;
			if (!c.CausesValidation)
			{
				flag = true;
			}
			else if (!this.ValidateThisControl(c, constraints))
			{
				flag = true;
			}
			else if (!this.ValidateControl(c))
			{
				flag = false;
			}
			if (recurse)
			{
				foreach (object obj in c.Controls)
				{
					Control control = (Control)obj;
					if (!this.ValidateNestedControls(control, constraints, recurse))
					{
						return false;
					}
				}
				return flag;
			}
			return flag;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009700 File Offset: 0x00007900
		private bool ValidateThisControl(Control c, ValidationConstraints constraints)
		{
			return constraints == ValidationConstraints.None || (((constraints & ValidationConstraints.Enabled) != ValidationConstraints.Enabled || c.Enabled) && ((constraints & ValidationConstraints.Selectable) != ValidationConstraints.Selectable || c.GetStyle(ControlStyles.Selectable)) && ((constraints & ValidationConstraints.TabStop) != ValidationConstraints.TabStop || c.TabStop) && ((constraints & ValidationConstraints.Visible) != ValidationConstraints.Visible || c.Visible));
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000247 RID: 583 RVA: 0x0000488F File Offset: 0x00002A8F
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000248 RID: 584 RVA: 0x00009758 File Offset: 0x00007958
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.AutoScaleMode == AutoScaleMode.Font)
			{
				this.PerformAutoScale();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x06000249 RID: 585 RVA: 0x00009770 File Offset: 0x00007970
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
		}

		// Token: 0x04000180 RID: 384
		private Control active_control;

		// Token: 0x04000181 RID: 385
		private Control unvalidated_control;

		// Token: 0x04000182 RID: 386
		private ArrayList pending_validation_chain;

		// Token: 0x04000183 RID: 387
		internal bool auto_select_child = true;

		// Token: 0x04000184 RID: 388
		private SizeF auto_scale_dimensions;

		// Token: 0x04000185 RID: 389
		private AutoScaleMode auto_scale_mode;

		// Token: 0x04000186 RID: 390
		private bool auto_scale_mode_set;

		// Token: 0x04000187 RID: 391
		private bool auto_scale_pending;

		// Token: 0x04000188 RID: 392
		private bool is_auto_scaling;

		// Token: 0x04000189 RID: 393
		internal bool validation_failed;

		// Token: 0x0400018A RID: 394
		private AutoValidate auto_validate = AutoValidate.Inherit;

		// Token: 0x0400018B RID: 395
		private static object OnValidateChanged = new object();
	}
}
