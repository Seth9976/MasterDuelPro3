using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents a field using an internal metadata token.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DE RID: 478
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeFieldHandle : ISerializable
	{
		// Token: 0x0600127C RID: 4732 RVA: 0x0004AE30 File Offset: 0x00049030
		internal RuntimeFieldHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0004AE3C File Offset: 0x0004903C
		private RuntimeFieldHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeFieldInfo runtimeFieldInfo = (RuntimeFieldInfo)info.GetValue("FieldObj", typeof(RuntimeFieldInfo));
			this.value = runtimeFieldInfo.FieldHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		/// <summary>Gets a handle to the field represented by the current instance.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the handle to the field represented by the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0004AEA3 File Offset: 0x000490A3
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by the current instance.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with serialization information. </param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="P:System.RuntimeFieldHandle.Value" /> property of the current instance is not a valid handle. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600127F RID: 4735 RVA: 0x0004AEAC File Offset: 0x000490AC
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("FieldObj", (RuntimeFieldInfo)FieldInfo.GetFieldFromHandle(this), typeof(RuntimeFieldInfo));
		}

		/// <summary>Indicates whether the current instance is equal to the specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.RuntimeFieldHandle" /> and equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001280 RID: 4736 RVA: 0x0004AF0C File Offset: 0x0004910C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeFieldHandle)obj).Value;
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06001281 RID: 4737 RVA: 0x0004AF54 File Offset: 0x00049154
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001282 RID: 4738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06001283 RID: 4739 RVA: 0x0004AF61 File Offset: 0x00049161
		internal static void SetValue(RuntimeFieldInfo field, object obj, object value, RuntimeType fieldType, FieldAttributes fieldAttr, RuntimeType declaringType, ref bool domainInitialized)
		{
			RuntimeFieldHandle.SetValueInternal(field, obj, value);
		}

		// Token: 0x06001284 RID: 4740
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetValueDirect(RuntimeFieldInfo field, RuntimeType fieldType, void* pTypedRef, object value, RuntimeType contextType);

		// Token: 0x0400077C RID: 1916
		private IntPtr value;
	}
}
