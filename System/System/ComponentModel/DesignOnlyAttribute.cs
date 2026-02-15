using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property can only be set at design time.</summary>
	// Token: 0x02000240 RID: 576
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignOnlyAttribute" /> class.</summary>
		/// <param name="isDesignOnly">true if a property can be set only at design time; false if the property can be set at design time and at run time. </param>
		// Token: 0x06000DDF RID: 3551 RVA: 0x0003E49E File Offset: 0x0003C69E
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.IsDesignOnly = isDesignOnly;
		}

		/// <summary>Gets a value indicating whether a property can be set only at design time.</summary>
		/// <returns>true if a property can be set only at design time; otherwise, false.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0003E4AD File Offset: 0x0003C6AD
		public bool IsDesignOnly { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003E4B8 File Offset: 0x0003C6B8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			bool? flag = ((designOnlyAttribute != null) ? new bool?(designOnlyAttribute.IsDesignOnly) : null);
			bool isDesignOnly = this.IsDesignOnly;
			return (flag.GetValueOrDefault() == isDesignOnly) & (flag != null);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003E504 File Offset: 0x0003C704
		public override int GetHashCode()
		{
			return this.IsDesignOnly.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003E51F File Offset: 0x0003C71F
		public override bool IsDefaultAttribute()
		{
			return this.IsDesignOnly == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		/// <summary>Specifies that a property can be set only at design time. This static field is read-only.</summary>
		// Token: 0x0400098A RID: 2442
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		/// <summary>Specifies that a property can be set at design time or at run time. This static field is read-only.</summary>
		// Token: 0x0400098B RID: 2443
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />, which is <see cref="F:System.ComponentModel.DesignOnlyAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x0400098C RID: 2444
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
