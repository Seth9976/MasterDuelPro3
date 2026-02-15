using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000062 RID: 98
	public class RespExtensionSet : SupportClass.AbstractSetSupport
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000FABA File Offset: 0x0000DCBA
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
		public RespExtensionSet()
		{
			this.map = new Hashtable();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000FADC File Offset: 0x0000DCDC
		public void registerResponseExtension(string oid, Type extClass)
		{
			lock (this)
			{
				if (!this.map.ContainsKey(oid))
				{
					this.map.Add(oid, extClass);
				}
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		public override IEnumerator GetEnumerator()
		{
			return this.map.Values.GetEnumerator();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000FB40 File Offset: 0x0000DD40
		public Type findResponseExtension(string searchOID)
		{
			Type type;
			lock (this)
			{
				if (this.map.ContainsKey(searchOID))
				{
					type = (Type)this.map[searchOID];
				}
				else
				{
					type = null;
				}
			}
			return type;
		}

		// Token: 0x04000222 RID: 546
		private Hashtable map;
	}
}
