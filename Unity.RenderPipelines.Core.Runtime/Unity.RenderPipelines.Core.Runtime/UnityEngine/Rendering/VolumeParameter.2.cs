using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F0 RID: 496
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class VolumeParameter<T> : VolumeParameter, IEquatable<VolumeParameter<T>>
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00033E2B File Offset: 0x0003202B
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00033E33 File Offset: 0x00032033
		public virtual T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00033E3C File Offset: 0x0003203C
		public VolumeParameter()
			: this(default(T), false)
		{
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00033E59 File Offset: 0x00032059
		protected VolumeParameter(T value, bool overrideState = false)
		{
			this.m_Value = value;
			this.overrideState = overrideState;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00033E6F File Offset: 0x0003206F
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			this.Interp((from as VolumeParameter<T>).value, (to as VolumeParameter<T>).value, t);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00033E8E File Offset: 0x0003208E
		public virtual void Interp(T from, T to, float t)
		{
			this.m_Value = ((t > 0f) ? to : from);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00033EA2 File Offset: 0x000320A2
		public void Override(T x)
		{
			this.overrideState = true;
			this.m_Value = x;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00033EB2 File Offset: 0x000320B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void SetValue(VolumeParameter parameter)
		{
			this.m_Value = ((VolumeParameter<T>)parameter).m_Value;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00033EC8 File Offset: 0x000320C8
		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + this.overrideState.GetHashCode();
			if (!EqualityComparer<T>.Default.Equals(this.value, default(T)))
			{
				int num = hash * 23;
				T value = this.value;
				hash = num + value.GetHashCode();
			}
			return hash;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00033F22 File Offset: 0x00032122
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.value, this.overrideState);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00033F44 File Offset: 0x00032144
		public static bool operator ==(VolumeParameter<T> lhs, T rhs)
		{
			if (lhs != null && lhs.value != null)
			{
				T value = lhs.value;
				return value.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00033F7D File Offset: 0x0003217D
		public static bool operator !=(VolumeParameter<T> lhs, T rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00033F89 File Offset: 0x00032189
		public bool Equals(VolumeParameter<T> other)
		{
			return other != null && (this == other || EqualityComparer<T>.Default.Equals(this.m_Value, other.m_Value));
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00033FAC File Offset: 0x000321AC
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((VolumeParameter<T>)obj)));
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00033FDA File Offset: 0x000321DA
		public override object Clone()
		{
			return new VolumeParameter<T>(base.GetValue<T>(), this.overrideState);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00033E2B File Offset: 0x0003202B
		public static explicit operator T(VolumeParameter<T> prop)
		{
			return prop.m_Value;
		}

		// Token: 0x0400094A RID: 2378
		[SerializeField]
		protected T m_Value;
	}
}
