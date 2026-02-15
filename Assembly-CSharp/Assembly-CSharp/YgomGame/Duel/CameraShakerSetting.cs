using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CAF RID: 3247
	public class CameraShakerSetting : ScriptableObject
	{
		// Token: 0x06005C7F RID: 23679 RVA: 0x0000216A File Offset: 0x0000036A
		public CameraShakerSetting.Info Get(string label)
		{
			return null;
		}

		// Token: 0x04009810 RID: 38928
		public List<CameraShakerSetting.Info> infoList;

		// Token: 0x02000CB0 RID: 3248
		[Serializable]
		public class Info
		{
			// Token: 0x06005C81 RID: 23681 RVA: 0x0000216A File Offset: 0x0000036A
			public CameraShakerSetting.Info Copy()
			{
				return null;
			}

			// Token: 0x06005C82 RID: 23682 RVA: 0x000F4FAC File Offset: 0x000F31AC
			public ValueTuple<Vector3, bool> GetShake(float time, Vector3 pre_shake, int frame_count, Vector3 big, Vector3 lit)
			{
				return default(ValueTuple<Vector3, bool>);
			}

			// Token: 0x04009811 RID: 38929
			public string label;

			// Token: 0x04009812 RID: 38930
			public float duration;

			// Token: 0x04009813 RID: 38931
			public float delay;

			// Token: 0x04009814 RID: 38932
			public bool usePerlinShake;

			// Token: 0x04009815 RID: 38933
			public float perlinCycle;

			// Token: 0x04009816 RID: 38934
			public Vector3 perlinPower;

			// Token: 0x04009817 RID: 38935
			public bool useCosShake;

			// Token: 0x04009818 RID: 38936
			public float cosCycle;

			// Token: 0x04009819 RID: 38937
			public Vector3 cosPower;

			// Token: 0x0400981A RID: 38938
			public bool useSinShake;

			// Token: 0x0400981B RID: 38939
			public float sinCycle;

			// Token: 0x0400981C RID: 38940
			public Vector3 sinPower;

			// Token: 0x0400981D RID: 38941
			public bool useSinShakeSub;

			// Token: 0x0400981E RID: 38942
			public float sinCycleSub;

			// Token: 0x0400981F RID: 38943
			public Vector3 sinPowerSub;

			// Token: 0x04009820 RID: 38944
			public bool useRandomShake;

			// Token: 0x04009821 RID: 38945
			public Vector3 randomMin;

			// Token: 0x04009822 RID: 38946
			public Vector3 randomMax;

			// Token: 0x04009823 RID: 38947
			public int randomDelayFrame;

			// Token: 0x04009824 RID: 38948
			public float randomDelayGain;
		}
	}
}
