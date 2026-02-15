using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000034 RID: 52
public class OnDestroyAction : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetDestroyAction(UnityAction onDestroy)
	{
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000216D File Offset: 0x0000036D
	private void OnDestroy()
	{
	}

	// Token: 0x0400012D RID: 301
	protected UnityAction onDestroy;
}
