using System;

namespace System.Xml.Serialization
{
	/// <summary>Applied to a Web service client proxy, enables you to specify an assembly that contains custom-made serializers. </summary>
	// Token: 0x020001E1 RID: 481
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class XmlSerializerAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class. </summary>
		// Token: 0x060018F2 RID: 6386 RVA: 0x00095E18 File Offset: 0x00094018
		public XmlSerializerAssemblyAttribute()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		// Token: 0x060018F3 RID: 6387 RVA: 0x00095E22 File Offset: 0x00094022
		public XmlSerializerAssemblyAttribute(string assemblyName)
			: this(assemblyName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name and location of the assembly.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		/// <param name="codeBase">A string that is the URL location of the assembly.</param>
		// Token: 0x060018F4 RID: 6388 RVA: 0x00095E2C File Offset: 0x0009402C
		public XmlSerializerAssemblyAttribute(string assemblyName, string codeBase)
		{
			this.assemblyName = assemblyName;
			this.codeBase = codeBase;
		}

		/// <summary>Gets or sets the location of the assembly that contains the serializers.</summary>
		/// <returns>A location, such as a path or URI, that points to the assembly.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x00095E42 File Offset: 0x00094042
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x00095E4A File Offset: 0x0009404A
		public string CodeBase
		{
			get
			{
				return this.codeBase;
			}
			set
			{
				this.codeBase = value;
			}
		}

		/// <summary>Gets or sets the name of the assembly that contains serializers for a specific set of types.</summary>
		/// <returns>The simple, unencrypted name of the assembly. </returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x00095E53 File Offset: 0x00094053
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x00095E5B File Offset: 0x0009405B
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
			set
			{
				this.assemblyName = value;
			}
		}

		// Token: 0x04000A9B RID: 2715
		private string assemblyName;

		// Token: 0x04000A9C RID: 2716
		private string codeBase;
	}
}
