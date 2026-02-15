using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D15 RID: 3349
	public class CardStatusLabel3DSetting : ScriptableObject
	{
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600609C RID: 24732 RVA: 0x0000216A File Offset: 0x0000036A
		protected static CardStatusLabel3DSetting Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600609D RID: 24733 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSize_Active
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSize_Inactive
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600609F RID: 24735 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSize_Slash
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSizeForMobile_Active
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060060A1 RID: 24737 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSizeForMobile_Inactive
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060060A2 RID: 24738 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSizeForMobile_Slash
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060060A3 RID: 24739 RVA: 0x000F537C File Offset: 0x000F357C
		public static Color FontColor_Normal
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060060A4 RID: 24740 RVA: 0x000F5394 File Offset: 0x000F3594
		public static Color FontColor_Changed
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060060A5 RID: 24741 RVA: 0x000F53AC File Offset: 0x000F35AC
		public static Color FontColor_Up
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060060A6 RID: 24742 RVA: 0x000F53C4 File Offset: 0x000F35C4
		public static Color FontColor_UpLight
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060060A7 RID: 24743 RVA: 0x000F53DC File Offset: 0x000F35DC
		public static Color FontColor_Down
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060060A8 RID: 24744 RVA: 0x000F53F4 File Offset: 0x000F35F4
		public static Color FontColor_DownLight
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060060A9 RID: 24745 RVA: 0x000F540C File Offset: 0x000F360C
		public static Color FontColor_Slash
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060060AA RID: 24746 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float FadeSpeed
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float BrightInactive
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x04009BF6 RID: 39926
		private static CardStatusLabel3DSetting m_Instance;

		// Token: 0x04009BF7 RID: 39927
		private const string PATH = "Duel/ScriptableObject/CardStatusLabel3DSetting";

		// Token: 0x04009BF8 RID: 39928
		[SerializeField]
		private int m_FontSize_Active;

		// Token: 0x04009BF9 RID: 39929
		[SerializeField]
		private int m_FontSize_Inactive;

		// Token: 0x04009BFA RID: 39930
		[SerializeField]
		private int m_FontSize_Slash;

		// Token: 0x04009BFB RID: 39931
		[SerializeField]
		private int m_FontSizeForMobile_Active;

		// Token: 0x04009BFC RID: 39932
		[SerializeField]
		private int m_FontSizeForMobile_Inactive;

		// Token: 0x04009BFD RID: 39933
		[SerializeField]
		private int m_FontSizeForMobile_Slash;

		// Token: 0x04009BFE RID: 39934
		[SerializeField]
		private Color m_FontColor_Normal;

		// Token: 0x04009BFF RID: 39935
		[SerializeField]
		private Color m_FontColor_Changed;

		// Token: 0x04009C00 RID: 39936
		[SerializeField]
		private Color m_FontColor_Slash;

		// Token: 0x04009C01 RID: 39937
		[SerializeField]
		private Color m_FontColor_Up;

		// Token: 0x04009C02 RID: 39938
		[SerializeField]
		private Color m_FontColor_UpLight;

		// Token: 0x04009C03 RID: 39939
		[SerializeField]
		private Color m_FontColor_Down;

		// Token: 0x04009C04 RID: 39940
		[SerializeField]
		private Color m_FontColor_DownLight;

		// Token: 0x04009C05 RID: 39941
		[SerializeField]
		private float m_FadeSpeed;

		// Token: 0x04009C06 RID: 39942
		[SerializeField]
		private float m_BrightInactive;
	}
}
