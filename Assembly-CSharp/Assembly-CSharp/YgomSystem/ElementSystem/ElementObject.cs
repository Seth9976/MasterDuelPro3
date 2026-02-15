using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.ElementSystem
{
	// Token: 0x0200077E RID: 1918
	public class ElementObject : MonoBehaviour
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003B8A RID: 15242 RVA: 0x0000216D File Offset: 0x0000036D
		public ElementObjectManager parentManager
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

		// Token: 0x06003B8B RID: 15243 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnValidate()
		{
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup()
		{
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x0000216D File Offset: 0x0000036D
		public void ElementCallback()
		{
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x0000216D File Offset: 0x0000036D
		public void ElementCallback(float value)
		{
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x0000216D File Offset: 0x0000036D
		public void ElementCallback(int value)
		{
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x0000216D File Offset: 0x0000036D
		public void ElementCallback(Vector2 value)
		{
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x0000216D File Offset: 0x0000036D
		public void ElementCallback(string value)
		{
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x0000216D File Offset: 0x0000036D
		public void ElementCallback(bool value)
		{
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04003488 RID: 13448
		public string label;

		// Token: 0x04003489 RID: 13449
		public UnityAction callbackVoid;

		// Token: 0x0400348A RID: 13450
		public UnityAction<int> callbackInt;

		// Token: 0x0400348B RID: 13451
		public UnityAction<float> callbackFloat;

		// Token: 0x0400348C RID: 13452
		public UnityAction<Vector2> callbackVector2;

		// Token: 0x0400348D RID: 13453
		public UnityAction<string> callbackString;

		// Token: 0x0400348E RID: 13454
		public UnityAction<bool> callbackBool;
	}
}
