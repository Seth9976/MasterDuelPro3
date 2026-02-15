using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000043 RID: 67
	public class TaskDialogButton : TaskDialogItem
	{
		// Token: 0x0600018F RID: 399 RVA: 0x000079E6 File Offset: 0x00005BE6
		public TaskDialogButton()
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000079F0 File Offset: 0x00005BF0
		public TaskDialogButton(ButtonType type)
			: base((int)type)
		{
			this._type = type;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007A02 File Offset: 0x00005C02
		public TaskDialogButton(IContainer container)
			: base(container)
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007A0D File Offset: 0x00005C0D
		public TaskDialogButton(string text)
		{
			base.Text = text;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007A20 File Offset: 0x00005C20
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00007A38 File Offset: 0x00005C38
		[Category("Appearance")]
		[Description("The type of the button.")]
		[DefaultValue(ButtonType.Custom)]
		public ButtonType ButtonType
		{
			get
			{
				return this._type;
			}
			set
			{
				bool flag = value > ButtonType.Custom;
				if (flag)
				{
					this.CheckDuplicateButton(value, null);
					this._type = value;
					base.Id = (int)value;
				}
				else
				{
					this._type = value;
					this.AutoAssignId();
					base.UpdateOwner();
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007A84 File Offset: 0x00005C84
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00007AA5 File Offset: 0x00005CA5
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the note associated with a command link button.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string CommandLinkNote
		{
			get
			{
				return this._commandLinkNote ?? string.Empty;
			}
			set
			{
				this._commandLinkNote = value;
				base.UpdateOwner();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007AB8 File Offset: 0x00005CB8
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00007AD0 File Offset: 0x00005CD0
		[Category("Behavior")]
		[Description("Indicates if the button is the default button on the dialog.")]
		[DefaultValue(false)]
		public bool Default
		{
			get
			{
				return this._default;
			}
			set
			{
				this._default = value;
				bool flag = value && base.Owner != null;
				if (flag)
				{
					foreach (TaskDialogButton taskDialogButton in base.Owner.Buttons)
					{
						bool flag2 = taskDialogButton != this;
						if (flag2)
						{
							taskDialogButton.Default = false;
						}
					}
				}
				base.UpdateOwner();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007B58 File Offset: 0x00005D58
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00007B70 File Offset: 0x00005D70
		[Category("Behavior")]
		[Description("Indicates whether the Task Dialog button or command link should have a User Account Control (UAC) shield icon (in other words, whether the action invoked by the button requires elevation).")]
		[DefaultValue(false)]
		public bool ElevationRequired
		{
			get
			{
				return this._elevationRequired;
			}
			set
			{
				this._elevationRequired = value;
				bool flag = base.Owner != null;
				if (flag)
				{
					base.Owner.SetButtonElevationRequired(this);
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007BA0 File Offset: 0x00005DA0
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00007BB8 File Offset: 0x00005DB8
		internal override int Id
		{
			get
			{
				return base.Id;
			}
			set
			{
				bool flag = base.Id != value;
				if (flag)
				{
					bool flag2 = this._type > ButtonType.Custom;
					if (flag2)
					{
						throw new InvalidOperationException(Resources.NonCustomTaskDialogButtonIdError);
					}
					base.Id = value;
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007BF8 File Offset: 0x00005DF8
		internal override void AutoAssignId()
		{
			bool flag = this._type == ButtonType.Custom;
			if (flag)
			{
				base.AutoAssignId();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007C1A File Offset: 0x00005E1A
		internal override void CheckDuplicate(TaskDialogItem itemToExclude)
		{
			this.CheckDuplicateButton(this._type, itemToExclude);
			base.CheckDuplicate(itemToExclude);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007C34 File Offset: 0x00005E34
		internal NativeMethods.TaskDialogCommonButtonFlags ButtonFlag
		{
			get
			{
				switch (this._type)
				{
				case ButtonType.Ok:
					return NativeMethods.TaskDialogCommonButtonFlags.OkButton;
				case ButtonType.Cancel:
					return NativeMethods.TaskDialogCommonButtonFlags.CancelButton;
				case ButtonType.Retry:
					return NativeMethods.TaskDialogCommonButtonFlags.RetryButton;
				case ButtonType.Yes:
					return NativeMethods.TaskDialogCommonButtonFlags.YesButton;
				case ButtonType.No:
					return NativeMethods.TaskDialogCommonButtonFlags.NoButton;
				case ButtonType.Close:
					return NativeMethods.TaskDialogCommonButtonFlags.CloseButton;
				}
				return (NativeMethods.TaskDialogCommonButtonFlags)0;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007C94 File Offset: 0x00005E94
		protected override IEnumerable ItemCollection
		{
			get
			{
				bool flag = base.Owner != null;
				IEnumerable enumerable;
				if (flag)
				{
					enumerable = base.Owner.Buttons;
				}
				else
				{
					enumerable = null;
				}
				return enumerable;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007CC4 File Offset: 0x00005EC4
		private void CheckDuplicateButton(ButtonType type, TaskDialogItem itemToExclude)
		{
			bool flag = type != ButtonType.Custom && base.Owner != null;
			if (flag)
			{
				foreach (TaskDialogButton taskDialogButton in base.Owner.Buttons)
				{
					bool flag2 = taskDialogButton != this && taskDialogButton != itemToExclude && taskDialogButton.ButtonType == type;
					if (flag2)
					{
						throw new InvalidOperationException(Resources.DuplicateButtonTypeError);
					}
				}
			}
		}

		// Token: 0x040001DF RID: 479
		private ButtonType _type;

		// Token: 0x040001E0 RID: 480
		private bool _elevationRequired;

		// Token: 0x040001E1 RID: 481
		private bool _default;

		// Token: 0x040001E2 RID: 482
		private string _commandLinkNote;
	}
}
