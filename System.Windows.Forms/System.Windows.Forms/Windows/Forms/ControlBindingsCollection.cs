using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Represents the collection of data bindings for a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000054 RID: 84
	[DefaultMember("Item")]
	[DefaultEvent("CollectionChanged")]
	[Editor("System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter("System.Windows.Forms.Design.ControlBindingsConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ControlBindingsCollection : BindingsCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> class with the specified bindable control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</param>
		// Token: 0x06000434 RID: 1076 RVA: 0x000107B0 File Offset: 0x0000E9B0
		public ControlBindingsCollection(IBindableComponent control)
		{
			this.bindable_component = control;
			control = control as Control;
			this.default_datasource_update_mode = DataSourceUpdateMode.OnValidation;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000107CE File Offset: 0x0000E9CE
		public IBindableComponent BindableComponent
		{
			get
			{
				return this.bindable_component;
			}
		}

		/// <summary>Adds a binding to the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to add. </param>
		// Token: 0x06000436 RID: 1078 RVA: 0x000107D8 File Offset: 0x0000E9D8
		protected override void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.Control != null && dataBinding.BindableComponent != this.bindable_component)
			{
				throw new ArgumentException("dataBinding belongs to another BindingsCollection");
			}
			for (int i = 0; i < this.Count; i++)
			{
				Binding binding = base[i];
				if (binding != null && binding.PropertyName.Length != 0 && dataBinding.PropertyName.Length != 0 && string.Compare(binding.PropertyName, dataBinding.PropertyName, true) == 0)
				{
					throw new ArgumentException("The binding is already in the collection");
				}
			}
			dataBinding.SetControl(this.bindable_component);
			dataBinding.Check();
			base.AddCore(dataBinding);
		}

		// Token: 0x04000229 RID: 553
		private IBindableComponent bindable_component;

		// Token: 0x0400022A RID: 554
		private DataSourceUpdateMode default_datasource_update_mode;
	}
}
