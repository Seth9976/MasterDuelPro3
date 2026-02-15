using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class AnimatorTriggerTest : MonoBehaviour
{
	// Token: 0x06000028 RID: 40 RVA: 0x00002939 File Offset: 0x00000B39
	private void Update()
	{
		if (this.test)
		{
			this.test = false;
			this.Test();
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002950 File Offset: 0x00000B50
	private void Test()
	{
		Animator controller = base.GetComponent<Animator>();
		if (controller != null)
		{
			controller.SetTrigger(this.trigger);
		}
	}

	// Token: 0x04000024 RID: 36
	public string trigger;

	// Token: 0x04000025 RID: 37
	public bool test;
}
