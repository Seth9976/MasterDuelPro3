using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class ParticleRingConverge : MonoBehaviour
{
	// Token: 0x060000E8 RID: 232 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000216D File Offset: 0x0000036D
	private void particle_ini()
	{
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000216D File Offset: 0x0000036D
	private void particle_move()
	{
	}

	// Token: 0x04000141 RID: 321
	private ParticleSystem pe;

	// Token: 0x04000142 RID: 322
	private ParticleSystem.Particle[] particle;

	// Token: 0x04000143 RID: 323
	public int particle_total;

	// Token: 0x04000144 RID: 324
	public int start_particle_total;

	// Token: 0x04000145 RID: 325
	public float particle_appearance_interval;

	// Token: 0x04000146 RID: 326
	public float particle_appear_radius;

	// Token: 0x04000147 RID: 327
	public float particle_appear_radius_max;

	// Token: 0x04000148 RID: 328
	public float appear_spread_time;

	// Token: 0x04000149 RID: 329
	public float appear_spread_speed;

	// Token: 0x0400014A RID: 330
	public float converge_radius_spread_time;

	// Token: 0x0400014B RID: 331
	public float converge_radius_spread_speed;

	// Token: 0x0400014C RID: 332
	public float converge_radius_max;

	// Token: 0x0400014D RID: 333
	public float anlgle_speed;

	// Token: 0x0400014E RID: 334
	public float anlgle_accel;

	// Token: 0x0400014F RID: 335
	public float anlgle_max_speed;

	// Token: 0x04000150 RID: 336
	public float circle_decrease_speed;

	// Token: 0x04000151 RID: 337
	private int particle_num;

	// Token: 0x04000152 RID: 338
	private int numParticlesAlive;

	// Token: 0x04000153 RID: 339
	private float base_particle_angle;

	// Token: 0x04000154 RID: 340
	private float particule_appearance_time;

	// Token: 0x04000155 RID: 341
	private float converge_radius;

	// Token: 0x04000156 RID: 342
	private float time;

	// Token: 0x04000157 RID: 343
	private List<ParticleRingConverge.ParticleInfo> particleInfos;

	// Token: 0x02000038 RID: 56
	private class ParticleInfo
	{
		// Token: 0x04000158 RID: 344
		public float particle_life;

		// Token: 0x04000159 RID: 345
		public float particle_start_pos;

		// Token: 0x0400015A RID: 346
		public float particle_range;

		// Token: 0x0400015B RID: 347
		public float particle_rotation_x;

		// Token: 0x0400015C RID: 348
		public float particle_rotation_y;

		// Token: 0x0400015D RID: 349
		public float particle_rotation_z;
	}
}
