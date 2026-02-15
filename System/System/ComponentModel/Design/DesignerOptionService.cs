using System;
using System.Collections;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a base class for getting and setting option values for a designer.</summary>
	// Token: 0x020002EC RID: 748
	public abstract class DesignerOptionService
	{
		/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
		/// <param name="options">The collection to populate.</param>
		// Token: 0x06001203 RID: 4611 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
		}

		/// <summary>Contains a collection of designer options. This class cannot be inherited.</summary>
		// Token: 0x020002ED RID: 749
		[DefaultMember("Item")]
		[TypeConverter(typeof(DesignerOptionService.DesignerOptionConverter))]
		[Editor("", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public sealed class DesignerOptionCollection : ICollection
		{
			/// <summary>Gets the number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</summary>
			/// <returns>The number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</returns>
			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06001204 RID: 4612 RVA: 0x00051E2F File Offset: 0x0005002F
			public int Count
			{
				get
				{
					this.EnsurePopulated();
					return this._children.Count;
				}
			}

			/// <summary>Gets the name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>The name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</returns>
			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06001205 RID: 4613 RVA: 0x00051E42 File Offset: 0x00050042
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			/// <summary>Gets the collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</summary>
			/// <returns>The collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</returns>
			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06001206 RID: 4614 RVA: 0x00051E4C File Offset: 0x0005004C
			public PropertyDescriptorCollection Properties
			{
				get
				{
					if (this._properties == null)
					{
						ArrayList arrayList;
						if (this._value != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._value);
							arrayList = new ArrayList(properties.Count);
							using (IEnumerator enumerator = properties.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
									arrayList.Add(new DesignerOptionService.DesignerOptionCollection.WrappedPropertyDescriptor(propertyDescriptor, this._value));
								}
								goto IL_0076;
							}
						}
						arrayList = new ArrayList(1);
						IL_0076:
						this.EnsurePopulated();
						foreach (object obj2 in this._children)
						{
							DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj2;
							arrayList.AddRange(designerOptionCollection.Properties);
						}
						PropertyDescriptor[] array = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
						this._properties = new PropertyDescriptorCollection(array, true);
					}
					return this._properties;
				}
			}

			/// <summary>Copies the entire collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The <paramref name="array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x06001207 RID: 4615 RVA: 0x00051F60 File Offset: 0x00050160
			public void CopyTo(Array array, int index)
			{
				this.EnsurePopulated();
				this._children.CopyTo(array, index);
			}

			// Token: 0x06001208 RID: 4616 RVA: 0x00051F75 File Offset: 0x00050175
			private void EnsurePopulated()
			{
				if (this._children == null)
				{
					this._service.PopulateOptionCollection(this);
					if (this._children == null)
					{
						this._children = new ArrayList(1);
					}
				}
			}

			/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</returns>
			// Token: 0x06001209 RID: 4617 RVA: 0x00051F9F File Offset: 0x0005019F
			public IEnumerator GetEnumerator()
			{
				this.EnsurePopulated();
				return this._children.GetEnumerator();
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized and, therefore, thread safe.</summary>
			/// <returns>true if the access to the collection is synchronized; otherwise, false.</returns>
			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x0600120A RID: 4618 RVA: 0x000028AE File Offset: 0x00000AAE
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x0600120B RID: 4619 RVA: 0x0001AE3D File Offset: 0x0001903D
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x04000B2A RID: 2858
			private DesignerOptionService _service;

			// Token: 0x04000B2B RID: 2859
			private string _name;

			// Token: 0x04000B2C RID: 2860
			private object _value;

			// Token: 0x04000B2D RID: 2861
			private ArrayList _children;

			// Token: 0x04000B2E RID: 2862
			private PropertyDescriptorCollection _properties;

			// Token: 0x020002EE RID: 750
			private sealed class WrappedPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x0600120C RID: 4620 RVA: 0x00051FB2 File Offset: 0x000501B2
				internal WrappedPropertyDescriptor(PropertyDescriptor property, object target)
					: base(property.Name, null)
				{
					this.property = property;
					this.target = target;
				}

				// Token: 0x170003B9 RID: 953
				// (get) Token: 0x0600120D RID: 4621 RVA: 0x00051FCF File Offset: 0x000501CF
				public override AttributeCollection Attributes
				{
					get
					{
						return this.property.Attributes;
					}
				}

				// Token: 0x170003BA RID: 954
				// (get) Token: 0x0600120E RID: 4622 RVA: 0x00051FDC File Offset: 0x000501DC
				public override Type ComponentType
				{
					get
					{
						return this.property.ComponentType;
					}
				}

				// Token: 0x170003BB RID: 955
				// (get) Token: 0x0600120F RID: 4623 RVA: 0x00051FE9 File Offset: 0x000501E9
				public override bool IsReadOnly
				{
					get
					{
						return this.property.IsReadOnly;
					}
				}

				// Token: 0x170003BC RID: 956
				// (get) Token: 0x06001210 RID: 4624 RVA: 0x00051FF6 File Offset: 0x000501F6
				public override Type PropertyType
				{
					get
					{
						return this.property.PropertyType;
					}
				}

				// Token: 0x06001211 RID: 4625 RVA: 0x00052003 File Offset: 0x00050203
				public override bool CanResetValue(object component)
				{
					return this.property.CanResetValue(this.target);
				}

				// Token: 0x06001212 RID: 4626 RVA: 0x00052016 File Offset: 0x00050216
				public override object GetValue(object component)
				{
					return this.property.GetValue(this.target);
				}

				// Token: 0x06001213 RID: 4627 RVA: 0x00052029 File Offset: 0x00050229
				public override void ResetValue(object component)
				{
					this.property.ResetValue(this.target);
				}

				// Token: 0x06001214 RID: 4628 RVA: 0x0005203C File Offset: 0x0005023C
				public override void SetValue(object component, object value)
				{
					this.property.SetValue(this.target, value);
				}

				// Token: 0x06001215 RID: 4629 RVA: 0x00052050 File Offset: 0x00050250
				public override bool ShouldSerializeValue(object component)
				{
					return this.property.ShouldSerializeValue(this.target);
				}

				// Token: 0x04000B2F RID: 2863
				private object target;

				// Token: 0x04000B30 RID: 2864
				private PropertyDescriptor property;
			}
		}

		// Token: 0x020002EF RID: 751
		internal sealed class DesignerOptionConverter : TypeConverter
		{
			// Token: 0x06001216 RID: 4630 RVA: 0x00003BCC File Offset: 0x00001DCC
			public override bool GetPropertiesSupported(ITypeDescriptorContext cxt)
			{
				return true;
			}

			// Token: 0x06001217 RID: 4631 RVA: 0x00052064 File Offset: 0x00050264
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext cxt, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				DesignerOptionService.DesignerOptionCollection designerOptionCollection = value as DesignerOptionService.DesignerOptionCollection;
				if (designerOptionCollection == null)
				{
					return propertyDescriptorCollection;
				}
				foreach (object obj in designerOptionCollection)
				{
					DesignerOptionService.DesignerOptionCollection designerOptionCollection2 = (DesignerOptionService.DesignerOptionCollection)obj;
					propertyDescriptorCollection.Add(new DesignerOptionService.DesignerOptionConverter.OptionPropertyDescriptor(designerOptionCollection2));
				}
				foreach (object obj2 in designerOptionCollection.Properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06001218 RID: 4632 RVA: 0x00052124 File Offset: 0x00050324
			public override object ConvertTo(ITypeDescriptorContext cxt, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return SR.GetString("(Collection)");
				}
				return base.ConvertTo(cxt, culture, value, destinationType);
			}

			// Token: 0x020002F0 RID: 752
			private class OptionPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x0600121A RID: 4634 RVA: 0x0005214F File Offset: 0x0005034F
				internal OptionPropertyDescriptor(DesignerOptionService.DesignerOptionCollection option)
					: base(option.Name, null)
				{
					this._option = option;
				}

				// Token: 0x170003BD RID: 957
				// (get) Token: 0x0600121B RID: 4635 RVA: 0x00052165 File Offset: 0x00050365
				public override Type ComponentType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x170003BE RID: 958
				// (get) Token: 0x0600121C RID: 4636 RVA: 0x00003BCC File Offset: 0x00001DCC
				public override bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x170003BF RID: 959
				// (get) Token: 0x0600121D RID: 4637 RVA: 0x00052165 File Offset: 0x00050365
				public override Type PropertyType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x0600121E RID: 4638 RVA: 0x000028AE File Offset: 0x00000AAE
				public override bool CanResetValue(object component)
				{
					return false;
				}

				// Token: 0x0600121F RID: 4639 RVA: 0x00052172 File Offset: 0x00050372
				public override object GetValue(object component)
				{
					return this._option;
				}

				// Token: 0x06001220 RID: 4640 RVA: 0x00002FA0 File Offset: 0x000011A0
				public override void ResetValue(object component)
				{
				}

				// Token: 0x06001221 RID: 4641 RVA: 0x00002FA0 File Offset: 0x000011A0
				public override void SetValue(object component, object value)
				{
				}

				// Token: 0x06001222 RID: 4642 RVA: 0x000028AE File Offset: 0x00000AAE
				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}

				// Token: 0x04000B31 RID: 2865
				private DesignerOptionService.DesignerOptionCollection _option;
			}
		}
	}
}
