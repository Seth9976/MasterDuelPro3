using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000565 RID: 1381
	public abstract class Binding : MonoBehaviour
	{
		// Token: 0x06002C0A RID: 11274
		public abstract void OnRebind();

		// Token: 0x06002C0B RID: 11275
		public abstract bool OnBinding();

		// Token: 0x06002C0C RID: 11276 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Start()
		{
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0000216D File Offset: 0x0000036D
		public void SourceChanged()
		{
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x0000216D File Offset: 0x0000036D
		public void TargetChanged()
		{
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateBinding()
		{
		}

		// Token: 0x04002A82 RID: 10882
		[SerializeField]
		public Binding.Mode mode;

		// Token: 0x02000566 RID: 1382
		public enum Mode
		{
			// Token: 0x04002A84 RID: 10884
			OneTime,
			// Token: 0x04002A85 RID: 10885
			OneWay,
			// Token: 0x04002A86 RID: 10886
			OneWayToSource,
			// Token: 0x04002A87 RID: 10887
			TwoWay
		}
	}
}
