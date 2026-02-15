using System;
using System.Runtime.Serialization;
using Unity;

namespace System.Xml.Linq
{
	/// <summary>Represents a name of an XML element or attribute. </summary>
	// Token: 0x0200001C RID: 28
	[Serializable]
	public sealed class XName : IEquatable<XName>, ISerializable
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004A5D File Offset: 0x00002C5D
		internal XName(XNamespace ns, string localName)
		{
			this._ns = ns;
			this._localName = XmlConvert.VerifyNCName(localName);
			this._hashCode = ns.GetHashCode() ^ localName.GetHashCode();
		}

		/// <summary>Gets the local (unqualified) part of the name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local (unqualified) part of the name.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004A8B File Offset: 0x00002C8B
		public string LocalName
		{
			get
			{
				return this._localName;
			}
		}

		/// <summary>Gets the namespace part of the fully qualified name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the namespace part of the name.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004A93 File Offset: 0x00002C93
		public XNamespace Namespace
		{
			get
			{
				return this._ns;
			}
		}

		/// <summary>Returns the URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>The URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004A9B File Offset: 0x00002C9B
		public string NamespaceName
		{
			get
			{
				return this._ns.NamespaceName;
			}
		}

		/// <summary>Returns the expanded XML name in the format {namespace}localname.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the expanded XML name in the format {namespace}localname.</returns>
		// Token: 0x060000A2 RID: 162 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public override string ToString()
		{
			if (this._ns.NamespaceName.Length == 0)
			{
				return this._localName;
			}
			return "{" + this._ns.NamespaceName + "}" + this._localName;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from an expanded name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		/// <param name="expandedName">A <see cref="T:System.String" /> that contains an expanded XML name in the format {namespace}localname.</param>
		// Token: 0x060000A3 RID: 163 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public static XName Get(string expandedName)
		{
			if (expandedName == null)
			{
				throw new ArgumentNullException("expandedName");
			}
			if (expandedName.Length == 0)
			{
				throw new ArgumentException(global::SR.Format("'{0}' is an invalid expanded name.", expandedName));
			}
			if (expandedName[0] != '{')
			{
				return XNamespace.None.GetName(expandedName);
			}
			int num = expandedName.LastIndexOf('}');
			if (num <= 1 || num == expandedName.Length - 1)
			{
				throw new ArgumentException(global::SR.Format("'{0}' is an invalid expanded name.", expandedName));
			}
			return XNamespace.Get(expandedName, 1, num - 1).GetName(expandedName, num + 1, expandedName.Length - num - 1);
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from a local name and a namespace.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object created from the specified local name and namespace.</returns>
		/// <param name="localName">A local (unqualified) name.</param>
		/// <param name="namespaceName">An XML namespace.</param>
		// Token: 0x060000A4 RID: 164 RVA: 0x00004B75 File Offset: 0x00002D75
		public static XName Get(string localName, string namespaceName)
		{
			return XNamespace.Get(namespaceName).GetName(localName);
		}

		/// <summary>Converts a string formatted as an expanded XML name (that is,{namespace}localname) to an <see cref="T:System.Xml.Linq.XName" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		/// <param name="expandedName">A string that contains an expanded XML name in the format {namespace}localname.</param>
		// Token: 0x060000A5 RID: 165 RVA: 0x00004B83 File Offset: 0x00002D83
		[CLSCompliant(false)]
		public static implicit operator XName(string expandedName)
		{
			if (expandedName == null)
			{
				return null;
			}
			return XName.Get(expandedName);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.Linq.XName" /> is equal to this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Xml.Linq.XName" /> is equal to the current <see cref="T:System.Xml.Linq.XName" />; otherwise false.</returns>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XName" /> to compare to the current <see cref="T:System.Xml.Linq.XName" />.</param>
		// Token: 0x060000A6 RID: 166 RVA: 0x00004B90 File Offset: 0x00002D90
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the hash code for the <see cref="T:System.Xml.Linq.XName" />.</returns>
		// Token: 0x060000A7 RID: 167 RVA: 0x00004B96 File Offset: 0x00002D96
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XName" /> are equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise false.</returns>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		// Token: 0x060000A8 RID: 168 RVA: 0x00004B90 File Offset: 0x00002D90
		public static bool operator ==(XName left, XName right)
		{
			return left == right;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004B90 File Offset: 0x00002D90
		bool IEquatable<XName>.Equals(XName other)
		{
			return this == other;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060000AA RID: 170 RVA: 0x00004B9E File Offset: 0x00002D9E
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004BA5 File Offset: 0x00002DA5
		internal XName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000045 RID: 69
		private XNamespace _ns;

		// Token: 0x04000046 RID: 70
		private string _localName;

		// Token: 0x04000047 RID: 71
		private int _hashCode;
	}
}
