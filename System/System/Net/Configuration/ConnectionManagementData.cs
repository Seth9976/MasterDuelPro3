using System;
using System.Collections;

namespace System.Net.Configuration
{
	// Token: 0x0200048B RID: 1163
	internal class ConnectionManagementData
	{
		// Token: 0x06001C8E RID: 7310 RVA: 0x0007CA5C File Offset: 0x0007AC5C
		public ConnectionManagementData(object parent)
		{
			this.data = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			if (parent != null && parent is ConnectionManagementData)
			{
				ConnectionManagementData connectionManagementData = (ConnectionManagementData)parent;
				foreach (object obj in connectionManagementData.data.Keys)
				{
					string text = (string)obj;
					this.data[text] = connectionManagementData.data[text];
				}
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0007CAF8 File Offset: 0x0007ACF8
		public void Add(string address, int nconns)
		{
			this.data[address] = (uint)nconns;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0007CB0C File Offset: 0x0007AD0C
		public uint GetMaxConnections(string hostOrIP)
		{
			object obj = this.data[hostOrIP];
			if (obj == null)
			{
				obj = this.data["*"];
			}
			if (obj == null)
			{
				return 2U;
			}
			return (uint)obj;
		}

		// Token: 0x04001399 RID: 5017
		private Hashtable data;
	}
}
