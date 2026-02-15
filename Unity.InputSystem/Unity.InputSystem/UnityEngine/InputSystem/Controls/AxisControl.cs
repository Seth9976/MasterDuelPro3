using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000214 RID: 532
	public class AxisControl : InputControl<float>
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x0005B150 File Offset: 0x00059350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected float Preprocess(float value)
		{
			if (this.scale)
			{
				value *= this.scaleFactor;
			}
			if (this.clamp == AxisControl.Clamp.ToConstantBeforeNormalize)
			{
				if (value < this.clampMin || value > this.clampMax)
				{
					value = this.clampConstant;
				}
			}
			else if (this.clamp == AxisControl.Clamp.BeforeNormalize)
			{
				value = Mathf.Clamp(value, this.clampMin, this.clampMax);
			}
			if (this.normalize)
			{
				value = NormalizeProcessor.Normalize(value, this.normalizeMin, this.normalizeMax, this.normalizeZero);
			}
			if (this.clamp == AxisControl.Clamp.AfterNormalize)
			{
				value = Mathf.Clamp(value, this.clampMin, this.clampMax);
			}
			if (this.invert)
			{
				value *= -1f;
			}
			return value;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0005B204 File Offset: 0x00059404
		private float Unpreprocess(float value)
		{
			if (this.invert)
			{
				value *= -1f;
			}
			if (this.normalize)
			{
				value = NormalizeProcessor.Denormalize(value, this.normalizeMin, this.normalizeMax, this.normalizeZero);
			}
			if (this.scale)
			{
				value /= this.scaleFactor;
			}
			return value;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0005B257 File Offset: 0x00059457
		public AxisControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatFloat;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0005B270 File Offset: 0x00059470
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.hasDefaultState && this.normalize && Mathf.Abs(this.normalizeZero) > Mathf.Epsilon)
			{
				this.m_DefaultState = base.stateBlock.FloatToPrimitiveValue(this.normalizeZero);
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0005B2C0 File Offset: 0x000594C0
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			int num = this.m_OptimizedControlDataType;
			if (num != 1113150533)
			{
				if (num == 1179407392)
				{
					return *(float*)((byte*)statePtr + this.m_StateBlock.m_ByteOffset);
				}
				float value = base.stateBlock.ReadFloat(statePtr);
				return this.Preprocess(value);
			}
			else
			{
				if (((byte*)statePtr)[this.m_StateBlock.m_ByteOffset] == 0)
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0005B330 File Offset: 0x00059530
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			int num = this.m_OptimizedControlDataType;
			if (num == 1113150533)
			{
				((byte*)statePtr)[this.m_StateBlock.m_ByteOffset] = ((value >= 0.5f) ? 1 : 0);
				return;
			}
			if (num == 1179407392)
			{
				*(float*)((byte*)statePtr + this.m_StateBlock.m_ByteOffset) = value;
				return;
			}
			value = this.Unpreprocess(value);
			base.stateBlock.WriteFloat(statePtr, value);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0005B3A0 File Offset: 0x000595A0
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			float num = base.ReadValueFromState(firstStatePtr);
			float valueInState = base.ReadValueFromState(secondStatePtr);
			return !Mathf.Approximately(num, valueInState);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0005B3C5 File Offset: 0x000595C5
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return this.EvaluateMagnitude(base.ReadValueFromStateWithCaching(statePtr));
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0005B3D4 File Offset: 0x000595D4
		private float EvaluateMagnitude(float value)
		{
			if (this.m_MinValue.isEmpty || this.m_MaxValue.isEmpty)
			{
				return Mathf.Abs(value);
			}
			float min = this.m_MinValue.ToSingle(null);
			float max = this.m_MaxValue.ToSingle(null);
			float clampedValue = Mathf.Clamp(value, min, max);
			if (min >= 0f)
			{
				return NormalizeProcessor.Normalize(clampedValue, min, max, 0f);
			}
			if (clampedValue < 0f)
			{
				return NormalizeProcessor.Normalize(Mathf.Abs(clampedValue), 0f, Mathf.Abs(min), 0f);
			}
			return NormalizeProcessor.Normalize(clampedValue, 0f, max, 0f);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0005B470 File Offset: 0x00059670
		protected override FourCC CalculateOptimizedControlDataType()
		{
			bool noProcessingNeeded = this.clamp == AxisControl.Clamp.None && !this.invert && !this.normalize && !this.scale;
			if (noProcessingNeeded && this.m_StateBlock.format == InputStateBlock.FormatFloat && this.m_StateBlock.sizeInBits == 32U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatFloat;
			}
			if (noProcessingNeeded && this.m_StateBlock.format == InputStateBlock.FormatBit && this.m_StateBlock.sizeInBits == 8U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatByte;
			}
			return InputStateBlock.FormatInvalid;
		}

		// Token: 0x04000BC6 RID: 3014
		public AxisControl.Clamp clamp;

		// Token: 0x04000BC7 RID: 3015
		public float clampMin;

		// Token: 0x04000BC8 RID: 3016
		public float clampMax;

		// Token: 0x04000BC9 RID: 3017
		public float clampConstant;

		// Token: 0x04000BCA RID: 3018
		public bool invert;

		// Token: 0x04000BCB RID: 3019
		public bool normalize;

		// Token: 0x04000BCC RID: 3020
		public float normalizeMin;

		// Token: 0x04000BCD RID: 3021
		public float normalizeMax;

		// Token: 0x04000BCE RID: 3022
		public float normalizeZero;

		// Token: 0x04000BCF RID: 3023
		public bool scale;

		// Token: 0x04000BD0 RID: 3024
		public float scaleFactor;

		// Token: 0x02000215 RID: 533
		public enum Clamp
		{
			// Token: 0x04000BD2 RID: 3026
			None,
			// Token: 0x04000BD3 RID: 3027
			BeforeNormalize,
			// Token: 0x04000BD4 RID: 3028
			AfterNormalize,
			// Token: 0x04000BD5 RID: 3029
			ToConstantBeforeNormalize
		}
	}
}
