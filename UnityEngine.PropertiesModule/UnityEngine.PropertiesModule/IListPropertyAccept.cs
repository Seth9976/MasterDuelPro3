using System;

namespace Unity.Properties
{
	// Token: 0x0200004E RID: 78
	public interface IListPropertyAccept<TList>
	{
		// Token: 0x06000133 RID: 307
		void Accept<TContainer>(IListPropertyVisitor visitor, Property<TContainer, TList> property, ref TContainer container, ref TList list);
	}
}
