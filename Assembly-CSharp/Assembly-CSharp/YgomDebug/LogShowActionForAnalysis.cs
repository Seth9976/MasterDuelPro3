using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomDebug
{
	// Token: 0x0200115D RID: 4445
	public class LogShowActionForAnalysis : LogItemBaseForAnalysis
	{
		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x0600846F RID: 33903 RVA: 0x0000216A File Offset: 0x0000036A
		protected DuelIconSprites m_IconSprites
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06008470 RID: 33904 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008471 RID: 33905 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowActionDataForAnalysis data)
		{
		}

		// Token: 0x06008472 RID: 33906 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetWordTable()
		{
		}

		// Token: 0x06008473 RID: 33907 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCards(ShowActionDataForAnalysis data)
		{
		}

		// Token: 0x06008474 RID: 33908 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCard(LogDataSideForAnalysis data, ref int cardidpre, ElementObjectManager cardeom)
		{
		}

		// Token: 0x06008475 RID: 33909 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPositionIcon(ShowActionDataForAnalysis data, ElementObjectManager poseom)
		{
		}

		// Token: 0x06008476 RID: 33910 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton SetFaceIcon(LogDataSideForAnalysis data, ElementObjectManager eomface)
		{
			return null;
		}

		// Token: 0x06008477 RID: 33911 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetAction(LogDataCenterForAnalysis data)
		{
		}

		// Token: 0x06008478 RID: 33912 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDiceResult(LogDataCenterForAnalysis data)
		{
		}

		// Token: 0x06008479 RID: 33913 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCoinResult(LogDataCenterForAnalysis data)
		{
		}

		// Token: 0x0600847A RID: 33914 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLPChange(LogDataCenterForAnalysis data)
		{
		}

		// Token: 0x0600847B RID: 33915 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCounterChange(LogDataCenterForAnalysis data)
		{
		}

		// Token: 0x0600847C RID: 33916 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardTexture(RawImage cardtexture, GameObject cardmask, int cardid, bool face, bool insight)
		{
		}

		// Token: 0x0600847D RID: 33917 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetWidth(bool isIndent)
		{
		}

		// Token: 0x0600847E RID: 33918 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetColor(bool team, bool indent)
		{
		}

		// Token: 0x0600847F RID: 33919 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnAdded()
		{
		}

		// Token: 0x06008480 RID: 33920 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnRemoved()
		{
		}

		// Token: 0x0400BF97 RID: 49047
		private const int FULLWIDTH = 320;

		// Token: 0x0400BF98 RID: 49048
		private const int FULLWIDTH_WIDTH = 670;

		// Token: 0x0400BF99 RID: 49049
		private const int INDENTWIDTH = 280;

		// Token: 0x0400BF9A RID: 49050
		private const int INDENTWIDTH_WIDTH = 580;

		// Token: 0x0400BF9B RID: 49051
		[SerializeField]
		protected Color m_Color_Team0;

		// Token: 0x0400BF9C RID: 49052
		[SerializeField]
		protected Color m_Color_Team1;

		// Token: 0x0400BF9D RID: 49053
		protected string LABEL_EO_CARDTEX;

		// Token: 0x0400BF9E RID: 49054
		protected string LABEL_EO_CARDMASK;

		// Token: 0x0400BF9F RID: 49055
		protected string LABEL_EO_CURSOR;

		// Token: 0x0400BFA0 RID: 49056
		protected string LABEL_EO_CTRICONL;

		// Token: 0x0400BFA1 RID: 49057
		protected string LABEL_EO_CTRICONR;

		// Token: 0x0400BFA2 RID: 49058
		protected string LABEL_EO_POSICONL;

		// Token: 0x0400BFA3 RID: 49059
		protected string LABEL_EO_POSICONR;

		// Token: 0x0400BFA4 RID: 49060
		protected string LABEL_EO_PLAYERICON;

		// Token: 0x0400BFA5 RID: 49061
		protected string LABEL_EO_CARDL;

		// Token: 0x0400BFA6 RID: 49062
		protected string LABEL_EO_CARDR;

		// Token: 0x0400BFA7 RID: 49063
		protected string LABEL_EO_ACTION;

		// Token: 0x0400BFA8 RID: 49064
		protected string LABEL_EO_ACTTEXT;

		// Token: 0x0400BFA9 RID: 49065
		protected string LABEL_EO_COIN;

		// Token: 0x0400BFAA RID: 49066
		protected string LABEL_EO_COINICON;

		// Token: 0x0400BFAB RID: 49067
		protected string LABEL_EO_DICE;

		// Token: 0x0400BFAC RID: 49068
		protected string LABEL_EO_DICEICON;

		// Token: 0x0400BFAD RID: 49069
		protected string LABEL_EO_ARROW;

		// Token: 0x0400BFAE RID: 49070
		protected string LABEL_EO_FACEICONL;

		// Token: 0x0400BFAF RID: 49071
		protected string LABEL_EO_FACEICONR;

		// Token: 0x0400BFB0 RID: 49072
		protected string LABEL_EO_FACEICON;

		// Token: 0x0400BFB1 RID: 49073
		protected string LABEL_EO_FACEICONFRAME;

		// Token: 0x0400BFB2 RID: 49074
		protected string LABEL_EO_LPCHANGE;

		// Token: 0x0400BFB3 RID: 49075
		protected string LABEL_EO_CHANGEVALUE;

		// Token: 0x0400BFB4 RID: 49076
		protected string LABEL_EO_RESTLP;

		// Token: 0x0400BFB5 RID: 49077
		protected string LABEL_EO_CHANGETYPE;

		// Token: 0x0400BFB6 RID: 49078
		protected string LABEL_EO_COUNTERCHANGE;

		// Token: 0x0400BFB7 RID: 49079
		protected string LABEL_EO_COUNTERNUMPRE;

		// Token: 0x0400BFB8 RID: 49080
		protected string LABEL_EO_COUNTERNUMAFT;

		// Token: 0x0400BFB9 RID: 49081
		protected string LABEL_EO_COUNTERTYPE;

		// Token: 0x0400BFBA RID: 49082
		protected string LABEL_EO_COUNTERICON;

		// Token: 0x0400BFBB RID: 49083
		protected string LABEL_EO_LINETOP;

		// Token: 0x0400BFBC RID: 49084
		protected string LABEL_EO_LINEBOTTOM;

		// Token: 0x0400BFBD RID: 49085
		protected string LABEL_EO_BACKGROUND;

		// Token: 0x0400BFBE RID: 49086
		protected string LABEL_EO_CARDNAME;

		// Token: 0x0400BFBF RID: 49087
		protected string LABEL_EO_CARDNAMELAYER;

		// Token: 0x0400BFC0 RID: 49088
		protected string LABEL_EO_BATTLEARROW;

		// Token: 0x0400BFC1 RID: 49089
		protected string LABEL_EO_COLORBARTEAM0;

		// Token: 0x0400BFC2 RID: 49090
		protected string LABEL_EO_COLORBARTEAM1;

		// Token: 0x0400BFC3 RID: 49091
		protected string LABEL_EO_CONTENT;

		// Token: 0x0400BFC4 RID: 49092
		protected string LABEL_EO_POSITIONICONROOT;

		// Token: 0x0400BFC5 RID: 49093
		protected int m_CardIdL;

		// Token: 0x0400BFC6 RID: 49094
		protected int m_CardIdR;

		// Token: 0x0400BFC7 RID: 49095
		protected static Dictionary<LOGACTIONTYPE, string> m_ActTypeStrDict;

		// Token: 0x0400BFC8 RID: 49096
		protected static Dictionary<Engine.DamageType, string> m_DmgTypeStrDict;

		// Token: 0x0400BFC9 RID: 49097
		private ElementObjectManager m_EOManager_Origin;
	}
}
