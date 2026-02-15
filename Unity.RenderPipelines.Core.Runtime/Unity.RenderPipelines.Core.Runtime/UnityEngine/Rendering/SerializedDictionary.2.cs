using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public abstract class SerializedDictionary<K, V, SK, SV> : Dictionary<K, V>, ISerializationCallbackReceiver
	{
		// Token: 0x060004EB RID: 1259
		public abstract SK SerializeKey(K key);

		// Token: 0x060004EC RID: 1260
		public abstract SV SerializeValue(V value);

		// Token: 0x060004ED RID: 1261
		public abstract K DeserializeKey(SK serializedKey);

		// Token: 0x060004EE RID: 1262
		public abstract V DeserializeValue(SV serializedValue);

		// Token: 0x060004EF RID: 1263 RVA: 0x000093B4 File Offset: 0x000075B4
		public void OnBeforeSerialize()
		{
			this.m_Keys.Clear();
			this.m_Values.Clear();
			foreach (KeyValuePair<K, V> kvp in this)
			{
				this.m_Keys.Add(this.SerializeKey(kvp.Key));
				this.m_Values.Add(this.SerializeValue(kvp.Value));
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00009444 File Offset: 0x00007644
		public void OnAfterDeserialize()
		{
			base.Clear();
			for (int i = 0; i < this.m_Keys.Count; i++)
			{
				base.Add(this.DeserializeKey(this.m_Keys[i]), this.DeserializeValue(this.m_Values[i]));
			}
		}

		// Token: 0x04000143 RID: 323
		[SerializeField]
		private List<SK> m_Keys = new List<SK>();

		// Token: 0x04000144 RID: 324
		[SerializeField]
		private List<SV> m_Values = new List<SV>();
	}
}
