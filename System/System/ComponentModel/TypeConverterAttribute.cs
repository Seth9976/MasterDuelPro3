using System;

namespace System.ComponentModel
{
	/// <summary>Specifies what type to use as a converter for the object this attribute is bound to.</summary>
	// Token: 0x020002A1 RID: 673
	[AttributeUsage(AttributeTargets.All)]
	public sealed class TypeConverterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class with the default type converter, which is an empty string ("").</summary>
		// Token: 0x0600101F RID: 4127 RVA: 0x00043FC7 File Offset: 0x000421C7
		public TypeConverterAttribute()
		{
			this.ConverterTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type as the data converter for the object this attribute is bound to.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the converter class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06001020 RID: 4128 RVA: 0x00043FDA File Offset: 0x000421DA
		public TypeConverterAttribute(Type type)
		{
			this.ConverterTypeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type name as the data converter for the object this attribute is bound to.</summary>
		/// <param name="typeName">The fully qualified name of the class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06001021 RID: 4129 RVA: 0x00043FEE File Offset: 0x000421EE
		public TypeConverterAttribute(string typeName)
		{
			this.ConverterTypeName = typeName;
		}

		/// <summary>Gets the fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to.</summary>
		/// <returns>The fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to, or an empty string ("") if none exists. The default value is an empty string ("").</returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00043FFD File Offset: 0x000421FD
		public string ConverterTypeName { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001023 RID: 4131 RVA: 0x00044008 File Offset: 0x00042208
		public override bool Equals(object obj)
		{
			TypeConverterAttribute typeConverterAttribute = obj as TypeConverterAttribute;
			return typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName == this.ConverterTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</returns>
		// Token: 0x06001024 RID: 4132 RVA: 0x00044032 File Offset: 0x00042232
		public override int GetHashCode()
		{
			return this.ConverterTypeName.GetHashCode();
		}

		/// <summary>Specifies the type to use as a converter for the object this attribute is bound to. </summary>
		// Token: 0x04000A50 RID: 2640
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();
	}
}
