using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x0200041E RID: 1054
	internal class ProviderData
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x00091374 File Offset: 0x0008F574
		public void CopyFrom(ProviderData other)
		{
			if (this.Ref == null)
			{
				this.Ref = other.Ref;
			}
			if (this.Id == null)
			{
				this.Id = other.Id;
			}
			if (this.Type == null)
			{
				this.Type = other.Type;
			}
			foreach (object obj in other.CustomProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
				{
					this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			if (other.CustomData != null)
			{
				if (this.CustomData == null)
				{
					this.CustomData = new ArrayList();
				}
				foreach (object obj2 in other.CustomData)
				{
					SinkProviderData sinkProviderData = (SinkProviderData)obj2;
					this.CustomData.Add(sinkProviderData);
				}
			}
		}

		// Token: 0x04001113 RID: 4371
		internal string Ref;

		// Token: 0x04001114 RID: 4372
		internal string Type;

		// Token: 0x04001115 RID: 4373
		internal string Id;

		// Token: 0x04001116 RID: 4374
		internal Hashtable CustomProperties = new Hashtable();

		// Token: 0x04001117 RID: 4375
		internal IList CustomData;
	}
}
