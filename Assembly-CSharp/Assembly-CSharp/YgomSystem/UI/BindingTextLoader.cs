using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Menu.Common;

namespace YgomSystem.UI
{
	// Token: 0x02000570 RID: 1392
	public class BindingTextLoader : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06002C61 RID: 11361 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002C62 RID: 11362 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06002C63 RID: 11363 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yRoutine()
		{
			return null;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetGroupId(Binding target)
		{
			return null;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetGroupId(BindingTextMeshProUGUI target)
		{
			return null;
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetGroupId(BindingTextMeshPro target)
		{
			return null;
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetGruopId(Binding binding, string textId)
		{
			return null;
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04002AB2 RID: 10930
		[SerializeField]
		private Binding[] m_BindingTexts;

		// Token: 0x04002AB3 RID: 10931
		private List<string> m_LoadTextGroups;

		// Token: 0x04002AB4 RID: 10932
		private IEnumerator m_Routine;
	}
}
