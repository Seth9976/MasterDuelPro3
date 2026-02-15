using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</summary>
	// Token: 0x020001AF RID: 431
	[AttributeUsage(AttributeTargets.Field)]
	public class XmlEnumAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class.</summary>
		// Token: 0x060014C5 RID: 5317 RVA: 0x0005995F File Offset: 0x00057B5F
		public XmlEnumAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class, and specifies the XML value that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates or recognizes (when it serializes or deserializes the enumeration, respectively).</summary>
		/// <param name="name">The overriding name of the enumeration member. </param>
		// Token: 0x060014C6 RID: 5318 RVA: 0x00063E36 File Offset: 0x00062036
		public XmlEnumAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration, or the value recognized when it deserializes the enumeration member.</summary>
		/// <returns>The value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes the enumeration, or the value recognized when it is deserializes the enumeration member.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00063E45 File Offset: 0x00062045
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00063E4D File Offset: 0x0006204D
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000971 RID: 2417
		private string name;
	}
}
