using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the known, but unreferenced, object found in an encoded SOAP XML stream during deserialization.</summary>
	// Token: 0x020001F0 RID: 496
	public class UnreferencedObjectEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.UnreferencedObjectEventArgs" /> class.</summary>
		/// <param name="o">The unreferenced object. </param>
		/// <param name="id">A unique string used to identify the unreferenced object. </param>
		// Token: 0x06001976 RID: 6518 RVA: 0x00096E00 File Offset: 0x00095000
		public UnreferencedObjectEventArgs(object o, string id)
		{
			this.o = o;
			this.id = id;
		}

		/// <summary>Gets the deserialized, but unreferenced, object.</summary>
		/// <returns>The deserialized, but unreferenced, object.</returns>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x00096E16 File Offset: 0x00095016
		public object UnreferencedObject
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the ID of the object.</summary>
		/// <returns>The ID of the object.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00096E1E File Offset: 0x0009501E
		public string UnreferencedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000ABB RID: 2747
		private object o;

		// Token: 0x04000ABC RID: 2748
		private string id;
	}
}
