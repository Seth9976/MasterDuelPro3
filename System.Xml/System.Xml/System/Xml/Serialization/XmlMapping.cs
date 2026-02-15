using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Supports mappings between .NET Framework types and XML Schema data types. </summary>
	// Token: 0x020001B3 RID: 435
	public abstract class XmlMapping
	{
		// Token: 0x060014CD RID: 5325 RVA: 0x00063E76 File Offset: 0x00062076
		internal XmlMapping(TypeScope scope, ElementAccessor accessor)
			: this(scope, accessor, XmlMappingAccess.Read | XmlMappingAccess.Write)
		{
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00063E81 File Offset: 0x00062081
		internal XmlMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access)
		{
			this.scope = scope;
			this.accessor = accessor;
			this.access = access;
			this.shallow = scope == null;
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00063EA8 File Offset: 0x000620A8
		internal ElementAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00063EB0 File Offset: 0x000620B0
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		/// <summary>Get the name of the mapped element.</summary>
		/// <returns>The name of the mapped element.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00063EB8 File Offset: 0x000620B8
		public string ElementName
		{
			get
			{
				return global::System.Xml.Serialization.Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		/// <summary>Gets the name of the XSD element of the mapping.</summary>
		/// <returns>The XSD element name.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00063ECA File Offset: 0x000620CA
		public string XsdElementName
		{
			get
			{
				return this.Accessor.Name;
			}
		}

		/// <summary>Gets the namespace of the mapped element.</summary>
		/// <returns>The namespace of the mapped element.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00063ED7 File Offset: 0x000620D7
		public string Namespace
		{
			get
			{
				return this.accessor.Namespace;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00063EE4 File Offset: 0x000620E4
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00063EEC File Offset: 0x000620EC
		internal bool GenerateSerializer
		{
			get
			{
				return this.generateSerializer;
			}
			set
			{
				this.generateSerializer = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00063EF5 File Offset: 0x000620F5
		internal bool IsReadable
		{
			get
			{
				return (this.access & XmlMappingAccess.Read) > XmlMappingAccess.None;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00063F02 File Offset: 0x00062102
		internal bool IsWriteable
		{
			get
			{
				return (this.access & XmlMappingAccess.Write) > XmlMappingAccess.None;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00063F0F File Offset: 0x0006210F
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00063F17 File Offset: 0x00062117
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		/// <summary>Sets the key used to look up the mapping.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the lookup key.</param>
		// Token: 0x060014DA RID: 5338 RVA: 0x00063F20 File Offset: 0x00062120
		public void SetKey(string key)
		{
			this.SetKeyInternal(key);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00063F29 File Offset: 0x00062129
		internal void SetKeyInternal(string key)
		{
			this.key = key;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00063F34 File Offset: 0x00062134
		internal static string GenerateKey(Type type, XmlRootAttribute root, string ns)
		{
			if (root == null)
			{
				root = (XmlRootAttribute)XmlAttributes.GetAttr(type, typeof(XmlRootAttribute));
			}
			return string.Concat(new string[]
			{
				type.FullName,
				":",
				(root == null) ? string.Empty : root.Key,
				":",
				(ns == null) ? string.Empty : ns
			});
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00063FA0 File Offset: 0x000621A0
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00063FA8 File Offset: 0x000621A8
		internal void CheckShallow()
		{
			if (this.shallow)
			{
				throw new InvalidOperationException(Res.GetString("This mapping was not crated by reflection importer and cannot be used in this context."));
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00063FC4 File Offset: 0x000621C4
		internal static bool IsShallow(XmlMapping[] mappings)
		{
			for (int i = 0; i < mappings.Length; i++)
			{
				if (mappings[i] == null || mappings[i].shallow)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal XmlMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000977 RID: 2423
		private TypeScope scope;

		// Token: 0x04000978 RID: 2424
		private bool generateSerializer;

		// Token: 0x04000979 RID: 2425
		private bool isSoap;

		// Token: 0x0400097A RID: 2426
		private ElementAccessor accessor;

		// Token: 0x0400097B RID: 2427
		private string key;

		// Token: 0x0400097C RID: 2428
		private bool shallow;

		// Token: 0x0400097D RID: 2429
		private XmlMappingAccess access;
	}
}
