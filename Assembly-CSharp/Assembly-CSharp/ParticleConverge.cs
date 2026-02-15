using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class ParticleConverge : MonoBehaviour
{
	// Token: 0x060000E3 RID: 227 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x0000216D File Offset: 0x0000036D
	private void particle_ini()
	{
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x0000216D File Offset: 0x0000036D
	private void lay_converge()
	{
	}

	// Token: 0x04000131 RID: 305
	private ParticleSystem pe;

	// Token: 0x04000132 RID: 306
	private ParticleSystem.Particle[] particle;

	// Token: 0x04000133 RID: 307
	public int particle_total;

	// Token: 0x04000134 RID: 308
	public float particle_radius;

	// Token: 0x04000135 RID: 309
	public float particule_start_time;

	// Token: 0x04000136 RID: 310
	public float particule_appearance_interval;

	// Token: 0x04000137 RID: 311
	private float particule_appearance_time;

	// Token: 0x04000138 RID: 312
	public float anlgle_speed;

	// Token: 0x04000139 RID: 313
	public float anlgle_accel;

	// Token: 0x0400013A RID: 314
	public float converge_time;

	// Token: 0x0400013B RID: 315
	public float converge_size;

	// Token: 0x0400013C RID: 316
	public float circle_decrease_speed;

	// Token: 0x0400013D RID: 317
	private int particle_num;

	// Token: 0x0400013E RID: 318
	private float angle_interval;

	// Token: 0x0400013F RID: 319
	private float particle_angle;

	// Token: 0x04000140 RID: 320
	private float time;
}
