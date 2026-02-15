using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000058 RID: 88
	public struct InputBindingCompositeContext
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		public unsafe IEnumerable<InputBindingCompositeContext.PartBinding> controls
		{
			get
			{
				if (this.m_State == null)
				{
					yield break;
				}
				int totalBindingCount = this.m_State.totalBindingCount;
				int num;
				for (int bindingIndex = this.m_BindingIndex + 1; bindingIndex < totalBindingCount; bindingIndex = num)
				{
					InputActionState.BindingState bindingState = *this.m_State.GetBindingState(bindingIndex);
					if (!bindingState.isPartOfComposite)
					{
						break;
					}
					int controlStartIndex = bindingState.controlStartIndex;
					for (int i = 0; i < bindingState.controlCount; i = num)
					{
						InputControl control = this.m_State.controls[controlStartIndex + i];
						yield return new InputBindingCompositeContext.PartBinding
						{
							part = bindingState.partIndex,
							control = control
						};
						num = i + 1;
					}
					num = bindingIndex + 1;
				}
				yield break;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00010FB5 File Offset: 0x0000F1B5
		public float EvaluateMagnitude(int partNumber)
		{
			return this.m_State.EvaluateCompositePartMagnitude(this.m_BindingIndex, partNumber);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public TValue ReadValue<TValue>(int partNumber) where TValue : struct, IComparable<TValue>
		{
			if (this.m_State == null)
			{
				return default(TValue);
			}
			int num;
			return this.m_State.ReadCompositePartValue<TValue, InputBindingCompositeContext.DefaultComparer<TValue>>(this.m_BindingIndex, partNumber, null, out num, default(InputBindingCompositeContext.DefaultComparer<TValue>));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001100C File Offset: 0x0000F20C
		public TValue ReadValue<TValue>(int partNumber, out InputControl sourceControl) where TValue : struct, IComparable<TValue>
		{
			if (this.m_State == null)
			{
				sourceControl = null;
				return default(TValue);
			}
			int controlIndex;
			TValue tvalue = this.m_State.ReadCompositePartValue<TValue, InputBindingCompositeContext.DefaultComparer<TValue>>(this.m_BindingIndex, partNumber, null, out controlIndex, default(InputBindingCompositeContext.DefaultComparer<TValue>));
			if (controlIndex != -1)
			{
				sourceControl = this.m_State.controls[controlIndex];
				return tvalue;
			}
			sourceControl = null;
			return tvalue;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00011064 File Offset: 0x0000F264
		public TValue ReadValue<TValue, TComparer>(int partNumber, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			if (this.m_State == null)
			{
				return default(TValue);
			}
			int num;
			return this.m_State.ReadCompositePartValue<TValue, TComparer>(this.m_BindingIndex, partNumber, null, out num, comparer);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001109C File Offset: 0x0000F29C
		public TValue ReadValue<TValue, TComparer>(int partNumber, out InputControl sourceControl, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			if (this.m_State == null)
			{
				sourceControl = null;
				return default(TValue);
			}
			int controlIndex;
			TValue tvalue = this.m_State.ReadCompositePartValue<TValue, TComparer>(this.m_BindingIndex, partNumber, null, out controlIndex, comparer);
			if (controlIndex != -1)
			{
				sourceControl = this.m_State.controls[controlIndex];
				return tvalue;
			}
			sourceControl = null;
			return tvalue;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000110EC File Offset: 0x0000F2EC
		public unsafe bool ReadValueAsButton(int partNumber)
		{
			if (this.m_State == null)
			{
				return false;
			}
			bool buttonValue = false;
			int num;
			this.m_State.ReadCompositePartValue<float, InputBindingCompositeContext.DefaultComparer<float>>(this.m_BindingIndex, partNumber, &buttonValue, out num, default(InputBindingCompositeContext.DefaultComparer<float>));
			return buttonValue;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011127 File Offset: 0x0000F327
		public unsafe void ReadValue(int partNumber, void* buffer, int bufferSize)
		{
			InputActionState state = this.m_State;
			if (state == null)
			{
				return;
			}
			state.ReadCompositePartValue(this.m_BindingIndex, partNumber, buffer, bufferSize);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011143 File Offset: 0x0000F343
		public object ReadValueAsObject(int partNumber)
		{
			return this.m_State.ReadCompositePartValueAsObject(this.m_BindingIndex, partNumber);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00011157 File Offset: 0x0000F357
		public double GetPressTime(int partNumber)
		{
			return this.m_State.GetCompositePartPressTime(this.m_BindingIndex, partNumber);
		}

		// Token: 0x04000209 RID: 521
		internal InputActionState m_State;

		// Token: 0x0400020A RID: 522
		internal int m_BindingIndex;

		// Token: 0x02000059 RID: 89
		public struct PartBinding
		{
			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001116B File Offset: 0x0000F36B
			// (set) Token: 0x06000424 RID: 1060 RVA: 0x00011173 File Offset: 0x0000F373
			public int part { readonly get; set; }

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001117C File Offset: 0x0000F37C
			// (set) Token: 0x06000426 RID: 1062 RVA: 0x00011184 File Offset: 0x0000F384
			public InputControl control { readonly get; set; }
		}

		// Token: 0x0200005A RID: 90
		private struct DefaultComparer<TValue> : IComparer<TValue> where TValue : IComparable<TValue>
		{
			// Token: 0x06000427 RID: 1063 RVA: 0x0001118D File Offset: 0x0000F38D
			public int Compare(TValue x, TValue y)
			{
				return x.CompareTo(y);
			}
		}
	}
}
