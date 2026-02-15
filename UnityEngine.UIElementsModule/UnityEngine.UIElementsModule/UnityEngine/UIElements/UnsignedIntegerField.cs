using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000169 RID: 361
	public class UnsignedIntegerField : TextValueField<uint>
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00035302 File Offset: 0x00033502
		private UnsignedIntegerField.UnsignedIntegerInput integerInput
		{
			get
			{
				return (UnsignedIntegerField.UnsignedIntegerInput)base.textInputBase;
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00035310 File Offset: 0x00033510
		protected override string ValueToString(uint v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0003533C File Offset: 0x0003353C
		protected override uint StringToValue(string str)
		{
			uint v;
			return UINumericFieldsUtils.TryConvertStringToUInt(str, base.textInputBase.originalText, out v) ? v : base.rawValue;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0003536E File Offset: 0x0003356E
		public UnsignedIntegerField()
			: this(null, 1000)
		{
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00035380 File Offset: 0x00033580
		public UnsignedIntegerField(string label, int maxLength = 1000)
			: base(label, maxLength, new UnsignedIntegerField.UnsignedIntegerInput())
		{
			base.AddToClassList(UnsignedIntegerField.ussClassName);
			base.labelElement.AddToClassList(UnsignedIntegerField.labelUssClassName);
			base.visualInput.AddToClassList(UnsignedIntegerField.inputUssClassName);
			base.AddLabelDragger<uint>();
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000353D4 File Offset: 0x000335D4
		internal override bool CanTryParse(string textString)
		{
			uint num;
			return uint.TryParse(textString, out num);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000353E9 File Offset: 0x000335E9
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, uint startValue)
		{
			this.integerInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x04000711 RID: 1809
		public new static readonly string ussClassName = "unity-unsigned-integer-field";

		// Token: 0x04000712 RID: 1810
		public new static readonly string labelUssClassName = UnsignedIntegerField.ussClassName + "__label";

		// Token: 0x04000713 RID: 1811
		public new static readonly string inputUssClassName = UnsignedIntegerField.ussClassName + "__input";

		// Token: 0x0200016A RID: 362
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<UnsignedIntegerField, UnsignedIntegerField.UxmlTraits>
		{
		}

		// Token: 0x0200016B RID: 363
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<uint, UxmlUnsignedIntAttributeDescription>
		{
		}

		// Token: 0x0200016C RID: 364
		private class UnsignedIntegerInput : TextValueField<uint>.TextValueInput
		{
			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00035441 File Offset: 0x00033641
			private UnsignedIntegerField parentUnsignedIntegerField
			{
				get
				{
					return (UnsignedIntegerField)base.parent;
				}
			}

			// Token: 0x06000AD6 RID: 2774 RVA: 0x0003544E File Offset: 0x0003364E
			internal UnsignedIntegerInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x000244C4 File Offset: 0x000226C4
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x00035464 File Offset: 0x00033664
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, uint startValue)
			{
				double sensitivity = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity((long)((ulong)startValue));
				float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				long v = (long)((ulong)this.StringToValue(base.text));
				v += (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity);
				bool isDelayed = this.parentUnsignedIntegerField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(Mathf.ClampToUInt(v));
				}
				else
				{
					this.parentUnsignedIntegerField.value = Mathf.ClampToUInt(v);
				}
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x000354EC File Offset: 0x000336EC
			protected override string ValueToString(uint v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x0003550C File Offset: 0x0003370C
			protected override uint StringToValue(string str)
			{
				uint v;
				UINumericFieldsUtils.TryConvertStringToUInt(str, base.originalText, out v);
				return v;
			}
		}
	}
}
