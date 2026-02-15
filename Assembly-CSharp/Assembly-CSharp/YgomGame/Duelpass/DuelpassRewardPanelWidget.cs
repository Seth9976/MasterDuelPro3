using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C47 RID: 3143
	public class DuelpassRewardPanelWidget
	{
		// Token: 0x060059D2 RID: 22994 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassRewardPanelWidget(ElementObjectManager duelpassUIeom, SelectionButton okButton = null)
		{
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReceiveFunctionOff()
		{
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveToGradeBeforeDuel()
		{
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveToAchievedGrade()
		{
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGradeUp(int grade)
		{
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetAttentionEntityIdx(int grade)
		{
			return 0;
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContents()
		{
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0000216D File Offset: 0x0000036D
		private void MakeScrollEntityList()
		{
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEntityCreate(GameObject duplicatedTemplate)
		{
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEntityUpdate(GameObject duplicatedTemplate, int entityIdx)
		{
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBackButton()
		{
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNextButton()
		{
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x0000216D File Offset: 0x0000036D
		private void SnapPage(int dir)
		{
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator SnapCoroutine(int snapSpan, Action onCompleteAction = null)
		{
			return null;
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnLockGoldPass()
		{
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectSelectionItemsFunc(GameObject go)
		{
			return null;
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSelectableDataIndexFunc(int idx)
		{
			return false;
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x000029CC File Offset: 0x00000BCC
		private int CalcLeftEdge()
		{
			return 0;
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollValueChanged()
		{
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartProgress()
		{
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndProgress()
		{
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePageButton()
		{
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x000029CC File Offset: 0x00000BCC
		private int CalcRelativeIdx(int x)
		{
			return 0;
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x0000216D File Offset: 0x0000036D
		private void FocusRelativePos(int diff, int y)
		{
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x000F4D70 File Offset: 0x000F2F70
		private ValueTuple<int, int> CheckCurrentButtonPos()
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x04009554 RID: 38228
		private InfinityScrollView infinityScroll;

		// Token: 0x04009555 RID: 38229
		private ScrollRect scrollRect;

		// Token: 0x04009556 RID: 38230
		private Dictionary<GameObject, DuelpassRewardColumnWidget> templateToWidgetDict;

		// Token: 0x04009557 RID: 38231
		private GameObject lockedIcon;

		// Token: 0x04009558 RID: 38232
		private SelectionButton backButton;

		// Token: 0x04009559 RID: 38233
		private SelectionButton nextButton;

		// Token: 0x0400955A RID: 38234
		private SelectionButton toDuelResultButton;

		// Token: 0x0400955B RID: 38235
		private float snapDurationTime;

		// Token: 0x0400955C RID: 38236
		private int attentionEntityIdx;

		// Token: 0x0400955D RID: 38237
		private int currentGrade;

		// Token: 0x0400955E RID: 38238
		private int achievedGrade;

		// Token: 0x0400955F RID: 38239
		private int duelpassScrollentityNum;

		// Token: 0x04009560 RID: 38240
		private int attentionEntityIdxMax;

		// Token: 0x04009561 RID: 38241
		private int target;

		// Token: 0x04009562 RID: 38242
		private bool isFirstChangeGrade;

		// Token: 0x04009563 RID: 38243
		private bool isReceivable;

		// Token: 0x04009564 RID: 38244
		private bool isLockedPageButton;

		// Token: 0x04009565 RID: 38245
		private List<DuelpassRewardColumnContext> entityContextList;

		// Token: 0x04009566 RID: 38246
		private Dictionary<int, DuelpassRewardColumnWidget> gradeToWidgetDict;

		// Token: 0x04009567 RID: 38247
		private const string k_ELabelAnalogDirectionItem = "AnalogDirectionItem";

		// Token: 0x04009568 RID: 38248
		private bool isPlayingAnimation;
	}
}
