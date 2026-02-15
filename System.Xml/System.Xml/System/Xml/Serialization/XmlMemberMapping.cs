using System;
using System.CodeDom.Compiler;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Maps a code entity in a .NET Framework Web service method to an element in a Web Services Description Language (WSDL) message.</summary>
	// Token: 0x020001B4 RID: 436
	public class XmlMemberMapping
	{
		// Token: 0x060014E1 RID: 5345 RVA: 0x00063FF1 File Offset: 0x000621F1
		internal XmlMemberMapping(MemberMapping mapping)
		{
			this.mapping = mapping;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00064000 File Offset: 0x00062200
		internal MemberMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00064008 File Offset: 0x00062208
		internal Accessor Accessor
		{
			get
			{
				return this.mapping.Accessor;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the .NET Framework type maps to an XML element or attribute of any type. </summary>
		/// <returns>true, if the type maps to an XML any element or attribute; otherwise, false.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00064015 File Offset: 0x00062215
		public bool Any
		{
			get
			{
				return this.Accessor.Any;
			}
		}

		/// <summary>Gets the unqualified name of the XML element declaration that applies to this mapping. </summary>
		/// <returns>The unqualified name of the XML element declaration that applies to this mapping.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00064022 File Offset: 0x00062222
		public string ElementName
		{
			get
			{
				return Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		/// <summary>Gets the XML element name as it appears in the service description document.</summary>
		/// <returns>The XML element name.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00064034 File Offset: 0x00062234
		public string XsdElementName
		{
			get
			{
				return this.Accessor.Name;
			}
		}

		/// <summary>Gets the XML namespace that applies to this mapping. </summary>
		/// <returns>The XML namespace that applies to this mapping.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00064041 File Offset: 0x00062241
		public string Namespace
		{
			get
			{
				return this.Accessor.Namespace;
			}
		}

		/// <summary>Gets the name of the Web service method member that is represented by this mapping. </summary>
		/// <returns>The name of the Web service method member represented by this mapping.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0006404E File Offset: 0x0006224E
		public string MemberName
		{
			get
			{
				return this.mapping.Name;
			}
		}

		/// <summary>Gets the type name of the .NET Framework type for this mapping. </summary>
		/// <returns>The type name of the .NET Framework type for this mapping.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x0006405B File Offset: 0x0006225B
		public string TypeName
		{
			get
			{
				if (this.Accessor.Mapping == null)
				{
					return string.Empty;
				}
				return this.Accessor.Mapping.TypeName;
			}
		}

		/// <summary>Gets the namespace of the .NET Framework type for this mapping.</summary>
		/// <returns>The namespace of the .NET Framework type for this mapping.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00064080 File Offset: 0x00062280
		public string TypeNamespace
		{
			get
			{
				if (this.Accessor.Mapping == null)
				{
					return null;
				}
				return this.Accessor.Mapping.Namespace;
			}
		}

		/// <summary>Gets the fully qualified type name of the .NET Framework type for this mapping. </summary>
		/// <returns>The fully qualified type name of the .NET Framework type for this mapping.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000640A1 File Offset: 0x000622A1
		public string TypeFullName
		{
			get
			{
				return this.mapping.TypeDesc.FullName;
			}
		}

		/// <summary>Gets a value that indicates whether the accompanying field in the .NET Framework type has a value specified.</summary>
		/// <returns>true, if the accompanying field has a value specified; otherwise, false.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x000640B3 File Offset: 0x000622B3
		public bool CheckSpecified
		{
			get
			{
				return this.mapping.CheckSpecified > SpecifiedAccessor.None;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000640C3 File Offset: 0x000622C3
		internal bool IsNullable
		{
			get
			{
				return this.mapping.IsNeedNullable;
			}
		}

		/// <summary>Returns the name of the type associated with the specified <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />.</summary>
		/// <returns>The name of the type.</returns>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  that contains the name of the type.</param>
		// Token: 0x060014EE RID: 5358 RVA: 0x000640D0 File Offset: 0x000622D0
		public string GenerateTypeName(CodeDomProvider codeProvider)
		{
			return this.mapping.GetTypeName(codeProvider);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal XmlMemberMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400097E RID: 2430
		private MemberMapping mapping;
	}
}
