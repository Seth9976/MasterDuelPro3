using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Menu.Common;
using YgomSystem.Utility;

namespace YgomSystem.UI
{
	// Token: 0x0200056C RID: 1388
	public class BindingPadIcon : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C2E RID: 11310 RVA: 0x0000216D File Offset: 0x0000036D
		public ShortcutIcon.Mode mode
		{
			get
			{
				return ShortcutIcon.Mode.None;
			}
			set
			{
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C30 RID: 11312 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectorManager.KeyType keyType
		{
			get
			{
				return SelectorManager.KeyType.None;
			}
			set
			{
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C32 RID: 11314 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectorManager.AnalogType analogType
		{
			get
			{
				return SelectorManager.AnalogType.None;
			}
			set
			{
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C34 RID: 11316 RVA: 0x0000216D File Offset: 0x0000036D
		public ShortcutIcon.AnalogDirection analogDirection
		{
			get
			{
				return ShortcutIcon.AnalogDirection.Horizontal;
			}
			set
			{
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C36 RID: 11318 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectorManager.MouseType mouseType
		{
			get
			{
				return SelectorManager.MouseType.None;
			}
			set
			{
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C38 RID: 11320 RVA: 0x0000216D File Offset: 0x0000036D
		public GamePadIconUtil.Variation iconVariation
		{
			get
			{
				return GamePadIconUtil.Variation.Var00;
			}
			set
			{
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06002C3A RID: 11322 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002C3B RID: 11323 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06002C3C RID: 11324 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ySequence()
		{
			return null;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x0000216D File Offset: 0x0000036D
		private void SourceChange()
		{
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04002AA2 RID: 10914
		[SerializeField]
		private ShortcutIcon.Mode m_Mode;

		// Token: 0x04002AA3 RID: 10915
		[SerializeField]
		private SelectorManager.KeyType m_KeyType;

		// Token: 0x04002AA4 RID: 10916
		[SerializeField]
		private SelectorManager.AnalogType m_AnalogType;

		// Token: 0x04002AA5 RID: 10917
		[SerializeField]
		private ShortcutIcon.AnalogDirection m_AnalogDirection;

		// Token: 0x04002AA6 RID: 10918
		[SerializeField]
		private SelectorManager.MouseType m_MouseType;

		// Token: 0x04002AA7 RID: 10919
		[SerializeField]
		private GamePadIconUtil.Variation m_IconVariation;

		// Token: 0x04002AA8 RID: 10920
		private IEnumerator m_SequenceRoutine;
	}
}
