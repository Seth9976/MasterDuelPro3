using System;
using System.Linq;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200018F RID: 399
	public static class ShaderUtils
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x00028308 File Offset: 0x00026508
		public static string GetShaderPath(ShaderPathID id)
		{
			int index = (int)id;
			int arrayLength = ShaderUtils.s_ShaderPaths.Length;
			if (arrayLength > 0 && index >= 0 && index < arrayLength)
			{
				return ShaderUtils.s_ShaderPaths[index];
			}
			Debug.LogError(string.Concat(new string[]
			{
				"Trying to access universal shader path out of bounds: (",
				id.ToString(),
				": ",
				index.ToString(),
				")"
			}));
			return "";
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002837C File Offset: 0x0002657C
		public static ShaderPathID GetEnumFromPath(string path)
		{
			return (ShaderPathID)Array.FindIndex<string>(ShaderUtils.s_ShaderPaths, (string m) => m == path);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000283AC File Offset: 0x000265AC
		public static bool IsLWShader(Shader shader)
		{
			return ShaderUtils.s_ShaderPaths.Contains(shader.name);
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000283BE File Offset: 0x000265BE
		internal static float PersistentDeltaTime
		{
			get
			{
				return Time.deltaTime;
			}
		}

		// Token: 0x040008E7 RID: 2279
		private static readonly string[] s_ShaderPaths = new string[]
		{
			"Universal Render Pipeline/Lit", "Universal Render Pipeline/Simple Lit", "Universal Render Pipeline/Unlit", "Universal Render Pipeline/Terrain/Lit", "Universal Render Pipeline/Particles/Lit", "Universal Render Pipeline/Particles/Simple Lit", "Universal Render Pipeline/Particles/Unlit", "Universal Render Pipeline/Baked Lit", "Universal Render Pipeline/Nature/SpeedTree7", "Universal Render Pipeline/Nature/SpeedTree7 Billboard",
			"Universal Render Pipeline/Nature/SpeedTree8_PBRLit", "Universal Render Pipeline/Complex Lit"
		};
	}
}
