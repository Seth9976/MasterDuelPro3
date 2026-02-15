using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200000A RID: 10
	[ExecuteInEditMode]
	internal class DisallowSmallMeshCulling : MonoBehaviour
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002336 File Offset: 0x00000536
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000233E File Offset: 0x0000053E
		public bool applyToChildrenRecursively
		{
			get
			{
				return this.m_applyToChildrenRecursively;
			}
			set
			{
				this.m_applyToChildrenRecursively = value;
				this.OnDisable();
				this.OnEnable();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002353 File Offset: 0x00000553
		private void OnEnable()
		{
			this.m_AppliedRecursively = this.applyToChildrenRecursively;
			if (this.applyToChildrenRecursively)
			{
				DisallowSmallMeshCulling.AllowSmallMeshCullingRecursively(base.transform, false);
				return;
			}
			DisallowSmallMeshCulling.AllowSmallMeshCulling(base.transform, false);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002382 File Offset: 0x00000582
		private void OnDisable()
		{
			if (this.m_AppliedRecursively)
			{
				DisallowSmallMeshCulling.AllowSmallMeshCullingRecursively(base.transform, true);
				return;
			}
			DisallowSmallMeshCulling.AllowSmallMeshCulling(base.transform, true);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023A8 File Offset: 0x000005A8
		private static void AllowSmallMeshCulling(Transform transform, bool allow)
		{
			MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
			if (renderer)
			{
				renderer.smallMeshCulling = allow;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023CC File Offset: 0x000005CC
		private static void AllowSmallMeshCullingRecursively(Transform transform, bool allow)
		{
			DisallowSmallMeshCulling.AllowSmallMeshCulling(transform, allow);
			foreach (object obj in transform)
			{
				Transform child = (Transform)obj;
				if (!child.GetComponent<DisallowGPUDrivenRendering>())
				{
					DisallowSmallMeshCulling.AllowSmallMeshCullingRecursively(child, allow);
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002434 File Offset: 0x00000634
		private void OnValidate()
		{
			this.OnDisable();
			this.OnEnable();
		}

		// Token: 0x0400000F RID: 15
		private bool m_AppliedRecursively;

		// Token: 0x04000010 RID: 16
		public bool m_applyToChildrenRecursively;
	}
}
