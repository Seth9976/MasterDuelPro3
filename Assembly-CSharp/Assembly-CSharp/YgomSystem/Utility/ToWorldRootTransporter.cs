using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000554 RID: 1364
	public class ToWorldRootTransporter : MonoBehaviour
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002BB3 RID: 11187 RVA: 0x0000216D File Offset: 0x0000036D
		public Transform target
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04002A40 RID: 10816
		[SerializeField]
		private Transform m_Target;
	}
}
