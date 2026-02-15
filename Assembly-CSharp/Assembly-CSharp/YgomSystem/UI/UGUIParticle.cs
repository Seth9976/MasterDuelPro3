using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000646 RID: 1606
	[ExecuteAlways]
	public class UGUIParticle : MaskableGraphic
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x0000216A File Offset: 0x0000036A
		private Camera renderCamera
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x0000216A File Offset: 0x0000036A
		public ParticleSystemRenderer particleSystemRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x0000216A File Offset: 0x0000036A
		public override Texture mainTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003234 RID: 12852 RVA: 0x0000216D File Offset: 0x0000036D
		public override Material material
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPopulateMesh(VertexHelper vh)
		{
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x04002ED0 RID: 11984
		private Camera m_RenderCameraCache;

		// Token: 0x04002ED1 RID: 11985
		private ParticleSystemRenderer m_ParticleSystemRendererCache;

		// Token: 0x04002ED2 RID: 11986
		private Mesh m_Mesh;

		// Token: 0x04002ED3 RID: 11987
		private List<Vector3> m_Vertices;

		// Token: 0x04002ED4 RID: 11988
		private List<Vector3> m_UVs;

		// Token: 0x04002ED5 RID: 11989
		private List<int> m_Triangles;

		// Token: 0x04002ED6 RID: 11990
		private List<Color32> m_Colors32;
	}
}
