using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Describes objects that contain both a managed pointer to a location and a runtime representation of the type that may be stored at that location.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B1 RID: 433
	[CLSCompliant(false)]
	[ComVisible(true)]
	[NonVersionable]
	public ref struct TypedReference
	{
		/// <summary>Makes a TypedReference for a field identified by a specified object and list of field descriptions.</summary>
		/// <returns>A <see cref="T:System.TypedReference" /> for the field described by the last element of <paramref name="flds" />.</returns>
		/// <param name="target">An object that contains the field described by the first element of <paramref name="flds" />. </param>
		/// <param name="flds">A list of field descriptions where each element describes a field that contains the field described by the succeeding element. Each described field must be a value type. The field descriptions must be RuntimeFieldInfo objects supplied by the type system.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> or <paramref name="flds" /> is null.-or- An element of <paramref name="flds" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flds" /> array has no elements.-or- An element of <paramref name="flds" /> is not a RuntimeFieldInfo.-or- The <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> or <see cref="P:System.Reflection.FieldInfo.IsStatic" /> property of an element of <paramref name="flds" /> is true. </exception>
		/// <exception cref="T:System.MissingMemberException">Parameter <paramref name="target" /> does not contain the field described by the first element of <paramref name="flds" />, or an element of <paramref name="flds" /> describes a field that is not contained in the field described by the succeeding element of <paramref name="flds" />.-or- The field described by an element of <paramref name="flds" /> is not a value type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001042 RID: 4162 RVA: 0x00044E34 File Offset: 0x00043034
		[CLSCompliant(false)]
		public unsafe static TypedReference MakeTypedReference(object target, FieldInfo[] flds)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (flds == null)
			{
				throw new ArgumentNullException("flds");
			}
			if (flds.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Array must not be of length zero."), "flds");
			}
			IntPtr[] array = new IntPtr[flds.Length];
			RuntimeType runtimeType = (RuntimeType)target.GetType();
			for (int i = 0; i < flds.Length; i++)
			{
				RuntimeFieldInfo runtimeFieldInfo = flds[i] as RuntimeFieldInfo;
				if (runtimeFieldInfo == null)
				{
					throw new ArgumentException(Environment.GetResourceString("FieldInfo must be a runtime FieldInfo object."));
				}
				if (runtimeFieldInfo.IsStatic)
				{
					throw new ArgumentException(Environment.GetResourceString("Field in TypedReferences cannot be static or init only."));
				}
				if (runtimeType != runtimeFieldInfo.GetDeclaringTypeInternal() && !runtimeType.IsSubclassOf(runtimeFieldInfo.GetDeclaringTypeInternal()))
				{
					throw new MissingMemberException(Environment.GetResourceString("FieldInfo does not match the target Type."));
				}
				RuntimeType runtimeType2 = (RuntimeType)runtimeFieldInfo.FieldType;
				if (runtimeType2.IsPrimitive)
				{
					throw new ArgumentException(Environment.GetResourceString("TypedReferences cannot be redefined as primitives."));
				}
				if (i < flds.Length - 1 && !runtimeType2.IsValueType)
				{
					throw new MissingMemberException(Environment.GetResourceString("TypedReference can only be made on nested value Types."));
				}
				array[i] = runtimeFieldInfo.FieldHandle.Value;
				runtimeType = runtimeType2;
			}
			TypedReference typedReference = default(TypedReference);
			TypedReference.InternalMakeTypedReference((void*)(&typedReference), target, array, runtimeType);
			return typedReference;
		}

		// Token: 0x06001043 RID: 4163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalMakeTypedReference(void* result, object target, IntPtr[] flds, RuntimeType lastFieldType);

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>The hash code of this object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001044 RID: 4164 RVA: 0x00044F7C File Offset: 0x0004317C
		public override int GetHashCode()
		{
			if (this.Type == IntPtr.Zero)
			{
				return 0;
			}
			return __reftype(this).GetHashCode();
		}

		/// <summary>Checks if this object is equal to the specified object.</summary>
		/// <returns>true if this object is equal to the specified object; otherwise, false.</returns>
		/// <param name="o">The object with which to compare the current object. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001045 RID: 4165 RVA: 0x00044FA4 File Offset: 0x000431A4
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Environment.GetResourceString("This feature is not currently implemented."));
		}

		/// <summary>Converts the specified TypedReference to an Object.</summary>
		/// <returns>An <see cref="T:System.Object" /> converted from a TypedReference.</returns>
		/// <param name="value">The TypedReference to be converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001046 RID: 4166 RVA: 0x00044FB5 File Offset: 0x000431B5
		public unsafe static object ToObject(TypedReference value)
		{
			return TypedReference.InternalToObject((void*)(&value));
		}

		// Token: 0x06001047 RID: 4167
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object InternalToObject(void* value);

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00044FBF File Offset: 0x000431BF
		internal bool IsNull
		{
			get
			{
				return this.Value == IntPtr.Zero && this.Type == IntPtr.Zero;
			}
		}

		/// <summary>Converts the specified value to a TypedReference. This method is not supported.</summary>
		/// <param name="target">The target of the conversion. </param>
		/// <param name="value">The value to be converted. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001049 RID: 4169 RVA: 0x00044FE5 File Offset: 0x000431E5
		[CLSCompliant(false)]
		public static void SetTypedReference(TypedReference target, object value)
		{
			throw new NotImplementedException("SetTypedReference");
		}

		// Token: 0x040006AC RID: 1708
		private RuntimeTypeHandle type;

		// Token: 0x040006AD RID: 1709
		private IntPtr Value;

		// Token: 0x040006AE RID: 1710
		private IntPtr Type;
	}
}
