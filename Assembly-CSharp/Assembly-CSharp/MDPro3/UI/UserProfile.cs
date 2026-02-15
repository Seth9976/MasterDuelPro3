using System;
using System.Collections.Generic;
using MDPro3.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x02001375 RID: 4981
	public class UserProfile : MonoBehaviour
	{
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06009033 RID: 36915 RVA: 0x0013B090 File Offset: 0x00139290
		private ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06009034 RID: 36916 RVA: 0x0013B0C4 File Offset: 0x001392C4
		private Image IconBG
		{
			get
			{
				return this.m_IconBG = ((this.m_IconBG != null) ? this.m_IconBG : this.Manager.GetElement<Image>("IconBG"));
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06009035 RID: 36917 RVA: 0x0013B100 File Offset: 0x00139300
		private Image IconRank
		{
			get
			{
				return this.m_IconRank = ((this.m_IconRank != null) ? this.m_IconRank : this.Manager.GetElement<Image>("IconRank"));
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06009036 RID: 36918 RVA: 0x0013B13C File Offset: 0x0013933C
		private Image IconTier1
		{
			get
			{
				return this.m_IconTier1 = ((this.m_IconTier1 != null) ? this.m_IconTier1 : this.Manager.GetElement<Image>("IconTier1"));
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06009037 RID: 36919 RVA: 0x0013B178 File Offset: 0x00139378
		private Image IconTier2
		{
			get
			{
				return this.m_IconTier2 = ((this.m_IconTier2 != null) ? this.m_IconTier2 : this.Manager.GetElement<Image>("IconTier2"));
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06009038 RID: 36920 RVA: 0x0013B1B4 File Offset: 0x001393B4
		private Image IconTier3
		{
			get
			{
				return this.m_IconTier3 = ((this.m_IconTier3 != null) ? this.m_IconTier3 : this.Manager.GetElement<Image>("IconTier3"));
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06009039 RID: 36921 RVA: 0x0013B1F0 File Offset: 0x001393F0
		public RawImage Avatar
		{
			get
			{
				return this.m_Avatar = ((this.m_Avatar != null) ? this.m_Avatar : this.Manager.GetElement<RawImage>("RawImageAvatar"));
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x0600903A RID: 36922 RVA: 0x0013B22C File Offset: 0x0013942C
		private TextMeshProUGUI TextUserName
		{
			get
			{
				return this.m_TextUserName = ((this.m_TextUserName != null) ? this.m_TextUserName : this.Manager.GetElement<TextMeshProUGUI>("TextUserName"));
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x0600903B RID: 36923 RVA: 0x0013B268 File Offset: 0x00139468
		private TextMeshProUGUI TextExpValue
		{
			get
			{
				return this.m_TextExpValue = ((this.m_TextExpValue != null) ? this.m_TextExpValue : this.Manager.GetElement<TextMeshProUGUI>("TextExpValue"));
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x0600903C RID: 36924 RVA: 0x0013B2A4 File Offset: 0x001394A4
		private TextMeshProUGUI TextDPValue
		{
			get
			{
				return this.m_TextDPValue = ((this.m_TextDPValue != null) ? this.m_TextDPValue : this.Manager.GetElement<TextMeshProUGUI>("TextDPValue"));
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x0600903D RID: 36925 RVA: 0x0013B2E0 File Offset: 0x001394E0
		private TextMeshProUGUI TextAthleticWinValue
		{
			get
			{
				return this.m_TextAthleticWinValue = ((this.m_TextAthleticWinValue != null) ? this.m_TextAthleticWinValue : this.Manager.GetElement<TextMeshProUGUI>("TextAthleticWinValue"));
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x0600903E RID: 36926 RVA: 0x0013B31C File Offset: 0x0013951C
		private TextMeshProUGUI TextAthleticWinRatioValue
		{
			get
			{
				return this.m_TextAthleticWinRatioValue = ((this.m_TextAthleticWinRatioValue != null) ? this.m_TextAthleticWinRatioValue : this.Manager.GetElement<TextMeshProUGUI>("TextAthleticWinRatioValue"));
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x0600903F RID: 36927 RVA: 0x0013B358 File Offset: 0x00139558
		private TextMeshProUGUI TextAthleticRankValue
		{
			get
			{
				return this.m_TextAthleticRankValue = ((this.m_TextAthleticRankValue != null) ? this.m_TextAthleticRankValue : this.Manager.GetElement<TextMeshProUGUI>("TextAthleticRankValue"));
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06009040 RID: 36928 RVA: 0x0013B394 File Offset: 0x00139594
		private TextMeshProUGUI TextEntertainCountValue
		{
			get
			{
				return this.m_TextEntertainCountValue = ((this.m_TextEntertainCountValue != null) ? this.m_TextEntertainCountValue : this.Manager.GetElement<TextMeshProUGUI>("TextEntertainCountValue"));
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06009041 RID: 36929 RVA: 0x0013B3D0 File Offset: 0x001395D0
		private TextMeshProUGUI TextEntertainRankValue
		{
			get
			{
				return this.m_TextEntertainRankValue = ((this.m_TextEntertainRankValue != null) ? this.m_TextEntertainRankValue : this.Manager.GetElement<TextMeshProUGUI>("TextEntertainRankValue"));
			}
		}

		// Token: 0x06009042 RID: 36930 RVA: 0x0013B40C File Offset: 0x0013960C
		public void SetProfile(MyCardUserExp data)
		{
			this.TextUserName.text = MyCard.account.user.name;
			this.TextExpValue.text = data.exp.ToString();
			this.TextDPValue.text = data.pt.ToString();
			this.TextAthleticWinValue.text = data.athletic_win.ToString();
			this.TextAthleticWinRatioValue.text = data.athletic_wl_ratio + "%";
			this.TextAthleticRankValue.text = data.arena_rank.ToString();
			this.TextEntertainCountValue.text = data.entertain_all.ToString();
			this.TextEntertainRankValue.text = data.exp_rank.ToString();
			List<Sprite> rankSprites = TextureManager.container.GetRankSprites(data.pt);
			this.IconBG.sprite = rankSprites[0];
			this.IconRank.sprite = rankSprites[1];
			this.IconTier1.sprite = rankSprites[2];
			this.IconTier2.sprite = rankSprites[3];
			this.IconTier3.sprite = rankSprites[4];
		}

		// Token: 0x0400CED2 RID: 52946
		private ElementObjectManager m_Manager;

		// Token: 0x0400CED3 RID: 52947
		private const string LABEL_IMG_ICONBG = "IconBG";

		// Token: 0x0400CED4 RID: 52948
		private Image m_IconBG;

		// Token: 0x0400CED5 RID: 52949
		private const string LABEL_IMG_ICONRANK = "IconRank";

		// Token: 0x0400CED6 RID: 52950
		private Image m_IconRank;

		// Token: 0x0400CED7 RID: 52951
		private const string LABEL_IMG_ICONTIER1 = "IconTier1";

		// Token: 0x0400CED8 RID: 52952
		private Image m_IconTier1;

		// Token: 0x0400CED9 RID: 52953
		private const string LABEL_IMG_ICONTIER2 = "IconTier2";

		// Token: 0x0400CEDA RID: 52954
		private Image m_IconTier2;

		// Token: 0x0400CEDB RID: 52955
		private const string LABEL_IMG_ICONTIER3 = "IconTier3";

		// Token: 0x0400CEDC RID: 52956
		private Image m_IconTier3;

		// Token: 0x0400CEDD RID: 52957
		private const string LABEL_RIMG_AVATAR = "RawImageAvatar";

		// Token: 0x0400CEDE RID: 52958
		private RawImage m_Avatar;

		// Token: 0x0400CEDF RID: 52959
		private const string LABEL_TXT_USERNAME = "TextUserName";

		// Token: 0x0400CEE0 RID: 52960
		private TextMeshProUGUI m_TextUserName;

		// Token: 0x0400CEE1 RID: 52961
		private const string LABEL_TXT_EXPVALUE = "TextExpValue";

		// Token: 0x0400CEE2 RID: 52962
		private TextMeshProUGUI m_TextExpValue;

		// Token: 0x0400CEE3 RID: 52963
		private const string LABEL_TXT_DPVALUE = "TextDPValue";

		// Token: 0x0400CEE4 RID: 52964
		private TextMeshProUGUI m_TextDPValue;

		// Token: 0x0400CEE5 RID: 52965
		private const string LABEL_TXT_ATHLETICWINVALUE = "TextAthleticWinValue";

		// Token: 0x0400CEE6 RID: 52966
		private TextMeshProUGUI m_TextAthleticWinValue;

		// Token: 0x0400CEE7 RID: 52967
		private const string LABEL_TXT_AthleticWinRatioVALUE = "TextAthleticWinRatioValue";

		// Token: 0x0400CEE8 RID: 52968
		private TextMeshProUGUI m_TextAthleticWinRatioValue;

		// Token: 0x0400CEE9 RID: 52969
		private const string LABEL_TXT_ATHLETICRANKVALUE = "TextAthleticRankValue";

		// Token: 0x0400CEEA RID: 52970
		private TextMeshProUGUI m_TextAthleticRankValue;

		// Token: 0x0400CEEB RID: 52971
		private const string LABEL_TXT_ENTERTAINCOUNTVALUE = "TextEntertainCountValue";

		// Token: 0x0400CEEC RID: 52972
		private TextMeshProUGUI m_TextEntertainCountValue;

		// Token: 0x0400CEED RID: 52973
		private const string LABEL_TXT_ENTERTAINRANKVALUE = "TextEntertainRankValue";

		// Token: 0x0400CEEE RID: 52974
		private TextMeshProUGUI m_TextEntertainRankValue;
	}
}
