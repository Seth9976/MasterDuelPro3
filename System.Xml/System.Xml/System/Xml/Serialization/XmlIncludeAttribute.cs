using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object.</summary>
	// Token: 0x020001B1 RID: 433
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class XmlIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlIncludeAttribute" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to include. </param>
		// Token: 0x060014CA RID: 5322 RVA: 0x00063E56 File Offset: 0x00062056
		public XmlIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to include.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object to include.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00063E65 File Offset: 0x00062065
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x00063E6D File Offset: 0x0006206D
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

		// Token: 0x04000972 RID: 2418
		private Type type;
	}
}
