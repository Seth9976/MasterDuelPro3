using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class ParticleSwirl_twist : MonoBehaviour
{
	// Token: 0x060000EE RID: 238 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000216D File Offset: 0x0000036D
	private void particle_ini()
	{
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x0000216D File Offset: 0x0000036D
	private void particle_move()
	{
	}

	// Token: 0x0400015E RID: 350
	private ParticleSystem pe;

	// Token: 0x0400015F RID: 351
	private ParticleSystem.Particle[] particle;

	// Token: 0x04000160 RID: 352
	public int particle_total;

	// Token: 0x04000161 RID: 353
	public int start_particle_total;

	// Token: 0x04000162 RID: 354
	public float particle_appearance_interval;

	// Token: 0x04000163 RID: 355
	public float particle_appear_radius;

	// Token: 0x04000164 RID: 356
	public float particle_appear_radius_max;

	// Token: 0x04000165 RID: 357
	public float particle_thick;

	// Token: 0x04000166 RID: 358
	public float appear_spread_time;

	// Token: 0x04000167 RID: 359
	public float appear_spread_speed;

	// Token: 0x04000168 RID: 360
	public bool particle_3D_rotation;

	// Token: 0x04000169 RID: 361
	public float converge_radius_spread_time;

	// Token: 0x0400016A RID: 362
	public float converge_radius_spread_speed;

	// Token: 0x0400016B RID: 363
	public float converge_radius_max;

	// Token: 0x0400016C RID: 364
	public float anlgle_speed;

	// Token: 0x0400016D RID: 365
	public float anlgle_accel;

	// Token: 0x0400016E RID: 366
	public float anlgle_max_speed;

	// Token: 0x0400016F RID: 367
	public float circle_decrease_speed;

	// Token: 0x04000170 RID: 368
	private int particle_num;

	// Token: 0x04000171 RID: 369
	private int numParticlesAlive;

	// Token: 0x04000172 RID: 370
	private float base_particle_angle;

	// Token: 0x04000173 RID: 371
	private float particule_appearance_time;

	// Token: 0x04000174 RID: 372
	private float converge_radius;

	// Token: 0x04000175 RID: 373
	private float base_particle_twist_angle;

	// Token: 0x04000176 RID: 374
	private float time;

	// Token: 0x04000177 RID: 375
	private int cut_num;

	// Token: 0x04000178 RID: 376
	private int cut_total;

	// Token: 0x04000179 RID: 377
	private List<ParticleSwirl_twist.ParticleInfo> particleInfos;

	// Token: 0x0200003A RID: 58
	private class ParticleInfo
	{
		// Token: 0x0400017A RID: 378
		public float particle_life;

		// Token: 0x0400017B RID: 379
		public float particle_start_pos;

		// Token: 0x0400017C RID: 380
		public float particle_range;

		// Token: 0x0400017D RID: 381
		public float particle_rotation_x;

		// Token: 0x0400017E RID: 382
		public float particle_rotation_y;

		// Token: 0x0400017F RID: 383
		public float particle_rotation_z;

		// Token: 0x04000180 RID: 384
		public float particle_twist_rotation;
	}
}
