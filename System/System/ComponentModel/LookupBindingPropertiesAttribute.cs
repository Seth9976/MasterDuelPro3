using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the properties that support lookup-based binding. This class cannot be inherited.</summary>
	// Token: 0x0200028C RID: 652
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LookupBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class using no parameters. </summary>
		// Token: 0x06000F6B RID: 3947 RVA: 0x000420A8 File Offset: 0x000402A8
		public LookupBindingPropertiesAttribute()
		{
			this.DataSource = null;
			this.DisplayMember = null;
			this.ValueMember = null;
			this.LookupMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="displayMember">The name of the property to be used for the display name.</param>
		/// <param name="valueMember">The name of the property to be used as the source for values.</param>
		/// <param name="lookupMember">The name of the property to be used for lookups.</param>
		// Token: 0x06000F6C RID: 3948 RVA: 0x000420CC File Offset: 0x000402CC
		public LookupBindingPropertiesAttribute(string dataSource, string displayMember, string valueMember, string lookupMember)
		{
			this.DataSource = dataSource;
			this.DisplayMember = displayMember;
			this.ValueMember = valueMember;
			this.LookupMember = lookupMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x000420F1 File Offset: 0x000402F1
		public string DataSource { get; }

		/// <summary>Gets the name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000420F9 File Offset: 0x000402F9
		public string DisplayMember { get; }

		/// <summary>Gets the name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00042101 File Offset: 0x00040301
		public string ValueMember { get; }

		/// <summary>Gets the name of the lookup member for the component to which this attribute is bound.</summary>
		/// <returns>The name of the lookup member for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00042109 File Offset: 0x00040309
		public string LookupMember { get; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance </param>
		// Token: 0x06000F71 RID: 3953 RVA: 0x00042114 File Offset: 0x00040314
		public override bool Equals(object obj)
		{
			LookupBindingPropertiesAttribute lookupBindingPropertiesAttribute = obj as LookupBindingPropertiesAttribute;
			return lookupBindingPropertiesAttribute != null && lookupBindingPropertiesAttribute.DataSource == this.DataSource && lookupBindingPropertiesAttribute.DisplayMember == this.DisplayMember && lookupBindingPropertiesAttribute.ValueMember == this.ValueMember && lookupBindingPropertiesAttribute.LookupMember == this.LookupMember;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" />.</returns>
		// Token: 0x06000F72 RID: 3954 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04000A1B RID: 2587
		public static readonly LookupBindingPropertiesAttribute Default = new LookupBindingPropertiesAttribute();
	}
}
