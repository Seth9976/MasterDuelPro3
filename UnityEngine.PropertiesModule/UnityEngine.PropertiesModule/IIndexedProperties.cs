using System;

namespace Unity.Properties
{
	// Token: 0x0200002F RID: 47
	public interface IIndexedProperties<TContainer>
	{
		// Token: 0x060000BA RID: 186
		bool TryGetProperty(ref TContainer container, int index, out IProperty<TContainer> property);
	}
}
