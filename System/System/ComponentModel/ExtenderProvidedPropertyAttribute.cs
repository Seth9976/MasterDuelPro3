using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a property that is offered by an extender provider. This class cannot be inherited.</summary>
	// Token: 0x02000279 RID: 633
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x00041D1F File Offset: 0x0003FF1F
		internal static ExtenderProvidedPropertyAttribute Create(PropertyDescriptor extenderProperty, Type receiverType, IExtenderProvider provider)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				ExtenderProperty = extenderProperty,
				ReceiverType = receiverType,
				Provider = provider
			};
		}

		/// <summary>Gets the property that is being provided.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> encapsulating the property that is being provided.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00041D3B File Offset: 0x0003FF3B
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00041D43 File Offset: 0x0003FF43
		public PropertyDescriptor ExtenderProperty { get; private set; }

		/// <summary>Gets the extender provider that is providing the property.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IExtenderProvider" /> that is providing the property.</returns>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00041D4C File Offset: 0x0003FF4C
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00041D54 File Offset: 0x0003FF54
		public IExtenderProvider Provider { get; private set; }

		/// <summary>Gets the type of object that can receive the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> describing the type of object that can receive the property.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00041D5D File Offset: 0x0003FF5D
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00041D65 File Offset: 0x0003FF65
		public Type ReceiverType { get; private set; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000F1C RID: 3868 RVA: 0x00041D70 File Offset: 0x0003FF70
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = obj as ExtenderProvidedPropertyAttribute;
			return extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.ExtenderProperty.Equals(this.ExtenderProperty) && extenderProvidedPropertyAttribute.Provider.Equals(this.Provider) && extenderProvidedPropertyAttribute.ReceiverType.Equals(this.ReceiverType);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F1D RID: 3869 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Provides an indication whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06000F1E RID: 3870 RVA: 0x00041DC6 File Offset: 0x0003FFC6
		public override bool IsDefaultAttribute()
		{
			return this.ReceiverType == null;
		}
	}
}
