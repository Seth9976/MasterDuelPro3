using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000197 RID: 407
	[AttributeUsage(AttributeTargets.Class)]
	public class SupportedOnRendererAttribute : Attribute
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0002985A File Offset: 0x00027A5A
		public Type[] rendererTypes { get; }

		// Token: 0x060008B1 RID: 2225 RVA: 0x00029862 File Offset: 0x00027A62
		public SupportedOnRendererAttribute(Type renderer)
			: this(new Type[] { renderer })
		{
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00029874 File Offset: 0x00027A74
		public SupportedOnRendererAttribute(params Type[] renderers)
		{
			if (renderers == null)
			{
				Debug.LogError("The SupportedOnRendererAttribute parameters cannot be null.");
				return;
			}
			foreach (Type r in renderers)
			{
				if (r == null || !typeof(ScriptableRendererData).IsAssignableFrom(r))
				{
					Debug.LogError("The SupportedOnRendererAttribute Attribute targets an invalid ScriptableRendererData. One of the types cannot be assigned from ScriptableRendererData");
					return;
				}
			}
			this.rendererTypes = renderers;
		}
	}
}
