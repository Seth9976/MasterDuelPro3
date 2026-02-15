using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Maintains a <see cref="T:System.Windows.Forms.Binding" /> between an object's property and a data-bound control property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016A RID: 362
	public class PropertyManager : BindingManagerBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyManager" /> class.</summary>
		// Token: 0x06000DEF RID: 3567 RVA: 0x0003F06E File Offset: 0x0003D26E
		public PropertyManager()
		{
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003F076 File Offset: 0x0003D276
		internal PropertyManager(object data_source)
		{
			this.SetDataSource(data_source);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003F088 File Offset: 0x0003D288
		internal void SetDataSource(object new_data_source)
		{
			if (this.changed_event != null)
			{
				this.changed_event.RemoveEventHandler(this.data_source, this.property_value_changed_handler);
			}
			this.data_source = new_data_source;
			if (this.property_name != null)
			{
				this.prop_desc = TypeDescriptor.GetProperties(this.data_source).Find(this.property_name, true);
				if (this.prop_desc == null)
				{
					return;
				}
				this.changed_event = TypeDescriptor.GetEvents(this.data_source).Find(this.property_name + "Changed", false);
				if (this.changed_event != null)
				{
					this.property_value_changed_handler = new EventHandler(this.PropertyValueChanged);
					this.changed_event.AddEventHandler(this.data_source, this.property_value_changed_handler);
				}
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003F141 File Offset: 0x0003D341
		private void PropertyValueChanged(object sender, EventArgs args)
		{
			this.OnCurrentChanged(args);
		}

		/// <summary>Gets the object to which the data-bound property belongs.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the object to which the property belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0003F14A File Offset: 0x0003D34A
		public override object Current
		{
			get
			{
				if (this.prop_desc != null)
				{
					return this.prop_desc.GetValue(this.data_source);
				}
				return this.data_source;
			}
		}

		/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0000493C File Offset: 0x00002B3C
		public override int Position
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00006F54 File Offset: 0x00005154
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003F16C File Offset: 0x0003D36C
		public override void EndCurrentEdit()
		{
			base.PullData();
			IEditableObject editableObject = this.data_source as IEditableObject;
			if (editableObject == null)
			{
				return;
			}
			editableObject.EndEdit();
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003F195 File Offset: 0x0003D395
		internal override PropertyDescriptorCollection GetItemPropertiesInternal()
		{
			return TypeDescriptor.GetProperties(this.data_source);
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0003F1A2 File Offset: 0x0003D3A2
		internal override bool IsSuspended
		{
			get
			{
				return this.data_source == null;
			}
		}

		/// <summary>Updates the current <see cref="T:System.Windows.Forms.Binding" /> between a data binding and a data-bound property.</summary>
		// Token: 0x06000DFA RID: 3578 RVA: 0x0000493C File Offset: 0x00002B3C
		[MonoTODO("Stub, does nothing")]
		protected override void UpdateIsBinding()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		/// <param name="ea">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DFB RID: 3579 RVA: 0x0003F1AD File Offset: 0x0003D3AD
		protected internal override void OnCurrentChanged(EventArgs ea)
		{
			base.PushData();
			if (this.onCurrentChangedHandler != null)
			{
				this.onCurrentChangedHandler(this, ea);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
		/// <param name="ea">An <see cref="T:System.EventArgs" /> containing the event data.</param>
		// Token: 0x06000DFC RID: 3580 RVA: 0x00003D19 File Offset: 0x00001F19
		protected override void OnCurrentItemChanged(EventArgs ea)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040008DD RID: 2269
		internal string property_name;

		// Token: 0x040008DE RID: 2270
		private PropertyDescriptor prop_desc;

		// Token: 0x040008DF RID: 2271
		private object data_source;

		// Token: 0x040008E0 RID: 2272
		private EventDescriptor changed_event;

		// Token: 0x040008E1 RID: 2273
		private EventHandler property_value_changed_handler;
	}
}
