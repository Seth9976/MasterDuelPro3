using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this property can be combined with properties belonging to other objects in a Properties window.</summary>
	// Token: 0x0200024E RID: 590
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MergablePropertyAttribute" /> class.</summary>
		/// <param name="allowMerge">true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false. </param>
		// Token: 0x06000E20 RID: 3616 RVA: 0x0003EA06 File Offset: 0x0003CC06
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.AllowMerge = allowMerge;
		}

		/// <summary>Gets a value indicating whether this property can be combined with properties belonging to other objects in a Properties window.</summary>
		/// <returns>true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0003EA15 File Offset: 0x0003CC15
		public bool AllowMerge { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06000E22 RID: 3618 RVA: 0x0003EA20 File Offset: 0x0003CC20
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			MergablePropertyAttribute mergablePropertyAttribute = obj as MergablePropertyAttribute;
			bool? flag = ((mergablePropertyAttribute != null) ? new bool?(mergablePropertyAttribute.AllowMerge) : null);
			bool allowMerge = this.AllowMerge;
			return (flag.GetValueOrDefault() == allowMerge) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MergablePropertyAttribute" />.</returns>
		// Token: 0x06000E23 RID: 3619 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000E24 RID: 3620 RVA: 0x0003EA6C File Offset: 0x0003CC6C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(MergablePropertyAttribute.Default);
		}

		/// <summary>Specifies that a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x040009AB RID: 2475
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);

		/// <summary>Specifies that a property cannot be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x040009AC RID: 2476
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.MergablePropertyAttribute.Yes" />, that is a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x040009AD RID: 2477
		public static readonly MergablePropertyAttribute Default = MergablePropertyAttribute.Yes;
	}
}
