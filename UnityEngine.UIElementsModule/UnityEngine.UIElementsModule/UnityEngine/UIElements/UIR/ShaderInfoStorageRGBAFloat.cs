using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000555 RID: 1365
	internal class ShaderInfoStorageRGBAFloat : ShaderInfoStorage<Color>
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x00095AA7 File Offset: 0x00093CA7
		public ShaderInfoStorageRGBAFloat(int initialSize = 64, int maxSize = 4096)
			: base(TextureFormat.RGBAFloat, ShaderInfoStorageRGBAFloat.s_Convert, initialSize, maxSize)
		{
		}

		// Token: 0x040012DD RID: 4829
		private static readonly Func<Color, Color> s_Convert = (Color c) => c;
	}
}
