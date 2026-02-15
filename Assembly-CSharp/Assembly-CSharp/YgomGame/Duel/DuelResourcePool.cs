using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000D92 RID: 3474
	public class DuelResourcePool : MonoBehaviour
	{
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06006615 RID: 26133 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006616 RID: 26134 RVA: 0x0000216D File Offset: 0x0000036D
		public SpriteContainer duelIcon
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

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06006617 RID: 26135 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006618 RID: 26136 RVA: 0x0000216D File Offset: 0x0000036D
		public SpriteContainer counterIcon
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

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06006619 RID: 26137 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInitialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600661A RID: 26138 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600661B RID: 26139 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTerminated
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

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600661D RID: 26141 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
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

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x0600661E RID: 26142 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600661F RID: 26143 RVA: 0x0000216D File Offset: 0x0000036D
		public Dictionary<Engine.AffectType, Texture2D> affectIcons
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

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06006620 RID: 26144 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006621 RID: 26145 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject shuffleDeckModel
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

		// Token: 0x06006622 RID: 26146 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelResourcePool Create(DuelGameObjectManager goManager, GameObject root, string name)
		{
			return null;
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadDuelIconContainer()
		{
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCounterIconContainer()
		{
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadShuffleDeckModel()
		{
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetFreeTextTurnChangeTex(int player, Action<Texture2D> onFinished)
		{
		}

		// Token: 0x06006629 RID: 26153 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator GetFreeTextTurnChangeTexImpl(int player, Action<Texture2D> onFinished)
		{
			return null;
		}

		// Token: 0x0400A044 RID: 41028
		private const string shuffleDeckModelPath = "Duel/Models/DeckModelWrapper";

		// Token: 0x0400A045 RID: 41029
		private Dictionary<int, Texture2D> freeTextTurnChangeTexs;
	}
}
