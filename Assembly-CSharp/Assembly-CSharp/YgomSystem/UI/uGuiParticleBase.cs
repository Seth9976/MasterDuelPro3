using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x0200065A RID: 1626
	public class uGuiParticleBase : MaskableGraphic
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x0000216A File Offset: 0x0000036A
		public override Texture mainTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Update()
		{
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddParticle(float lifetime, float delaytime, Vector2 pos, Vector2 velocity, float rot, float scale, Color color, int sid, Vector2 endVelocity, float endRot, float endScale, Color endColor, int endSid)
		{
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPopulateMesh(VertexHelper vh)
		{
		}

		// Token: 0x04002F4F RID: 12111
		public Texture texture;

		// Token: 0x04002F50 RID: 12112
		public int divide;

		// Token: 0x04002F51 RID: 12113
		public AnimationCurve colorCurve;

		// Token: 0x04002F52 RID: 12114
		public AnimationCurve velocityCurve;

		// Token: 0x04002F53 RID: 12115
		public AnimationCurve scaleCurve;

		// Token: 0x04002F54 RID: 12116
		private List<uGuiParticleBase.prim> particles;

		// Token: 0x04002F55 RID: 12117
		private Vector2[] uvs;

		// Token: 0x0200065B RID: 1627
		private struct param
		{
			// Token: 0x04002F56 RID: 12118
			public Vector2 vel;

			// Token: 0x04002F57 RID: 12119
			public float rot;

			// Token: 0x04002F58 RID: 12120
			public float scale;

			// Token: 0x04002F59 RID: 12121
			public Color col;

			// Token: 0x04002F5A RID: 12122
			public int sid;
		}

		// Token: 0x0200065C RID: 1628
		private class prim
		{
			// Token: 0x1700031B RID: 795
			// (get) Token: 0x06003305 RID: 13061 RVA: 0x000F29D8 File Offset: 0x000F0BD8
			public Color color
			{
				get
				{
					return default(Color);
				}
			}

			// Token: 0x1700031C RID: 796
			// (get) Token: 0x06003306 RID: 13062 RVA: 0x000F29F0 File Offset: 0x000F0BF0
			public Vector2 pos
			{
				get
				{
					return default(Vector2);
				}
			}

			// Token: 0x1700031D RID: 797
			// (get) Token: 0x06003307 RID: 13063 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float scale
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x1700031E RID: 798
			// (get) Token: 0x06003308 RID: 13064 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float rot
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x1700031F RID: 799
			// (get) Token: 0x06003309 RID: 13065 RVA: 0x000029CC File Offset: 0x00000BCC
			public int sid
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600330A RID: 13066 RVA: 0x00002739 File Offset: 0x00000939
			public prim(uGuiParticleBase pbase_, float lifetime_, float delaytime_, Vector2 pos_, Vector2 vel, float rot, float scale, Color color, int sid, Vector2 dstVel, float dstRot, float dstScale, Color dstColor, int dstSid)
			{
			}

			// Token: 0x0600330B RID: 13067 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool update(float tm)
			{
				return false;
			}

			// Token: 0x0600330C RID: 13068 RVA: 0x0000216A File Offset: 0x0000036A
			public static Vector2[] CreateUvs(int divide, Texture tex)
			{
				return null;
			}

			// Token: 0x0600330D RID: 13069 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool AddVert(VertexHelper vh, Vector2[] uvs)
			{
				return false;
			}

			// Token: 0x04002F5B RID: 12123
			public uGuiParticleBase pbase;

			// Token: 0x04002F5C RID: 12124
			public float delay;

			// Token: 0x04002F5D RID: 12125
			public float time;

			// Token: 0x04002F5E RID: 12126
			public float lifetime;

			// Token: 0x04002F5F RID: 12127
			public uGuiParticleBase.param src;

			// Token: 0x04002F60 RID: 12128
			public uGuiParticleBase.param dst;

			// Token: 0x04002F61 RID: 12129
			private float pt;

			// Token: 0x04002F62 RID: 12130
			private Vector2 ps;
		}
	}
}
