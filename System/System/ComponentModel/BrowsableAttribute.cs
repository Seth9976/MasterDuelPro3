using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property or event should be displayed in a Properties window.</summary>
	// Token: 0x0200023D RID: 573
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BrowsableAttribute" /> class.</summary>
		/// <param name="browsable">true if a property or event can be modified at design time; otherwise, false. The default is true. </param>
		// Token: 0x06000DCE RID: 3534 RVA: 0x0003E2C5 File Offset: 0x0003C4C5
		public BrowsableAttribute(bool browsable)
		{
			this.Browsable = browsable;
		}

		/// <summary>Gets a value indicating whether an object is browsable.</summary>
		/// <returns>true if the object is browsable; otherwise, false.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
		public bool Browsable { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003E2DC File Offset: 0x0003C4DC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			bool? flag = ((browsableAttribute != null) ? new bool?(browsableAttribute.Browsable) : null);
			bool browsable = this.Browsable;
			return (flag.GetValueOrDefault() == browsable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003E328 File Offset: 0x0003C528
		public override int GetHashCode()
		{
			return this.Browsable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003E343 File Offset: 0x0003C543
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BrowsableAttribute.Default);
		}

		/// <summary>Specifies that a property or event can be modified at design time. This static field is read-only.</summary>
		// Token: 0x04000983 RID: 2435
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		/// <summary>Specifies that a property or event cannot be modified at design time. This static field is read-only.</summary>
		// Token: 0x04000984 RID: 2436
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BrowsableAttribute" />, which is <see cref="F:System.ComponentModel.BrowsableAttribute.Yes" />. This static field is read-only.</summary>
		// Token: 0x04000985 RID: 2437
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;
	}
}
