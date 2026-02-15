using System;

namespace Unity.Properties
{
	// Token: 0x02000030 RID: 48
	public interface INamedProperties<TContainer>
	{
		// Token: 0x060000BB RID: 187
		bool TryGetProperty(ref TContainer container, string name, out IProperty<TContainer> property);
	}
}
