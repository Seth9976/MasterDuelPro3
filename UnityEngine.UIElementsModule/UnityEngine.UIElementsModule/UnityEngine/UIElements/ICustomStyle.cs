using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CA RID: 714
	public interface ICustomStyle
	{
		// Token: 0x060013D0 RID: 5072
		bool TryGetValue(CustomStyleProperty<float> property, out float value);

		// Token: 0x060013D1 RID: 5073
		bool TryGetValue(CustomStyleProperty<int> property, out int value);

		// Token: 0x060013D2 RID: 5074
		bool TryGetValue(CustomStyleProperty<Color> property, out Color value);

		// Token: 0x060013D3 RID: 5075
		bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value);

		// Token: 0x060013D4 RID: 5076
		bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value);

		// Token: 0x060013D5 RID: 5077
		bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value);

		// Token: 0x060013D6 RID: 5078
		bool TryGetValue(CustomStyleProperty<string> property, out string value);
	}
}
