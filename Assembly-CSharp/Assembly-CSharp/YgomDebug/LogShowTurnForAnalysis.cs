using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomDebug
{
	// Token: 0x02001163 RID: 4451
	public class LogShowTurnForAnalysis : LogItemBaseForAnalysis
	{
		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06008494 RID: 33940 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008495 RID: 33941 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowTurnDataForAnalysis data)
		{
		}

		// Token: 0x06008496 RID: 33942 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetFaceIcon(int playerid, GameObject root)
		{
		}

		// Token: 0x06008497 RID: 33943 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLP(string lplabel, int value)
		{
		}

		// Token: 0x06008498 RID: 33944 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTurn(int value, int playerid)
		{
		}

		// Token: 0x0400BFDF RID: 49119
		public static int SINGALCHARWIDTH;

		// Token: 0x0400BFE0 RID: 49120
		public static string TEXT_TURN;

		// Token: 0x0400BFE1 RID: 49121
		protected string LABEL_EO_PLAERICON;

		// Token: 0x0400BFE2 RID: 49122
		[SerializeField]
		protected string m_Label_FaceIconL;

		// Token: 0x0400BFE3 RID: 49123
		[SerializeField]
		protected string m_Label_FaceIconR;

		// Token: 0x0400BFE4 RID: 49124
		[SerializeField]
		protected string m_Label_FaceIcon;

		// Token: 0x0400BFE5 RID: 49125
		[SerializeField]
		protected string m_Label_FaceIconCursor;

		// Token: 0x0400BFE6 RID: 49126
		[SerializeField]
		protected string m_Label_LPValueL;

		// Token: 0x0400BFE7 RID: 49127
		[SerializeField]
		protected string m_Label_LPValueR;

		// Token: 0x0400BFE8 RID: 49128
		[SerializeField]
		protected string m_Label_TurnText;

		// Token: 0x0400BFE9 RID: 49129
		[SerializeField]
		protected string m_Label_TurnBg;

		// Token: 0x0400BFEA RID: 49130
		[SerializeField]
		protected Color m_Color_Team0;

		// Token: 0x0400BFEB RID: 49131
		[SerializeField]
		protected Color m_Color_Team1;

		// Token: 0x0400BFEC RID: 49132
		protected int m_IconIDL;

		// Token: 0x0400BFED RID: 49133
		protected int m_IconIDR;

		// Token: 0x0400BFEE RID: 49134
		private ElementObjectManager m_EOManager_Origin;
	}
}
