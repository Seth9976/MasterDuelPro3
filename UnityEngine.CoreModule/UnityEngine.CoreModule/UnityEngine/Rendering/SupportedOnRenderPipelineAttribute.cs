using System;
using System.Reflection;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x0200036B RID: 875
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class SupportedOnRenderPipelineAttribute : Attribute
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0003461E File Offset: 0x0003281E
		public Type[] renderPipelineTypes { get; }

		// Token: 0x060018AB RID: 6315 RVA: 0x00034626 File Offset: 0x00032826
		public SupportedOnRenderPipelineAttribute(Type renderPipeline)
			: this(new Type[] { renderPipeline })
		{
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0003463C File Offset: 0x0003283C
		public SupportedOnRenderPipelineAttribute(params Type[] renderPipeline)
		{
			bool flag = renderPipeline == null;
			if (flag)
			{
				Debug.LogError("The SupportedOnRenderPipelineAttribute parameters cannot be null.");
			}
			else
			{
				foreach (Type r in renderPipeline)
				{
					bool flag2 = r != null && typeof(RenderPipelineAsset).IsAssignableFrom(r);
					if (!flag2)
					{
						Debug.LogError("The SupportedOnRenderPipelineAttribute Attribute targets an invalid RenderPipelineAsset. One of the types cannot be assigned from RenderPipelineAsset: [" + renderPipeline.SerializedView((Type t) => t.Name) + "].");
						return;
					}
				}
				this.renderPipelineTypes = ((renderPipeline.Length == 0) ? SupportedOnRenderPipelineAttribute.k_DefaultRenderPipelineAsset.Value : renderPipeline);
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000346F8 File Offset: 0x000328F8
		public SupportedOnRenderPipelineAttribute.SupportedMode GetSupportedMode(Type renderPipelineAssetType)
		{
			return SupportedOnRenderPipelineAttribute.GetSupportedMode(this.renderPipelineTypes, renderPipelineAssetType);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00034708 File Offset: 0x00032908
		internal static SupportedOnRenderPipelineAttribute.SupportedMode GetSupportedMode(Type[] renderPipelineTypes, Type renderPipelineAssetType)
		{
			bool flag = renderPipelineTypes == null;
			if (flag)
			{
				throw new ArgumentNullException("Parameter renderPipelineTypes cannot be null.");
			}
			bool flag2 = renderPipelineAssetType == null;
			SupportedOnRenderPipelineAttribute.SupportedMode supportedMode;
			if (flag2)
			{
				supportedMode = SupportedOnRenderPipelineAttribute.SupportedMode.Unsupported;
			}
			else
			{
				for (int i = 0; i < renderPipelineTypes.Length; i++)
				{
					bool flag3 = renderPipelineTypes[i] == renderPipelineAssetType;
					if (flag3)
					{
						return SupportedOnRenderPipelineAttribute.SupportedMode.Supported;
					}
				}
				for (int j = 0; j < renderPipelineTypes.Length; j++)
				{
					bool flag4 = renderPipelineTypes[j].IsAssignableFrom(renderPipelineAssetType);
					if (flag4)
					{
						return SupportedOnRenderPipelineAttribute.SupportedMode.SupportedByBaseClass;
					}
				}
				supportedMode = SupportedOnRenderPipelineAttribute.SupportedMode.Unsupported;
			}
			return supportedMode;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00034794 File Offset: 0x00032994
		public static bool IsTypeSupportedOnRenderPipeline(Type type, Type renderPipelineAssetType)
		{
			SupportedOnRenderPipelineAttribute supportedOnAttribute = type.GetCustomAttribute<SupportedOnRenderPipelineAttribute>();
			return supportedOnAttribute == null || supportedOnAttribute.GetSupportedMode(renderPipelineAssetType) > SupportedOnRenderPipelineAttribute.SupportedMode.Unsupported;
		}

		// Token: 0x04000A34 RID: 2612
		private static readonly Lazy<Type[]> k_DefaultRenderPipelineAsset = new Lazy<Type[]>(() => new Type[] { typeof(RenderPipelineAsset) });

		// Token: 0x0200036C RID: 876
		public enum SupportedMode
		{
			// Token: 0x04000A37 RID: 2615
			Unsupported,
			// Token: 0x04000A38 RID: 2616
			Supported,
			// Token: 0x04000A39 RID: 2617
			SupportedByBaseClass
		}
	}
}
