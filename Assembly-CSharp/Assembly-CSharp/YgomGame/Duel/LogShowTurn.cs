using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000EC5 RID: 3781
	public class LogShowTurn : LogItemBase
	{
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06006E3D RID: 28221 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006E3E RID: 28222 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowTurnData data)
		{
		}

		// Token: 0x06006E3F RID: 28223 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetFaceIcon(int playerid, GameObject root)
		{
		}

		// Token: 0x06006E40 RID: 28224 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLP(string lplabel, int value)
		{
		}

		// Token: 0x06006E41 RID: 28225 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTurn(int value, int playerid)
		{
		}

		// Token: 0x0400A910 RID: 43280
		public static int SINGALCHARWIDTH;

		// Token: 0x0400A911 RID: 43281
		public static string TEXT_TURN;

		// Token: 0x0400A912 RID: 43282
		protected string LABEL_EO_PLAERICON;

		// Token: 0x0400A913 RID: 43283
		[SerializeField]
		protected string m_Label_FaceIconL;

		// Token: 0x0400A914 RID: 43284
		[SerializeField]
		protected string m_Label_FaceIconR;

		// Token: 0x0400A915 RID: 43285
		[SerializeField]
		protected string m_Label_FaceIcon;

		// Token: 0x0400A916 RID: 43286
		[SerializeField]
		protected string m_Label_FaceIconCursor;

		// Token: 0x0400A917 RID: 43287
		[SerializeField]
		protected string m_Label_LPValueL;

		// Token: 0x0400A918 RID: 43288
		[SerializeField]
		protected string m_Label_LPValueR;

		// Token: 0x0400A919 RID: 43289
		[SerializeField]
		protected string m_Label_TurnText;

		// Token: 0x0400A91A RID: 43290
		[SerializeField]
		protected string m_Label_TurnBg;

		// Token: 0x0400A91B RID: 43291
		[SerializeField]
		protected Color m_Color_Team0;

		// Token: 0x0400A91C RID: 43292
		[SerializeField]
		protected Color m_Color_Team1;

		// Token: 0x0400A91D RID: 43293
		protected int m_IconIDL;

		// Token: 0x0400A91E RID: 43294
		protected int m_IconIDR;

		// Token: 0x0400A91F RID: 43295
		private ElementObjectManager m_EOManager_Origin;
	}
}
