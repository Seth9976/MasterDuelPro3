using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the property this attribute is bound to is read-only or read/write. This class cannot be inherited</summary>
	// Token: 0x0200024F RID: 591
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ReadOnlyAttribute" /> class.</summary>
		/// <param name="isReadOnly">true to show that the property this attribute is bound to is read-only; false to show that the property is read/write. </param>
		// Token: 0x06000E26 RID: 3622 RVA: 0x0003EA9B File Offset: 0x0003CC9B
		public ReadOnlyAttribute(bool isReadOnly)
		{
			this.IsReadOnly = isReadOnly;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to is read-only.</summary>
		/// <returns>true if the property this attribute is bound to is read-only; false if the property is read/write.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0003EAAA File Offset: 0x0003CCAA
		public bool IsReadOnly { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x06000E28 RID: 3624 RVA: 0x0003EAB4 File Offset: 0x0003CCB4
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			ReadOnlyAttribute readOnlyAttribute = value as ReadOnlyAttribute;
			bool? flag = ((readOnlyAttribute != null) ? new bool?(readOnlyAttribute.IsReadOnly) : null);
			bool isReadOnly = this.IsReadOnly;
			return (flag.GetValueOrDefault() == isReadOnly) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ReadOnlyAttribute" />.</returns>
		// Token: 0x06000E29 RID: 3625 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000E2A RID: 3626 RVA: 0x0003EB00 File Offset: 0x0003CD00
		public override bool IsDefaultAttribute()
		{
			return this.IsReadOnly == ReadOnlyAttribute.Default.IsReadOnly;
		}

		/// <summary>Specifies that the property this attribute is bound to is read-only and cannot be modified in the server explorer. This static field is read-only.</summary>
		// Token: 0x040009AF RID: 2479
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		/// <summary>Specifies that the property this attribute is bound to is read/write and can be modified. This static field is read-only.</summary>
		// Token: 0x040009B0 RID: 2480
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.ReadOnlyAttribute" />, which is <see cref="F:System.ComponentModel.ReadOnlyAttribute.No" /> (that is, the property this attribute is bound to is read/write). This static field is read-only.</summary>
		// Token: 0x040009B1 RID: 2481
		public static readonly ReadOnlyAttribute Default = ReadOnlyAttribute.No;
	}
}
