using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B1F RID: 2847
	public class BindingProfileFrameIcon : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052DE RID: 21214 RVA: 0x0000216D File Offset: 0x0000036D
		public int baseId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060052DF RID: 21215 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052E0 RID: 21216 RVA: 0x0000216D File Offset: 0x0000036D
		public int frameId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060052E2 RID: 21218 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isLarge
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x0000216A File Offset: 0x0000036A
		private Image image
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060052E4 RID: 21220 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x060052E5 RID: 21221 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060052E6 RID: 21222 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x060052E7 RID: 21223 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingProfileFrameIcon Binding(Image target, int baseId = 0, int frameId = 0, bool isLarge = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingProfileFrameIcon Binding(GameObject target, int baseId, int frameId, bool isLarge = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingProfileFrameIcon BindingBase(Image target, int baseId, bool isLarge = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingProfileFrameIcon BindingBase(GameObject target, int baseId, bool isLarge = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingProfileFrameIcon BindingFrame(Image target, int frameId, bool isLarge = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingProfileFrameIcon BindingFrame(GameObject target, int frameId, bool isLarge = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0000216D File Offset: 0x0000036D
		public void SourceChanged()
		{
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yBindingRoutine(int baseId, int frameId, bool isLarge, bool async)
		{
			return null;
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040090F3 RID: 37107
		private readonly string k_MatLabelFrameTex;

		// Token: 0x040090F4 RID: 37108
		[SerializeField]
		private int m_BaseId;

		// Token: 0x040090F5 RID: 37109
		[SerializeField]
		private int m_FrameId;

		// Token: 0x040090F6 RID: 37110
		[SerializeField]
		private bool m_IsLarge;

		// Token: 0x040090F7 RID: 37111
		private bool m_Async;

		// Token: 0x040090F8 RID: 37112
		private uint m_BaseSpriteCrc;

		// Token: 0x040090F9 RID: 37113
		private uint m_FrameSpriteCrc;

		// Token: 0x040090FA RID: 37114
		private uint m_FrameMatCrc;

		// Token: 0x040090FB RID: 37115
		private Image m_ImageCache;

		// Token: 0x040090FC RID: 37116
		private Material m_ModiedMaterial;

		// Token: 0x040090FD RID: 37117
		private IEnumerator m_BindingRoutine;
	}
}
