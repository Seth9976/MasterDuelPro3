using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
	// Token: 0x0200024A RID: 586
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
		/// <param name="immutable">true if the object is immutable; otherwise, false. </param>
		// Token: 0x06000E0F RID: 3599 RVA: 0x0003E887 File Offset: 0x0003CA87
		public ImmutableObjectAttribute(bool immutable)
		{
			this.Immutable = immutable;
		}

		/// <summary>Gets whether the object is immutable.</summary>
		/// <returns>true if the object is immutable; otherwise, false.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003E896 File Offset: 0x0003CA96
		public bool Immutable { get; }

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000E11 RID: 3601 RVA: 0x0003E8A0 File Offset: 0x0003CAA0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			bool? flag = ((immutableObjectAttribute != null) ? new bool?(immutableObjectAttribute.Immutable) : null);
			bool immutable = this.Immutable;
			return (flag.GetValueOrDefault() == immutable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
		// Token: 0x06000E12 RID: 3602 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06000E13 RID: 3603 RVA: 0x0003E8EC File Offset: 0x0003CAEC
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ImmutableObjectAttribute.Default);
		}

		/// <summary>Specifies that an object has no subproperties that can be edited. This static field is read-only.</summary>
		// Token: 0x040009A2 RID: 2466
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		/// <summary>Specifies that an object has at least one editable subproperty. This static field is read-only.</summary>
		// Token: 0x040009A3 RID: 2467
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
		// Token: 0x040009A4 RID: 2468
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;
	}
}
