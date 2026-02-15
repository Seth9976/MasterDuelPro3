using System;
using System.Reflection;

namespace System.Xml
{
	/// <summary>Represents an XML qualified name.</summary>
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class XmlQualifiedName
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class.</summary>
		// Token: 0x06000F33 RID: 3891 RVA: 0x0004C6F6 File Offset: 0x0004A8F6
		public XmlQualifiedName()
			: this(string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class with the specified name.</summary>
		/// <param name="name">The local name to use as the name of the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		// Token: 0x06000F34 RID: 3892 RVA: 0x0004C708 File Offset: 0x0004A908
		public XmlQualifiedName(string name)
			: this(name, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class with the specified name and namespace.</summary>
		/// <param name="name">The local name to use as the name of the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		/// <param name="ns">The namespace for the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		// Token: 0x06000F35 RID: 3893 RVA: 0x0004C716 File Offset: 0x0004A916
		public XmlQualifiedName(string name, string ns)
		{
			this.ns = ((ns == null) ? string.Empty : ns);
			this.name = ((name == null) ? string.Empty : name);
		}

		/// <summary>Gets a string representation of the namespace of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A string representation of the namespace or String.Empty if a namespace is not defined for the object.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0004C740 File Offset: 0x0004A940
		public string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		/// <summary>Gets a string representation of the qualified name of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A string representation of the qualified name or String.Empty if a name is not defined for the object.</returns>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0004C748 File Offset: 0x0004A948
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A hash code for this object.</returns>
		// Token: 0x06000F38 RID: 3896 RVA: 0x0004C750 File Offset: 0x0004A950
		public override int GetHashCode()
		{
			if (this.hash == 0)
			{
				if (XmlQualifiedName.hashCodeDelegate == null)
				{
					XmlQualifiedName.hashCodeDelegate = XmlQualifiedName.GetHashCodeDelegate();
				}
				this.hash = XmlQualifiedName.hashCodeDelegate(this.Name, this.Name.Length, 0L);
			}
			return this.hash;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlQualifiedName" /> is empty.</summary>
		/// <returns>true if name and namespace are empty strings; otherwise, false.</returns>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0004C79F File Offset: 0x0004A99F
		public bool IsEmpty
		{
			get
			{
				return this.Name.Length == 0 && this.Namespace.Length == 0;
			}
		}

		/// <summary>Returns the string value of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>The string value of the <see cref="T:System.Xml.XmlQualifiedName" /> in the format of namespace:localname. If the object does not have a namespace defined, this method returns just the local name.</returns>
		// Token: 0x06000F3A RID: 3898 RVA: 0x0004C7BE File Offset: 0x0004A9BE
		public override string ToString()
		{
			if (this.Namespace.Length != 0)
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.XmlQualifiedName" /> object is equal to the current <see cref="T:System.Xml.XmlQualifiedName" /> object. </summary>
		/// <returns>true if the two are the same instance object; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x06000F3B RID: 3899 RVA: 0x0004C7EC File Offset: 0x0004A9EC
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			XmlQualifiedName xmlQualifiedName = other as XmlQualifiedName;
			return xmlQualifiedName != null && this.Name == xmlQualifiedName.Name && this.Namespace == xmlQualifiedName.Namespace;
		}

		/// <summary>Compares two <see cref="T:System.Xml.XmlQualifiedName" /> objects.</summary>
		/// <returns>true if the two objects have the same name and namespace values; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		/// <param name="b">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x06000F3C RID: 3900 RVA: 0x0004C837 File Offset: 0x0004AA37
		public static bool operator ==(XmlQualifiedName a, XmlQualifiedName b)
		{
			return a == b || (a != null && b != null && a.Name == b.Name && a.Namespace == b.Namespace);
		}

		/// <summary>Compares two <see cref="T:System.Xml.XmlQualifiedName" /> objects.</summary>
		/// <returns>true if the name and namespace values for the two objects differ; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		/// <param name="b">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x06000F3D RID: 3901 RVA: 0x0004C86D File Offset: 0x0004AA6D
		public static bool operator !=(XmlQualifiedName a, XmlQualifiedName b)
		{
			return !(a == b);
		}

		/// <summary>Returns the string value of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>The string value of the <see cref="T:System.Xml.XmlQualifiedName" /> in the format of namespace:localname. If the object does not have a namespace defined, this method returns just the local name.</returns>
		/// <param name="name">The name of the object. </param>
		/// <param name="ns">The namespace of the object. </param>
		// Token: 0x06000F3E RID: 3902 RVA: 0x0004C879 File Offset: 0x0004AA79
		public static string ToString(string name, string ns)
		{
			if (ns != null && ns.Length != 0)
			{
				return ns + ":" + name;
			}
			return name;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0004C894 File Offset: 0x0004AA94
		private static XmlQualifiedName.HashCodeOfStringDelegate GetHashCodeDelegate()
		{
			if (!XmlQualifiedName.IsRandomizedHashingDisabled())
			{
				MethodInfo method = typeof(string).GetMethod("InternalMarvin32HashString", BindingFlags.Static | BindingFlags.NonPublic);
				if (method != null)
				{
					return (XmlQualifiedName.HashCodeOfStringDelegate)Delegate.CreateDelegate(typeof(XmlQualifiedName.HashCodeOfStringDelegate), method);
				}
			}
			return new XmlQualifiedName.HashCodeOfStringDelegate(XmlQualifiedName.GetHashCodeOfString);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		private static bool IsRandomizedHashingDisabled()
		{
			return false;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0004C8EA File Offset: 0x0004AAEA
		private static int GetHashCodeOfString(string s, int length, long additionalEntropy)
		{
			return s.GetHashCode();
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0004C8F2 File Offset: 0x0004AAF2
		internal void Init(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.hash = 0;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0004C909 File Offset: 0x0004AB09
		internal void SetNamespace(string ns)
		{
			this.ns = ns;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0004C912 File Offset: 0x0004AB12
		internal void Verify()
		{
			XmlConvert.VerifyNCName(this.name);
			if (this.ns.Length != 0)
			{
				XmlConvert.ToUri(this.ns);
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0004C939 File Offset: 0x0004AB39
		internal void Atomize(XmlNameTable nameTable)
		{
			this.name = nameTable.Add(this.name);
			this.ns = nameTable.Add(this.ns);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0004C960 File Offset: 0x0004AB60
		internal static XmlQualifiedName Parse(string s, IXmlNamespaceResolver nsmgr, out string prefix)
		{
			string text;
			ValidateNames.ParseQNameThrow(s, out prefix, out text);
			string text2 = nsmgr.LookupNamespace(prefix);
			if (text2 == null)
			{
				if (prefix.Length != 0)
				{
					throw new XmlException("'{0}' is an undeclared prefix.", prefix);
				}
				text2 = string.Empty;
			}
			return new XmlQualifiedName(text, text2);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0004C9A5 File Offset: 0x0004ABA5
		internal XmlQualifiedName Clone()
		{
			return (XmlQualifiedName)base.MemberwiseClone();
		}

		// Token: 0x04000777 RID: 1911
		private static XmlQualifiedName.HashCodeOfStringDelegate hashCodeDelegate = null;

		// Token: 0x04000778 RID: 1912
		private string name;

		// Token: 0x04000779 RID: 1913
		private string ns;

		// Token: 0x0400077A RID: 1914
		[NonSerialized]
		private int hash;

		/// <summary>Provides an empty <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		// Token: 0x0400077B RID: 1915
		public static readonly XmlQualifiedName Empty = new XmlQualifiedName(string.Empty);

		// Token: 0x02000130 RID: 304
		// (Invoke) Token: 0x06000F4A RID: 3914
		private delegate int HashCodeOfStringDelegate(string s, int sLen, long additionalEntropy);
	}
}
