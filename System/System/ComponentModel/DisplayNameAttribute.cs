using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the display name for a property, event, or public void method which takes no arguments. </summary>
	// Token: 0x02000244 RID: 580
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public class DisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class.</summary>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003E6C2 File Offset: 0x0003C8C2
		public DisplayNameAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class using the display name.</summary>
		/// <param name="displayName">The display name.</param>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003E6CF File Offset: 0x0003C8CF
		public DisplayNameAttribute(string displayName)
		{
			this.DisplayNameValue = displayName;
		}

		/// <summary>Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003E6DE File Offset: 0x0003C8DE
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		/// <summary>Gets or sets the display name.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0003E6E6 File Offset: 0x0003C8E6
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0003E6EE File Offset: 0x0003C8EE
		protected string DisplayNameValue { get; set; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.DisplayNameAttribute" /> instances are equal.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.DisplayNameAttribute" /> to test the value equality of.</param>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.DisplayName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.DisplayNameAttribute" />.</returns>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003E728 File Offset: 0x0003C928
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000DFA RID: 3578 RVA: 0x0003E735 File Offset: 0x0003C935
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DisplayNameAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DisplayNameAttribute" />. This field is read-only.</summary>
		// Token: 0x0400099B RID: 2459
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();
	}
}
