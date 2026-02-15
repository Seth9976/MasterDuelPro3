using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property should be localized. This class cannot be inherited.</summary>
	// Token: 0x0200024D RID: 589
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LocalizableAttribute" /> class.</summary>
		/// <param name="isLocalizable">true if a property should be localized; otherwise, false. </param>
		// Token: 0x06000E1A RID: 3610 RVA: 0x0003E96C File Offset: 0x0003CB6C
		public LocalizableAttribute(bool isLocalizable)
		{
			this.IsLocalizable = isLocalizable;
		}

		/// <summary>Gets a value indicating whether a property should be localized.</summary>
		/// <returns>true if a property should be localized; otherwise, false.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0003E97B File Offset: 0x0003CB7B
		public bool IsLocalizable { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000E1C RID: 3612 RVA: 0x0003E984 File Offset: 0x0003CB84
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			bool? flag = ((localizableAttribute != null) ? new bool?(localizableAttribute.IsLocalizable) : null);
			bool isLocalizable = this.IsLocalizable;
			return (flag.GetValueOrDefault() == isLocalizable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</returns>
		// Token: 0x06000E1D RID: 3613 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000E1E RID: 3614 RVA: 0x0003E9D0 File Offset: 0x0003CBD0
		public override bool IsDefaultAttribute()
		{
			return this.IsLocalizable == LocalizableAttribute.Default.IsLocalizable;
		}

		/// <summary>Specifies that a property should be localized. This static field is read-only.</summary>
		// Token: 0x040009A8 RID: 2472
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		/// <summary>Specifies that a property should not be localized. This static field is read-only.</summary>
		// Token: 0x040009A9 RID: 2473
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.LocalizableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x040009AA RID: 2474
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
