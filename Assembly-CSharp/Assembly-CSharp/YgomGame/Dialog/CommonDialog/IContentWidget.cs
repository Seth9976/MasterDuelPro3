using System;
using UnityEngine;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F86 RID: 3974
	public interface IContentWidget
	{
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06007497 RID: 29847
		GameObject gameObject { get; }

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06007498 RID: 29848
		Transform transform { get; }

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06007499 RID: 29849
		CommonDialogContentContainerWidget parentWidget { get; }

		// Token: 0x0600749A RID: 29850
		IContentWidget DuplicateInstantiate();

		// Token: 0x0600749B RID: 29851
		void Binding(IEntryData entryData);

		// Token: 0x0600749C RID: 29852
		void Initialize(CommonDialogContentContainerWidget parentWidget);
	}
}
