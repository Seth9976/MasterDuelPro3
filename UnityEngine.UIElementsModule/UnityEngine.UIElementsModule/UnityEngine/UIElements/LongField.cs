using System;
using System.Globalization;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F8 RID: 248
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class LongField : TextValueField<long>
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000248BC File Offset: 0x00022ABC
		private LongField.LongInput longInput
		{
			get
			{
				return (LongField.LongInput)base.textInputBase;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000248CC File Offset: 0x00022ACC
		protected override string ValueToString(long v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000248F8 File Offset: 0x00022AF8
		protected override long StringToValue(string str)
		{
			long v;
			return UINumericFieldsUtils.TryConvertStringToLong(str, base.textInputBase.originalText, out v) ? v : base.rawValue;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002492A File Offset: 0x00022B2A
		public LongField()
			: this(null, 1000)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002493C File Offset: 0x00022B3C
		public LongField(string label, int maxLength = 1000)
			: base(label, maxLength, new LongField.LongInput())
		{
			base.AddToClassList(LongField.ussClassName);
			base.labelElement.AddToClassList(LongField.labelUssClassName);
			base.visualInput.AddToClassList(LongField.inputUssClassName);
			base.AddLabelDragger<long>();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00024990 File Offset: 0x00022B90
		internal override bool CanTryParse(string textString)
		{
			long num;
			return long.TryParse(textString, out num);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000249A5 File Offset: 0x00022BA5
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, long startValue)
		{
			this.longInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x040004B7 RID: 1207
		public new static readonly string ussClassName = "unity-long-field";

		// Token: 0x040004B8 RID: 1208
		public new static readonly string labelUssClassName = LongField.ussClassName + "__label";

		// Token: 0x040004B9 RID: 1209
		public new static readonly string inputUssClassName = LongField.ussClassName + "__input";

		// Token: 0x020000F9 RID: 249
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<LongField, LongField.UxmlTraits>
		{
		}

		// Token: 0x020000FA RID: 250
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<long, UxmlLongAttributeDescription>
		{
		}

		// Token: 0x020000FB RID: 251
		private class LongInput : TextValueField<long>.TextValueInput
		{
			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060007AA RID: 1962 RVA: 0x000249FD File Offset: 0x00022BFD
			private LongField parentLongField
			{
				get
				{
					return (LongField)base.parent;
				}
			}

			// Token: 0x060007AB RID: 1963 RVA: 0x00024A0A File Offset: 0x00022C0A
			internal LongInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060007AC RID: 1964 RVA: 0x00024A20 File Offset: 0x00022C20
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x00024A38 File Offset: 0x00022C38
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, long startValue)
			{
				double sensitivity = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity(startValue);
				float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				long v = this.StringToValue(base.text);
				long niceDelta = (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity);
				v = this.ClampMinMaxLongValue(niceDelta, v);
				bool isDelayed = this.parentLongField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(v);
				}
				else
				{
					this.parentLongField.value = v;
				}
			}

			// Token: 0x060007AE RID: 1966 RVA: 0x00024ABC File Offset: 0x00022CBC
			private long ClampMinMaxLongValue(long niceDelta, long value)
			{
				long niceDeltaAbs = Math.Abs(niceDelta);
				bool flag = niceDelta > 0L;
				long num;
				if (flag)
				{
					bool flag2 = value > 0L && niceDeltaAbs > long.MaxValue - value;
					if (flag2)
					{
						num = long.MaxValue;
					}
					else
					{
						num = value + niceDelta;
					}
				}
				else
				{
					bool flag3 = value < 0L && value < long.MinValue + niceDeltaAbs;
					if (flag3)
					{
						num = long.MinValue;
					}
					else
					{
						num = value - niceDeltaAbs;
					}
				}
				return num;
			}

			// Token: 0x060007AF RID: 1967 RVA: 0x00024B38 File Offset: 0x00022D38
			protected override string ValueToString(long v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x060007B0 RID: 1968 RVA: 0x00024B58 File Offset: 0x00022D58
			protected override long StringToValue(string str)
			{
				long v;
				UINumericFieldsUtils.TryConvertStringToLong(str, base.originalText, out v);
				return v;
			}
		}
	}
}
