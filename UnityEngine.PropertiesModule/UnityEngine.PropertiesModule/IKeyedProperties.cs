using System;

namespace Unity.Properties
{
	// Token: 0x02000031 RID: 49
	public interface IKeyedProperties<TContainer, TKey>
	{
		// Token: 0x060000BC RID: 188
		bool TryGetProperty(ref TContainer container, TKey key, out IProperty<TContainer> property);
	}
}
