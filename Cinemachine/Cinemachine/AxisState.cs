using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000061 RID: 97
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[Serializable]
	public struct AxisState
	{
		// Token: 0x0600024B RID: 587 RVA: 0x000108E8 File Offset: 0x0000EAE8
		public AxisState(float minValue, float maxValue, bool wrap, bool rangeLocked, float maxSpeed, float accelTime, float decelTime, string name, bool invert)
		{
			this.m_MinValue = minValue;
			this.m_MaxValue = maxValue;
			this.m_Wrap = wrap;
			this.ValueRangeLocked = rangeLocked;
			this.HasRecentering = false;
			this.m_Recentering = new AxisState.Recentering(false, 1f, 2f);
			this.m_SpeedMode = AxisState.SpeedMode.MaxSpeed;
			this.m_MaxSpeed = maxSpeed;
			this.m_AccelTime = accelTime;
			this.m_DecelTime = decelTime;
			this.Value = (minValue + maxValue) / 2f;
			this.m_InputAxisName = name;
			this.m_InputAxisValue = 0f;
			this.m_InvertInput = invert;
			this.m_CurrentSpeed = 0f;
			this.m_InputAxisProvider = null;
			this.m_InputAxisIndex = 0;
			this.m_LastUpdateTime = 0f;
			this.m_LastUpdateFrame = 0;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000109A4 File Offset: 0x0000EBA4
		public void Validate()
		{
			if (this.m_SpeedMode == AxisState.SpeedMode.MaxSpeed)
			{
				this.m_MaxSpeed = Mathf.Max(0f, this.m_MaxSpeed);
			}
			this.m_AccelTime = Mathf.Max(0f, this.m_AccelTime);
			this.m_DecelTime = Mathf.Max(0f, this.m_DecelTime);
			this.m_MaxValue = Mathf.Clamp(this.m_MaxValue, this.m_MinValue, this.m_MaxValue);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00010A18 File Offset: 0x0000EC18
		public void Reset()
		{
			this.m_InputAxisValue = 0f;
			this.m_CurrentSpeed = 0f;
			this.m_LastUpdateTime = 0f;
			this.m_LastUpdateFrame = 0;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00010A42 File Offset: 0x0000EC42
		public void SetInputAxisProvider(int axis, AxisState.IInputAxisProvider provider)
		{
			this.m_InputAxisIndex = axis;
			this.m_InputAxisProvider = provider;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00010A52 File Offset: 0x0000EC52
		public bool HasInputProvider
		{
			get
			{
				return this.m_InputAxisProvider != null;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00010A60 File Offset: 0x0000EC60
		public bool Update(float deltaTime)
		{
			if (Time.frameCount == this.m_LastUpdateFrame)
			{
				return false;
			}
			this.m_LastUpdateFrame = Time.frameCount;
			if (deltaTime > 0f && this.m_LastUpdateTime != 0f)
			{
				deltaTime = Time.realtimeSinceStartup - this.m_LastUpdateTime;
			}
			this.m_LastUpdateTime = Time.realtimeSinceStartup;
			if (this.m_InputAxisProvider != null)
			{
				this.m_InputAxisValue = this.m_InputAxisProvider.GetAxisValue(this.m_InputAxisIndex);
			}
			else if (!string.IsNullOrEmpty(this.m_InputAxisName))
			{
				try
				{
					this.m_InputAxisValue = CinemachineCore.GetInputAxis(this.m_InputAxisName);
				}
				catch (ArgumentException ex)
				{
					Debug.LogError(ex.ToString());
				}
			}
			float input = this.m_InputAxisValue;
			if (this.m_InvertInput)
			{
				input *= -1f;
			}
			if (this.m_SpeedMode == AxisState.SpeedMode.MaxSpeed)
			{
				return this.MaxSpeedUpdate(input, deltaTime);
			}
			input *= this.m_MaxSpeed;
			if (deltaTime < 0f)
			{
				this.m_CurrentSpeed = 0f;
			}
			else if (deltaTime > 0.0001f)
			{
				float dampTime = ((Mathf.Abs(input) < Mathf.Abs(this.m_CurrentSpeed)) ? this.m_DecelTime : this.m_AccelTime);
				this.m_CurrentSpeed += Damper.Damp(input - this.m_CurrentSpeed, dampTime, deltaTime);
				float range = this.m_MaxValue - this.m_MinValue;
				if (!this.m_Wrap && this.m_DecelTime > 0.0001f && range > 0.0001f)
				{
					float v0 = this.ClampValue(this.Value);
					float v = this.ClampValue(v0 + this.m_CurrentSpeed * deltaTime);
					if (((this.m_CurrentSpeed > 0f) ? (this.m_MaxValue - v) : (v - this.m_MinValue)) < 0.1f * range && Mathf.Abs(this.m_CurrentSpeed) > 0.0001f)
					{
						this.m_CurrentSpeed = Damper.Damp(v - v0, this.m_DecelTime, deltaTime) / deltaTime;
					}
				}
				input = this.m_CurrentSpeed * deltaTime;
			}
			this.Value = this.ClampValue(this.Value + this.m_CurrentSpeed);
			return Mathf.Abs(input) > 0.0001f;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00010C7C File Offset: 0x0000EE7C
		private float ClampValue(float v)
		{
			float r = this.m_MaxValue - this.m_MinValue;
			if (this.m_Wrap && r > 0.0001f)
			{
				v = (v - this.m_MinValue) % r;
				v += this.m_MinValue + ((v < 0f) ? r : 0f);
			}
			return Mathf.Clamp(v, this.m_MinValue, this.m_MaxValue);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		private bool MaxSpeedUpdate(float input, float deltaTime)
		{
			if (this.m_MaxSpeed > 0.0001f)
			{
				float targetSpeed = input * this.m_MaxSpeed;
				if (Mathf.Abs(targetSpeed) < 0.0001f || (Mathf.Sign(this.m_CurrentSpeed) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) < Mathf.Abs(this.m_CurrentSpeed)))
				{
					float delta = Mathf.Min(Mathf.Abs(targetSpeed - this.m_CurrentSpeed) / Mathf.Max(0.0001f, this.m_DecelTime) * deltaTime, Mathf.Abs(this.m_CurrentSpeed));
					this.m_CurrentSpeed -= Mathf.Sign(this.m_CurrentSpeed) * delta;
				}
				else
				{
					float a = Mathf.Abs(targetSpeed - this.m_CurrentSpeed) / Mathf.Max(0.0001f, this.m_AccelTime);
					this.m_CurrentSpeed += Mathf.Sign(targetSpeed) * a * deltaTime;
					if (Mathf.Sign(this.m_CurrentSpeed) == Mathf.Sign(targetSpeed) && Mathf.Abs(this.m_CurrentSpeed) > Mathf.Abs(targetSpeed))
					{
						this.m_CurrentSpeed = targetSpeed;
					}
				}
			}
			float maxSpeed = this.GetMaxSpeed();
			this.m_CurrentSpeed = Mathf.Clamp(this.m_CurrentSpeed, -maxSpeed, maxSpeed);
			if (Mathf.Abs(this.m_CurrentSpeed) < 0.0001f)
			{
				this.m_CurrentSpeed = 0f;
			}
			this.Value += this.m_CurrentSpeed * deltaTime;
			if (this.Value > this.m_MaxValue || this.Value < this.m_MinValue)
			{
				if (this.m_Wrap)
				{
					if (this.Value > this.m_MaxValue)
					{
						this.Value = this.m_MinValue + (this.Value - this.m_MaxValue);
					}
					else
					{
						this.Value = this.m_MaxValue + (this.Value - this.m_MinValue);
					}
				}
				else
				{
					this.Value = Mathf.Clamp(this.Value, this.m_MinValue, this.m_MaxValue);
					this.m_CurrentSpeed = 0f;
				}
			}
			return Mathf.Abs(input) > 0.0001f;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00010EDC File Offset: 0x0000F0DC
		private float GetMaxSpeed()
		{
			float range = this.m_MaxValue - this.m_MinValue;
			if (!this.m_Wrap && range > 0f)
			{
				float threshold = range / 10f;
				if (this.m_CurrentSpeed > 0f && this.m_MaxValue - this.Value < threshold)
				{
					float t = (this.m_MaxValue - this.Value) / threshold;
					return Mathf.Lerp(0f, this.m_MaxSpeed, t);
				}
				if (this.m_CurrentSpeed < 0f && this.Value - this.m_MinValue < threshold)
				{
					float t2 = (this.Value - this.m_MinValue) / threshold;
					return Mathf.Lerp(0f, this.m_MaxSpeed, t2);
				}
			}
			return this.m_MaxSpeed;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00010F99 File Offset: 0x0000F199
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00010FA1 File Offset: 0x0000F1A1
		public bool ValueRangeLocked { readonly get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00010FAA File Offset: 0x0000F1AA
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00010FB2 File Offset: 0x0000F1B2
		public bool HasRecentering { readonly get; set; }

		// Token: 0x04000239 RID: 569
		[NoSaveDuringPlay]
		[Tooltip("The current value of the axis.")]
		public float Value;

		// Token: 0x0400023A RID: 570
		[Tooltip("How to interpret the Max Speed setting: in units/second, or as a direct input value multiplier")]
		public AxisState.SpeedMode m_SpeedMode;

		// Token: 0x0400023B RID: 571
		[Tooltip("The maximum speed of this axis in units/second, or the input value multiplier, depending on the Speed Mode")]
		public float m_MaxSpeed;

		// Token: 0x0400023C RID: 572
		[Tooltip("The amount of time in seconds it takes to accelerate to MaxSpeed with the supplied Axis at its maximum value")]
		public float m_AccelTime;

		// Token: 0x0400023D RID: 573
		[Tooltip("The amount of time in seconds it takes to decelerate the axis to zero if the supplied axis is in a neutral position")]
		public float m_DecelTime;

		// Token: 0x0400023E RID: 574
		[FormerlySerializedAs("m_AxisName")]
		[Tooltip("The name of this axis as specified in Unity Input manager. Setting to an empty string will disable the automatic updating of this axis")]
		public string m_InputAxisName;

		// Token: 0x0400023F RID: 575
		[NoSaveDuringPlay]
		[Tooltip("The value of the input axis.  A value of 0 means no input.  You can drive this directly from a custom input system, or you can set the Axis Name and have the value driven by the internal Input Manager")]
		public float m_InputAxisValue;

		// Token: 0x04000240 RID: 576
		[FormerlySerializedAs("m_InvertAxis")]
		[Tooltip("If checked, then the raw value of the input axis will be inverted before it is used")]
		public bool m_InvertInput;

		// Token: 0x04000241 RID: 577
		[Tooltip("The minimum value for the axis")]
		public float m_MinValue;

		// Token: 0x04000242 RID: 578
		[Tooltip("The maximum value for the axis")]
		public float m_MaxValue;

		// Token: 0x04000243 RID: 579
		[Tooltip("If checked, then the axis will wrap around at the min/max values, forming a loop")]
		public bool m_Wrap;

		// Token: 0x04000244 RID: 580
		[Tooltip("Automatic recentering to at-rest position")]
		public AxisState.Recentering m_Recentering;

		// Token: 0x04000245 RID: 581
		private float m_CurrentSpeed;

		// Token: 0x04000246 RID: 582
		private float m_LastUpdateTime;

		// Token: 0x04000247 RID: 583
		private int m_LastUpdateFrame;

		// Token: 0x04000248 RID: 584
		private const float Epsilon = 0.0001f;

		// Token: 0x04000249 RID: 585
		private AxisState.IInputAxisProvider m_InputAxisProvider;

		// Token: 0x0400024A RID: 586
		private int m_InputAxisIndex;

		// Token: 0x02000062 RID: 98
		public enum SpeedMode
		{
			// Token: 0x0400024E RID: 590
			MaxSpeed,
			// Token: 0x0400024F RID: 591
			InputValueGain
		}

		// Token: 0x02000063 RID: 99
		public interface IInputAxisProvider
		{
			// Token: 0x06000258 RID: 600
			float GetAxisValue(int axis);
		}

		// Token: 0x02000064 RID: 100
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct Recentering
		{
			// Token: 0x06000259 RID: 601 RVA: 0x00010FBC File Offset: 0x0000F1BC
			public Recentering(bool enabled, float waitTime, float recenteringTime)
			{
				this.m_enabled = enabled;
				this.m_WaitTime = waitTime;
				this.m_RecenteringTime = recenteringTime;
				this.mLastAxisInputTime = 0f;
				this.mRecenteringVelocity = 0f;
				this.m_LegacyHeadingDefinition = (this.m_LegacyVelocityFilterStrength = -1);
				this.m_LastUpdateTime = 0f;
			}

			// Token: 0x0600025A RID: 602 RVA: 0x0001100F File Offset: 0x0000F20F
			public void Validate()
			{
				this.m_WaitTime = Mathf.Max(0f, this.m_WaitTime);
				this.m_RecenteringTime = Mathf.Max(0f, this.m_RecenteringTime);
			}

			// Token: 0x0600025B RID: 603 RVA: 0x0001103D File Offset: 0x0000F23D
			public void CopyStateFrom(ref AxisState.Recentering other)
			{
				if (this.mLastAxisInputTime != other.mLastAxisInputTime)
				{
					other.mRecenteringVelocity = 0f;
				}
				this.mLastAxisInputTime = other.mLastAxisInputTime;
			}

			// Token: 0x0600025C RID: 604 RVA: 0x00011064 File Offset: 0x0000F264
			public void CancelRecentering()
			{
				this.mLastAxisInputTime = Time.realtimeSinceStartup;
				this.mRecenteringVelocity = 0f;
			}

			// Token: 0x0600025D RID: 605 RVA: 0x0001107C File Offset: 0x0000F27C
			public void RecenterNow()
			{
				this.mLastAxisInputTime = -1f;
			}

			// Token: 0x0600025E RID: 606 RVA: 0x0001108C File Offset: 0x0000F28C
			public void DoRecentering(ref AxisState axis, float deltaTime, float recenterTarget)
			{
				if (deltaTime > 0f)
				{
					deltaTime = Time.realtimeSinceStartup - this.m_LastUpdateTime;
				}
				this.m_LastUpdateTime = Time.realtimeSinceStartup;
				if (!this.m_enabled && deltaTime >= 0f)
				{
					return;
				}
				recenterTarget = axis.ClampValue(recenterTarget);
				if (deltaTime < 0f)
				{
					this.CancelRecentering();
					if (this.m_enabled)
					{
						axis.Value = recenterTarget;
					}
					return;
				}
				float v = axis.ClampValue(axis.Value);
				float delta = recenterTarget - v;
				if (delta == 0f)
				{
					return;
				}
				if (this.mLastAxisInputTime >= 0f && Time.realtimeSinceStartup < this.mLastAxisInputTime + this.m_WaitTime)
				{
					return;
				}
				float r = axis.m_MaxValue - axis.m_MinValue;
				if (axis.m_Wrap && Mathf.Abs(delta) > r * 0.5f)
				{
					v += Mathf.Sign(recenterTarget - v) * r;
				}
				if (this.m_RecenteringTime < 0.001f || Mathf.Abs(v - recenterTarget) < 0.001f)
				{
					v = recenterTarget;
				}
				else
				{
					v = Mathf.SmoothDamp(v, recenterTarget, ref this.mRecenteringVelocity, this.m_RecenteringTime, 9999f, deltaTime);
				}
				axis.Value = axis.ClampValue(v);
			}

			// Token: 0x0600025F RID: 607 RVA: 0x000111A8 File Offset: 0x0000F3A8
			internal bool LegacyUpgrade(ref int heading, ref int velocityFilter)
			{
				if (this.m_LegacyHeadingDefinition != -1 && this.m_LegacyVelocityFilterStrength != -1)
				{
					heading = this.m_LegacyHeadingDefinition;
					velocityFilter = this.m_LegacyVelocityFilterStrength;
					this.m_LegacyHeadingDefinition = (this.m_LegacyVelocityFilterStrength = -1);
					return true;
				}
				return false;
			}

			// Token: 0x04000250 RID: 592
			[Tooltip("If checked, will enable automatic recentering of the axis. If unchecked, recenting is disabled.")]
			public bool m_enabled;

			// Token: 0x04000251 RID: 593
			[Tooltip("If no user input has been detected on the axis, the axis will wait this long in seconds before recentering.")]
			public float m_WaitTime;

			// Token: 0x04000252 RID: 594
			[Tooltip("How long it takes to reach destination once recentering has started.")]
			public float m_RecenteringTime;

			// Token: 0x04000253 RID: 595
			private float m_LastUpdateTime;

			// Token: 0x04000254 RID: 596
			private float mLastAxisInputTime;

			// Token: 0x04000255 RID: 597
			private float mRecenteringVelocity;

			// Token: 0x04000256 RID: 598
			[SerializeField]
			[HideInInspector]
			[FormerlySerializedAs("m_HeadingDefinition")]
			private int m_LegacyHeadingDefinition;

			// Token: 0x04000257 RID: 599
			[SerializeField]
			[HideInInspector]
			[FormerlySerializedAs("m_VelocityFilterStrength")]
			private int m_LegacyVelocityFilterStrength;
		}
	}
}
