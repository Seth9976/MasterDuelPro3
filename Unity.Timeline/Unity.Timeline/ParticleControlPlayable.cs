using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000057 RID: 87
	public class ParticleControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00009B48 File Offset: 0x00007D48
		public static ScriptPlayable<ParticleControlPlayable> Create(PlayableGraph graph, ParticleSystem component, uint randomSeed)
		{
			if (component == null)
			{
				return ScriptPlayable<ParticleControlPlayable>.Null;
			}
			ScriptPlayable<ParticleControlPlayable> handle = ScriptPlayable<ParticleControlPlayable>.Create(graph, 0);
			handle.GetBehaviour().Initialize(component, randomSeed);
			return handle;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00009B7B File Offset: 0x00007D7B
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00009B83 File Offset: 0x00007D83
		public ParticleSystem particleSystem { get; private set; }

		// Token: 0x060002E1 RID: 737 RVA: 0x00009B8C File Offset: 0x00007D8C
		public void Initialize(ParticleSystem ps, uint randomSeed)
		{
			this.m_RandomSeed = Math.Max(1U, randomSeed);
			this.particleSystem = ps;
			ParticleControlPlayable.SetRandomSeed(this.particleSystem, this.m_RandomSeed);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009BB4 File Offset: 0x00007DB4
		private static void SetRandomSeed(ParticleSystem particleSystem, uint randomSeed)
		{
			if (particleSystem == null)
			{
				return;
			}
			particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			if (particleSystem.useAutoRandomSeed)
			{
				particleSystem.useAutoRandomSeed = false;
				particleSystem.randomSeed = randomSeed;
			}
			for (int i = 0; i < particleSystem.subEmitters.subEmittersCount; i++)
			{
				ParticleControlPlayable.SetRandomSeed(particleSystem.subEmitters.GetSubEmitterSystem(i), randomSeed += 1U);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009C1C File Offset: 0x00007E1C
		public override void PrepareFrame(Playable playable, FrameData data)
		{
			if (this.particleSystem == null || !this.particleSystem.gameObject.activeInHierarchy)
			{
				this.m_LastPlayableTime = float.MaxValue;
				return;
			}
			float time = (float)playable.GetTime<Playable>();
			float particleTime = this.particleSystem.time;
			if (this.m_LastPlayableTime > time || !Mathf.Approximately(particleTime, this.m_LastParticleTime))
			{
				this.Simulate(time, true);
			}
			else if (this.m_LastPlayableTime < time)
			{
				this.Simulate(time - this.m_LastPlayableTime, false);
			}
			this.m_LastPlayableTime = time;
			this.m_LastParticleTime = this.particleSystem.time;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009CBA File Offset: 0x00007EBA
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.m_LastPlayableTime = float.MaxValue;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009CBA File Offset: 0x00007EBA
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			this.m_LastPlayableTime = float.MaxValue;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00009CC8 File Offset: 0x00007EC8
		private void Simulate(float time, bool restart)
		{
			float maxTime = Time.maximumDeltaTime;
			if (restart)
			{
				this.particleSystem.Simulate(0f, false, true, false);
			}
			while (time > maxTime)
			{
				this.particleSystem.Simulate(maxTime, false, false, false);
				time -= maxTime;
			}
			if (time > 0f)
			{
				this.particleSystem.Simulate(time, false, false, false);
			}
		}

		// Token: 0x0400014A RID: 330
		private const float kUnsetTime = 3.4028235E+38f;

		// Token: 0x0400014B RID: 331
		private float m_LastPlayableTime = float.MaxValue;

		// Token: 0x0400014C RID: 332
		private float m_LastParticleTime = float.MaxValue;

		// Token: 0x0400014D RID: 333
		private uint m_RandomSeed = 1U;
	}
}
