using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000EBF RID: 3775
	public class LogShowAction : LogItemBase
	{
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06006E16 RID: 28182 RVA: 0x0000216A File Offset: 0x0000036A
		protected DuelIconSprites m_IconSprites
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06006E17 RID: 28183 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006E18 RID: 28184 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowActionData data)
		{
		}

		// Token: 0x06006E19 RID: 28185 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetWordTable()
		{
		}

		// Token: 0x06006E1A RID: 28186 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCards(ShowActionData data)
		{
		}

		// Token: 0x06006E1B RID: 28187 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton SetCard(LogDataSide data, ref int cardidpre, ElementObjectManager cardeom)
		{
			return null;
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPositionIcon(ShowActionData data, ElementObjectManager poseom)
		{
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton SetFaceIcon(LogDataSide data, ElementObjectManager eomface)
		{
			return null;
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetAction(LogDataCenter data)
		{
		}

		// Token: 0x06006E1F RID: 28191 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDiceResult(LogDataCenter data)
		{
		}

		// Token: 0x06006E20 RID: 28192 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCoinResult(LogDataCenter data)
		{
		}

		// Token: 0x06006E21 RID: 28193 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLPChange(LogDataCenter data)
		{
		}

		// Token: 0x06006E22 RID: 28194 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCounterChange(LogDataCenter data)
		{
		}

		// Token: 0x06006E23 RID: 28195 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardTexture(RawImage cardtexture, GameObject cardmask, int cardid, bool face, bool insight)
		{
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetWidth(bool isIndent)
		{
		}

		// Token: 0x06006E25 RID: 28197 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetColor(bool team, bool indent)
		{
		}

		// Token: 0x06006E26 RID: 28198 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnAdded()
		{
		}

		// Token: 0x06006E27 RID: 28199 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnRemoved()
		{
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnCreated(DuelClient host)
		{
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCallBack(bool isleft)
		{
		}

		// Token: 0x0400A8C8 RID: 43208
		private const int FULLWIDTH = 320;

		// Token: 0x0400A8C9 RID: 43209
		private const int FULLWIDTH_WIDTH = 670;

		// Token: 0x0400A8CA RID: 43210
		private const int INDENTWIDTH = 280;

		// Token: 0x0400A8CB RID: 43211
		private const int INDENTWIDTH_WIDTH = 580;

		// Token: 0x0400A8CC RID: 43212
		[SerializeField]
		protected Color m_Color_Team0;

		// Token: 0x0400A8CD RID: 43213
		[SerializeField]
		protected Color m_Color_Team1;

		// Token: 0x0400A8CE RID: 43214
		protected string LABEL_EO_CARDTEX;

		// Token: 0x0400A8CF RID: 43215
		protected string LABEL_EO_CARDMASK;

		// Token: 0x0400A8D0 RID: 43216
		protected string LABEL_EO_CURSOR;

		// Token: 0x0400A8D1 RID: 43217
		protected string LABEL_EO_CTRICONL;

		// Token: 0x0400A8D2 RID: 43218
		protected string LABEL_EO_CTRICONR;

		// Token: 0x0400A8D3 RID: 43219
		protected string LABEL_EO_POSICONL;

		// Token: 0x0400A8D4 RID: 43220
		protected string LABEL_EO_POSICONR;

		// Token: 0x0400A8D5 RID: 43221
		protected string LABEL_EO_PLAYERICON;

		// Token: 0x0400A8D6 RID: 43222
		protected string LABEL_EO_CARDL;

		// Token: 0x0400A8D7 RID: 43223
		protected string LABEL_EO_CARDR;

		// Token: 0x0400A8D8 RID: 43224
		protected string LABEL_EO_ACTION;

		// Token: 0x0400A8D9 RID: 43225
		protected string LABEL_EO_ACTTEXT;

		// Token: 0x0400A8DA RID: 43226
		protected string LABEL_EO_COIN;

		// Token: 0x0400A8DB RID: 43227
		protected string LABEL_EO_COINICON;

		// Token: 0x0400A8DC RID: 43228
		protected string LABEL_EO_DICE;

		// Token: 0x0400A8DD RID: 43229
		protected string LABEL_EO_DICEICON;

		// Token: 0x0400A8DE RID: 43230
		protected string LABEL_EO_ARROW;

		// Token: 0x0400A8DF RID: 43231
		protected string LABEL_EO_FACEICONL;

		// Token: 0x0400A8E0 RID: 43232
		protected string LABEL_EO_FACEICONR;

		// Token: 0x0400A8E1 RID: 43233
		protected string LABEL_EO_FACEICON;

		// Token: 0x0400A8E2 RID: 43234
		protected string LABEL_EO_FACEICONFRAME;

		// Token: 0x0400A8E3 RID: 43235
		protected string LABEL_EO_LPCHANGE;

		// Token: 0x0400A8E4 RID: 43236
		protected string LABEL_EO_CHANGEVALUE;

		// Token: 0x0400A8E5 RID: 43237
		protected string LABEL_EO_RESTLP;

		// Token: 0x0400A8E6 RID: 43238
		protected string LABEL_EO_CHANGETYPE;

		// Token: 0x0400A8E7 RID: 43239
		protected string LABEL_EO_COUNTERCHANGE;

		// Token: 0x0400A8E8 RID: 43240
		protected string LABEL_EO_COUNTERNUMPRE;

		// Token: 0x0400A8E9 RID: 43241
		protected string LABEL_EO_COUNTERNUMAFT;

		// Token: 0x0400A8EA RID: 43242
		protected string LABEL_EO_COUNTERTYPE;

		// Token: 0x0400A8EB RID: 43243
		protected string LABEL_EO_COUNTERICON;

		// Token: 0x0400A8EC RID: 43244
		protected string LABEL_EO_LINETOP;

		// Token: 0x0400A8ED RID: 43245
		protected string LABEL_EO_LINEBOTTOM;

		// Token: 0x0400A8EE RID: 43246
		protected string LABEL_EO_BACKGROUND;

		// Token: 0x0400A8EF RID: 43247
		protected string LABEL_EO_CARDNAME;

		// Token: 0x0400A8F0 RID: 43248
		protected string LABEL_EO_CARDNAMELAYER;

		// Token: 0x0400A8F1 RID: 43249
		protected string LABEL_EO_BATTLEARROW;

		// Token: 0x0400A8F2 RID: 43250
		protected string LABEL_EO_COLORBARTEAM0;

		// Token: 0x0400A8F3 RID: 43251
		protected string LABEL_EO_COLORBARTEAM1;

		// Token: 0x0400A8F4 RID: 43252
		protected string LABEL_EO_CONTENT;

		// Token: 0x0400A8F5 RID: 43253
		protected string LABEL_EO_POSITIONICONROOT;

		// Token: 0x0400A8F6 RID: 43254
		protected int m_CardIdL;

		// Token: 0x0400A8F7 RID: 43255
		protected int m_CardIdR;

		// Token: 0x0400A8F8 RID: 43256
		protected static Dictionary<LOGACTIONTYPE, string> m_ActTypeStrDict;

		// Token: 0x0400A8F9 RID: 43257
		protected static Dictionary<Engine.DamageType, string> m_DmgTypeStrDict;

		// Token: 0x0400A8FA RID: 43258
		private ElementObjectManager m_EOManager_Origin;
	}
}
