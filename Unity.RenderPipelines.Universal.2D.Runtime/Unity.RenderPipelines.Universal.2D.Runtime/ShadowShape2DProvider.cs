using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007E RID: 126
	public abstract class ShadowShape2DProvider
	{
		// Token: 0x0600030F RID: 783 RVA: 0x000172F7 File Offset: 0x000154F7
		public virtual string ProviderName(string componentName)
		{
			return componentName;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000020A7 File Offset: 0x000002A7
		public virtual int Priority()
		{
			return 0;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public virtual void Enabled(Component sourceComponent)
		{
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public virtual void Disabled(Component sourceComponent)
		{
		}

		// Token: 0x06000313 RID: 787
		public abstract bool IsShapeSource(Component sourceComponent);

		// Token: 0x06000314 RID: 788 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public virtual void OnPersistantDataCreated(Component sourceComponent, ShadowShape2D persistantShadowShape)
		{
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public virtual void OnBeforeRender(Component sourceComponent, Bounds worldCullingBounds, ShadowShape2D persistantShadowShape)
		{
		}
	}
}
