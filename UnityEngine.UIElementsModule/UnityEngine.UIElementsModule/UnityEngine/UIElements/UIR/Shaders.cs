using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000557 RID: 1367
	internal static class Shaders
	{
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x00095ADD File Offset: 0x00093CDD
		public static Material runtimeMaterial
		{
			get
			{
				return Shaders.GetOrCreateMaterial(ref Shaders.s_RuntimeMaterial, Shaders.k_Runtime);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00095AEE File Offset: 0x00093CEE
		public static Material runtimeWorldMaterial
		{
			get
			{
				return Shaders.GetOrCreateMaterial(ref Shaders.s_RuntimeWorldMaterial, Shaders.k_RuntimeWorld);
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x00095AFF File Offset: 0x00093CFF
		public static Material editorMaterial
		{
			get
			{
				return Shaders.GetOrCreateMaterial(ref Shaders.s_EditorMaterial, Shaders.k_Editor);
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00095B10 File Offset: 0x00093D10
		private static Material GetOrCreateMaterial(ref Material material, string shaderName)
		{
			bool flag = material == null;
			if (flag)
			{
				Shader shader = Shader.Find(shaderName);
				bool flag2 = shader == null;
				if (flag2)
				{
					Debug.LogError("Could not find shader '" + shaderName + "'");
					return null;
				}
				material = new Material(shader);
				material.hideFlags = HideFlags.DontSave;
			}
			return material;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00095B71 File Offset: 0x00093D71
		public static void Acquire()
		{
			Shaders.s_RefCount++;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00095B80 File Offset: 0x00093D80
		public static void Release()
		{
			Shaders.s_RefCount--;
			Debug.Assert(Shaders.s_RefCount >= 0, "UIR materials acquire/release don't match.");
			bool flag = Shaders.s_RefCount < 1;
			if (flag)
			{
				Shaders.s_RefCount = 0;
				UIRUtility.Destroy(Shaders.s_RuntimeMaterial);
				UIRUtility.Destroy(Shaders.s_RuntimeWorldMaterial);
				UIRUtility.Destroy(Shaders.s_EditorMaterial);
				Shaders.s_RuntimeMaterial = null;
				Shaders.s_RuntimeWorldMaterial = null;
				Shaders.s_EditorMaterial = null;
			}
		}

		// Token: 0x040012DF RID: 4831
		public static readonly string k_AtlasBlit = "Hidden/Internal-UIRAtlasBlitCopy";

		// Token: 0x040012E0 RID: 4832
		public static readonly string k_Editor = "Hidden/UIElements/EditorUIE";

		// Token: 0x040012E1 RID: 4833
		public static readonly string k_Runtime = "Hidden/Internal-UIRDefault";

		// Token: 0x040012E2 RID: 4834
		public static readonly string k_RuntimeWorld = "Hidden/Internal-UIRDefaultWorld";

		// Token: 0x040012E3 RID: 4835
		public static readonly string k_ColorConversionBlit = "Hidden/Internal-UIE-ColorConversionBlit";

		// Token: 0x040012E4 RID: 4836
		public static readonly string k_ForceGammaKeyword = "UIE_FORCE_GAMMA";

		// Token: 0x040012E5 RID: 4837
		private static Material s_RuntimeMaterial;

		// Token: 0x040012E6 RID: 4838
		private static Material s_RuntimeWorldMaterial;

		// Token: 0x040012E7 RID: 4839
		private static Material s_EditorMaterial;

		// Token: 0x040012E8 RID: 4840
		private static int s_RefCount;
	}
}
