using System;

namespace System.ComponentModel
{
	/// <summary>Specifies when a component property can be bound to an application setting.</summary>
	// Token: 0x0200029B RID: 667
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> class. </summary>
		/// <param name="bindable">true to specify that a property is appropriate to bind settings to; otherwise, false.</param>
		// Token: 0x06001003 RID: 4099 RVA: 0x00043CBD File Offset: 0x00041EBD
		public SettingsBindableAttribute(bool bindable)
		{
			this.Bindable = bindable;
		}

		/// <summary>Gets a value indicating whether a property is appropriate to bind settings to. </summary>
		/// <returns>true if the property is appropriate to bind settings to; otherwise, false.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00043CCC File Offset: 0x00041ECC
		public bool Bindable { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> objects are equal.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">The value to compare to.</param>
		// Token: 0x06001005 RID: 4101 RVA: 0x00043CD4 File Offset: 0x00041ED4
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is SettingsBindableAttribute && ((SettingsBindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001006 RID: 4102 RVA: 0x00043CFC File Offset: 0x00041EFC
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		/// <summary>Specifies that a property is appropriate to bind settings to.</summary>
		// Token: 0x04000A45 RID: 2629
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		/// <summary>Specifies that a property is not appropriate to bind settings to.</summary>
		// Token: 0x04000A46 RID: 2630
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);
	}
}
