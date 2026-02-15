using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the component associated with this attribute has been inherited from a base class. This class cannot be inherited.</summary>
	// Token: 0x0200026E RID: 622
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class InheritanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class with the specified inheritance level.</summary>
		/// <param name="inheritanceLevel">An <see cref="T:System.ComponentModel.InheritanceLevel" /> that indicates the level of inheritance to set this attribute to. </param>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00041295 File Offset: 0x0003F495
		public InheritanceAttribute(InheritanceLevel inheritanceLevel)
		{
			this.InheritanceLevel = inheritanceLevel;
		}

		/// <summary>Gets or sets the current inheritance level stored in this attribute.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceLevel" /> stored in this attribute.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000412A4 File Offset: 0x0003F4A4
		public InheritanceLevel InheritanceLevel { get; }

		/// <summary>Override to test for equality.</summary>
		/// <returns>true if the object is the same; otherwise, false.</returns>
		/// <param name="value">The object to test. </param>
		// Token: 0x06000EBB RID: 3771 RVA: 0x000412AC File Offset: 0x0003F4AC
		public override bool Equals(object value)
		{
			return value == this || (value is InheritanceAttribute && ((InheritanceAttribute)value).InheritanceLevel == this.InheritanceLevel);
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06000EBC RID: 3772 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06000EBD RID: 3773 RVA: 0x000412D1 File Offset: 0x0003F4D1
		public override bool IsDefaultAttribute()
		{
			return this.Equals(InheritanceAttribute.Default);
		}

		/// <summary>Converts this attribute to a string.</summary>
		/// <returns>A string that represents this <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EBE RID: 3774 RVA: 0x000412DE File Offset: 0x0003F4DE
		public override string ToString()
		{
			return TypeDescriptor.GetConverter(typeof(InheritanceLevel)).ConvertToString(this.InheritanceLevel);
		}

		/// <summary>Specifies that the component is inherited. This field is read-only.</summary>
		// Token: 0x040009DE RID: 2526
		public static readonly InheritanceAttribute Inherited = new InheritanceAttribute(InheritanceLevel.Inherited);

		/// <summary>Specifies that the component is inherited and is read-only. This field is read-only.</summary>
		// Token: 0x040009DF RID: 2527
		public static readonly InheritanceAttribute InheritedReadOnly = new InheritanceAttribute(InheritanceLevel.InheritedReadOnly);

		/// <summary>Specifies that the component is not inherited. This field is read-only.</summary>
		// Token: 0x040009E0 RID: 2528
		public static readonly InheritanceAttribute NotInherited = new InheritanceAttribute(InheritanceLevel.NotInherited);

		/// <summary>Specifies that the default value for <see cref="T:System.ComponentModel.InheritanceAttribute" /> is <see cref="F:System.ComponentModel.InheritanceAttribute.NotInherited" />. This field is read-only.</summary>
		// Token: 0x040009E1 RID: 2529
		public static readonly InheritanceAttribute Default = InheritanceAttribute.NotInherited;
	}
}
