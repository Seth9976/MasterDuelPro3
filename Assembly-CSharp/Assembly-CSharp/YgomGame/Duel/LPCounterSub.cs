using System;
using UnityEngine;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000EAD RID: 3757
	public class LPCounterSub : MonoBehaviour
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06006D68 RID: 28008 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_SubText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06006D69 RID: 28009 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool active
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006D6A RID: 28010 RVA: 0x0000216D File Offset: 0x0000036D
		public void ApplyEffect(int value, int targetlp, Vector2 startpos, Engine.DamageType type, Action<Color, int> onFinished)
		{
		}

		// Token: 0x06006D6B RID: 28011 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEffectEnd()
		{
		}

		// Token: 0x06006D6C RID: 28012 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reset()
		{
		}

		// Token: 0x0400A87D RID: 43133
		protected const string LABEL_TW_EFFECTRECOVER = "RecoverEffect";

		// Token: 0x0400A87E RID: 43134
		protected const string LABEL_TW_EFFECTDAMAGE = "DamageEffect";

		// Token: 0x0400A87F RID: 43135
		protected const string LABEL_TW_EFFECTCOST = "CostEffect";

		// Token: 0x0400A880 RID: 43136
		protected Action<Color, int> onFinished;

		// Token: 0x0400A881 RID: 43137
		protected int m_TargetLP;

		// Token: 0x0400A882 RID: 43138
		protected ExtendedTextMeshProUGUI m_SubText_Origin;
	}
}
