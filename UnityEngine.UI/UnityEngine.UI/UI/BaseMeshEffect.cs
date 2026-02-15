using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000083 RID: 131
	[ExecuteAlways]
	public abstract class BaseMeshEffect : UIBehaviour, IMeshModifier
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00016F31 File Offset: 0x00015131
		protected Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00016F53 File Offset: 0x00015153
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00016F74 File Offset: 0x00015174
		protected override void OnDisable()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDisable();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00016F95 File Offset: 0x00015195
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00016FB8 File Offset: 0x000151B8
		public virtual void ModifyMesh(Mesh mesh)
		{
			using (VertexHelper vh = new VertexHelper(mesh))
			{
				this.ModifyMesh(vh);
				vh.FillMesh(mesh);
			}
		}

		// Token: 0x06000531 RID: 1329
		public abstract void ModifyMesh(VertexHelper vh);

		// Token: 0x0400026E RID: 622
		[NonSerialized]
		private Graphic m_Graphic;
	}
}
