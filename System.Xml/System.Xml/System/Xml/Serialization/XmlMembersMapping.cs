using System;
using System.Text;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between .NET Framework Web service methods and Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x020001B5 RID: 437
	public class XmlMembersMapping : XmlMapping
	{
		// Token: 0x060014F0 RID: 5360 RVA: 0x000640E0 File Offset: 0x000622E0
		internal XmlMembersMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access)
			: base(scope, accessor, access)
		{
			MembersMapping membersMapping = (MembersMapping)accessor.Mapping;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(":");
			this.mappings = new XmlMemberMapping[membersMapping.Members.Length];
			for (int i = 0; i < this.mappings.Length; i++)
			{
				if (membersMapping.Members[i].TypeDesc.Type != null)
				{
					stringBuilder.Append(XmlMapping.GenerateKey(membersMapping.Members[i].TypeDesc.Type, null, null));
					stringBuilder.Append(":");
				}
				this.mappings[i] = new XmlMemberMapping(membersMapping.Members[i]);
			}
			base.SetKeyInternal(stringBuilder.ToString());
		}

		/// <summary>Gets the name of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The name of the .NET Framework type.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x000641A1 File Offset: 0x000623A1
		public string TypeName
		{
			get
			{
				return base.Accessor.Mapping.TypeName;
			}
		}

		/// <summary>Gets the namespace of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The .NET Framework namespace of the mapping.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000641B3 File Offset: 0x000623B3
		public string TypeNamespace
		{
			get
			{
				return base.Accessor.Mapping.Namespace;
			}
		}

		/// <summary>Gets an item that contains internal type mapping information for a .NET Framework code entity that belongs to a Web service method being mapped to a SOAP message.</summary>
		/// <returns>The requested <see cref="T:System.Xml.Serialization.XmlMemberMapping" />.</returns>
		/// <param name="index">The index of the mapping to return.</param>
		// Token: 0x17000509 RID: 1289
		public XmlMemberMapping this[int index]
		{
			get
			{
				return this.mappings[index];
			}
		}

		/// <summary>Gets the number of .NET Framework code entities that belong to a Web service method to which a SOAP message is being mapped. </summary>
		/// <returns>The number of mappings in the collection.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000641CF File Offset: 0x000623CF
		public int Count
		{
			get
			{
				return this.mappings.Length;
			}
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal XmlMembersMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400097F RID: 2431
		private XmlMemberMapping[] mappings;
	}
}
