using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C45 RID: 3141
	public class DuelpassRewardColumnWidget
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600599E RID: 22942 RVA: 0x0000216D File Offset: 0x0000036D
		public int ScrollEntityIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600599F RID: 22943 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059A0 RID: 22944 RVA: 0x0000216D File Offset: 0x0000036D
		public int Grade
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060059A1 RID: 22945 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060059A2 RID: 22946 RVA: 0x0000216D File Offset: 0x0000036D
		public TweenColorTo GradeTextColor
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060059A3 RID: 22947 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060059A4 RID: 22948 RVA: 0x0000216D File Offset: 0x0000036D
		public TweenColorTo GradeTextBase
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060059A6 RID: 22950 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject TextColor
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060059A7 RID: 22951 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060059A8 RID: 22952 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject BaseColor
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassRewardColumnWidget(GameObject gob)
		{
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDownTransition(SelectionItem item)
		{
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init(DuelpassRewardColumnContext context, int achievedGrade)
		{
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSide(DuelpassRewardColumnWidget left, DuelpassRewardColumnWidget right)
		{
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSide()
		{
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReceiveFunctionOff()
		{
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAchievement(int achievedGrade)
		{
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAchievementWithTween(int achievedGrade)
		{
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x0000216D File Offset: 0x0000036D
		public void Achieved()
		{
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x0000216D File Offset: 0x0000036D
		public void TweenToAchieved()
		{
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator PlayGradeUpSe(int grade)
		{
			return null;
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeAchieved()
		{
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectColumnButton(int row)
		{
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectButton(int row, bool GoldGrade = false)
		{
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CheckHasButton(SelectionButton button)
		{
			return 0;
		}

		// Token: 0x04009551 RID: 38225
		private TMP_Text gradeText;

		// Token: 0x04009552 RID: 38226
		private DuelpassRewardButtonWidget normalRewardButton;

		// Token: 0x04009553 RID: 38227
		private DuelpassRewardButtonWidget goldRewardButton;
	}
}
