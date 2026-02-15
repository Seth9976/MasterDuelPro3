using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the property can be used as an application setting.</summary>
	// Token: 0x02000294 RID: 660
	[AttributeUsage(AttributeTargets.Property)]
	[Obsolete("Use System.ComponentModel.SettingsBindableAttribute instead to work with the new settings model.")]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" /> class.</summary>
		/// <param name="recommendedAsConfigurable">true if the property this attribute is bound to can be used as an application setting; otherwise, false. </param>
		// Token: 0x06000FE1 RID: 4065 RVA: 0x000432DC File Offset: 0x000414DC
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.RecommendedAsConfigurable = recommendedAsConfigurable;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to can be used as an application setting.</summary>
		/// <returns>true if the property this attribute is bound to can be used as an application setting; otherwise, false.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000432EB File Offset: 0x000414EB
		public bool RecommendedAsConfigurable { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06000FE3 RID: 4067 RVA: 0x000432F4 File Offset: 0x000414F4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecommendedAsConfigurableAttribute recommendedAsConfigurableAttribute = obj as RecommendedAsConfigurableAttribute;
			return recommendedAsConfigurableAttribute != null && recommendedAsConfigurableAttribute.RecommendedAsConfigurable == this.RecommendedAsConfigurable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />.</returns>
		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value for the class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06000FE5 RID: 4069 RVA: 0x00043321 File Offset: 0x00041521
		public override bool IsDefaultAttribute()
		{
			return !this.RecommendedAsConfigurable;
		}

		/// <summary>Specifies that a property cannot be used as an application setting. This static field is read-only.</summary>
		// Token: 0x04000A37 RID: 2615
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		/// <summary>Specifies that a property can be used as an application setting. This static field is read-only.</summary>
		// Token: 0x04000A38 RID: 2616
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />, which is <see cref="F:System.ComponentModel.RecommendedAsConfigurableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04000A39 RID: 2617
		public static readonly RecommendedAsConfigurableAttribute Default = RecommendedAsConfigurableAttribute.No;
	}
}
