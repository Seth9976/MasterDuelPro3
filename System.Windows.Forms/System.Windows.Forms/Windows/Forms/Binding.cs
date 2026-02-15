using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the simple binding between the property value of an object and the property value of a control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000019 RID: 25
	[TypeConverter(typeof(ListBindingConverter))]
	public class Binding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that simple-binds the indicated control property to the specified data member of the data source.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
		/// <param name="dataMember">The property or list to bind to. </param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.</exception>
		// Token: 0x06000068 RID: 104 RVA: 0x00003128 File Offset: 0x00001328
		public Binding(string propertyName, object dataSource, string dataMember)
			: this(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class with the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; enables formatting with the specified format string; sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source; and sets the specified format provider.</summary>
		/// <param name="propertyName">The name of the control property to bind. </param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
		/// <param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider" /> to override default formatting behavior.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000069 RID: 105 RVA: 0x00003148 File Offset: 0x00001348
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
		{
			this.property_name = propertyName;
			this.data_source = dataSource;
			this.data_member = dataMember;
			this.binding_member_info = new BindingMemberInfo(dataMember);
			this.datasource_update_mode = dataSourceUpdateMode;
			this.null_value = nullValue;
			this.format_string = formatString;
			this.format_info = formatInfo;
		}

		/// <summary>Gets the control the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000031A7 File Offset: 0x000013A7
		[DefaultValue(null)]
		public IBindableComponent BindableComponent
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets the control that the binding belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the binding belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000031AF File Offset: 0x000013AF
		[DefaultValue(null)]
		public Control Control
		{
			get
			{
				return this.control as Control;
			}
		}

		/// <summary>Gets a value indicating whether the binding is active.</summary>
		/// <returns>true if the binding is active; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000031BC File Offset: 0x000013BC
		public bool IsBinding
		{
			get
			{
				return this.manager != null && !this.manager.IsSuspended && this.is_binding;
			}
		}

		/// <summary>Gets or sets the name of the control's data-bound property.</summary>
		/// <returns>The name of a control property to bind to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000031DB File Offset: 0x000013DB
		[DefaultValue("")]
		public string PropertyName
		{
			get
			{
				return this.property_name;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
		// Token: 0x0600006E RID: 110 RVA: 0x000031E3 File Offset: 0x000013E3
		protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
		{
			if (this.BindingComplete != null)
			{
				this.BindingComplete(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Format" /> event.</summary>
		/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
		// Token: 0x0600006F RID: 111 RVA: 0x000031FA File Offset: 0x000013FA
		protected virtual void OnFormat(ConvertEventArgs cevent)
		{
			if (this.Format != null)
			{
				this.Format(this, cevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Parse" /> event.</summary>
		/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
		// Token: 0x06000070 RID: 112 RVA: 0x00003211 File Offset: 0x00001411
		protected virtual void OnParse(ConvertEventArgs cevent)
		{
			if (this.Parse != null)
			{
				this.Parse(this, cevent);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003228 File Offset: 0x00001428
		internal void SetControl(IBindableComponent control)
		{
			if (control == this.control)
			{
				return;
			}
			this.control_property = TypeDescriptor.GetProperties(control).Find(this.property_name, true);
			if (this.control_property == null)
			{
				throw new ArgumentException("Cannot bind to property '" + this.property_name + "' on target control.");
			}
			if (this.control_property.IsReadOnly)
			{
				throw new ArgumentException("Cannot bind to property '" + this.property_name + "' because it is read only.");
			}
			this.data_type = this.control_property.PropertyType;
			Control control2 = control as Control;
			if (control2 != null)
			{
				control2.Validating += this.ControlValidatingHandler;
				if (!control2.IsHandleCreated)
				{
					control2.HandleCreated += this.ControlCreatedHandler;
				}
			}
			EventDescriptor propertyChangedEvent = this.GetPropertyChangedEvent(control, this.property_name);
			if (propertyChangedEvent != null)
			{
				propertyChangedEvent.AddEventHandler(control, new EventHandler(this.ControlPropertyChangedHandler));
			}
			this.control = control;
			this.UpdateIsBinding();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000331C File Offset: 0x0000151C
		internal void Check()
		{
			if (this.control == null || this.control.BindingContext == null)
			{
				return;
			}
			if (this.manager == null)
			{
				this.manager = this.control.BindingContext[this.data_source, this.binding_member_info.BindingPath];
				if (this.manager.Position > -1 && this.binding_member_info.BindingField != string.Empty && TypeDescriptor.GetProperties(this.manager.Current).Find(this.binding_member_info.BindingField, true) == null)
				{
					throw new ArgumentException("Cannot bind to property '" + this.binding_member_info.BindingField + "' on DataSource.", "dataMember");
				}
				this.manager.AddBinding(this);
				this.manager.PositionChanged += this.PositionChangedHandler;
				if (this.manager is PropertyManager)
				{
					EventDescriptor propertyChangedEvent = this.GetPropertyChangedEvent(this.manager.Current, this.binding_member_info.BindingField);
					if (propertyChangedEvent != null)
					{
						propertyChangedEvent.AddEventHandler(this.manager.Current, new EventHandler(this.SourcePropertyChangedHandler));
					}
				}
			}
			if (this.manager.Position == -1)
			{
				return;
			}
			if (!this.checked_isnull)
			{
				this.is_null_desc = TypeDescriptor.GetProperties(this.manager.Current).Find(this.property_name + "IsNull", false);
				this.checked_isnull = true;
			}
			this.PushData();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000349B File Offset: 0x0000169B
		internal bool PullData()
		{
			return this.PullData(false);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034A4 File Offset: 0x000016A4
		private bool PullData(bool force)
		{
			if (!this.IsBinding || this.manager.Current == null)
			{
				return true;
			}
			if (!force && this.datasource_update_mode == DataSourceUpdateMode.Never)
			{
				return true;
			}
			this.data = this.control_property.GetValue(this.control);
			if (this.data == null)
			{
				this.data = this.datasource_null_value;
			}
			try
			{
				this.SetPropertyValue(this.data);
			}
			catch (Exception ex)
			{
				if (this.formatting_enabled)
				{
					this.FireBindingComplete(BindingCompleteContext.DataSourceUpdate, ex, ex.Message);
					return false;
				}
				throw ex;
			}
			if (this.formatting_enabled)
			{
				this.FireBindingComplete(BindingCompleteContext.DataSourceUpdate, null, null);
			}
			return true;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003554 File Offset: 0x00001754
		internal void PushData()
		{
			this.PushData(false);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003560 File Offset: 0x00001760
		private void PushData(bool force)
		{
			if (this.manager == null || this.manager.IsSuspended || this.manager.Count == 0 || this.manager.Position == -1)
			{
				return;
			}
			if (!force && this.control_update_mode == ControlUpdateMode.Never)
			{
				return;
			}
			if (this.is_null_desc != null && (bool)this.is_null_desc.GetValue(this.manager.Current))
			{
				this.data = Convert.DBNull;
				return;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.manager.Current).Find(this.binding_member_info.BindingField, true);
			if (propertyDescriptor == null)
			{
				this.data = this.manager.Current;
			}
			else
			{
				this.data = propertyDescriptor.GetValue(this.manager.Current);
			}
			if ((this.data == null || this.data == DBNull.Value) && this.null_value != null)
			{
				this.data = this.null_value;
			}
			try
			{
				this.data = this.FormatData(this.data);
				this.SetControlValue(this.data);
			}
			catch (Exception ex)
			{
				if (this.formatting_enabled)
				{
					this.FireBindingComplete(BindingCompleteContext.ControlUpdate, ex, ex.Message);
					return;
				}
				throw ex;
			}
			if (this.formatting_enabled)
			{
				this.FireBindingComplete(BindingCompleteContext.ControlUpdate, null, null);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000036B4 File Offset: 0x000018B4
		internal void UpdateIsBinding()
		{
			this.is_binding = false;
			if (this.control == null || (this.control is Control && !((Control)this.control).IsHandleCreated))
			{
				return;
			}
			this.is_binding = true;
			this.PushData();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000036F2 File Offset: 0x000018F2
		private void SetControlValue(object data)
		{
			this.control_property.SetValue(this.control, data);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003708 File Offset: 0x00001908
		private void SetPropertyValue(object data)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.manager.Current).Find(this.binding_member_info.BindingField, true);
			if (propertyDescriptor.IsReadOnly)
			{
				return;
			}
			data = this.ParseData(data, propertyDescriptor.PropertyType);
			propertyDescriptor.SetValue(this.manager.Current, data);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003764 File Offset: 0x00001964
		private void ControlValidatingHandler(object sender, CancelEventArgs e)
		{
			if (this.datasource_update_mode != DataSourceUpdateMode.OnValidation)
			{
				return;
			}
			bool flag = true;
			try
			{
				flag = this.PullData();
			}
			catch
			{
				flag = false;
			}
			e.Cancel = !flag;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000037A4 File Offset: 0x000019A4
		private void ControlCreatedHandler(object o, EventArgs args)
		{
			this.UpdateIsBinding();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000037AC File Offset: 0x000019AC
		private void PositionChangedHandler(object sender, EventArgs e)
		{
			this.Check();
			this.PushData();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000037BC File Offset: 0x000019BC
		private EventDescriptor GetPropertyChangedEvent(object o, string property_name)
		{
			if (o == null || property_name == null || property_name.Length == 0)
			{
				return null;
			}
			string text = property_name + "Changed";
			Type typeFromHandle = typeof(EventHandler);
			EventDescriptor eventDescriptor = null;
			foreach (object obj in TypeDescriptor.GetEvents(o))
			{
				EventDescriptor eventDescriptor2 = (EventDescriptor)obj;
				if (eventDescriptor2.Name == text && eventDescriptor2.EventType == typeFromHandle)
				{
					eventDescriptor = eventDescriptor2;
					break;
				}
			}
			return eventDescriptor;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003860 File Offset: 0x00001A60
		private void SourcePropertyChangedHandler(object o, EventArgs args)
		{
			this.PushData();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003868 File Offset: 0x00001A68
		private void ControlPropertyChangedHandler(object o, EventArgs args)
		{
			if (this.datasource_update_mode != DataSourceUpdateMode.OnPropertyChanged)
			{
				return;
			}
			this.PullData();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000387C File Offset: 0x00001A7C
		private object ParseData(object data, Type data_type)
		{
			ConvertEventArgs convertEventArgs = new ConvertEventArgs(data, data_type);
			this.OnParse(convertEventArgs);
			if (data_type.IsInstanceOfType(convertEventArgs.Value))
			{
				return convertEventArgs.Value;
			}
			if (convertEventArgs.Value == Convert.DBNull)
			{
				return convertEventArgs.Value;
			}
			if (convertEventArgs.Value != null)
			{
				return this.ConvertData(convertEventArgs.Value, data_type);
			}
			bool flag = data_type.IsGenericType && !data_type.ContainsGenericParameters && data_type.GetGenericTypeDefinition() == typeof(Nullable<>);
			if (!data_type.IsValueType || flag)
			{
				return null;
			}
			return Convert.DBNull;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003914 File Offset: 0x00001B14
		private object FormatData(object data)
		{
			ConvertEventArgs convertEventArgs = new ConvertEventArgs(data, this.data_type);
			this.OnFormat(convertEventArgs);
			if (this.data_type.IsInstanceOfType(convertEventArgs.Value))
			{
				return convertEventArgs.Value;
			}
			if (this.formatting_enabled)
			{
				if ((convertEventArgs.Value == null || convertEventArgs.Value == Convert.DBNull) && this.null_value != null)
				{
					return this.null_value;
				}
				if (convertEventArgs.Value is IFormattable && this.data_type == typeof(string))
				{
					return ((IFormattable)convertEventArgs.Value).ToString(this.format_string, this.format_info);
				}
			}
			if (convertEventArgs.Value == null && this.data_type == typeof(object))
			{
				return Convert.DBNull;
			}
			return this.ConvertData(data, this.data_type);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000039F0 File Offset: 0x00001BF0
		private object ConvertData(object data, Type data_type)
		{
			if (data == null)
			{
				return null;
			}
			TypeConverter typeConverter = TypeDescriptor.GetConverter(data.GetType());
			if (typeConverter != null && typeConverter.CanConvertTo(data_type))
			{
				return typeConverter.ConvertTo(data, data_type);
			}
			typeConverter = TypeDescriptor.GetConverter(data_type);
			if (typeConverter != null && typeConverter.CanConvertFrom(data.GetType()))
			{
				return typeConverter.ConvertFrom(data);
			}
			if (data is IConvertible)
			{
				object obj = Convert.ChangeType(data, data_type);
				if (data_type.IsInstanceOfType(obj))
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003A60 File Offset: 0x00001C60
		private void FireBindingComplete(BindingCompleteContext context, Exception exc, string error_message)
		{
			BindingCompleteEventArgs bindingCompleteEventArgs = new BindingCompleteEventArgs(this, (exc == null) ? BindingCompleteState.Success : BindingCompleteState.Exception, context);
			if (exc != null)
			{
				bindingCompleteEventArgs.SetException(exc);
				bindingCompleteEventArgs.SetErrorText(error_message);
			}
			this.OnBindingComplete(bindingCompleteEventArgs);
		}

		// Token: 0x04000099 RID: 153
		private string property_name;

		// Token: 0x0400009A RID: 154
		private object data_source;

		// Token: 0x0400009B RID: 155
		private string data_member;

		// Token: 0x0400009C RID: 156
		private bool is_binding;

		// Token: 0x0400009D RID: 157
		private bool checked_isnull;

		// Token: 0x0400009E RID: 158
		private BindingMemberInfo binding_member_info;

		// Token: 0x0400009F RID: 159
		private IBindableComponent control;

		// Token: 0x040000A0 RID: 160
		private BindingManagerBase manager;

		// Token: 0x040000A1 RID: 161
		private PropertyDescriptor control_property;

		// Token: 0x040000A2 RID: 162
		private PropertyDescriptor is_null_desc;

		// Token: 0x040000A3 RID: 163
		private object data;

		// Token: 0x040000A4 RID: 164
		private Type data_type;

		// Token: 0x040000A5 RID: 165
		private DataSourceUpdateMode datasource_update_mode;

		// Token: 0x040000A6 RID: 166
		private ControlUpdateMode control_update_mode;

		// Token: 0x040000A7 RID: 167
		private object datasource_null_value = Convert.DBNull;

		// Token: 0x040000A8 RID: 168
		private object null_value;

		// Token: 0x040000A9 RID: 169
		private IFormatProvider format_info;

		// Token: 0x040000AA RID: 170
		private string format_string;

		// Token: 0x040000AB RID: 171
		private bool formatting_enabled;

		// Token: 0x040000AC RID: 172
		[CompilerGenerated]
		private ConvertEventHandler Format;

		// Token: 0x040000AD RID: 173
		[CompilerGenerated]
		private ConvertEventHandler Parse;

		// Token: 0x040000AE RID: 174
		[CompilerGenerated]
		private BindingCompleteEventHandler BindingComplete;
	}
}
