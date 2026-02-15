using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class FlowerTornado : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x0000216D File Offset: 0x0000036D
	private void Start()
	{
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000216D File Offset: 0x0000036D
	private void particle_ini()
	{
	}

	// Token: 0x060000AF RID: 175 RVA: 0x0000216D File Offset: 0x0000036D
	private void particle_move()
	{
	}

	// Token: 0x040000CC RID: 204
	private ParticleSystem pe;

	// Token: 0x040000CD RID: 205
	private ParticleSystem.Particle[] particle;

	// Token: 0x040000CE RID: 206
	public int particle_total;

	// Token: 0x040000CF RID: 207
	public int start_particle_total;

	// Token: 0x040000D0 RID: 208
	public float particle_appearance_interval;

	// Token: 0x040000D1 RID: 209
	public float particle_appear_radius;

	// Token: 0x040000D2 RID: 210
	public float particle_appear_radius_max;

	// Token: 0x040000D3 RID: 211
	public float particle_thick;

	// Token: 0x040000D4 RID: 212
	public float particle_spread;

	// Token: 0x040000D5 RID: 213
	public float particle_radius_ellipse;

	// Token: 0x040000D6 RID: 214
	public float appear_spread_time;

	// Token: 0x040000D7 RID: 215
	public float appear_spread_speed;

	// Token: 0x040000D8 RID: 216
	public bool particle_3D_rotation;

	// Token: 0x040000D9 RID: 217
	public float particle_distortion;

	// Token: 0x040000DA RID: 218
	public float converge_radius_spread_time;

	// Token: 0x040000DB RID: 219
	public float converge_radius_spread_speed;

	// Token: 0x040000DC RID: 220
	public float converge_radius_max;

	// Token: 0x040000DD RID: 221
	public float anlgle_speed;

	// Token: 0x040000DE RID: 222
	public float anlgle_accel;

	// Token: 0x040000DF RID: 223
	public float anlgle_max_speed;

	// Token: 0x040000E0 RID: 224
	public float circle_decrease_speed;

	// Token: 0x040000E1 RID: 225
	public int add_particle_total;

	// Token: 0x040000E2 RID: 226
	public float add_particle_time;

	// Token: 0x040000E3 RID: 227
	public float gravity_start;

	// Token: 0x040000E4 RID: 228
	public float gravity_accel;

	// Token: 0x040000E5 RID: 229
	public float gravity_float;

	// Token: 0x040000E6 RID: 230
	private int particle_num;

	// Token: 0x040000E7 RID: 231
	private int numParticlesAlive;

	// Token: 0x040000E8 RID: 232
	private float base_particle_angle;

	// Token: 0x040000E9 RID: 233
	private float particule_appearance_time;

	// Token: 0x040000EA RID: 234
	private float converge_radius;

	// Token: 0x040000EB RID: 235
	private float base_particle_twist_angle;

	// Token: 0x040000EC RID: 236
	private float time;

	// Token: 0x040000ED RID: 237
	private int cut_num;

	// Token: 0x040000EE RID: 238
	private int cut_total;

	// Token: 0x040000EF RID: 239
	private List<FlowerTornado.ParticleInfo> particleInfos;

	// Token: 0x02000027 RID: 39
	private class ParticleInfo
	{
		// Token: 0x040000F0 RID: 240
		public float particle_life;

		// Token: 0x040000F1 RID: 241
		public float particle_start_pos;

		// Token: 0x040000F2 RID: 242
		public float particle_revo_range;

		// Token: 0x040000F3 RID: 243
		public float particle_rotation_x;

		// Token: 0x040000F4 RID: 244
		public float particle_rotation_y;

		// Token: 0x040000F5 RID: 245
		public float particle_rotation_z;

		// Token: 0x040000F6 RID: 246
		public float particle_twist_rotation;

		// Token: 0x040000F7 RID: 247
		public float particle_dist;

		// Token: 0x040000F8 RID: 248
		public float particle_gravity;

		// Token: 0x040000F9 RID: 249
		public float particle_range;

		// Token: 0x040000FA RID: 250
		public float particle_spread_speed;

		// Token: 0x040000FB RID: 251
		public float particle_spread_accel;
	}
}
