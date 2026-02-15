using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	/// <summary>Manages a list of <see cref="T:System.Windows.Forms.Binding" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005D RID: 93
	[DefaultMember("Item")]
	public class CurrencyManager : BindingManagerBase
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00011183 File Offset: 0x0000F383
		internal CurrencyManager(object data_source)
		{
			this.SetDataSource(data_source);
		}

		/// <summary>Gets the list for this <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that contains the list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00011192 File Offset: 0x0000F392
		public IList List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the current item in the list.</summary>
		/// <returns>A list item of type <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001119A File Offset: 0x0000F39A
		public override object Current
		{
			get
			{
				if (this.listposition == -1 || this.listposition >= this.list.Count)
				{
					throw new IndexOutOfRangeException("list position");
				}
				return this.list[this.listposition];
			}
		}

		/// <summary>Gets the number of items in the list.</summary>
		/// <returns>The number of items in the list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000111D4 File Offset: 0x0000F3D4
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the position you are at within the list.</summary>
		/// <returns>A number between 0 and <see cref="P:System.Windows.Forms.CurrencyManager.Count" /> minus 1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000111E1 File Offset: 0x0000F3E1
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x000111EC File Offset: 0x0000F3EC
		public override int Position
		{
			get
			{
				return this.listposition;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (value >= this.list.Count)
				{
					value = this.list.Count - 1;
				}
				if (this.listposition == value)
				{
					return;
				}
				if (this.listposition != -1)
				{
					this.EndCurrentEdit();
				}
				this.listposition = value;
				this.OnCurrentChanged(EventArgs.Empty);
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011254 File Offset: 0x0000F454
		internal void SetDataSource(object data_source)
		{
			if (this.data_source is IBindingList)
			{
				((IBindingList)this.data_source).ListChanged -= this.ListChangedHandler;
			}
			if (data_source is IListSource)
			{
				data_source = ((IListSource)data_source).GetList();
			}
			this.data_source = data_source;
			if (data_source != null)
			{
				this.finalType = data_source.GetType();
			}
			this.listposition = -1;
			if (this.data_source is IBindingList)
			{
				((IBindingList)this.data_source).ListChanged += this.ListChangedHandler;
			}
			this.list = (IList)data_source;
			this.ListChangedHandler(null, new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		/// <summary>Gets the property descriptor collection for the underlying list.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for the list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600047D RID: 1149 RVA: 0x000112FF File Offset: 0x0000F4FF
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return ListBindingHelper.GetListItemProperties(this.list);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001130C File Offset: 0x0000F50C
		internal override bool IsSuspended
		{
			get
			{
				return this.Count == 0 || this.binding_suspended;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00011320 File Offset: 0x0000F520
		private void BeginEdit()
		{
			IEditableObject editableObject = this.Current as IEditableObject;
			if (editableObject != null)
			{
				try
				{
					editableObject.BeginEdit();
					this.editing = true;
				}
				catch
				{
				}
			}
		}

		/// <summary>Ends the current edit operation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000480 RID: 1152 RVA: 0x00011360 File Offset: 0x0000F560
		public override void EndCurrentEdit()
		{
			if (this.listposition == -1)
			{
				return;
			}
			IEditableObject editableObject = this.Current as IEditableObject;
			if (editableObject != null)
			{
				this.editing = false;
				editableObject.EndEdit();
			}
			if (this.list is ICancelAddNew)
			{
				((ICancelAddNew)this.list).EndNew(this.listposition);
			}
		}

		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000481 RID: 1153 RVA: 0x000113B6 File Offset: 0x0000F5B6
		protected internal override void OnCurrentChanged(EventArgs e)
		{
			if (this.onCurrentChangedHandler != null)
			{
				this.onCurrentChangedHandler(this, e);
			}
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, e);
			}
		}

		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000482 RID: 1154 RVA: 0x000113E2 File Offset: 0x0000F5E2
		protected override void OnCurrentItemChanged(EventArgs e)
		{
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000483 RID: 1155 RVA: 0x000113F9 File Offset: 0x0000F5F9
		protected virtual void OnItemChanged(ItemChangedEventArgs e)
		{
			if (this.ItemChanged != null)
			{
				this.ItemChanged(this, e);
			}
			this.transfering_data = true;
			base.PushData();
			this.transfering_data = false;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00011424 File Offset: 0x0000F624
		private void OnListChanged(ListChangedEventArgs args)
		{
			if (this.ListChanged != null)
			{
				this.ListChanged(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000485 RID: 1157 RVA: 0x0001143B File Offset: 0x0000F63B
		protected virtual void OnPositionChanged(EventArgs e)
		{
			if (this.onPositionChangedHandler != null)
			{
				this.onPositionChangedHandler(this, e);
			}
		}

		/// <summary>Updates the status of the binding.</summary>
		// Token: 0x06000486 RID: 1158 RVA: 0x00011454 File Offset: 0x0000F654
		protected override void UpdateIsBinding()
		{
			this.UpdateItem();
			foreach (object obj in base.Bindings)
			{
				((Binding)obj).UpdateIsBinding();
			}
			this.ChangeRecordState(this.listposition, false, false, true, false);
			this.OnItemChanged(new ItemChangedEventArgs(-1));
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000114CC File Offset: 0x0000F6CC
		private void ChangeRecordState(int newPosition, bool validating, bool endCurrentEdit, bool firePositionChanged, bool pullData)
		{
			if (endCurrentEdit)
			{
				this.EndCurrentEdit();
			}
			int num = this.listposition;
			this.listposition = newPosition;
			if (this.listposition >= this.list.Count)
			{
				this.listposition = this.list.Count - 1;
			}
			if (num != -1 && this.listposition != -1)
			{
				this.OnCurrentChanged(EventArgs.Empty);
			}
			if (firePositionChanged)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001153B File Offset: 0x0000F73B
		private void UpdateItem()
		{
			if (!this.transfering_data && this.listposition == -1 && this.list.Count > 0)
			{
				this.listposition = 0;
				this.BeginEdit();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.MetaDataChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000489 RID: 1161 RVA: 0x00011569 File Offset: 0x0000F769
		protected void OnMetaDataChanged(EventArgs e)
		{
			if (this.MetaDataChanged != null)
			{
				this.MetaDataChanged(this, e);
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011580 File Offset: 0x0000F780
		private void ListChangedHandler(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				base.PushData();
				this.UpdateIsBinding();
				this.OnListChanged(e);
				return;
			case ListChangedType.ItemAdded:
				if (this.list.Count == 1)
				{
					this.ChangeRecordState(e.NewIndex, false, false, true, false);
					this.OnItemChanged(new ItemChangedEventArgs(-1));
					this.OnListChanged(e);
					return;
				}
				if (e.NewIndex <= this.listposition)
				{
					this.ChangeRecordState(this.listposition + 1, false, false, false, false);
					this.OnItemChanged(new ItemChangedEventArgs(-1));
					this.OnListChanged(e);
					this.OnPositionChanged(EventArgs.Empty);
					return;
				}
				this.OnItemChanged(new ItemChangedEventArgs(-1));
				this.OnListChanged(e);
				return;
			case ListChangedType.ItemDeleted:
				if (this.list.Count == 0)
				{
					this.listposition = -1;
					this.UpdateIsBinding();
					this.OnPositionChanged(EventArgs.Empty);
					this.OnCurrentChanged(EventArgs.Empty);
				}
				else if (e.NewIndex <= this.listposition)
				{
					this.ChangeRecordState(e.NewIndex, false, false, e.NewIndex != this.listposition, false);
				}
				this.OnItemChanged(new ItemChangedEventArgs(-1));
				this.OnListChanged(e);
				return;
			case ListChangedType.ItemChanged:
				if (this.editing)
				{
					if (e.NewIndex == this.listposition)
					{
						this.OnCurrentItemChanged(EventArgs.Empty);
					}
					this.OnItemChanged(new ItemChangedEventArgs(e.NewIndex));
				}
				this.OnListChanged(e);
				return;
			case ListChangedType.PropertyDescriptorAdded:
			case ListChangedType.PropertyDescriptorDeleted:
			case ListChangedType.PropertyDescriptorChanged:
				this.OnMetaDataChanged(EventArgs.Empty);
				this.OnListChanged(e);
				return;
			}
			this.OnListChanged(e);
		}

		/// <summary>Occurs when the current item has been altered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600048B RID: 1163 RVA: 0x00011720 File Offset: 0x0000F920
		// (remove) Token: 0x0600048C RID: 1164 RVA: 0x00011758 File Offset: 0x0000F958
		public event ItemChangedEventHandler ItemChanged;

		/// <summary>Specifies the current position of the <see cref="T:System.Windows.Forms.CurrencyManager" /> in the list.</summary>
		// Token: 0x04000253 RID: 595
		protected int listposition;

		/// <summary>Specifies the data type of the list.</summary>
		// Token: 0x04000254 RID: 596
		protected Type finalType;

		// Token: 0x04000255 RID: 597
		private IList list;

		// Token: 0x04000256 RID: 598
		private bool binding_suspended;

		// Token: 0x04000257 RID: 599
		private object data_source;

		// Token: 0x04000258 RID: 600
		private bool editing;

		// Token: 0x04000259 RID: 601
		[CompilerGenerated]
		private ListChangedEventHandler ListChanged;

		// Token: 0x0400025B RID: 603
		[CompilerGenerated]
		private EventHandler MetaDataChanged;
	}
}
