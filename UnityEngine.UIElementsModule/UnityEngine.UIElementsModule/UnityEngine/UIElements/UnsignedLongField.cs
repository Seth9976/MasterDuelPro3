using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200016D RID: 365
	public class UnsignedLongField : TextValueField<ulong>
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0003552E File Offset: 0x0003372E
		private UnsignedLongField.UnsignedLongInput unsignedLongInput
		{
			get
			{
				return (UnsignedLongField.UnsignedLongInput)base.textInputBase;
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0003553C File Offset: 0x0003373C
		protected override string ValueToString(ulong v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00035568 File Offset: 0x00033768
		protected override ulong StringToValue(string str)
		{
			ulong v;
			return UINumericFieldsUtils.TryConvertStringToULong(str, base.textInputBase.originalText, out v) ? v : base.rawValue;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0003559A File Offset: 0x0003379A
		public UnsignedLongField()
			: this(null, 1000)
		{
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000355AC File Offset: 0x000337AC
		public UnsignedLongField(string label, int maxLength = 1000)
			: base(label, maxLength, new UnsignedLongField.UnsignedLongInput())
		{
			base.AddToClassList(UnsignedLongField.ussClassName);
			base.labelElement.AddToClassList(UnsignedLongField.labelUssClassName);
			base.visualInput.AddToClassList(UnsignedLongField.inputUssClassName);
			base.AddLabelDragger<ulong>();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00035600 File Offset: 0x00033800
		internal override bool CanTryParse(string textString)
		{
			ulong num;
			return ulong.TryParse(textString, out num);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00035615 File Offset: 0x00033815
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, ulong startValue)
		{
			this.unsignedLongInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x04000714 RID: 1812
		public new static readonly string ussClassName = "unity-unsigned-long-field";

		// Token: 0x04000715 RID: 1813
		public new static readonly string labelUssClassName = UnsignedLongField.ussClassName + "__label";

		// Token: 0x04000716 RID: 1814
		public new static readonly string inputUssClassName = UnsignedLongField.ussClassName + "__input";

		// Token: 0x0200016E RID: 366
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<UnsignedLongField, UnsignedLongField.UxmlTraits>
		{
		}

		// Token: 0x0200016F RID: 367
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<ulong, UxmlUnsignedLongAttributeDescription>
		{
		}

		// Token: 0x02000170 RID: 368
		private class UnsignedLongInput : TextValueField<ulong>.TextValueInput
		{
			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0003566D File Offset: 0x0003386D
			private UnsignedLongField parentUnsignedLongField
			{
				get
				{
					return (UnsignedLongField)base.parent;
				}
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x0003567A File Offset: 0x0003387A
			internal UnsignedLongInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000244C4 File Offset: 0x000226C4
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x00035690 File Offset: 0x00033890
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, ulong startValue)
			{
				double sensitivity = NumericFieldDraggerUtility.CalculateIntDragSensitivity(startValue);
				float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				ulong v = this.StringToValue(base.text);
				long niceDelta = (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity);
				v = this.ClampToMinMaxULongValue(niceDelta, v);
				bool isDelayed = this.parentUnsignedLongField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(v);
				}
				else
				{
					this.parentUnsignedLongField.value = v;
				}
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x00035714 File Offset: 0x00033914
			private ulong ClampToMinMaxULongValue(long niceDelta, ulong value)
			{
				ulong niceDeltaAbs = (ulong)Math.Abs(niceDelta);
				bool flag = niceDelta > 0L;
				ulong num;
				if (flag)
				{
					bool flag2 = niceDeltaAbs > ulong.MaxValue - value;
					if (flag2)
					{
						num = ulong.MaxValue;
					}
					else
					{
						num = value + niceDeltaAbs;
					}
				}
				else
				{
					bool flag3 = niceDeltaAbs > value;
					if (flag3)
					{
						num = 0UL;
					}
					else
					{
						num = value - niceDeltaAbs;
					}
				}
				return num;
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x00035764 File Offset: 0x00033964
			protected override string ValueToString(ulong v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x06000AEB RID: 2795 RVA: 0x00035784 File Offset: 0x00033984
			protected override ulong StringToValue(string str)
			{
				ulong v;
				UINumericFieldsUtils.TryConvertStringToULong(str, base.originalText, out v);
				return v;
			}
		}
	}
}
