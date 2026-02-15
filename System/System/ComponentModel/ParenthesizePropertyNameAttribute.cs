using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the name of the associated property is displayed with parentheses in the Properties window. This class cannot be inherited.</summary>
	// Token: 0x020002D8 RID: 728
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class that indicates that the associated property should not be shown with parentheses.</summary>
		// Token: 0x060011C8 RID: 4552 RVA: 0x00051630 File Offset: 0x0004F830
		public ParenthesizePropertyNameAttribute()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class, using the specified value to indicate whether the attribute is displayed with parentheses.</summary>
		/// <param name="needParenthesis">true if the name should be enclosed in parentheses; otherwise, false. </param>
		// Token: 0x060011C9 RID: 4553 RVA: 0x00051639 File Offset: 0x0004F839
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.needParenthesis = needParenthesis;
		}

		/// <summary>Gets a value indicating whether the Properties window displays the name of the property in parentheses in the Properties window.</summary>
		/// <returns>true if the property is displayed with parentheses; otherwise, false.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00051648 File Offset: 0x0004F848
		public bool NeedParenthesis
		{
			get
			{
				return this.needParenthesis;
			}
		}

		/// <summary>Compares the specified object to this object and tests for equality.</summary>
		/// <returns>true if equal; otherwise, false.</returns>
		/// <param name="o">The object to be compared. </param>
		// Token: 0x060011CB RID: 4555 RVA: 0x00051650 File Offset: 0x0004F850
		public override bool Equals(object o)
		{
			return o is ParenthesizePropertyNameAttribute && ((ParenthesizePropertyNameAttribute)o).NeedParenthesis == this.needParenthesis;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x060011CC RID: 4556 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x060011CD RID: 4557 RVA: 0x0005166F File Offset: 0x0004F86F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParenthesizePropertyNameAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class with a default value that indicates that the associated property should not be shown with parentheses. This field is read-only.</summary>
		// Token: 0x04000ADA RID: 2778
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();

		// Token: 0x04000ADB RID: 2779
		private bool needParenthesis;
	}
}
