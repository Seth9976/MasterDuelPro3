using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the filter string and filter type to use for a toolbox item.</summary>
	// Token: 0x0200029F RID: 671
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		// Token: 0x06001016 RID: 4118 RVA: 0x00043E9B File Offset: 0x0004209B
		public ToolboxItemFilterAttribute(string filterString)
			: this(filterString, ToolboxItemFilterType.Allow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string and type.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		/// <param name="filterType">A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> indicating the type of the filter. </param>
		// Token: 0x06001017 RID: 4119 RVA: 0x00043EA5 File Offset: 0x000420A5
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			this.FilterString = filterString ?? string.Empty;
			this.FilterType = filterType;
		}

		/// <summary>Gets the filter string for the toolbox item.</summary>
		/// <returns>The filter string for the toolbox item.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00043EC4 File Offset: 0x000420C4
		public string FilterString { get; }

		/// <summary>Gets the type of the filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> that indicates the type of the filter.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00043ECC File Offset: 0x000420CC
		public ToolboxItemFilterType FilterType { get; }

		/// <summary>Gets the type ID for the attribute.</summary>
		/// <returns>The type ID for this attribute. All <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects with the same filter string return the same type ID.</returns>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00043ED4 File Offset: 0x000420D4
		public override object TypeId
		{
			get
			{
				string text;
				if ((text = this._typeId) == null)
				{
					text = (this._typeId = base.GetType().FullName + this.FilterString);
				}
				return text;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x0600101B RID: 4123 RVA: 0x00043F0C File Offset: 0x0004210C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterType.Equals(this.FilterType) && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00043F5D File Offset: 0x0004215D
		public override int GetHashCode()
		{
			return this.FilterString.GetHashCode();
		}

		/// <summary>Indicates whether the specified object has a matching filter string.</summary>
		/// <returns>true if the specified object has a matching filter string; otherwise, false.</returns>
		/// <param name="obj">The object to test for a matching filter string. </param>
		// Token: 0x0600101D RID: 4125 RVA: 0x00043F6C File Offset: 0x0004216C
		public override bool Match(object obj)
		{
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00043F9B File Offset: 0x0004219B
		public override string ToString()
		{
			return this.FilterString + "," + Enum.GetName(typeof(ToolboxItemFilterType), this.FilterType);
		}

		// Token: 0x04000A48 RID: 2632
		private string _typeId;
	}
}
