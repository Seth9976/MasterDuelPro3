using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Card
{
	// Token: 0x020010ED RID: 4333
	public class AutoReleaseCardMaterial : MonoBehaviour
	{
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x060080FF RID: 33023 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06008100 RID: 33024 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Initialized
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

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06008101 RID: 33025 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06008102 RID: 33026 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06008103 RID: 33027 RVA: 0x0000216D File Offset: 0x0000036D
		private Material m_Material
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06008104 RID: 33028 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(CardMaterialManager cardMaterialManager, bool isResetParent)
		{
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateMateriaInfo(int cardid, int finishid, AutoReleaseCardMaterial.InstanceType type, UnityAction onFinish)
		{
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBecameVisible()
		{
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBecameInvisible()
		{
		}

		// Token: 0x06008108 RID: 33032 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardPicture()
		{
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400B97E RID: 47486
		public Func<bool> IdleWhenInvisible;

		// Token: 0x0400B97F RID: 47487
		private CardMaterialManager cardMaterialManager;

		// Token: 0x0400B980 RID: 47488
		private int m_Cardid;

		// Token: 0x0400B981 RID: 47489
		private int m_Finishid;

		// Token: 0x0400B982 RID: 47490
		private bool m_ResetMatWhenVisible;

		// Token: 0x0400B983 RID: 47491
		private AutoReleaseCardMaterial.InstanceType m_Type;

		// Token: 0x0400B984 RID: 47492
		private float m_FakeBlendCache;

		// Token: 0x0400B985 RID: 47493
		private UnityAction m_OnFinish;

		// Token: 0x020010EE RID: 4334
		public enum InstanceType
		{
			// Token: 0x0400B987 RID: 47495
			None,
			// Token: 0x0400B988 RID: 47496
			RawImage,
			// Token: 0x0400B989 RID: 47497
			MeshRenderer
		}
	}
}
