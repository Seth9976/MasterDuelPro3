using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000B4 RID: 180
	[DocumentationSorting(DocumentationSortingAttribute.Level.API)]
	[Serializable]
	public class CinemachineImpulseDefinition
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x000175D4 File Offset: 0x000157D4
		public void OnValidate()
		{
			RuntimeUtility.NormalizeCurve(this.m_CustomImpulseShape, true, false);
			this.m_ImpulseDuration = Mathf.Max(0.0001f, this.m_ImpulseDuration);
			this.m_DissipationDistance = Mathf.Max(0.0001f, this.m_DissipationDistance);
			this.m_DissipationRate = Mathf.Clamp01(this.m_DissipationRate);
			this.m_PropagationSpeed = Mathf.Max(1f, this.m_PropagationSpeed);
			this.m_ImpactRadius = Mathf.Max(0f, this.m_ImpactRadius);
			this.m_TimeEnvelope.Validate();
			this.m_PropagationSpeed = Mathf.Max(1f, this.m_PropagationSpeed);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001767C File Offset: 0x0001587C
		private static void CreateStandardShapes()
		{
			int max = 0;
			foreach (object value in Enum.GetValues(typeof(CinemachineImpulseDefinition.ImpulseShapes)))
			{
				max = Mathf.Max(max, (int)value);
			}
			CinemachineImpulseDefinition.sStandardShapes = new AnimationCurve[max + 1];
			CinemachineImpulseDefinition.sStandardShapes[1] = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f, -3.2f, -3.2f),
				new Keyframe(1f, 0f, 0f, 0f)
			});
			CinemachineImpulseDefinition.sStandardShapes[2] = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, -4.9f, -4.9f),
				new Keyframe(0.2f, 0f, 8.25f, 8.25f),
				new Keyframe(1f, 0f, -0.25f, -0.25f)
			});
			CinemachineImpulseDefinition.sStandardShapes[3] = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, -1.4f, -7.9f, -7.9f),
				new Keyframe(0.27f, 0.78f, 23.4f, 23.4f),
				new Keyframe(0.54f, -0.12f, 22.6f, 22.6f),
				new Keyframe(0.75f, 0.042f, 9.23f, 9.23f),
				new Keyframe(0.9f, -0.02f, 5.8f, 5.8f),
				new Keyframe(0.95f, -0.006f, -3f, -3f),
				new Keyframe(1f, 0f, 0f, 0f)
			});
			CinemachineImpulseDefinition.sStandardShapes[4] = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 0f, 0f),
				new Keyframe(0.1f, 0.25f, 0f, 0f),
				new Keyframe(0.2f, 0f, 0f, 0f),
				new Keyframe(0.3f, 0.75f, 0f, 0f),
				new Keyframe(0.4f, 0f, 0f, 0f),
				new Keyframe(0.5f, 1f, 0f, 0f),
				new Keyframe(0.6f, 0f, 0f, 0f),
				new Keyframe(0.7f, 0.75f, 0f, 0f),
				new Keyframe(0.8f, 0f, 0f, 0f),
				new Keyframe(0.9f, 0.25f, 0f, 0f),
				new Keyframe(1f, 0f, 0f, 0f)
			});
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00017A1C File Offset: 0x00015C1C
		internal static AnimationCurve GetStandardCurve(CinemachineImpulseDefinition.ImpulseShapes shape)
		{
			if (CinemachineImpulseDefinition.sStandardShapes == null)
			{
				CinemachineImpulseDefinition.CreateStandardShapes();
			}
			return CinemachineImpulseDefinition.sStandardShapes[(int)shape];
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00017A34 File Offset: 0x00015C34
		internal AnimationCurve ImpulseCurve
		{
			get
			{
				if (this.m_ImpulseShape == CinemachineImpulseDefinition.ImpulseShapes.Custom)
				{
					if (this.m_CustomImpulseShape == null)
					{
						this.m_CustomImpulseShape = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
					}
					return this.m_CustomImpulseShape;
				}
				return CinemachineImpulseDefinition.GetStandardCurve(this.m_ImpulseShape);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00017A82 File Offset: 0x00015C82
		public void CreateEvent(Vector3 position, Vector3 velocity)
		{
			this.CreateAndReturnEvent(position, velocity);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00017A90 File Offset: 0x00015C90
		public CinemachineImpulseManager.ImpulseEvent CreateAndReturnEvent(Vector3 position, Vector3 velocity)
		{
			if (this.m_ImpulseType == CinemachineImpulseDefinition.ImpulseTypes.Legacy)
			{
				return this.LegacyCreateAndReturnEvent(position, velocity);
			}
			if ((this.m_ImpulseShape == CinemachineImpulseDefinition.ImpulseShapes.Custom && this.m_CustomImpulseShape == null) || Mathf.Abs(this.m_DissipationDistance) < 0.0001f || Mathf.Abs(this.m_ImpulseDuration) < 0.0001f)
			{
				return null;
			}
			CinemachineImpulseManager.ImpulseEvent e = CinemachineImpulseManager.Instance.NewImpulseEvent();
			e.m_Envelope = new CinemachineImpulseManager.EnvelopeDefinition
			{
				m_SustainTime = this.m_ImpulseDuration
			};
			e.m_SignalSource = new CinemachineImpulseDefinition.SignalSource(this, velocity);
			e.m_Position = position;
			e.m_Radius = ((this.m_ImpulseType == CinemachineImpulseDefinition.ImpulseTypes.Uniform) ? 9999999f : 0f);
			e.m_Channel = this.m_ImpulseChannel;
			e.m_DirectionMode = CinemachineImpulseManager.ImpulseEvent.DirectionMode.Fixed;
			e.m_DissipationDistance = ((this.m_ImpulseType == CinemachineImpulseDefinition.ImpulseTypes.Uniform) ? 0f : this.m_DissipationDistance);
			e.m_PropagationSpeed = ((this.m_ImpulseType == CinemachineImpulseDefinition.ImpulseTypes.Propagating) ? this.m_PropagationSpeed : 9999999f);
			e.m_CustomDissipation = this.m_DissipationRate;
			CinemachineImpulseManager.Instance.AddImpulseEvent(e);
			return e;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00017B9C File Offset: 0x00015D9C
		private CinemachineImpulseManager.ImpulseEvent LegacyCreateAndReturnEvent(Vector3 position, Vector3 velocity)
		{
			if (this.m_RawSignal == null || Mathf.Abs(this.m_TimeEnvelope.Duration) < 0.0001f)
			{
				return null;
			}
			CinemachineImpulseManager.ImpulseEvent e = CinemachineImpulseManager.Instance.NewImpulseEvent();
			e.m_Envelope = this.m_TimeEnvelope;
			e.m_Envelope = this.m_TimeEnvelope;
			if (this.m_TimeEnvelope.m_ScaleWithImpact)
			{
				CinemachineImpulseManager.ImpulseEvent impulseEvent = e;
				impulseEvent.m_Envelope.m_DecayTime = impulseEvent.m_Envelope.m_DecayTime * Mathf.Sqrt(velocity.magnitude);
			}
			e.m_SignalSource = new CinemachineImpulseDefinition.LegacySignalSource(this, velocity);
			e.m_Position = position;
			e.m_Radius = this.m_ImpactRadius;
			e.m_Channel = this.m_ImpulseChannel;
			e.m_DirectionMode = this.m_DirectionMode;
			e.m_DissipationMode = this.m_DissipationMode;
			e.m_DissipationDistance = this.m_DissipationDistance;
			e.m_PropagationSpeed = this.m_PropagationSpeed;
			CinemachineImpulseManager.Instance.AddImpulseEvent(e);
			return e;
		}

		// Token: 0x040003AD RID: 941
		[CinemachineImpulseChannelProperty]
		[Tooltip("Impulse events generated here will appear on the channels included in the mask.")]
		public int m_ImpulseChannel = 1;

		// Token: 0x040003AE RID: 942
		[Tooltip("Shape of the impact signal")]
		public CinemachineImpulseDefinition.ImpulseShapes m_ImpulseShape;

		// Token: 0x040003AF RID: 943
		[Tooltip("Defines the custom shape of the impact signal that will be generated.")]
		public AnimationCurve m_CustomImpulseShape = new AnimationCurve();

		// Token: 0x040003B0 RID: 944
		[Tooltip("The time during which the impact signal will occur.  The signal shape will be stretched to fill that time.")]
		public float m_ImpulseDuration = 0.2f;

		// Token: 0x040003B1 RID: 945
		[Tooltip("How the impulse travels through space and time.")]
		public CinemachineImpulseDefinition.ImpulseTypes m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Legacy;

		// Token: 0x040003B2 RID: 946
		[Tooltip("This defines how the widely signal will spread within the effect radius before dissipating with distance from the impact point")]
		[Range(0f, 1f)]
		public float m_DissipationRate;

		// Token: 0x040003B3 RID: 947
		[Header("Signal Shape")]
		[Tooltip("Legacy mode only: Defines the signal that will be generated.")]
		[CinemachineEmbeddedAssetProperty(true)]
		public SignalSourceAsset m_RawSignal;

		// Token: 0x040003B4 RID: 948
		[Tooltip("Legacy mode only: Gain to apply to the amplitudes defined in the signal source.  1 is normal.  Setting this to 0 completely mutes the signal.")]
		public float m_AmplitudeGain = 1f;

		// Token: 0x040003B5 RID: 949
		[Tooltip("Legacy mode only: Scale factor to apply to the time axis.  1 is normal.  Larger magnitudes will make the signal progress more rapidly.")]
		public float m_FrequencyGain = 1f;

		// Token: 0x040003B6 RID: 950
		[Tooltip("Legacy mode only: How to fit the signal into the envelope time")]
		public CinemachineImpulseDefinition.RepeatMode m_RepeatMode;

		// Token: 0x040003B7 RID: 951
		[Tooltip("Legacy mode only: Randomize the signal start time")]
		public bool m_Randomize = true;

		// Token: 0x040003B8 RID: 952
		[Tooltip("Legacy mode only: This defines the time-envelope of the signal.  The raw signal will be time-scaled to fit in the envelope.")]
		public CinemachineImpulseManager.EnvelopeDefinition m_TimeEnvelope = CinemachineImpulseManager.EnvelopeDefinition.Default();

		// Token: 0x040003B9 RID: 953
		[Header("Spatial Range")]
		[Tooltip("Legacy mode only: The signal will have full amplitude in this radius surrounding the impact point.  Beyond that it will dissipate with distance.")]
		public float m_ImpactRadius = 100f;

		// Token: 0x040003BA RID: 954
		[Tooltip("Legacy mode only: How the signal direction behaves as the listener moves away from the origin.")]
		public CinemachineImpulseManager.ImpulseEvent.DirectionMode m_DirectionMode;

		// Token: 0x040003BB RID: 955
		[Tooltip("Legacy mode only: This defines how the signal will dissipate with distance beyond the impact radius.")]
		public CinemachineImpulseManager.ImpulseEvent.DissipationMode m_DissipationMode = CinemachineImpulseManager.ImpulseEvent.DissipationMode.ExponentialDecay;

		// Token: 0x040003BC RID: 956
		[Tooltip("The signal will have no effect outside this radius surrounding the impact point.")]
		public float m_DissipationDistance = 100f;

		// Token: 0x040003BD RID: 957
		[Tooltip("The speed (m/s) at which the impulse propagates through space.  High speeds allow listeners to react instantaneously, while slower speeds allow listeners in the scene to react as if to a wave spreading from the source.")]
		public float m_PropagationSpeed = 343f;

		// Token: 0x040003BE RID: 958
		private static AnimationCurve[] sStandardShapes;

		// Token: 0x020000B5 RID: 181
		public enum ImpulseShapes
		{
			// Token: 0x040003C0 RID: 960
			Custom,
			// Token: 0x040003C1 RID: 961
			Recoil,
			// Token: 0x040003C2 RID: 962
			Bump,
			// Token: 0x040003C3 RID: 963
			Explosion,
			// Token: 0x040003C4 RID: 964
			Rumble
		}

		// Token: 0x020000B6 RID: 182
		public enum ImpulseTypes
		{
			// Token: 0x040003C6 RID: 966
			Uniform,
			// Token: 0x040003C7 RID: 967
			Dissipating,
			// Token: 0x040003C8 RID: 968
			Propagating,
			// Token: 0x040003C9 RID: 969
			Legacy
		}

		// Token: 0x020000B7 RID: 183
		public enum RepeatMode
		{
			// Token: 0x040003CB RID: 971
			Stretch,
			// Token: 0x040003CC RID: 972
			Loop
		}

		// Token: 0x020000B8 RID: 184
		private class SignalSource : ISignalSource6D
		{
			// Token: 0x06000417 RID: 1047 RVA: 0x00017D0B File Offset: 0x00015F0B
			public SignalSource(CinemachineImpulseDefinition def, Vector3 velocity)
			{
				this.m_Def = def;
				this.m_Velocity = velocity;
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x00017D21 File Offset: 0x00015F21
			public float SignalDuration
			{
				get
				{
					return this.m_Def.m_ImpulseDuration;
				}
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x00017D2E File Offset: 0x00015F2E
			public void GetSignal(float timeSinceSignalStart, out Vector3 pos, out Quaternion rot)
			{
				pos = this.m_Velocity * this.m_Def.ImpulseCurve.Evaluate(timeSinceSignalStart / this.SignalDuration);
				rot = Quaternion.identity;
			}

			// Token: 0x040003CD RID: 973
			private CinemachineImpulseDefinition m_Def;

			// Token: 0x040003CE RID: 974
			private Vector3 m_Velocity;
		}

		// Token: 0x020000B9 RID: 185
		private class LegacySignalSource : ISignalSource6D
		{
			// Token: 0x0600041A RID: 1050 RVA: 0x00017D64 File Offset: 0x00015F64
			public LegacySignalSource(CinemachineImpulseDefinition def, Vector3 velocity)
			{
				this.m_Def = def;
				this.m_Velocity = velocity;
				if (this.m_Def.m_Randomize && this.m_Def.m_RawSignal.SignalDuration <= 0f)
				{
					this.m_StartTimeOffset = global::UnityEngine.Random.Range(-1000f, 1000f);
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x00017DBE File Offset: 0x00015FBE
			public float SignalDuration
			{
				get
				{
					return this.m_Def.m_RawSignal.SignalDuration;
				}
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x00017DD0 File Offset: 0x00015FD0
			public void GetSignal(float timeSinceSignalStart, out Vector3 pos, out Quaternion rot)
			{
				float time = this.m_StartTimeOffset + timeSinceSignalStart * this.m_Def.m_FrequencyGain;
				float signalDuration = this.SignalDuration;
				if (signalDuration > 0f)
				{
					if (this.m_Def.m_RepeatMode == CinemachineImpulseDefinition.RepeatMode.Loop)
					{
						time %= signalDuration;
					}
					else if (this.m_Def.m_TimeEnvelope.Duration > 0.0001f)
					{
						time *= this.m_Def.m_TimeEnvelope.Duration / signalDuration;
					}
				}
				this.m_Def.m_RawSignal.GetSignal(time, out pos, out rot);
				float gain = this.m_Velocity.magnitude;
				Vector3 normalized = this.m_Velocity.normalized;
				gain *= this.m_Def.m_AmplitudeGain;
				pos *= gain;
				pos = Quaternion.FromToRotation(Vector3.down, this.m_Velocity) * pos;
				rot = Quaternion.SlerpUnclamped(Quaternion.identity, rot, gain);
			}

			// Token: 0x040003CF RID: 975
			private CinemachineImpulseDefinition m_Def;

			// Token: 0x040003D0 RID: 976
			private Vector3 m_Velocity;

			// Token: 0x040003D1 RID: 977
			private float m_StartTimeOffset;
		}
	}
}
