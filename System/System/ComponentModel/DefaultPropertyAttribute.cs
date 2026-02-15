using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default property for a component.</summary>
	// Token: 0x0200026C RID: 620
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the default property for the component this attribute is bound to. </param>
		// Token: 0x06000EAC RID: 3756 RVA: 0x000411CF File Offset: 0x0003F3CF
		public DefaultPropertyAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default property for the component this attribute is bound to.</summary>
		/// <returns>The name of the default property for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000411DE File Offset: 0x0003F3DE
		public string Name { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000EAE RID: 3758 RVA: 0x000411E8 File Offset: 0x0003F3E8
		public override bool Equals(object obj)
		{
			DefaultPropertyAttribute defaultPropertyAttribute = obj as DefaultPropertyAttribute;
			return defaultPropertyAttribute != null && defaultPropertyAttribute.Name == this.Name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000EAF RID: 3759 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x040009DC RID: 2524
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
