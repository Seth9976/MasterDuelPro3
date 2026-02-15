using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x0200023F RID: 575
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with no parameters.</summary>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003E410 File Offset: 0x0003C610
		public DescriptionAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with a description.</summary>
		/// <param name="description">The description text. </param>
		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003E41D File Offset: 0x0003C61D
		public DescriptionAttribute(string description)
		{
			this.DescriptionValue = description;
		}

		/// <summary>Gets the description stored in this attribute.</summary>
		/// <returns>The description stored in this attribute.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003E42C File Offset: 0x0003C62C
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the string stored as the description.</summary>
		/// <returns>The string stored as the description. The default value is an empty string ("").</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0003E434 File Offset: 0x0003C634
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0003E43C File Offset: 0x0003C63C
		protected string DescriptionValue { get; set; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000DDB RID: 3547 RVA: 0x0003E448 File Offset: 0x0003C648
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003E478 File Offset: 0x0003C678
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance.</summary>
		/// <returns>true, if this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance; otherwise, false.</returns>
		// Token: 0x06000DDD RID: 3549 RVA: 0x0003E485 File Offset: 0x0003C685
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DescriptionAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DescriptionAttribute" />, which is an empty string (""). This static field is read-only.</summary>
		// Token: 0x04000987 RID: 2439
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();
	}
}
