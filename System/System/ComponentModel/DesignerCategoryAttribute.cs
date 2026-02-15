using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the designer for a class belongs to a certain category.</summary>
	// Token: 0x02000241 RID: 577
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with an empty string ("").</summary>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003E555 File Offset: 0x0003C755
		public DesignerCategoryAttribute()
		{
			this.Category = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with the given category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003E568 File Offset: 0x0003C768
		public DesignerCategoryAttribute(string category)
		{
			this.Category = category;
		}

		/// <summary>Gets the name of the category.</summary>
		/// <returns>The name of the category.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0003E577 File Offset: 0x0003C777
		public string Category { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003E580 File Offset: 0x0003C780
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerCategoryAttribute designerCategoryAttribute = obj as DesignerCategoryAttribute;
			return designerCategoryAttribute != null && designerCategoryAttribute.Category == this.Category;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003E5B0 File Offset: 0x0003C7B0
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000DEA RID: 3562 RVA: 0x0003E5BD File Offset: 0x0003C7BD
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(DesignerCategoryAttribute.Default.Category);
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.Category;
			}
		}

		/// <summary>Specifies that a component marked with this category use a component designer. This field is read-only.</summary>
		// Token: 0x0400098D RID: 2445
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		/// <summary>Specifies that a component marked with this category cannot use a visual designer. This static field is read-only.</summary>
		// Token: 0x0400098E RID: 2446
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute();

		/// <summary>Specifies that a component marked with this category use a form designer. This static field is read-only.</summary>
		// Token: 0x0400098F RID: 2447
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		/// <summary>Specifies that a component marked with this category use a generic designer. This static field is read-only.</summary>
		// Token: 0x04000990 RID: 2448
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");
	}
}
