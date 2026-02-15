using System;
using System.Collections;

namespace System.Runtime.Remoting
{
	// Token: 0x0200041D RID: 1053
	internal class ChannelData
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x0009113D File Offset: 0x0008F33D
		internal ArrayList ServerProviders
		{
			get
			{
				if (this._serverProviders == null)
				{
					this._serverProviders = new ArrayList();
				}
				return this._serverProviders;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x00091158 File Offset: 0x0008F358
		public ArrayList ClientProviders
		{
			get
			{
				if (this._clientProviders == null)
				{
					this._clientProviders = new ArrayList();
				}
				return this._clientProviders;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x00091173 File Offset: 0x0008F373
		public Hashtable CustomProperties
		{
			get
			{
				if (this._customProperties == null)
				{
					this._customProperties = new Hashtable();
				}
				return this._customProperties;
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00091190 File Offset: 0x0008F390
		public void CopyFrom(ChannelData other)
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
			if (this.DelayLoadAsClientChannel == null)
			{
				this.DelayLoadAsClientChannel = other.DelayLoadAsClientChannel;
			}
			if (other._customProperties != null)
			{
				foreach (object obj in other._customProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
					{
						this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			if (this._serverProviders == null && other._serverProviders != null)
			{
				foreach (object obj2 in other._serverProviders)
				{
					ProviderData providerData = (ProviderData)obj2;
					ProviderData providerData2 = new ProviderData();
					providerData2.CopyFrom(providerData);
					this.ServerProviders.Add(providerData2);
				}
			}
			if (this._clientProviders == null && other._clientProviders != null)
			{
				foreach (object obj3 in other._clientProviders)
				{
					ProviderData providerData3 = (ProviderData)obj3;
					ProviderData providerData4 = new ProviderData();
					providerData4.CopyFrom(providerData3);
					this.ClientProviders.Add(providerData4);
				}
			}
		}

		// Token: 0x0400110C RID: 4364
		internal string Ref;

		// Token: 0x0400110D RID: 4365
		internal string Type;

		// Token: 0x0400110E RID: 4366
		internal string Id;

		// Token: 0x0400110F RID: 4367
		internal string DelayLoadAsClientChannel;

		// Token: 0x04001110 RID: 4368
		private ArrayList _serverProviders = new ArrayList();

		// Token: 0x04001111 RID: 4369
		private ArrayList _clientProviders = new ArrayList();

		// Token: 0x04001112 RID: 4370
		private Hashtable _customProperties = new Hashtable();
	}
}
