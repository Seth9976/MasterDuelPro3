using System;
using DG.Tweening.Core;

namespace DG.Tweening.Plugins.Core
{
	// Token: 0x02000094 RID: 148
	public interface IPlugSetter<T1, out T2, TPlugin, out TPlugOptions>
	{
		// Token: 0x06000371 RID: 881
		DOGetter<T1> Getter();

		// Token: 0x06000372 RID: 882
		DOSetter<T1> Setter();

		// Token: 0x06000373 RID: 883
		T2 EndValue();

		// Token: 0x06000374 RID: 884
		TPlugOptions GetOptions();
	}
}
