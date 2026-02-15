using System;

namespace System.Xml.Serialization
{
	/// <summary>When applied to a type, stores the name of a static method of the type that returns an XML schema and a <see cref="T:System.Xml.XmlQualifiedName" /> (or <see cref="T:System.Xml.Schema.XmlSchemaType" /> for anonymous types) that controls the serialization of the type.</summary>
	// Token: 0x020001C2 RID: 450
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class XmlSchemaProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> class, taking the name of the static method that supplies the type's XML schema.</summary>
		/// <param name="methodName">The name of the static method that must be implemented.</param>
		// Token: 0x060015DC RID: 5596 RVA: 0x0006F4E1 File Offset: 0x0006D6E1
		public XmlSchemaProviderAttribute(string methodName)
		{
			this.methodName = methodName;
		}

		/// <summary>Gets the name of the static method that supplies the type's XML schema and the name of its XML Schema data type.</summary>
		/// <returns>The name of the method that is invoked by the XML infrastructure to return an XML schema.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0006F4F0 File Offset: 0x0006D6F0
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		/// <summary>Gets or sets a value that determines whether the target class is a wildcard, or that the schema for the class has contains only an xs:any element.</summary>
		/// <returns>true, if the class is a wildcard, or if the schema contains only the xs:any element; otherwise, false.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0006F4F8 File Offset: 0x0006D6F8
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0006F500 File Offset: 0x0006D700
		public bool IsAny
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x040009B4 RID: 2484
		private string methodName;

		// Token: 0x040009B5 RID: 2485
		private bool any;
	}
}
