using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
[DisallowMultipleComponent]
public class UICanvas : MonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000216A File Offset: 0x0000036A
	protected virtual Camera GetCamera()
	{
		return null;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000216A File Offset: 0x0000036A
	protected virtual Camera GetSelfCamera()
	{
		return null;
	}
}
