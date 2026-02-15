using System;
using System.Collections.Generic;

namespace UnityEngine.Timeline
{
	// Token: 0x02000071 RID: 113
	public interface IPropertyCollector
	{
		// Token: 0x06000335 RID: 821
		void PushActiveGameObject(GameObject gameObject);

		// Token: 0x06000336 RID: 822
		void PopActiveGameObject();

		// Token: 0x06000337 RID: 823
		void AddFromClip(AnimationClip clip);

		// Token: 0x06000338 RID: 824
		void AddFromClips(IEnumerable<AnimationClip> clips);

		// Token: 0x06000339 RID: 825
		void AddFromName<T>(string name) where T : Component;

		// Token: 0x0600033A RID: 826
		void AddFromName(string name);

		// Token: 0x0600033B RID: 827
		void AddFromClip(GameObject obj, AnimationClip clip);

		// Token: 0x0600033C RID: 828
		void AddFromClips(GameObject obj, IEnumerable<AnimationClip> clips);

		// Token: 0x0600033D RID: 829
		void AddFromName<T>(GameObject obj, string name) where T : Component;

		// Token: 0x0600033E RID: 830
		void AddFromName(GameObject obj, string name);

		// Token: 0x0600033F RID: 831
		void AddFromName(Component component, string name);

		// Token: 0x06000340 RID: 832
		void AddFromComponent(GameObject obj, Component component);

		// Token: 0x06000341 RID: 833
		void AddObjectProperties(Object obj, AnimationClip clip);
	}
}
