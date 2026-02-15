using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000306 RID: 774
	[RequiredByNativeCode]
	[AssetFileNameExtension("playable", new string[] { })]
	[Serializable]
	public abstract class PlayableAsset : ScriptableObject, IPlayableAsset
	{
		// Token: 0x06001553 RID: 5459
		public abstract Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0002D124 File Offset: 0x0002B324
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0002D13C File Offset: 0x0002B33C
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0002D154 File Offset: 0x0002B354
		[RequiredByNativeCode]
		internal unsafe static void Internal_CreatePlayable(PlayableAsset asset, PlayableGraph graph, GameObject go, IntPtr ptr)
		{
			bool flag = asset == null;
			Playable result;
			if (flag)
			{
				result = Playable.Null;
			}
			else
			{
				result = asset.CreatePlayable(graph, go);
			}
			Playable* handle = (Playable*)ptr.ToPointer();
			*handle = result;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0002D190 File Offset: 0x0002B390
		[RequiredByNativeCode]
		internal unsafe static void Internal_GetPlayableAssetDuration(PlayableAsset asset, IntPtr ptrToDouble)
		{
			double d = asset.duration;
			double* ptr = (double*)ptrToDouble.ToPointer();
			*ptr = d;
		}
	}
}
