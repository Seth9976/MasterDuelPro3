using System;
using UnityEngine;

namespace YgomSystem.Home
{
	// Token: 0x0200075D RID: 1885
	public abstract class ActiveFalseTarget<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x06003AEC RID: 15084 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06003AED RID: 15085
		protected abstract bool IsActive();

		// Token: 0x04003469 RID: 13417
		[SerializeField]
		private GameObject targetGameObject;

		// Token: 0x0400346A RID: 13418
		protected T component;
	}
}
