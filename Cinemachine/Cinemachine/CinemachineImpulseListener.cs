using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000BA RID: 186
	[SaveDuringPlay]
	[AddComponentMenu("")]
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[ExecuteAlways]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineImpulseListener.html")]
	public class CinemachineImpulseListener : CinemachineExtension
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x00017EC4 File Offset: 0x000160C4
		private void Reset()
		{
			this.m_ApplyAfter = CinemachineCore.Stage.Noise;
			this.m_ChannelMask = 1;
			this.m_Gain = 1f;
			this.m_Use2DDistance = false;
			this.m_UseCameraSpace = true;
			this.m_ReactionSettings = new CinemachineImpulseListener.ImpulseReaction
			{
				m_AmplitudeGain = 1f,
				m_FrequencyGain = 1f,
				m_Duration = 1f
			};
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00017F2C File Offset: 0x0001612C
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage == this.m_ApplyAfter && deltaTime >= 0f)
			{
				Vector3 impulsePos;
				Quaternion impulseRot;
				bool impulseAt = CinemachineImpulseManager.Instance.GetImpulseAt(state.FinalPosition, this.m_Use2DDistance, this.m_ChannelMask, out impulsePos, out impulseRot);
				Vector3 reactionPos;
				Quaternion reactionRot;
				bool haveReaction = this.m_ReactionSettings.GetReaction(deltaTime, impulsePos, out reactionPos, out reactionRot);
				if (impulseAt)
				{
					impulseRot = Quaternion.SlerpUnclamped(Quaternion.identity, impulseRot, this.m_Gain);
					impulsePos *= this.m_Gain;
				}
				if (haveReaction)
				{
					impulsePos += reactionPos;
					impulseRot *= reactionRot;
				}
				if (impulseAt || haveReaction)
				{
					if (this.m_UseCameraSpace)
					{
						impulsePos = state.RawOrientation * impulsePos;
					}
					state.PositionCorrection += impulsePos;
					state.OrientationCorrection *= impulseRot;
				}
			}
		}

		// Token: 0x040003D2 RID: 978
		[Tooltip("When to apply the impulse reaction.  Default is after the Noise stage.  Modify this if necessary to influence the ordering of extension effects")]
		public CinemachineCore.Stage m_ApplyAfter = CinemachineCore.Stage.Aim;

		// Token: 0x040003D3 RID: 979
		[Tooltip("Impulse events on channels not included in the mask will be ignored.")]
		[CinemachineImpulseChannelProperty]
		public int m_ChannelMask;

		// Token: 0x040003D4 RID: 980
		[Tooltip("Gain to apply to the Impulse signal.  1 is normal strength.  Setting this to 0 completely mutes the signal.")]
		public float m_Gain;

		// Token: 0x040003D5 RID: 981
		[Tooltip("Enable this to perform distance calculation in 2D (ignore Z)")]
		public bool m_Use2DDistance;

		// Token: 0x040003D6 RID: 982
		[Tooltip("Enable this to process all impulse signals in camera space")]
		public bool m_UseCameraSpace;

		// Token: 0x040003D7 RID: 983
		[Tooltip("This controls the secondary reaction of the listener to the incoming impulse.  The impulse might be for example a sharp shock, and the secondary reaction could be a vibration whose amplitude and duration is controlled by the size of the original impulse.  This allows different listeners to respond in different ways to the same impulse signal.")]
		public CinemachineImpulseListener.ImpulseReaction m_ReactionSettings;

		// Token: 0x020000BB RID: 187
		[Serializable]
		public struct ImpulseReaction
		{
			// Token: 0x06000420 RID: 1056 RVA: 0x0001800B File Offset: 0x0001620B
			public void ReSeed()
			{
				this.m_NoiseOffsets = new Vector3(global::UnityEngine.Random.Range(-1000f, 1000f), global::UnityEngine.Random.Range(-1000f, 1000f), global::UnityEngine.Random.Range(-1000f, 1000f));
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x00018048 File Offset: 0x00016248
			public bool GetReaction(float deltaTime, Vector3 impulsePos, out Vector3 pos, out Quaternion rot)
			{
				if (!this.m_Initialized)
				{
					this.m_Initialized = true;
					this.m_CurrentAmount = 0f;
					this.m_CurrentDamping = 0f;
					this.m_CurrentTime = CinemachineCore.CurrentTime * this.m_FrequencyGain;
					if (this.m_NoiseOffsets == Vector3.zero)
					{
						this.ReSeed();
					}
				}
				pos = Vector3.zero;
				rot = Quaternion.identity;
				float sqrMag = impulsePos.sqrMagnitude;
				if (this.m_SecondaryNoise == null || (sqrMag < 0.001f && this.m_CurrentAmount < 0.0001f))
				{
					return false;
				}
				if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Playback && TargetPositionCache.HasCurrentTime)
				{
					this.m_CurrentTime = TargetPositionCache.CurrentTime * this.m_FrequencyGain;
				}
				else
				{
					this.m_CurrentTime += deltaTime * this.m_FrequencyGain;
				}
				this.m_CurrentAmount = Mathf.Max(this.m_CurrentAmount, Mathf.Sqrt(sqrMag));
				this.m_CurrentDamping = Mathf.Max(this.m_CurrentDamping, Mathf.Max(1f, Mathf.Sqrt(this.m_CurrentAmount)) * this.m_Duration);
				float gain = this.m_CurrentAmount * this.m_AmplitudeGain;
				pos = NoiseSettings.GetCombinedFilterResults(this.m_SecondaryNoise.PositionNoise, this.m_CurrentTime, this.m_NoiseOffsets) * gain;
				rot = Quaternion.Euler(NoiseSettings.GetCombinedFilterResults(this.m_SecondaryNoise.OrientationNoise, this.m_CurrentTime, this.m_NoiseOffsets) * gain);
				this.m_CurrentAmount -= Damper.Damp(this.m_CurrentAmount, this.m_CurrentDamping, deltaTime);
				this.m_CurrentDamping -= Damper.Damp(this.m_CurrentDamping, this.m_CurrentDamping, deltaTime);
				return true;
			}

			// Token: 0x040003D8 RID: 984
			[Tooltip("Secondary shake that will be triggered by the primary impulse.")]
			[NoiseSettingsProperty]
			public NoiseSettings m_SecondaryNoise;

			// Token: 0x040003D9 RID: 985
			[Tooltip("Gain to apply to the amplitudes defined in the signal source.  1 is normal.  Setting this to 0 completely mutes the signal.")]
			public float m_AmplitudeGain;

			// Token: 0x040003DA RID: 986
			[Tooltip("Scale factor to apply to the time axis.  1 is normal.  Larger magnitudes will make the signal progress more rapidly.")]
			public float m_FrequencyGain;

			// Token: 0x040003DB RID: 987
			[Tooltip("How long the secondary reaction lasts.")]
			public float m_Duration;

			// Token: 0x040003DC RID: 988
			private float m_CurrentAmount;

			// Token: 0x040003DD RID: 989
			private float m_CurrentTime;

			// Token: 0x040003DE RID: 990
			private float m_CurrentDamping;

			// Token: 0x040003DF RID: 991
			private bool m_Initialized;

			// Token: 0x040003E0 RID: 992
			[SerializeField]
			[HideInInspector]
			private Vector3 m_NoiseOffsets;
		}
	}
}
