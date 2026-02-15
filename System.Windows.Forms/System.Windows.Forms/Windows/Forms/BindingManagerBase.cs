using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Manages all <see cref="T:System.Windows.Forms.Binding" /> objects that are bound to the same data source and data member. This class is abstract.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000020 RID: 32
	public abstract class BindingManagerBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingManagerBase" /> class.</summary>
		// Token: 0x06000097 RID: 151 RVA: 0x00002A07 File Offset: 0x00000C07
		public BindingManagerBase()
		{
		}

		/// <summary>Gets the collection of bindings being managed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects managed by this <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003CF6 File Offset: 0x00001EF6
		public BindingsCollection Bindings
		{
			get
			{
				if (this.bindings == null)
				{
					this.bindings = new BindingsCollection();
				}
				return this.bindings;
			}
		}

		/// <summary>When overridden in a derived class, gets the number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
		/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000099 RID: 153
		public abstract int Count { get; }

		/// <summary>When overridden in a derived class, gets the current object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009A RID: 154
		public abstract object Current { get; }

		/// <summary>When overridden in a derived class, gets or sets the position in the underlying list that controls bound to this data source point to.</summary>
		/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009B RID: 155
		// (set) Token: 0x0600009C RID: 156
		public abstract int Position { get; set; }

		/// <summary>When overridden in a derived class, ends the current edit.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600009D RID: 157
		public abstract void EndCurrentEdit();

		/// <summary>When overridden in a derived class, gets the collection of property descriptors for the binding.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600009E RID: 158 RVA: 0x00003D11 File Offset: 0x00001F11
		public virtual PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemPropertiesInternal();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D19 File Offset: 0x00001F19
		internal virtual PropertyDescriptorCollection GetItemPropertiesInternal()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002D70 File Offset: 0x00000F70
		internal virtual bool IsSuspended
		{
			get
			{
				return false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000A1 RID: 161
		protected internal abstract void OnCurrentChanged(EventArgs e);

		/// <summary>Pulls data from the data-bound control into the data source, returning no information.</summary>
		// Token: 0x060000A2 RID: 162 RVA: 0x00003D20 File Offset: 0x00001F20
		protected void PullData()
		{
			try
			{
				if (!this.transfering_data)
				{
					this.transfering_data = true;
					this.UpdateIsBinding();
				}
				foreach (object obj in this.Bindings)
				{
					((Binding)obj).PullData();
				}
			}
			finally
			{
				this.transfering_data = false;
			}
		}

		/// <summary>Pushes data from the data source into the data-bound control, returning no information.</summary>
		// Token: 0x060000A3 RID: 163 RVA: 0x00003DA0 File Offset: 0x00001FA0
		protected void PushData()
		{
			try
			{
				if (!this.transfering_data)
				{
					this.transfering_data = true;
					this.UpdateIsBinding();
				}
				foreach (object obj in this.Bindings)
				{
					((Binding)obj).PushData();
				}
			}
			finally
			{
				this.transfering_data = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000A4 RID: 164
		protected abstract void OnCurrentItemChanged(EventArgs e);

		/// <summary>When overridden in a derived class, updates the binding.</summary>
		// Token: 0x060000A5 RID: 165
		protected abstract void UpdateIsBinding();

		// Token: 0x060000A6 RID: 166 RVA: 0x00003E20 File Offset: 0x00002020
		internal void AddBinding(Binding binding)
		{
			if (this.Bindings.Contains(binding))
			{
				return;
			}
			this.Bindings.Add(binding);
		}

		/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingManagerBase.Position" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000A7 RID: 167 RVA: 0x00003E3D File Offset: 0x0000203D
		// (remove) Token: 0x060000A8 RID: 168 RVA: 0x00003E56 File Offset: 0x00002056
		public event EventHandler PositionChanged
		{
			add
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Combine(this.onPositionChangedHandler, value);
			}
			remove
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Remove(this.onPositionChangedHandler, value);
			}
		}

		// Token: 0x040000BF RID: 191
		private BindingsCollection bindings;

		// Token: 0x040000C0 RID: 192
		internal bool transfering_data;

		/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		// Token: 0x040000C1 RID: 193
		protected EventHandler onCurrentChangedHandler;

		/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
		// Token: 0x040000C2 RID: 194
		protected EventHandler onPositionChangedHandler;

		// Token: 0x040000C3 RID: 195
		internal EventHandler onCurrentItemChangedHandler;
	}
}
