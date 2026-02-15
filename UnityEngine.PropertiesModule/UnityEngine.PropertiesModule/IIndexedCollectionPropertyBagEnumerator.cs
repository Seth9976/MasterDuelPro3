using System;

namespace Unity.Properties
{
	// Token: 0x0200002B RID: 43
	internal interface IIndexedCollectionPropertyBagEnumerator<TContainer>
	{
		// Token: 0x060000A3 RID: 163
		int GetCount(ref TContainer container);

		// Token: 0x060000A4 RID: 164
		IProperty<TContainer> GetSharedProperty();

		// Token: 0x060000A5 RID: 165
		IndexedCollectionSharedPropertyState GetSharedPropertyState();

		// Token: 0x060000A6 RID: 166
		void SetSharedPropertyState(IndexedCollectionSharedPropertyState state);
	}
}
