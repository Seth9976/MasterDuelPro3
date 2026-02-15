using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default binding property for a component. This class cannot be inherited.</summary>
	// Token: 0x0200026A RID: 618
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultBindingPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using no parameters. </summary>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x000029F9 File Offset: 0x00000BF9
		public DefaultBindingPropertyAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using the specified property name.</summary>
		/// <param name="name">The name of the default binding property.</param>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00041131 File Offset: 0x0003F331
		public DefaultBindingPropertyAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</summary>
		/// <returns>The name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00041140 File Offset: 0x0003F340
		public string Name { get; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance</param>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x00041148 File Offset: 0x0003F348
		public override bool Equals(object obj)
		{
			DefaultBindingPropertyAttribute defaultBindingPropertyAttribute = obj as DefaultBindingPropertyAttribute;
			return defaultBindingPropertyAttribute != null && defaultBindingPropertyAttribute.Name == this.Name;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class.</summary>
		// Token: 0x040009D8 RID: 2520
		public static readonly DefaultBindingPropertyAttribute Default = new DefaultBindingPropertyAttribute();
	}
}
