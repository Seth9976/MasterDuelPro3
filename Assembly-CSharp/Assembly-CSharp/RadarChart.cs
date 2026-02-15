using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class RadarChart
{
	// Token: 0x06000133 RID: 307 RVA: 0x0000216D File Offset: 0x0000036D
	public void Initialize(byte _vertexNum = 5)
	{
	}

	// Token: 0x040001C8 RID: 456
	private byte vertexNum;

	// Token: 0x040001C9 RID: 457
	private float maxLength;

	// Token: 0x040001CA RID: 458
	private float deltaTheta;

	// Token: 0x040001CB RID: 459
	private float yRate;

	// Token: 0x040001CC RID: 460
	private float yOffset;

	// Token: 0x040001CD RID: 461
	[HideInInspector]
	public float[] rates;
}
