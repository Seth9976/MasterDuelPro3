using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides a common implementation of members for the <see cref="T:System.Windows.Forms.ListBox" /> and <see cref="T:System.Windows.Forms.ComboBox" /> classes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000104 RID: 260
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
	public abstract class ListControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListControl" /> class. </summary>
		// Token: 0x06000937 RID: 2359 RVA: 0x0002709D File Offset: 0x0002529D
		protected ListControl()
		{
			this.value_member = new BindingMemberInfo(string.Empty);
			this.display_member = string.Empty;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
		}

		/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior. </summary>
		/// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000270D7 File Offset: 0x000252D7
		[Browsable(false)]
		[DefaultValue(null)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.format_info;
			}
		}

		/// <summary>Gets or sets the format-specifier characters that indicate how a value is to be displayed.</summary>
		/// <returns>The string of format-specifier characters that indicates how a value is to be displayed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000270DF File Offset: 0x000252DF
		[DefaultValue("")]
		[MergableProperty(false)]
		[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string FormatString
		{
			get
			{
				return this.format_string;
			}
		}

		/// <summary>Gets or sets a value indicating whether formatting is applied to the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property of the <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>true if formatting of the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is enabled; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000270E7 File Offset: 0x000252E7
		[DefaultValue(false)]
		public bool FormattingEnabled
		{
			get
			{
				return this.formatting_enabled;
			}
		}

		/// <summary>Gets or sets the property to display for this <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>A <see cref="T:System.String" /> specifying the name of an object property that is contained in the collection specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. The default is an empty string (""). </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000270EF File Offset: 0x000252EF
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MWFCategory("Data")]
		public string DisplayMember
		{
			get
			{
				return this.display_member;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the zero-based index of the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600093C RID: 2364
		// (set) Token: 0x0600093D RID: 2365
		public abstract int SelectedIndex { get; set; }

		/// <summary>Gets a value indicating whether the list enables selection of list items.</summary>
		/// <returns>true if the list enables list item selection; otherwise, false. The default is true.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00006F54 File Offset: 0x00005154
		protected virtual bool AllowSelection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns the current value of the <see cref="T:System.Windows.Forms.ListControl" /> item, if it is a property of an object given the item and the property name.</summary>
		/// <returns>The filtered object.</returns>
		/// <param name="item">The object the <see cref="T:System.Windows.Forms.ListControl" /> item is bound to.</param>
		/// <param name="field">The property name of the item the <see cref="T:System.Windows.Forms.ListControl" /> is bound to.</param>
		// Token: 0x06000940 RID: 2368 RVA: 0x000270F8 File Offset: 0x000252F8
		protected object FilterItemOnProperty(object item, string field)
		{
			if (item == null)
			{
				return null;
			}
			if (field == null || field == string.Empty)
			{
				return item;
			}
			PropertyDescriptor propertyDescriptor;
			if (this.data_manager != null)
			{
				propertyDescriptor = this.data_manager.GetItemProperties().Find(field, true);
			}
			else
			{
				propertyDescriptor = TypeDescriptor.GetProperties(item).Find(field, true);
			}
			if (propertyDescriptor == null)
			{
				return item;
			}
			return propertyDescriptor.GetValue(item);
		}

		/// <summary>Returns the text representation of the specified item.</summary>
		/// <returns>If the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is not specified, the value returned by <see cref="M:System.Windows.Forms.ListControl.GetItemText(System.Object)" /> is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property for the object specified in the <paramref name="item" /> parameter.</returns>
		/// <param name="item">The object from which to get the contents to display. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000941 RID: 2369 RVA: 0x00027158 File Offset: 0x00025358
		public string GetItemText(object item)
		{
			object obj = this.FilterItemOnProperty(item, this.DisplayMember);
			if (obj == null)
			{
				obj = item;
			}
			string text = obj.ToString();
			if (this.FormattingEnabled)
			{
				ListControlConvertEventArgs listControlConvertEventArgs = new ListControlConvertEventArgs(obj, typeof(string), item);
				this.OnFormat(listControlConvertEventArgs);
				if (listControlConvertEventArgs.Value.ToString() != text)
				{
					return listControlConvertEventArgs.Value.ToString();
				}
				if (obj is IFormattable)
				{
					return ((IFormattable)obj).ToString(string.IsNullOrEmpty(this.FormatString) ? null : this.FormatString, this.FormatInfo);
				}
			}
			return text;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control. The default is null.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000271F0 File Offset: 0x000253F0
		protected CurrencyManager DataManager
		{
			get
			{
				return this.data_manager;
			}
		}

		/// <summary>Handles special input keys, such as PAGE UP, PAGE DOWN, HOME, END, and so on.</summary>
		/// <returns>true if the <paramref name="keyData" /> parameter specifies the <see cref="F:System.Windows.Forms.Keys.End" />, <see cref="F:System.Windows.Forms.Keys.Home" />, <see cref="F:System.Windows.Forms.Keys.PageUp" />, or <see cref="F:System.Windows.Forms.Keys.PageDown" /> key; false if the <paramref name="keyData" /> parameter specifies <see cref="F:System.Windows.Forms.Keys.Alt" />.</returns>
		/// <param name="keyData">One of the values of <see cref="T:System.Windows.Forms.Keys" />.</param>
		// Token: 0x06000943 RID: 2371 RVA: 0x000271F8 File Offset: 0x000253F8
		protected override bool IsInputKey(Keys keyData)
		{
			return keyData - Keys.ShiftKey <= 1 || keyData - Keys.Space <= 8;
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000944 RID: 2372 RVA: 0x0002720C File Offset: 0x0002540C
		protected override void OnBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged(e);
			if (this.last_binding_context == this.BindingContext)
			{
				return;
			}
			this.last_binding_context = this.BindingContext;
			this.ConnectToDataSource();
			if (this.DataManager != null)
			{
				this.SetItemsCore(this.DataManager.List);
				if (this.AllowSelection)
				{
					this.SelectedIndex = this.DataManager.Position;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.Format" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListControlConvertEventArgs" /> that contains the event data. </param>
		// Token: 0x06000945 RID: 2373 RVA: 0x00027274 File Offset: 0x00025474
		protected virtual void OnFormat(ListControlConvertEventArgs e)
		{
			ListControlConvertEventHandler listControlConvertEventHandler = (ListControlConvertEventHandler)base.Events[ListControl.FormatEvent];
			if (listControlConvertEventHandler != null)
			{
				listControlConvertEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000946 RID: 2374 RVA: 0x000272A2 File Offset: 0x000254A2
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.data_manager == null)
			{
				return;
			}
			if (this.data_manager.Position == this.SelectedIndex)
			{
				return;
			}
			this.data_manager.Position = this.SelectedIndex;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000947 RID: 2375 RVA: 0x000272D4 File Offset: 0x000254D4
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.SelectedValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>When overridden in a derived class, resynchronizes the data of the object at the specified index with the contents of the data source.</summary>
		/// <param name="index">The zero-based index of the item whose data to refresh. </param>
		// Token: 0x06000948 RID: 2376
		protected abstract void RefreshItem(int index);

		/// <summary>When overridden in a derived class, sets the specified array of objects in a collection in the derived class.</summary>
		/// <param name="items">An array of items.</param>
		// Token: 0x06000949 RID: 2377
		protected abstract void SetItemsCore(IList items);

		// Token: 0x0600094A RID: 2378 RVA: 0x00027304 File Offset: 0x00025504
		private void ConnectToDataSource()
		{
			if (this.BindingContext == null)
			{
				return;
			}
			CurrencyManager currencyManager = null;
			if (this.data_source != null)
			{
				currencyManager = (CurrencyManager)this.BindingContext[this.data_source];
			}
			if (currencyManager != this.data_manager)
			{
				if (this.data_manager != null)
				{
					this.data_manager.PositionChanged -= this.OnPositionChanged;
					this.data_manager.ItemChanged -= this.OnItemChanged;
				}
				if (currencyManager != null)
				{
					currencyManager.PositionChanged += this.OnPositionChanged;
					currencyManager.ItemChanged += this.OnItemChanged;
				}
				this.data_manager = currencyManager;
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000273A8 File Offset: 0x000255A8
		private void OnItemChanged(object sender, ItemChangedEventArgs e)
		{
			if (e.Index == -1)
			{
				this.SetItemsCore(this.data_manager.List);
			}
			else
			{
				this.RefreshItem(e.Index);
			}
			if (this.AllowSelection && this.SelectedIndex == -1 && this.data_manager.Count == 1)
			{
				this.SelectedIndex = this.data_manager.Position;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002740D File Offset: 0x0002560D
		private void OnPositionChanged(object sender, EventArgs e)
		{
			if (this.AllowSelection && this.data_manager.Count > 1)
			{
				this.SelectedIndex = this.data_manager.Position;
			}
		}

		// Token: 0x04000684 RID: 1668
		private object data_source;

		// Token: 0x04000685 RID: 1669
		private BindingMemberInfo value_member;

		// Token: 0x04000686 RID: 1670
		private string display_member;

		// Token: 0x04000687 RID: 1671
		private CurrencyManager data_manager;

		// Token: 0x04000688 RID: 1672
		private BindingContext last_binding_context;

		// Token: 0x04000689 RID: 1673
		private IFormatProvider format_info;

		// Token: 0x0400068A RID: 1674
		private string format_string = string.Empty;

		// Token: 0x0400068B RID: 1675
		private bool formatting_enabled;

		// Token: 0x0400068C RID: 1676
		private static object DataSourceChangedEvent = new object();

		// Token: 0x0400068D RID: 1677
		private static object DisplayMemberChangedEvent = new object();

		// Token: 0x0400068E RID: 1678
		private static object FormatEvent = new object();

		// Token: 0x0400068F RID: 1679
		private static object FormatInfoChangedEvent = new object();

		// Token: 0x04000690 RID: 1680
		private static object FormatStringChangedEvent = new object();

		// Token: 0x04000691 RID: 1681
		private static object FormattingEnabledChangedEvent = new object();

		// Token: 0x04000692 RID: 1682
		private static object SelectedValueChangedEvent = new object();

		// Token: 0x04000693 RID: 1683
		private static object ValueMemberChangedEvent = new object();
	}
}
