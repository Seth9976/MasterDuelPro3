using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;

namespace YgomGame.Colosseum
{
	// Token: 0x02001021 RID: 4129
	public class BindingEventLogo : MonoBehaviour
	{
		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06007C06 RID: 31750 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingEventLogo.Context EventLogoContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06007C07 RID: 31751 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007C08 RID: 31752 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06007C09 RID: 31753 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<IAsyncProgressContent> YgomGame_002EMenu_002ECommon_002EIAsyncProgressContainer_002EasyncProgressContents
		{
			get
			{
				return null;
			}
		}

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06007C0A RID: 31754 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06007C0B RID: 31755 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06007C0C RID: 31756 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingEventLogo Binding(GameObject target, BindingEventLogo.Context context)
		{
			return null;
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateData(BindingEventLogo.Context context)
		{
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x0000216D File Offset: 0x0000036D
		public void Load(bool isEqualPrefab, bool isEqualLogo, bool isEqualSubIds, bool isEqualIsLarge)
		{
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindEventLogoMonster(GameObject parent, BindingEventLogo.Context context)
		{
		}

		// Token: 0x06007C12 RID: 31762 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshVisible()
		{
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearProgressContents()
		{
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x0000216D File Offset: 0x0000036D
		private void AssignProgressContent(IAsyncProgressContent progressContent)
		{
		}

		// Token: 0x0400B3D9 RID: 46041
		[SerializeField]
		private BindingEventLogo.Context eventLogoContext;

		// Token: 0x0400B3DA RID: 46042
		private bool onLoadStart;

		// Token: 0x0400B3DB RID: 46043
		private GameObject bindChild;

		// Token: 0x0400B3DC RID: 46044
		private ElementObjectManager bindEom;

		// Token: 0x0400B3DD RID: 46045
		private bool m_Visible;

		// Token: 0x0400B3DE RID: 46046
		private List<IAsyncProgressContent> m_AsyncProgressContents;

		// Token: 0x02001022 RID: 4130
		public enum PrefabType
		{
			// Token: 0x0400B3E0 RID: 46048
			NONE,
			// Token: 0x0400B3E1 RID: 46049
			TYPE,
			// Token: 0x0400B3E2 RID: 46050
			ATTRIBUTE
		}

		// Token: 0x02001023 RID: 4131
		[Serializable]
		public class Context
		{
			// Token: 0x06007C17 RID: 31767 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsEqualSubIds(BindingEventLogo.Context other)
			{
				return false;
			}

			// Token: 0x06007C18 RID: 31768 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(BindingEventLogo.Context other)
			{
			}

			// Token: 0x0400B3E3 RID: 46051
			public BindingEventLogo.PrefabType prefabType;

			// Token: 0x0400B3E4 RID: 46052
			public int logoId;

			// Token: 0x0400B3E5 RID: 46053
			public List<int> subIds;

			// Token: 0x0400B3E6 RID: 46054
			public bool isLarge;
		}
	}
}
