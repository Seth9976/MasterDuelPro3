using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004B4 RID: 1204
	public class RandomRotator : MonoBehaviour
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000F16B8 File Offset: 0x000EF8B8
		// (set) Token: 0x060026B0 RID: 9904 RVA: 0x0000216D File Offset: 0x0000036D
		public Quaternion rotation
		{
			[CompilerGenerated]
			get
			{
				return default(Quaternion);
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0000216D File Offset: 0x0000036D
		public void Start()
		{
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0000216D File Offset: 0x0000036D
		public void Apply()
		{
		}

		// Token: 0x040027B0 RID: 10160
		[SerializeField]
		private Vector3 axis;

		// Token: 0x040027B1 RID: 10161
		[SerializeField]
		private float angleMin;

		// Token: 0x040027B2 RID: 10162
		[SerializeField]
		private float angleMax;
	}
}
