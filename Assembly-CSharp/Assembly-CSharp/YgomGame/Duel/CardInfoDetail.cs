using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000CD4 RID: 3284
	public class CardInfoDetail : CardInfoBase
	{
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_PendulumArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06005DD0 RID: 24016 RVA: 0x0000216A File Offset: 0x0000036A
		protected Transform m_TunerRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_LimitIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_DescAreaBg
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_ParamAreaBg
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_PenDescAreaBg
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DspPendulum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_BackButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_ParaAreaBottom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsResourceLoaded()
		{
			return false;
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadResource()
		{
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadResource()
		{
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action finishedCallback, string prefPath)
		{
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFullScreenUiBg(FullScreenUiBg fullScreenUiBg)
		{
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardByCardInfoData(CardInfoData cardInfoData)
		{
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardByUniqueId(int uniqueId)
		{
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardByCardId(int cardid, int styleid = 1)
		{
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close(bool closebg = true)
		{
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRegulationId(int regid)
		{
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetScrollByAnalogStickEnable(bool enable)
		{
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InitializeBase()
		{
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ResetNameScroll()
		{
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitleAreaByBasicVal(int cardIdOrg, ref Engine.BasicVal basicalval)
		{
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitleAreaByCardId(int cardid)
		{
		}

		// Token: 0x06005DE8 RID: 24040 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCardArea(CardInfoData cardinfodata)
		{
		}

		// Token: 0x06005DE9 RID: 24041 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetParameterArea(CardInfoData cardinfodata)
		{
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDescriptionArea(CardInfoData cardinfodata)
		{
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPendulumTag()
		{
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetParaAreaBottom()
		{
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDspContent(int cardidorg, int effectid, bool isInField = false, bool hasPenScale = false, int effflag = 0)
		{
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLimitIcon(int cardid)
		{
		}

		// Token: 0x04009967 RID: 39271
		protected const int MAXCOUNTERNUM = 6;

		// Token: 0x04009968 RID: 39272
		protected const string PATH_PREHAB = "Prefabs/Duel/UI/CardInfoDetail";

		// Token: 0x04009969 RID: 39273
		internal const string PATH_PREHAB_DOWNLOAD = "Prefabs/Duel/UI/CardInfoDetail_Download";

		// Token: 0x0400996A RID: 39274
		internal const string PATH_PREHAB_BROWSER = "Prefabs/Duel/UI/CardInfoDetail_Browser";

		// Token: 0x0400996B RID: 39275
		protected const string WORD_EFFECT = "\ufffd";

		// Token: 0x0400996C RID: 39276
		protected const string WORD_TUNER = "チ\ufffd";

		// Token: 0x0400996D RID: 39277
		protected const string LABEL_EO_BUTTONBACK = "ButtonBack";

		// Token: 0x0400996E RID: 39278
		protected const string LABEL_EO_COUNTERNUM = "CounterNum";

		// Token: 0x0400996F RID: 39279
		protected const string LABEL_EO_ICONCOUNTER = "IconCounter";

		// Token: 0x04009970 RID: 39280
		protected const string LABEL_EO_ICONLIMIT = "IconLimit";

		// Token: 0x04009971 RID: 39281
		protected const string LABEL_EO_ICONSTATUEBYBATTLE = "IconStatueByBattle";

		// Token: 0x04009972 RID: 39282
		protected const string LABEL_EO_ICONSTATUECANTREVIVE = "IconStatueCantRevive";

		// Token: 0x04009973 RID: 39283
		protected const string LABEL_EO_ICONSTATUEDEMENSIONHOLE = "IconStatueDemensionHole";

		// Token: 0x04009974 RID: 39284
		protected const string LABEL_EO_ICONSTATUEDISABLE = "IconStatueDiable";

		// Token: 0x04009975 RID: 39285
		protected const string LABEL_EO_ICONSTATUEFUSIONMAT = "IconStatueFusionMat";

		// Token: 0x04009976 RID: 39286
		protected const string LABEL_EO_ICONSTATUELIGHTFORCE = "IconStatueLightForce";

		// Token: 0x04009977 RID: 39287
		protected const string LABEL_EO_ICONSTATUESYNCMAT = "IconStatueSyncMat";

		// Token: 0x04009978 RID: 39288
		protected const string LABEL_EO_ICONSTATUECANTATTACK = "IconStatueCantAttack";

		// Token: 0x04009979 RID: 39289
		protected const string LABEL_EO_ROOTTUNER = "TunerGroup";

		// Token: 0x0400997A RID: 39290
		protected const string LABEL_EO_ROOTTYPE = "TypeGroup";

		// Token: 0x0400997B RID: 39291
		protected const string LABEL_EO_PENDULUMDESCAREA = "PendulumDescriptionArea";

		// Token: 0x0400997C RID: 39292
		protected const string LABEL_EO_PLATEDESC = "PlateDescription";

		// Token: 0x0400997D RID: 39293
		protected const string LABEL_EO_PLATEPARAMATOR = "PlateParamator";

		// Token: 0x0400997E RID: 39294
		protected const string LABEL_EO_PLATEPENDESC = "PlatePendulumDescription";

		// Token: 0x0400997F RID: 39295
		protected const string LABEL_EO_TEXTPENDESCVALUE = "TextPendulumDescriptionValue";

		// Token: 0x04009980 RID: 39296
		protected const string LABEL_EO_TEXTPENDESC = "TextPendulumDescriptionItem";

		// Token: 0x04009981 RID: 39297
		protected const string LABEL_EO_PARAMATORAREABOTTOM = "ParamatorAreaBottom";

		// Token: 0x04009982 RID: 39298
		protected int m_RegId;

		// Token: 0x04009983 RID: 39299
		protected bool m_EnableScrollByAnalogStick;

		// Token: 0x04009984 RID: 39300
		protected SelectionItem m_DspTextArea;

		// Token: 0x04009985 RID: 39301
		protected SelectionItem m_PenTextArea;

		// Token: 0x04009986 RID: 39302
		protected UiSwitchTweenAnimationController m_UiSwitchTweenAnimationController;

		// Token: 0x04009987 RID: 39303
		protected FullScreenUiBg m_FullScreenUiBg;
	}
}
