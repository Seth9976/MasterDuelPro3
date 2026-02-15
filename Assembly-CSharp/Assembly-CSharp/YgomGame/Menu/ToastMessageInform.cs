using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AFF RID: 2815
	public class ToastMessageInform : InformContentBase
	{
		// Token: 0x060051E1 RID: 20961 RVA: 0x0000216A File Offset: 0x0000036A
		private Sprite GetArgIcon()
		{
			return null;
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenWithBlock(string message, Action callback = null)
		{
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string message, Action callback = null)
		{
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string message, ToastMessageInform.IconType iconType)
		{
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string message, float yPos, ToastMessageInform.IconType iconType)
		{
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0000216D File Offset: 0x0000036D
		private static void InnerOpen(string message, Action callback = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPush()
		{
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedView(ElementObjectManager eom)
		{
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04009046 RID: 36934
		private const string k_PrefPath = "Common/ToastMessage/ToastMessage";

		// Token: 0x04009047 RID: 36935
		private const string k_ArgKeyMessage = "message";

		// Token: 0x04009048 RID: 36936
		private const string k_ArgKeyYPos = "yPos";

		// Token: 0x04009049 RID: 36937
		private const string k_ArgKeyIconType = "icon";

		// Token: 0x0400904A RID: 36938
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x0400904B RID: 36939
		private readonly string k_ELabelMessageText;

		// Token: 0x0400904C RID: 36940
		private readonly string k_ELabelIconImage;

		// Token: 0x0400904D RID: 36941
		private readonly string k_TweenShow;

		// Token: 0x0400904E RID: 36942
		private readonly string k_TweenHide;

		// Token: 0x0400904F RID: 36943
		[SerializeField]
		private ViewCreater m_ViewCreater;

		// Token: 0x04009050 RID: 36944
		[SerializeField]
		private float m_LifeSecond;

		// Token: 0x04009051 RID: 36945
		[SerializeField]
		private Sprite m_InformSprite;

		// Token: 0x04009052 RID: 36946
		private ElementObjectManager m_View;

		// Token: 0x04009053 RID: 36947
		private bool _end;

		// Token: 0x02000B00 RID: 2816
		public enum IconType
		{
			// Token: 0x04009055 RID: 36949
			None,
			// Token: 0x04009056 RID: 36950
			Inform
		}
	}
}
