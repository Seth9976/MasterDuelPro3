using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.Effect
{
	// Token: 0x02000780 RID: 1920
	public class AspectScaler : MonoBehaviour
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003BB2 RID: 15282 RVA: 0x0000216D File Offset: 0x0000036D
		public Camera viewCamera
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

		// Token: 0x06003BB3 RID: 15283 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(Camera view_camera)
		{
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x0000216D File Offset: 0x0000036D
		public void Apply()
		{
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x04003495 RID: 13461
		[SerializeField]
		private bool applyOnUpdate;

		// Token: 0x04003496 RID: 13462
		[SerializeField]
		private float aspect;
	}
}
