using System;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x020000BE RID: 190
	[DocumentationSorting(DocumentationSortingAttribute.Level.API)]
	public class CinemachineImpulseManager
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x000026D7 File Offset: 0x000008D7
		private CinemachineImpulseManager()
		{
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00018207 File Offset: 0x00016407
		public static CinemachineImpulseManager Instance
		{
			get
			{
				if (CinemachineImpulseManager.sInstance == null)
				{
					CinemachineImpulseManager.sInstance = new CinemachineImpulseManager();
				}
				return CinemachineImpulseManager.sInstance;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001821F File Offset: 0x0001641F
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeModule()
		{
			if (CinemachineImpulseManager.sInstance != null)
			{
				CinemachineImpulseManager.sInstance.Clear();
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00018234 File Offset: 0x00016434
		internal static float EvaluateDissipationScale(float spread, float normalizedDistance)
		{
			float b = -0.8f + 1.6f * (1f - spread);
			b = (1f - b) * 0.5f;
			float t = Mathf.Clamp01(normalizedDistance) / ((1f / Mathf.Clamp01(b) - 2f) * (1f - normalizedDistance) + 1f);
			return 1f - SplineHelpers.Bezier1(t, 0f, 0f, 1f, 1f);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000182AC File Offset: 0x000164AC
		public bool GetImpulseAt(Vector3 listenerLocation, bool distance2D, int channelMask, out Vector3 pos, out Quaternion rot)
		{
			bool nontrivialResult = false;
			pos = Vector3.zero;
			rot = Quaternion.identity;
			if (this.m_ActiveEvents != null)
			{
				for (int i = this.m_ActiveEvents.Count - 1; i >= 0; i--)
				{
					CinemachineImpulseManager.ImpulseEvent e = this.m_ActiveEvents[i];
					if (e == null || e.Expired)
					{
						this.m_ActiveEvents.RemoveAt(i);
						if (e != null)
						{
							if (this.m_ExpiredEvents == null)
							{
								this.m_ExpiredEvents = new List<CinemachineImpulseManager.ImpulseEvent>();
							}
							e.Clear();
							this.m_ExpiredEvents.Add(e);
						}
					}
					else if ((e.m_Channel & channelMask) != 0)
					{
						Vector3 pos2 = Vector3.zero;
						Quaternion rot2 = Quaternion.identity;
						if (e.GetDecayedSignal(listenerLocation, distance2D, out pos2, out rot2))
						{
							nontrivialResult = true;
							pos += pos2;
							rot *= rot2;
						}
					}
				}
			}
			return nontrivialResult;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001839B File Offset: 0x0001659B
		public float CurrentTime
		{
			get
			{
				if (!this.IgnoreTimeScale)
				{
					return CinemachineCore.CurrentTime;
				}
				return Time.realtimeSinceStartup;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000183B0 File Offset: 0x000165B0
		public CinemachineImpulseManager.ImpulseEvent NewImpulseEvent()
		{
			if (this.m_ExpiredEvents == null || this.m_ExpiredEvents.Count == 0)
			{
				return new CinemachineImpulseManager.ImpulseEvent
				{
					m_CustomDissipation = -1f
				};
			}
			CinemachineImpulseManager.ImpulseEvent impulseEvent = this.m_ExpiredEvents[this.m_ExpiredEvents.Count - 1];
			this.m_ExpiredEvents.RemoveAt(this.m_ExpiredEvents.Count - 1);
			return impulseEvent;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00018413 File Offset: 0x00016613
		public void AddImpulseEvent(CinemachineImpulseManager.ImpulseEvent e)
		{
			if (this.m_ActiveEvents == null)
			{
				this.m_ActiveEvents = new List<CinemachineImpulseManager.ImpulseEvent>();
			}
			if (e != null)
			{
				e.m_StartTime = this.CurrentTime;
				this.m_ActiveEvents.Add(e);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00018444 File Offset: 0x00016644
		public void Clear()
		{
			if (this.m_ActiveEvents != null)
			{
				for (int i = 0; i < this.m_ActiveEvents.Count; i++)
				{
					this.m_ActiveEvents[i].Clear();
				}
				this.m_ActiveEvents.Clear();
			}
		}

		// Token: 0x040003E1 RID: 993
		private static CinemachineImpulseManager sInstance;

		// Token: 0x040003E2 RID: 994
		private const float Epsilon = 0.0001f;

		// Token: 0x040003E3 RID: 995
		private List<CinemachineImpulseManager.ImpulseEvent> m_ExpiredEvents;

		// Token: 0x040003E4 RID: 996
		private List<CinemachineImpulseManager.ImpulseEvent> m_ActiveEvents;

		// Token: 0x040003E5 RID: 997
		public bool IgnoreTimeScale;

		// Token: 0x020000BF RID: 191
		[Serializable]
		public struct EnvelopeDefinition
		{
			// Token: 0x0600042D RID: 1069 RVA: 0x0001848C File Offset: 0x0001668C
			public static CinemachineImpulseManager.EnvelopeDefinition Default()
			{
				return new CinemachineImpulseManager.EnvelopeDefinition
				{
					m_DecayTime = 0.7f,
					m_SustainTime = 0.2f,
					m_ScaleWithImpact = true
				};
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x000184C2 File Offset: 0x000166C2
			public float Duration
			{
				get
				{
					if (this.m_HoldForever)
					{
						return -1f;
					}
					return this.m_AttackTime + this.m_SustainTime + this.m_DecayTime;
				}
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x000184E8 File Offset: 0x000166E8
			public float GetValueAt(float offset)
			{
				if (offset >= 0f)
				{
					if (offset < this.m_AttackTime && this.m_AttackTime > 0.0001f)
					{
						if (this.m_AttackShape == null || this.m_AttackShape.length < 2)
						{
							return Damper.Damp(1f, this.m_AttackTime, offset);
						}
						return this.m_AttackShape.Evaluate(offset / this.m_AttackTime);
					}
					else
					{
						offset -= this.m_AttackTime;
						if (this.m_HoldForever || offset < this.m_SustainTime)
						{
							return 1f;
						}
						offset -= this.m_SustainTime;
						if (offset < this.m_DecayTime && this.m_DecayTime > 0.0001f)
						{
							if (this.m_DecayShape == null || this.m_DecayShape.length < 2)
							{
								return 1f - Damper.Damp(1f, this.m_DecayTime, offset);
							}
							return this.m_DecayShape.Evaluate(offset / this.m_DecayTime);
						}
					}
				}
				return 0f;
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x000185DC File Offset: 0x000167DC
			public void ChangeStopTime(float offset, bool forceNoDecay)
			{
				if (offset < 0f)
				{
					offset = 0f;
				}
				if (offset < this.m_AttackTime)
				{
					this.m_AttackTime = 0f;
				}
				this.m_SustainTime = offset - this.m_AttackTime;
				if (forceNoDecay)
				{
					this.m_DecayTime = 0f;
				}
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x00018628 File Offset: 0x00016828
			public void Clear()
			{
				this.m_AttackShape = (this.m_DecayShape = null);
				this.m_AttackTime = (this.m_SustainTime = (this.m_DecayTime = 0f));
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x00018664 File Offset: 0x00016864
			public void Validate()
			{
				this.m_AttackTime = Mathf.Max(0f, this.m_AttackTime);
				this.m_DecayTime = Mathf.Max(0f, this.m_DecayTime);
				this.m_SustainTime = Mathf.Max(0f, this.m_SustainTime);
			}

			// Token: 0x040003E6 RID: 998
			[Tooltip("Normalized curve defining the shape of the start of the envelope.  If blank a default curve will be used")]
			public AnimationCurve m_AttackShape;

			// Token: 0x040003E7 RID: 999
			[Tooltip("Normalized curve defining the shape of the end of the envelope.  If blank a default curve will be used")]
			public AnimationCurve m_DecayShape;

			// Token: 0x040003E8 RID: 1000
			[Tooltip("Duration in seconds of the attack.  Attack curve will be scaled to fit.  Must be >= 0.")]
			public float m_AttackTime;

			// Token: 0x040003E9 RID: 1001
			[Tooltip("Duration in seconds of the central fully-scaled part of the envelope.  Must be >= 0.")]
			public float m_SustainTime;

			// Token: 0x040003EA RID: 1002
			[Tooltip("Duration in seconds of the decay.  Decay curve will be scaled to fit.  Must be >= 0.")]
			public float m_DecayTime;

			// Token: 0x040003EB RID: 1003
			[Tooltip("If checked, signal amplitude scaling will also be applied to the time envelope of the signal.  Stronger signals will last longer.")]
			public bool m_ScaleWithImpact;

			// Token: 0x040003EC RID: 1004
			[Tooltip("If true, then duration is infinite.")]
			public bool m_HoldForever;
		}

		// Token: 0x020000C0 RID: 192
		public class ImpulseEvent
		{
			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x000186B4 File Offset: 0x000168B4
			public bool Expired
			{
				get
				{
					float d = this.m_Envelope.Duration;
					float maxDistance = this.m_Radius + this.m_DissipationDistance;
					float time = CinemachineImpulseManager.Instance.CurrentTime - maxDistance / Mathf.Max(1f, this.m_PropagationSpeed);
					return d > 0f && this.m_StartTime + d <= time;
				}
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00018711 File Offset: 0x00016911
			public void Cancel(float time, bool forceNoDecay)
			{
				this.m_Envelope.m_HoldForever = false;
				this.m_Envelope.ChangeStopTime(time - this.m_StartTime, forceNoDecay);
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x00018734 File Offset: 0x00016934
			public float DistanceDecay(float distance)
			{
				float radius = Mathf.Max(this.m_Radius, 0f);
				if (distance < radius)
				{
					return 1f;
				}
				distance -= radius;
				if (distance >= this.m_DissipationDistance)
				{
					return 0f;
				}
				if (this.m_CustomDissipation >= 0f)
				{
					return CinemachineImpulseManager.EvaluateDissipationScale(this.m_CustomDissipation, distance / this.m_DissipationDistance);
				}
				switch (this.m_DissipationMode)
				{
				default:
					return Mathf.Lerp(1f, 0f, distance / this.m_DissipationDistance);
				case CinemachineImpulseManager.ImpulseEvent.DissipationMode.SoftDecay:
					return 0.5f * (1f + Mathf.Cos(3.1415927f * (distance / this.m_DissipationDistance)));
				case CinemachineImpulseManager.ImpulseEvent.DissipationMode.ExponentialDecay:
					return 1f - Damper.Damp(1f, this.m_DissipationDistance, distance);
				}
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x000187FC File Offset: 0x000169FC
			public bool GetDecayedSignal(Vector3 listenerPosition, bool use2D, out Vector3 pos, out Quaternion rot)
			{
				if (this.m_SignalSource != null)
				{
					float distance = (use2D ? Vector2.Distance(listenerPosition, this.m_Position) : Vector3.Distance(listenerPosition, this.m_Position));
					float time = CinemachineImpulseManager.Instance.CurrentTime - this.m_StartTime - distance / Mathf.Max(1f, this.m_PropagationSpeed);
					float scale = this.m_Envelope.GetValueAt(time) * this.DistanceDecay(distance);
					if (scale != 0f)
					{
						this.m_SignalSource.GetSignal(time, out pos, out rot);
						pos *= scale;
						rot = Quaternion.SlerpUnclamped(Quaternion.identity, rot, scale);
						if (this.m_DirectionMode == CinemachineImpulseManager.ImpulseEvent.DirectionMode.RotateTowardSource && distance > 0.0001f)
						{
							Quaternion q = Quaternion.FromToRotation(Vector3.up, listenerPosition - this.m_Position);
							if (this.m_Radius > 0.0001f)
							{
								float t = Mathf.Clamp01(distance / this.m_Radius);
								q = Quaternion.Slerp(q, Quaternion.identity, Mathf.Cos(3.1415927f * t / 2f));
							}
							pos = q * pos;
						}
						return true;
					}
				}
				pos = Vector3.zero;
				rot = Quaternion.identity;
				return false;
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x0001894C File Offset: 0x00016B4C
			public void Clear()
			{
				this.m_Envelope.Clear();
				this.m_StartTime = 0f;
				this.m_SignalSource = null;
				this.m_Position = Vector3.zero;
				this.m_Channel = 0;
				this.m_Radius = 0f;
				this.m_DissipationDistance = 100f;
				this.m_DissipationMode = CinemachineImpulseManager.ImpulseEvent.DissipationMode.ExponentialDecay;
				this.m_CustomDissipation = -1f;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x000026D7 File Offset: 0x000008D7
			internal ImpulseEvent()
			{
			}

			// Token: 0x040003ED RID: 1005
			public float m_StartTime;

			// Token: 0x040003EE RID: 1006
			public CinemachineImpulseManager.EnvelopeDefinition m_Envelope;

			// Token: 0x040003EF RID: 1007
			public ISignalSource6D m_SignalSource;

			// Token: 0x040003F0 RID: 1008
			public Vector3 m_Position;

			// Token: 0x040003F1 RID: 1009
			public float m_Radius;

			// Token: 0x040003F2 RID: 1010
			public CinemachineImpulseManager.ImpulseEvent.DirectionMode m_DirectionMode;

			// Token: 0x040003F3 RID: 1011
			public int m_Channel;

			// Token: 0x040003F4 RID: 1012
			public CinemachineImpulseManager.ImpulseEvent.DissipationMode m_DissipationMode;

			// Token: 0x040003F5 RID: 1013
			public float m_DissipationDistance;

			// Token: 0x040003F6 RID: 1014
			public float m_CustomDissipation;

			// Token: 0x040003F7 RID: 1015
			public float m_PropagationSpeed;

			// Token: 0x020000C1 RID: 193
			public enum DirectionMode
			{
				// Token: 0x040003F9 RID: 1017
				Fixed,
				// Token: 0x040003FA RID: 1018
				RotateTowardSource
			}

			// Token: 0x020000C2 RID: 194
			public enum DissipationMode
			{
				// Token: 0x040003FC RID: 1020
				LinearDecay,
				// Token: 0x040003FD RID: 1021
				SoftDecay,
				// Token: 0x040003FE RID: 1022
				ExponentialDecay
			}
		}
	}
}
