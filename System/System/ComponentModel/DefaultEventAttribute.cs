using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default event for a component.</summary>
	// Token: 0x0200026B RID: 619
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultEventAttribute" /> class.</summary>
		/// <param name="name">The name of the default event for the component this attribute is bound to. </param>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0004117E File Offset: 0x0003F37E
		public DefaultEventAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default event for the component this attribute is bound to.</summary>
		/// <returns>The name of the default event for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0004118D File Offset: 0x0003F38D
		public string Name { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultEventAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00041198 File Offset: 0x0003F398
		public override bool Equals(object obj)
		{
			DefaultEventAttribute defaultEventAttribute = obj as DefaultEventAttribute;
			return defaultEventAttribute != null && defaultEventAttribute.Name == this.Name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000EAA RID: 3754 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultEventAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x040009DA RID: 2522
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
