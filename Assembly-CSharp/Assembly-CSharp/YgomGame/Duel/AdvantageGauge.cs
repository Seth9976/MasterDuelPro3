using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000C78 RID: 3192
	public class AdvantageGauge : MonoBehaviour
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06005BC6 RID: 23494 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005BC7 RID: 23495 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool isCalc
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x0000216A File Offset: 0x0000036A
		public static AdvantageGauge Create(Transform parent)
		{
			return null;
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SwitchGaugeVisible()
		{
			return false;
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SwitchEffectVisible()
		{
			return false;
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x040096CC RID: 38604
		private static AdvantageGauge instance;

		// Token: 0x040096CD RID: 38605
		private float m_AdvantageCurrent;

		// Token: 0x040096CE RID: 38606
		private float m_AdvantageTarget;

		// Token: 0x040096CF RID: 38607
		private float m_AdvantageWave;

		// Token: 0x040096D0 RID: 38608
		private float m_AdvantageBias;

		// Token: 0x040096D1 RID: 38609
		private float m_AdvantageBiasPre;

		// Token: 0x040096D2 RID: 38610
		private int playerid;

		// Token: 0x040096D3 RID: 38611
		private int rivalid;

		// Token: 0x040096D4 RID: 38612
		private bool m_ShowEffect;

		// Token: 0x040096D5 RID: 38613
		private ElementObjectManager m_Eomanager;

		// Token: 0x040096D6 RID: 38614
		[SerializeField]
		private Material material;
	}
}
