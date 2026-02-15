using System;
using System.Globalization;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B8 RID: 184
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class DoubleField : TextValueField<double>
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001CB6E File Offset: 0x0001AD6E
		private DoubleField.DoubleInput doubleInput
		{
			get
			{
				return (DoubleField.DoubleInput)base.textInputBase;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001CB7C File Offset: 0x0001AD7C
		protected override string ValueToString(double v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		protected override double StringToValue(string str)
		{
			double v;
			return UINumericFieldsUtils.TryConvertStringToDouble(str, base.textInputBase.originalText, out v) ? v : base.rawValue;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001CBDA File Offset: 0x0001ADDA
		public DoubleField()
			: this(null, 1000)
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		public DoubleField(string label, int maxLength = 1000)
			: base(label, maxLength, new DoubleField.DoubleInput())
		{
			base.AddToClassList(DoubleField.ussClassName);
			base.labelElement.AddToClassList(DoubleField.labelUssClassName);
			base.visualInput.AddToClassList(DoubleField.inputUssClassName);
			base.AddLabelDragger<double>();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001CC40 File Offset: 0x0001AE40
		internal override bool CanTryParse(string textString)
		{
			double num;
			return double.TryParse(textString, out num);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001CC55 File Offset: 0x0001AE55
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, double startValue)
		{
			this.doubleInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x040003B2 RID: 946
		public new static readonly string ussClassName = "unity-double-field";

		// Token: 0x040003B3 RID: 947
		public new static readonly string labelUssClassName = DoubleField.ussClassName + "__label";

		// Token: 0x040003B4 RID: 948
		public new static readonly string inputUssClassName = DoubleField.ussClassName + "__input";

		// Token: 0x020000B9 RID: 185
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<DoubleField, DoubleField.UxmlTraits>
		{
		}

		// Token: 0x020000BA RID: 186
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<double, UxmlDoubleAttributeDescription>
		{
		}

		// Token: 0x020000BB RID: 187
		private class DoubleInput : TextValueField<double>.TextValueInput
		{
			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001CCAD File Offset: 0x0001AEAD
			private DoubleField parentDoubleField
			{
				get
				{
					return (DoubleField)base.parent;
				}
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0001CCBA File Offset: 0x0001AEBA
			internal DoubleInput()
			{
				base.formatString = UINumericFieldsUtils.k_DoubleFieldFormatString;
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000607 RID: 1543 RVA: 0x0001CCD0 File Offset: 0x0001AED0
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForFloat;
				}
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x0001CCD8 File Offset: 0x0001AED8
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, double startValue)
			{
				double sensitivity = NumericFieldDraggerUtility.CalculateFloatDragSensitivity(startValue);
				float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				double v = this.StringToValue(base.text);
				v += (double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity;
				v = Mathf.RoundBasedOnMinimumDifference(v, sensitivity);
				bool isDelayed = this.parentDoubleField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(v);
				}
				else
				{
					this.parentDoubleField.value = v;
				}
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0001CD54 File Offset: 0x0001AF54
			protected override string ValueToString(double v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x0001CD74 File Offset: 0x0001AF74
			protected override double StringToValue(string str)
			{
				double v;
				UINumericFieldsUtils.TryConvertStringToDouble(str, base.originalText, out v);
				return v;
			}
		}
	}
}
