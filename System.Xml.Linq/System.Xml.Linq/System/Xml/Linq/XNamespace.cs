using System;
using System.Threading;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML namespace. This class cannot be inherited. </summary>
	// Token: 0x0200001D RID: 29
	public sealed class XNamespace
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00004BAC File Offset: 0x00002DAC
		internal XNamespace(string namespaceName)
		{
			this._namespaceName = namespaceName;
			this._hashCode = namespaceName.GetHashCode();
			this._names = new XHashtable<XName>(new XHashtable<XName>.ExtractKeyDelegate(XNamespace.ExtractLocalName), 8);
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of this namespace.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the URI of the namespace.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004BDF File Offset: 0x00002DDF
		public string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.Linq.XName" /> object created from this <see cref="T:System.Xml.Linq.XNamespace" /> and the specified local name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> created from this <see cref="T:System.Xml.Linq.XNamespace" /> and the specified local name.</returns>
		/// <param name="localName">A <see cref="T:System.String" /> that contains a local name.</param>
		// Token: 0x060000AE RID: 174 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public XName GetName(string localName)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			return this.GetName(localName, 0, localName.Length);
		}

		/// <summary>Returns the URI of this <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>The URI of this <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		// Token: 0x060000AF RID: 175 RVA: 0x00004BDF File Offset: 0x00002DDF
		public override string ToString()
		{
			return this._namespaceName;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to no namespace.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to no namespace.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004C05 File Offset: 0x00002E05
		public static XNamespace None
		{
			get
			{
				return XNamespace.EnsureNamespace(ref XNamespace.s_refNone, string.Empty);
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to the XML URI (http://www.w3.org/XML/1998/namespace).</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to the XML URI (http://www.w3.org/XML/1998/namespace).</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004C16 File Offset: 0x00002E16
		public static XNamespace Xml
		{
			get
			{
				return XNamespace.EnsureNamespace(ref XNamespace.s_refXml, "http://www.w3.org/XML/1998/namespace");
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to the xmlns URI (http://www.w3.org/2000/xmlns/).</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to the xmlns URI (http://www.w3.org/2000/xmlns/).</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004C27 File Offset: 0x00002E27
		public static XNamespace Xmlns
		{
			get
			{
				return XNamespace.EnsureNamespace(ref XNamespace.s_refXmlns, "http://www.w3.org/2000/xmlns/");
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XNamespace" /> for the specified Uniform Resource Identifier (URI).</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> created from the specified URI.</returns>
		/// <param name="namespaceName">A <see cref="T:System.String" /> that contains a namespace URI.</param>
		// Token: 0x060000B3 RID: 179 RVA: 0x00004C38 File Offset: 0x00002E38
		public static XNamespace Get(string namespaceName)
		{
			if (namespaceName == null)
			{
				throw new ArgumentNullException("namespaceName");
			}
			return XNamespace.Get(namespaceName, 0, namespaceName.Length);
		}

		/// <summary>Converts a string containing a Uniform Resource Identifier (URI) to an <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> constructed from the URI string.</returns>
		/// <param name="namespaceName">A <see cref="T:System.String" /> that contains the namespace URI.</param>
		// Token: 0x060000B4 RID: 180 RVA: 0x00004C55 File Offset: 0x00002E55
		[CLSCompliant(false)]
		public static implicit operator XNamespace(string namespaceName)
		{
			if (namespaceName == null)
			{
				return null;
			}
			return XNamespace.Get(namespaceName);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.Linq.XNamespace" /> is equal to the current <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether the specified <see cref="T:System.Xml.Linq.XNamespace" /> is equal to the current <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XNamespace" /> to compare to the current <see cref="T:System.Xml.Linq.XNamespace" />.</param>
		// Token: 0x060000B5 RID: 181 RVA: 0x00004B90 File Offset: 0x00002D90
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the hash code for the <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		// Token: 0x060000B6 RID: 182 RVA: 0x00004C62 File Offset: 0x00002E62
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XNamespace" /> are equal.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether <paramref name="left" /> and <paramref name="right" /> are equal.</returns>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		// Token: 0x060000B7 RID: 183 RVA: 0x00004B90 File Offset: 0x00002D90
		public static bool operator ==(XNamespace left, XNamespace right)
		{
			return left == right;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XNamespace" /> are not equal.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether <paramref name="left" /> and <paramref name="right" /> are not equal.</returns>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		// Token: 0x060000B8 RID: 184 RVA: 0x00004C6A File Offset: 0x00002E6A
		public static bool operator !=(XNamespace left, XNamespace right)
		{
			return left != right;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004C74 File Offset: 0x00002E74
		internal XName GetName(string localName, int index, int count)
		{
			XName xname;
			if (this._names.TryGetValue(localName, index, count, out xname))
			{
				return xname;
			}
			return this._names.Add(new XName(this, localName.Substring(index, count)));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004CB0 File Offset: 0x00002EB0
		internal static XNamespace Get(string namespaceName, int index, int count)
		{
			if (count == 0)
			{
				return XNamespace.None;
			}
			if (XNamespace.s_namespaces == null)
			{
				Interlocked.CompareExchange<XHashtable<WeakReference>>(ref XNamespace.s_namespaces, new XHashtable<WeakReference>(new XHashtable<WeakReference>.ExtractKeyDelegate(XNamespace.ExtractNamespace), 32), null);
			}
			for (;;)
			{
				WeakReference weakReference;
				if (!XNamespace.s_namespaces.TryGetValue(namespaceName, index, count, out weakReference))
				{
					if (count == "http://www.w3.org/XML/1998/namespace".Length && string.CompareOrdinal(namespaceName, index, "http://www.w3.org/XML/1998/namespace", 0, count) == 0)
					{
						break;
					}
					if (count == "http://www.w3.org/2000/xmlns/".Length && string.CompareOrdinal(namespaceName, index, "http://www.w3.org/2000/xmlns/", 0, count) == 0)
					{
						goto Block_7;
					}
					weakReference = XNamespace.s_namespaces.Add(new WeakReference(new XNamespace(namespaceName.Substring(index, count))));
				}
				XNamespace xnamespace = ((weakReference != null) ? ((XNamespace)weakReference.Target) : null);
				if (!(xnamespace == null))
				{
					return xnamespace;
				}
			}
			return XNamespace.Xml;
			Block_7:
			return XNamespace.Xmlns;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004D7F File Offset: 0x00002F7F
		private static string ExtractLocalName(XName n)
		{
			return n.LocalName;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004D88 File Offset: 0x00002F88
		private static string ExtractNamespace(WeakReference r)
		{
			XNamespace xnamespace;
			if (r == null || (xnamespace = (XNamespace)r.Target) == null)
			{
				return null;
			}
			return xnamespace.NamespaceName;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004DB8 File Offset: 0x00002FB8
		private static XNamespace EnsureNamespace(ref WeakReference refNmsp, string namespaceName)
		{
			XNamespace xnamespace;
			for (;;)
			{
				WeakReference weakReference = refNmsp;
				if (weakReference != null)
				{
					xnamespace = (XNamespace)weakReference.Target;
					if (xnamespace != null)
					{
						break;
					}
				}
				Interlocked.CompareExchange<WeakReference>(ref refNmsp, new WeakReference(new XNamespace(namespaceName)), weakReference);
			}
			return xnamespace;
		}

		// Token: 0x04000048 RID: 72
		private static XHashtable<WeakReference> s_namespaces;

		// Token: 0x04000049 RID: 73
		private static WeakReference s_refNone;

		// Token: 0x0400004A RID: 74
		private static WeakReference s_refXml;

		// Token: 0x0400004B RID: 75
		private static WeakReference s_refXmlns;

		// Token: 0x0400004C RID: 76
		private string _namespaceName;

		// Token: 0x0400004D RID: 77
		private int _hashCode;

		// Token: 0x0400004E RID: 78
		private XHashtable<XName> _names;
	}
}
