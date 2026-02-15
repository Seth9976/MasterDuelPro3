using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Provides default functionality for all SOAP attributes.</summary>
	// Token: 0x0200046B RID: 1131
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>true if the target object of the current attribute must be serialized as an XML attribute; false if the target object must be serialized as a subelement.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0009670C File Offset: 0x0009490C
		public virtual bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
		}

		/// <summary>Gets or sets the XML namespace name.</summary>
		/// <returns>The XML namespace name under which the target of the current attribute is serialized.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x00096714 File Offset: 0x00094914
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0009671C File Offset: 0x0009491C
		internal virtual void SetReflectionObject(object reflectionObject)
		{
			this.ReflectInfo = reflectionObject;
		}

		// Token: 0x040011A2 RID: 4514
		private bool _useAttribute;

		/// <summary>The XML namespace to which the target of the current SOAP attribute is serialized.</summary>
		// Token: 0x040011A3 RID: 4515
		protected string ProtXmlNamespace;

		/// <summary>A reflection object used by attribute classes derived from the <see cref="T:System.Runtime.Remoting.Metadata.SoapAttribute" /> class to set XML serialization information.</summary>
		// Token: 0x040011A4 RID: 4516
		protected object ReflectInfo;
	}
}
