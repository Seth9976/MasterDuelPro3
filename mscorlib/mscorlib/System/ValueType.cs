using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides the base class for value types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F9 RID: 505
	[ComVisible(true)]
	[Serializable]
	public abstract class ValueType
	{
		// Token: 0x0600135E RID: 4958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalEquals(object o1, object o2, out object[] fields);

		// Token: 0x0600135F RID: 4959 RVA: 0x0004EE00 File Offset: 0x0004D000
		internal static bool DefaultEquals(object o1, object o2)
		{
			if (o1 == null && o2 == null)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			RuntimeType runtimeType = (RuntimeType)o1.GetType();
			RuntimeType runtimeType2 = (RuntimeType)o2.GetType();
			if (runtimeType != runtimeType2)
			{
				return false;
			}
			object[] array;
			bool flag = ValueType.InternalEquals(o1, o2, out array);
			if (array == null)
			{
				return flag;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				object obj = array[i];
				object obj2 = array[i + 1];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
		/// <param name="obj">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001360 RID: 4960 RVA: 0x0004062F File Offset: 0x0003E82F
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x06001361 RID: 4961
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o, out object[] fields);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001362 RID: 4962 RVA: 0x0004EE80 File Offset: 0x0004D080
		public override int GetHashCode()
		{
			object[] array;
			int num = ValueType.InternalGetHashCode(this, out array);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						num ^= array[i].GetHashCode();
					}
				}
			}
			return num;
		}

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001363 RID: 4963 RVA: 0x0004EEB8 File Offset: 0x0004D0B8
		public override string ToString()
		{
			return base.GetType().FullName;
		}
	}
}
