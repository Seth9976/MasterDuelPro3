using System;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009E8 RID: 2536
	[Serializable]
	public class SerializableKeyFrame : ISerializationCallbackReceiver
	{
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x000F49C0 File Offset: 0x000F2BC0
		public Keyframe keyFrame
		{
			get
			{
				return default(Keyframe);
			}
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x00002739 File Offset: 0x00000939
		public SerializableKeyFrame(Keyframe keyFrame)
		{
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAfterDeserialize()
		{
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x040087BB RID: 34747
		private Keyframe m_KeyFrame;

		// Token: 0x040087BC RID: 34748
		[SerializeField]
		private float[] p;

		// Token: 0x040087BD RID: 34749
		[SerializeField]
		private WeightedMode m;
	}
}
