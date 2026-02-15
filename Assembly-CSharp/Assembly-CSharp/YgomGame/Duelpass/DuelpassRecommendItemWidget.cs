using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomGame.Duel;
using YgomSystem.UI;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C3F RID: 3135
	public class DuelpassRecommendItemWidget : MonoBehaviour
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06005960 RID: 22880 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005961 RID: 22881 RVA: 0x0000216D File Offset: 0x0000036D
		public Character2D Character
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

		// Token: 0x06005962 RID: 22882 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init()
		{
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartSwap()
		{
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x0000216D File Offset: 0x0000036D
		public void BindAll()
		{
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator SwapCoroutine(float swapSpan)
		{
			return null;
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindItem(DuelpassRewardContext context, GameObject holderTemplate, GameObject binder)
		{
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x0000216D File Offset: 0x0000036D
		private void TweenTargetItemBinderPlayLabel(GameObject template, string label)
		{
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopTween()
		{
		}

		// Token: 0x04009528 RID: 38184
		[SerializeField]
		public GameObject itemHolder;

		// Token: 0x04009529 RID: 38185
		[SerializeField]
		private GameObject holderParent;

		// Token: 0x0400952A RID: 38186
		[SerializeField]
		private GameObject wallpaperBgObject;

		// Token: 0x0400952B RID: 38187
		private List<ValueTuple<DuelpassRewardContext, GameObject, GameObject>> recommends;

		// Token: 0x0400952C RID: 38188
		[SerializeField]
		private SelectionButton mateButton;

		// Token: 0x0400952D RID: 38189
		[SerializeField]
		private TMP_Text gradeText;

		// Token: 0x0400952E RID: 38190
		private GameObject currentWallpaperGo;

		// Token: 0x0400952F RID: 38191
		private readonly string LABEL_ITEMBINDER;

		// Token: 0x04009530 RID: 38192
		private readonly string LABEL_WALLPAPERBG;

		// Token: 0x04009531 RID: 38193
		private int nowItemIdx;

		// Token: 0x04009532 RID: 38194
		private int nextItemIdx;
	}
}
