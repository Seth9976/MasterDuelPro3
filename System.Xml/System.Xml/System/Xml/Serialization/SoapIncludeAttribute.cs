using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object as encoded SOAP XML.</summary>
	// Token: 0x0200018F RID: 399
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class SoapIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapIncludeAttribute" /> class using the specified type.</summary>
		/// <param name="type">The type of the object to include. </param>
		// Token: 0x060012C0 RID: 4800 RVA: 0x0005A48D File Offset: 0x0005868D
		public SoapIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to use when serializing or deserializing an object.</summary>
		/// <returns>The type of the object to include.</returns>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0005A49C File Offset: 0x0005869C
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x0005A4A4 File Offset: 0x000586A4
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x040008CF RID: 2255
		private Type type;
	}
}
