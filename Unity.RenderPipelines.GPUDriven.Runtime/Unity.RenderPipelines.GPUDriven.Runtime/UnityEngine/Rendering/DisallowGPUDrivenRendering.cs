using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000009 RID: 9
	[ExecuteInEditMode]
	internal class DisallowGPUDrivenRendering : MonoBehaviour
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002222 File Offset: 0x00000422
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000222A File Offset: 0x0000042A
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

		// Token: 0x0600000F RID: 15 RVA: 0x0000223F File Offset: 0x0000043F
		private void OnEnable()
		{
			this.m_AppliedRecursively = this.applyToChildrenRecursively;
			if (this.applyToChildrenRecursively)
			{
				DisallowGPUDrivenRendering.AllowGPUDrivenRenderingRecursively(base.transform, false);
				return;
			}
			DisallowGPUDrivenRendering.AllowGPUDrivenRendering(base.transform, false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000226E File Offset: 0x0000046E
		private void OnDisable()
		{
			if (this.m_AppliedRecursively)
			{
				DisallowGPUDrivenRendering.AllowGPUDrivenRenderingRecursively(base.transform, true);
				return;
			}
			DisallowGPUDrivenRendering.AllowGPUDrivenRendering(base.transform, true);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002294 File Offset: 0x00000494
		private static void AllowGPUDrivenRendering(Transform transform, bool allow)
		{
			MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
			if (renderer)
			{
				renderer.allowGPUDrivenRendering = allow;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022B8 File Offset: 0x000004B8
		private static void AllowGPUDrivenRenderingRecursively(Transform transform, bool allow)
		{
			DisallowGPUDrivenRendering.AllowGPUDrivenRendering(transform, allow);
			foreach (object obj in transform)
			{
				Transform child = (Transform)obj;
				if (!child.GetComponent<DisallowGPUDrivenRendering>())
				{
					DisallowGPUDrivenRendering.AllowGPUDrivenRenderingRecursively(child, allow);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002320 File Offset: 0x00000520
		private void OnValidate()
		{
			this.OnDisable();
			this.OnEnable();
		}

		// Token: 0x0400000D RID: 13
		private bool m_AppliedRecursively;

		// Token: 0x0400000E RID: 14
		[FormerlySerializedAs("applyToChildrenRecursively")]
		public bool m_applyToChildrenRecursively;
	}
}
