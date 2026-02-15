using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000301 RID: 769
	public interface IPlayableBehaviour
	{
		// Token: 0x06001540 RID: 5440
		[RequiredByNativeCode]
		void OnGraphStart(Playable playable);

		// Token: 0x06001541 RID: 5441
		[RequiredByNativeCode]
		void OnGraphStop(Playable playable);

		// Token: 0x06001542 RID: 5442
		[RequiredByNativeCode]
		void OnPlayableCreate(Playable playable);

		// Token: 0x06001543 RID: 5443
		[RequiredByNativeCode]
		void OnPlayableDestroy(Playable playable);

		// Token: 0x06001544 RID: 5444
		[RequiredByNativeCode]
		void OnBehaviourPlay(Playable playable, FrameData info);

		// Token: 0x06001545 RID: 5445
		[RequiredByNativeCode]
		void OnBehaviourPause(Playable playable, FrameData info);

		// Token: 0x06001546 RID: 5446
		[RequiredByNativeCode]
		void PrepareFrame(Playable playable, FrameData info);

		// Token: 0x06001547 RID: 5447
		[RequiredByNativeCode]
		void ProcessFrame(Playable playable, FrameData info, object playerData);
	}
}
