using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000C9 RID: 201
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void Awake()
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnEnable()
		{
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void Start()
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnDisable()
		{
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001CAB7 File Offset: 0x0001ACB7
		public virtual bool IsActive()
		{
			return base.isActiveAndEnabled;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00002209 File Offset: 0x00000409
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001CABF File Offset: 0x0001ACBF
		public bool IsDestroyed()
		{
			return this == null;
		}
	}
}
