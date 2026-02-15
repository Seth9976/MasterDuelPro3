using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200043E RID: 1086
	internal class DynamicPropertyCollection
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x00094AF8 File Offset: 0x00092CF8
		public bool HasProperties
		{
			get
			{
				return this._properties.Count > 0;
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00094B08 File Offset: 0x00092D08
		public bool RegisterDynamicProperty(IDynamicProperty prop)
		{
			bool flag2;
			lock (this)
			{
				if (this.FindProperty(prop.Name) != -1)
				{
					throw new InvalidOperationException("Another property by this name already exists");
				}
				ArrayList arrayList = new ArrayList(this._properties);
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = new DynamicPropertyCollection.DynamicPropertyReg();
				dynamicPropertyReg.Property = prop;
				IContributeDynamicSink contributeDynamicSink = prop as IContributeDynamicSink;
				if (contributeDynamicSink != null)
				{
					dynamicPropertyReg.Sink = contributeDynamicSink.GetDynamicSink();
				}
				arrayList.Add(dynamicPropertyReg);
				this._properties = arrayList;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00094BA0 File Offset: 0x00092DA0
		public bool UnregisterDynamicProperty(string name)
		{
			bool flag2;
			lock (this)
			{
				int num = this.FindProperty(name);
				if (num == -1)
				{
					throw new RemotingException("A property with the name " + name + " was not found");
				}
				this._properties.RemoveAt(num);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00094C08 File Offset: 0x00092E08
		public void NotifyMessage(bool start, IMessage msg, bool client_site, bool async)
		{
			ArrayList properties = this._properties;
			if (start)
			{
				using (IEnumerator enumerator = properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = (DynamicPropertyCollection.DynamicPropertyReg)obj;
						if (dynamicPropertyReg.Sink != null)
						{
							dynamicPropertyReg.Sink.ProcessMessageStart(msg, client_site, async);
						}
					}
					return;
				}
			}
			foreach (object obj2 in properties)
			{
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg2 = (DynamicPropertyCollection.DynamicPropertyReg)obj2;
				if (dynamicPropertyReg2.Sink != null)
				{
					dynamicPropertyReg2.Sink.ProcessMessageFinish(msg, client_site, async);
				}
			}
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00094CCC File Offset: 0x00092ECC
		private int FindProperty(string name)
		{
			for (int i = 0; i < this._properties.Count; i++)
			{
				if (((DynamicPropertyCollection.DynamicPropertyReg)this._properties[i]).Property.Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04001176 RID: 4470
		private ArrayList _properties = new ArrayList();

		// Token: 0x0200043F RID: 1087
		private class DynamicPropertyReg
		{
			// Token: 0x04001177 RID: 4471
			public IDynamicProperty Property;

			// Token: 0x04001178 RID: 4472
			public IDynamicMessageSink Sink;
		}
	}
}
