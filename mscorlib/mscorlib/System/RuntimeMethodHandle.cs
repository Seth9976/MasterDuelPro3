using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	/// <summary>
	///   <see cref="T:System.RuntimeMethodHandle" /> is a handle to the internal metadata representation of a method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DF RID: 479
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001285 RID: 4741 RVA: 0x0004AF6B File Offset: 0x0004916B
		internal RuntimeMethodHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004AF74 File Offset: 0x00049174
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)info.GetValue("MethodObj", typeof(RuntimeMethodInfo));
			this.value = runtimeMethodInfo.MethodHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		/// <summary>Gets the value of this instance.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> that is the internal metadata representation of a method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004AFDB File Offset: 0x000491DB
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by this instance.</summary>
		/// <param name="info">The object to populate with serialization information. </param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeMethodHandle.Value" /> is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001288 RID: 4744 RVA: 0x0004AFE4 File Offset: 0x000491E4
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
			info.AddValue("MethodObj", (RuntimeMethodInfo)MethodBase.GetMethodFromHandle(this), typeof(RuntimeMethodInfo));
		}

		/// <summary>Indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.RuntimeMethodHandle" /> and equal to the value of this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Object" /> to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001289 RID: 4745 RVA: 0x0004B044 File Offset: 0x00049244
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeMethodHandle)obj).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600128A RID: 4746 RVA: 0x0004B08C File Offset: 0x0004928C
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0004B09C File Offset: 0x0004929C
		internal static string ConstructInstantiation(RuntimeMethodInfo method, TypeNameFormatFlags format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type[] genericArguments = method.GetGenericArguments();
			stringBuilder.Append("[");
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(genericArguments[i].Name);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0004B101 File Offset: 0x00049301
		internal bool IsNullHandle()
		{
			return this.value == IntPtr.Zero;
		}

		// Token: 0x0400077D RID: 1917
		private IntPtr value;
	}
}
