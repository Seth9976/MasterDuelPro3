using System;
using System.Collections;
using System.ComponentModel;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000047 RID: 71
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	public abstract class TaskDialogItem : Component
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00007D9F File Offset: 0x00005F9F
		protected TaskDialogItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007DC0 File Offset: 0x00005FC0
		protected TaskDialogItem(IContainer container)
		{
			bool flag = container != null;
			if (flag)
			{
				container.Add(this);
			}
			this.InitializeComponent();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007DFA File Offset: 0x00005FFA
		internal TaskDialogItem(int id)
		{
			this.InitializeComponent();
			this._id = id;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007E20 File Offset: 0x00006020
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00007E38 File Offset: 0x00006038
		[Browsable(false)]
		public TaskDialog Owner
		{
			get
			{
				return this._owner;
			}
			internal set
			{
				this._owner = value;
				this.AutoAssignId();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007E4C File Offset: 0x0000604C
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007E6D File Offset: 0x0000606D
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the item.")]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
				this.UpdateOwner();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007E80 File Offset: 0x00006080
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007E98 File Offset: 0x00006098
		[Category("Behavior")]
		[Description("Indicates whether the item is enabled.")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
				bool flag = this.Owner != null;
				if (flag)
				{
					this.Owner.SetItemEnabled(this);
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007ECC File Offset: 0x000060CC
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00007EE4 File Offset: 0x000060E4
		[Category("Data")]
		[Description("The id of the item.")]
		[DefaultValue(0)]
		internal virtual int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.CheckDuplicateId(null, value);
				this._id = value;
				this.UpdateOwner();
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007F00 File Offset: 0x00006100
		public void Click()
		{
			bool flag = this.Owner == null;
			if (flag)
			{
				throw new InvalidOperationException(Resources.NoAssociatedTaskDialogError);
			}
			this.Owner.ClickItem(this);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B1 RID: 433
		protected abstract IEnumerable ItemCollection { get; }

		// Token: 0x060001B2 RID: 434 RVA: 0x00007F34 File Offset: 0x00006134
		protected void UpdateOwner()
		{
			bool flag = this.Owner != null;
			if (flag)
			{
				this.Owner.UpdateDialog();
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007F5B File Offset: 0x0000615B
		internal virtual void CheckDuplicate(TaskDialogItem itemToExclude)
		{
			this.CheckDuplicateId(itemToExclude, this._id);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007F6C File Offset: 0x0000616C
		internal virtual void AutoAssignId()
		{
			bool flag = this.ItemCollection != null;
			if (flag)
			{
				int num = 9;
				foreach (object obj in this.ItemCollection)
				{
					TaskDialogItem taskDialogItem = (TaskDialogItem)obj;
					bool flag2 = taskDialogItem.Id > num;
					if (flag2)
					{
						num = taskDialogItem.Id;
					}
				}
				this.Id = num + 1;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007FF8 File Offset: 0x000061F8
		private void CheckDuplicateId(TaskDialogItem itemToExclude, int id)
		{
			bool flag = id != 0;
			if (flag)
			{
				IEnumerable itemCollection = this.ItemCollection;
				bool flag2 = itemCollection != null;
				if (flag2)
				{
					foreach (object obj in itemCollection)
					{
						TaskDialogItem taskDialogItem = (TaskDialogItem)obj;
						bool flag3 = taskDialogItem != this && taskDialogItem != itemToExclude && taskDialogItem.Id == id;
						if (flag3)
						{
							throw new InvalidOperationException(Resources.DuplicateItemIdError);
						}
					}
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008094 File Offset: 0x00006294
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.components != null;
				if (flag)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000080E4 File Offset: 0x000062E4
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x040001ED RID: 493
		private TaskDialog _owner;

		// Token: 0x040001EE RID: 494
		private int _id;

		// Token: 0x040001EF RID: 495
		private bool _enabled = true;

		// Token: 0x040001F0 RID: 496
		private string _text;

		// Token: 0x040001F1 RID: 497
		private IContainer components = null;
	}
}
