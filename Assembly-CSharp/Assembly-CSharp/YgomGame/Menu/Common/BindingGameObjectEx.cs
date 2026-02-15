using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B15 RID: 2837
	public class BindingGameObjectEx : BindingGameObject, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600524D RID: 21069 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x0600524E RID: 21070 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600524F RID: 21071 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingGameObjectEx Binding(GameObject target, string prefabPath, bool fitParentSize = false, bool async = true, bool rebind = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingGameObjectEx Binding(GameObject target, string prefabPath, BindingGameObjectEx.FitMode fitMode = BindingGameObjectEx.FitMode.NONE, bool async = true, bool rebind = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0000216A File Offset: 0x0000036A
		private static BindingGameObjectEx BindingFitModeNone(GameObject target, string prefabPath, bool async = true, bool rebind = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0000216A File Offset: 0x0000036A
		private static BindingGameObjectEx BindingFitModeSize(GameObject target, string prefabPath, bool async = true, bool rebind = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x0000216A File Offset: 0x0000036A
		private static BindingGameObjectEx BindingFitModeScale(GameObject target, string prefabPath, bool async = true, bool rebind = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0000216A File Offset: 0x0000036A
		private static BindingGameObjectEx BindingFitModeScaleLowestOrHighest(GameObject target, string prefabPath, bool isHighest, bool async = true, bool rebind = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x0000216A File Offset: 0x0000036A
		private static BindingGameObjectEx BindingFitModeScaleByParent(GameObject target, string prefabPath, bool envelopeParent, bool async = true, bool rebind = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x000029CC File Offset: 0x00000BCC
		public new bool IsDone()
		{
			return false;
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ClearProgressContent()
		{
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AssignProgressContent(IAsyncProgressContent progressContent)
		{
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x000F4C34 File Offset: 0x000F2E34
		public static Vector3 CalcFitScale(BindingGameObjectEx.FitMode fitMode, Vector2 parentSize, Vector2 childSize)
		{
			return default(Vector3);
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void PostModifiyByArgs(GameObject go)
		{
		}

		// Token: 0x040090A1 RID: 37025
		private List<IAsyncProgressContent> m_AsyncProgressContents;

		// Token: 0x02000B16 RID: 2838
		public enum FitMode
		{
			// Token: 0x040090A3 RID: 37027
			NONE,
			// Token: 0x040090A4 RID: 37028
			SIZE,
			// Token: 0x040090A5 RID: 37029
			SCALE,
			// Token: 0x040090A6 RID: 37030
			SCALE_FIT_LOWEST,
			// Token: 0x040090A7 RID: 37031
			SCALE_FIT_HIGHEST,
			// Token: 0x040090A8 RID: 37032
			SCALE_FIT_IN_PARENT,
			// Token: 0x040090A9 RID: 37033
			SCALE_ENVELOPE_PARENT
		}
	}
}
