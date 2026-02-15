using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000072 RID: 114
	public interface IPropertyPreview
	{
		// Token: 0x06000342 RID: 834
		void GatherProperties(PlayableDirector director, IPropertyCollector driver);
	}
}
