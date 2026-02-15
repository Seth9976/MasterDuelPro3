using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D12 RID: 3346
	public class CardStatusLabel3D
	{
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x0600606F RID: 24687 RVA: 0x000029CC File Offset: 0x00000BCC
		private int m_SlhFontSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06006070 RID: 24688 RVA: 0x000029CC File Offset: 0x00000BCC
		private int m_ActiveFontSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06006071 RID: 24689 RVA: 0x000029CC File Offset: 0x00000BCC
		private int m_InactiveFontSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006073 RID: 24691 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool visibleAll
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetLabelPool()
		{
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardStatusLabel3D Create(CardRoot cardroot)
		{
			return null;
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitComponent()
		{
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitRotationList()
		{
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitParameters()
		{
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator UpdatePowerPointProcess()
		{
			return null;
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLevelRank()
		{
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateAttribute()
		{
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdtaeLink()
		{
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateType()
		{
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMagicType()
		{
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTunerIcon()
		{
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePendulumScale()
		{
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateOverlayUnits()
		{
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCounter()
		{
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLinkMarker()
		{
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateStateIcons()
		{
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeCounter()
		{
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x000029CC File Offset: 0x00000BCC
		private int ChangePowerPoint(int powernow, int powertarget)
		{
			return 0;
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetPowerPointString(int powershow, int powerorg, bool active)
		{
			return null;
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePowerPointImpl()
		{
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x0000216A File Offset: 0x0000036A
		private SpriteRenderer GetStateIcon(int index)
		{
			return null;
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddStateIcon(CardStatusLabel3D.StateIconType icontype, int iconindex)
		{
			return false;
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x000F5364 File Offset: 0x000F3564
		private Color GetValueColor(int valuedisp, int valueorg)
		{
			return default(Color);
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x0000216D File Offset: 0x0000036D
		private void LinkMarkerZoomIn()
		{
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x0000216D File Offset: 0x0000036D
		private void LinkMarkerZoomOut()
		{
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLinkMarkerShineEnable(bool enable)
		{
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(CardRoot cardroot)
		{
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateCardStatues()
		{
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x0000216D File Offset: 0x0000036D
		public void AtkDefSwitch()
		{
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x0000216D File Offset: 0x0000036D
		public void DefAtkSwitch()
		{
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide(bool immediate = false)
		{
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(bool immediate = false)
		{
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopAllTween()
		{
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopStateChangeTween()
		{
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopShowHideTween()
		{
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x0000216D File Offset: 0x0000036D
		public void RotateStatueLabel(Quaternion rotation)
		{
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HideAll(bool immediate = false)
		{
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowAll(bool immediate = false)
		{
		}

		// Token: 0x04009B89 RID: 39817
		public const int UNINITIALIZEDVALUE = 0;

		// Token: 0x04009B8A RID: 39818
		private const int STATUSICONCOUNT = 5;

		// Token: 0x04009B8B RID: 39819
		private const string LABEL_EO_TR_STATUSLABEL = "StatusLabelRoot";

		// Token: 0x04009B8C RID: 39820
		private const string LABEL_EO_TR_OVERLAYUNITS = "MonsterMaterialsRoot";

		// Token: 0x04009B8D RID: 39821
		private const string LABEL_EO_TR_POWERPOINT = "CardAttackBody";

		// Token: 0x04009B8E RID: 39822
		private const string LABEL_EO_TR_ATTRBUTE = "CardAttribute";

		// Token: 0x04009B8F RID: 39823
		private const string LABEL_EO_TR_ATTRBUTEOUTLINE = "IconAttributeChange";

		// Token: 0x04009B90 RID: 39824
		private const string LABEL_EO_TR_LINKNUM = "LinkCount";

		// Token: 0x04009B91 RID: 39825
		private const string LABEL_EO_TR_LAVELRANK = "CardLevel";

		// Token: 0x04009B92 RID: 39826
		private const string LABEL_EO_TR_PENDULUM = "CardPendulumBody";

		// Token: 0x04009B93 RID: 39827
		private const string LABEL_EO_TR_PENDULUM_L = "PendulumLeft";

		// Token: 0x04009B94 RID: 39828
		private const string LABEL_EO_TR_PENDULUMTEXT_L = "TextPendulumLeftRoot";

		// Token: 0x04009B95 RID: 39829
		private const string LABEL_EO_TR_PENDULUM_R = "PendulumRight";

		// Token: 0x04009B96 RID: 39830
		private const string LABEL_EO_TR_PENDULUMTEXT_R = "TextPendulumRightRoot";

		// Token: 0x04009B97 RID: 39831
		private const string LABEL_EO_TR_TYPE = "CardType";

		// Token: 0x04009B98 RID: 39832
		private const string LABEL_EO_TR_TYPEICONOUTLINE = "IconTypeChange";

		// Token: 0x04009B99 RID: 39833
		private const string LABEL_EO_TR_TUNERICON = "TunerIconRoot";

		// Token: 0x04009B9A RID: 39834
		private const string LABEL_EO_TR_TUNERICONOUTLINE = "TunerIconOutline";

		// Token: 0x04009B9B RID: 39835
		private const string LABEL_EO_TR_MAGICTYPE = "MagicTypeBase";

		// Token: 0x04009B9C RID: 39836
		private const string LABEL_EO_TR_COUNTER = "CardCounter";

		// Token: 0x04009B9D RID: 39837
		private const string LABEL_EO_TR_LINKMARKERROOT = "LinkMarkerRoot";

		// Token: 0x04009B9E RID: 39838
		private const string LABEL_EO_TR_LINKMARKER = "LinkMarker";

		// Token: 0x04009B9F RID: 39839
		private const string LABEL_EO_TR_DEFAULTSHOW = "DefaultShow";

		// Token: 0x04009BA0 RID: 39840
		private const string LABEL_EO_TR_DEFAULTHIDE = "DefaultHide";

		// Token: 0x04009BA1 RID: 39841
		private const string LABEL_EO_TR_SWITCHTWEENROOT = "FieldCardChangeIcon";

		// Token: 0x04009BA2 RID: 39842
		private const string LABEL_EO_TR_STATEICONNROOT = "StatusIcon";

		// Token: 0x04009BA3 RID: 39843
		private const string LABEL_EO_TXT_POWERPOINT = "TextPowerPoint";

		// Token: 0x04009BA4 RID: 39844
		private const string LABEL_EO_TXT_OVERLAYUNITS = "TextMonsterMaterials";

		// Token: 0x04009BA5 RID: 39845
		private const string LABEL_EO_TXT_LINKNUM = "TextLinkCount";

		// Token: 0x04009BA6 RID: 39846
		private const string LABEL_EO_TXT_LAVELRANK = "TextLevel";

		// Token: 0x04009BA7 RID: 39847
		private const string LABEL_EO_TXT_PENDULUM_L = "TextPendulumLeft";

		// Token: 0x04009BA8 RID: 39848
		private const string LABEL_EO_TXT_PENDULUM_R = "TextPendulumRight";

		// Token: 0x04009BA9 RID: 39849
		private const string LABEL_EO_TXT_COUNTER = "TextCounter";

		// Token: 0x04009BAA RID: 39850
		private const string LABEL_EO_IMG_ATTRIBUTE = "IconAttribute";

		// Token: 0x04009BAB RID: 39851
		private const string LABEL_EO_IMG_LEVELICON = "IconLevel";

		// Token: 0x04009BAC RID: 39852
		private const string LABEL_EO_IMG_TYPEICON = "IconType";

		// Token: 0x04009BAD RID: 39853
		private const string LABEL_EO_IMG_MAGICTYPE = "MagicType";

		// Token: 0x04009BAE RID: 39854
		private const string LABEL_EO_IMG_MAGICTYPEOUTLINE = "MagicTypeChange";

		// Token: 0x04009BAF RID: 39855
		private const string LABEL_EO_IMG_COUNTER = "IconCounter";

		// Token: 0x04009BB0 RID: 39856
		private const string LABEL_EO_IMG_STATUS = "IconStatus";

		// Token: 0x04009BB1 RID: 39857
		private const string LABEL_TW_SCALEOUT = "ScaleOut";

		// Token: 0x04009BB2 RID: 39858
		private const string LABEL_TW_SCALEIN = "ScaleIn";

		// Token: 0x04009BB3 RID: 39859
		private const string LABEL_TW_CHANGECOUNTER = "ChangeCounter";

		// Token: 0x04009BB4 RID: 39860
		private const string LABEL_TW_ATTACK = "Attack";

		// Token: 0x04009BB5 RID: 39861
		private const string LABEL_TW_DEFENCE = "Defense";

		// Token: 0x04009BB6 RID: 39862
		private const string LABEL_TW_SHOWLABEL = "ShowLabel";

		// Token: 0x04009BB7 RID: 39863
		private const string LABEL_TW_HIDELABEL = "HideLabel";

		// Token: 0x04009BB8 RID: 39864
		public static List<CardStatusLabel3D> m_LabelPool;

		// Token: 0x04009BB9 RID: 39865
		private CardRoot m_CardRoot;

		// Token: 0x04009BBA RID: 39866
		private Transform m_RootStatesLabel;

		// Token: 0x04009BBB RID: 39867
		private Transform m_RootOverlayUnits;

		// Token: 0x04009BBC RID: 39868
		private Transform m_RootPowerPoint;

		// Token: 0x04009BBD RID: 39869
		private Transform m_RootArribute;

		// Token: 0x04009BBE RID: 39870
		private Transform m_RootLinkNum;

		// Token: 0x04009BBF RID: 39871
		private Transform m_RootLinkMarker;

		// Token: 0x04009BC0 RID: 39872
		private Transform m_RootLavelRank;

		// Token: 0x04009BC1 RID: 39873
		private Transform m_RootPendulum;

		// Token: 0x04009BC2 RID: 39874
		private Transform m_RootPendulumL;

		// Token: 0x04009BC3 RID: 39875
		private Transform m_RootPendulumTextL;

		// Token: 0x04009BC4 RID: 39876
		private Transform m_RootPendulumR;

		// Token: 0x04009BC5 RID: 39877
		private Transform m_RootPendulumTextR;

		// Token: 0x04009BC6 RID: 39878
		private Transform m_RootType;

		// Token: 0x04009BC7 RID: 39879
		private Transform m_RootMagicType;

		// Token: 0x04009BC8 RID: 39880
		private Transform m_RootCounter;

		// Token: 0x04009BC9 RID: 39881
		private Transform m_RootDefaultShow;

		// Token: 0x04009BCA RID: 39882
		private Transform m_RootDefaultHide;

		// Token: 0x04009BCB RID: 39883
		private Transform m_RootStateIcons;

		// Token: 0x04009BCC RID: 39884
		private Transform m_RootTunerIcon;

		// Token: 0x04009BCD RID: 39885
		private Transform m_OutlineMagicType;

		// Token: 0x04009BCE RID: 39886
		private Transform m_OutlineTunerIcon;

		// Token: 0x04009BCF RID: 39887
		private Transform m_OutlineArribute;

		// Token: 0x04009BD0 RID: 39888
		private Transform m_OutlineMonsterTypeIcon;

		// Token: 0x04009BD1 RID: 39889
		private TextMeshPro m_TmpPower;

		// Token: 0x04009BD2 RID: 39890
		private TextMeshPro m_TmpOverlayUnits;

		// Token: 0x04009BD3 RID: 39891
		private TextMeshPro m_TmpLinkNum;

		// Token: 0x04009BD4 RID: 39892
		private TextMeshPro m_TmpLavelRank;

		// Token: 0x04009BD5 RID: 39893
		private TextMeshPro m_TmpPendulumL;

		// Token: 0x04009BD6 RID: 39894
		private TextMeshPro m_TmpPendulumR;

		// Token: 0x04009BD7 RID: 39895
		private TextMeshPro m_TmpCounter;

		// Token: 0x04009BD8 RID: 39896
		private SpriteRenderer m_SrLavelRank;

		// Token: 0x04009BD9 RID: 39897
		private SpriteRenderer m_SrAttribute;

		// Token: 0x04009BDA RID: 39898
		private SpriteRenderer m_SrTypeIcon;

		// Token: 0x04009BDB RID: 39899
		private SpriteRenderer m_SrMagicTypeIcon;

		// Token: 0x04009BDC RID: 39900
		private SpriteRenderer m_SrCounter;

		// Token: 0x04009BDD RID: 39901
		private bool m_AtkInitialized;

		// Token: 0x04009BDE RID: 39902
		private bool m_DefInitialized;

		// Token: 0x04009BDF RID: 39903
		private bool m_BackAtkDefInitialized;

		// Token: 0x04009BE0 RID: 39904
		private bool m_Visible;

		// Token: 0x04009BE1 RID: 39905
		private int m_ShowAtk;

		// Token: 0x04009BE2 RID: 39906
		private int m_ShowDef;

		// Token: 0x04009BE3 RID: 39907
		private Tween m_TwChangeCounter;

		// Token: 0x04009BE4 RID: 39908
		private Tween m_TwAtkToDef;

		// Token: 0x04009BE5 RID: 39909
		private Tween m_TwDefToAtk;

		// Token: 0x04009BE6 RID: 39910
		private Tween m_TwShowLabel;

		// Token: 0x04009BE7 RID: 39911
		private Tween m_TwHideLabel;

		// Token: 0x04009BE8 RID: 39912
		private int m_CurrentCounterIndex;

		// Token: 0x04009BE9 RID: 39913
		protected List<Transform> m_RotateItemList;

		// Token: 0x04009BEA RID: 39914
		protected Dictionary<int, int> m_MarkerPosTable;

		// Token: 0x04009BEB RID: 39915
		private ElementObjectManager m_RootSwitchTween;

		// Token: 0x04009BEC RID: 39916
		private bool m_LinkMarkerShowing;

		// Token: 0x04009BED RID: 39917
		public bool ShowFullStatus;

		// Token: 0x02000D13 RID: 3347
		private enum StatusType
		{
			// Token: 0x04009BEF RID: 39919
			Monster,
			// Token: 0x04009BF0 RID: 39920
			Magic,
			// Token: 0x04009BF1 RID: 39921
			PendulumCard
		}

		// Token: 0x02000D14 RID: 3348
		private enum StateIconType
		{
			// Token: 0x04009BF3 RID: 39923
			DISABLE,
			// Token: 0x04009BF4 RID: 39924
			UNATTACKABLE,
			// Token: 0x04009BF5 RID: 39925
			TRUNER
		}
	}
}
