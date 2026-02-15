using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001EF RID: 495
	public abstract class VolumeParameter : ICloneable
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00033DCF File Offset: 0x00031FCF
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00033DD7 File Offset: 0x00031FD7
		public virtual bool overrideState
		{
			get
			{
				return this.m_OverrideState;
			}
			set
			{
				this.m_OverrideState = value;
			}
		}

		// Token: 0x06000DFA RID: 3578
		internal abstract void Interp(VolumeParameter from, VolumeParameter to, float t);

		// Token: 0x06000DFB RID: 3579 RVA: 0x00033DE0 File Offset: 0x00031FE0
		public T GetValue<T>()
		{
			return ((VolumeParameter<T>)this).value;
		}

		// Token: 0x06000DFC RID: 3580
		public abstract void SetValue(VolumeParameter parameter);

		// Token: 0x06000DFD RID: 3581 RVA: 0x00005704 File Offset: 0x00003904
		protected internal virtual void OnEnable()
		{
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00005704 File Offset: 0x00003904
		protected internal virtual void OnDisable()
		{
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00033DED File Offset: 0x00031FED
		public static bool IsObjectParameter(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObjectParameter<>)) || (type.BaseType != null && VolumeParameter.IsObjectParameter(type.BaseType));
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void Release()
		{
		}

		// Token: 0x06000E01 RID: 3585
		public abstract object Clone();

		// Token: 0x04000948 RID: 2376
		public const string k_DebuggerDisplay = "{m_Value} ({m_OverrideState})";

		// Token: 0x04000949 RID: 2377
		[SerializeField]
		protected bool m_OverrideState;
	}
}
