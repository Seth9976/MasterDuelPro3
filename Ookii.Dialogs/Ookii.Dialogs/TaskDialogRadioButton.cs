using System;
using System.Collections;
using System.ComponentModel;

namespace Ookii.Dialogs
{
	// Token: 0x0200004A RID: 74
	public class TaskDialogRadioButton : TaskDialogItem
	{
		// Token: 0x060001BF RID: 447 RVA: 0x000079E6 File Offset: 0x00005BE6
		public TaskDialogRadioButton()
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007A02 File Offset: 0x00005C02
		public TaskDialogRadioButton(IContainer container)
			: base(container)
		{
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000833C File Offset: 0x0000653C
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00008354 File Offset: 0x00006554
		[Category("Appearance")]
		[Description("Indicates whether the radio button is checked.")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this._checked;
			}
			set
			{
				this._checked = value;
				bool flag = value && base.Owner != null;
				if (flag)
				{
					foreach (TaskDialogRadioButton taskDialogRadioButton in base.Owner.RadioButtons)
					{
						bool flag2 = taskDialogRadioButton != this;
						if (flag2)
						{
							taskDialogRadioButton.Checked = false;
						}
					}
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000083D4 File Offset: 0x000065D4
		protected override IEnumerable ItemCollection
		{
			get
			{
				bool flag = base.Owner != null;
				IEnumerable enumerable;
				if (flag)
				{
					enumerable = base.Owner.RadioButtons;
				}
				else
				{
					enumerable = null;
				}
				return enumerable;
			}
		}

		// Token: 0x040001F4 RID: 500
		private bool _checked;
	}
}
