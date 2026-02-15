using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	/// <summary>Represents a type using an internal metadata token.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E0 RID: 480
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x0600128D RID: 4749 RVA: 0x0004B113 File Offset: 0x00049313
		internal RuntimeTypeHandle(IntPtr val)
		{
			this.value = val;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0004B11C File Offset: 0x0004931C
		internal RuntimeTypeHandle(RuntimeType type)
		{
			this = new RuntimeTypeHandle(type._impl.value);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0004B130 File Offset: 0x00049330
		private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeType runtimeType = (RuntimeType)info.GetValue("TypeObj", typeof(RuntimeType));
			this.value = runtimeType.TypeHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		/// <summary>Gets a handle to the type represented by this instance.</summary>
		/// <returns>A handle to the type represented by this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0004B197 File Offset: 0x00049397
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the type represented by the current instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">(Reserved) The location where serialized data will be stored and retrieved. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeTypeHandle.Value" /> is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001291 RID: 4753 RVA: 0x0004B1A0 File Offset: 0x000493A0
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
			info.AddValue("TypeObj", Type.GetTypeHandle(this), typeof(RuntimeType));
		}

		/// <summary>Indicates whether the specified object is equal to the current <see cref="T:System.RuntimeTypeHandle" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.RuntimeTypeHandle" /> structure and is equal to the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to the current instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001292 RID: 4754 RVA: 0x0004B204 File Offset: 0x00049404
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeTypeHandle)obj).Value;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.RuntimeTypeHandle" /> structure is equal to the current <see cref="T:System.RuntimeTypeHandle" /> structure.</summary>
		/// <returns>true if the value of <paramref name="handle" /> is equal to the value of this instance; otherwise, false.</returns>
		/// <param name="handle">The <see cref="T:System.RuntimeTypeHandle" /> structure to compare to the current instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001293 RID: 4755 RVA: 0x0004B24C File Offset: 0x0004944C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeTypeHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001294 RID: 4756 RVA: 0x0004B260 File Offset: 0x00049460
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001295 RID: 4757
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeAttributes GetAttributes(RuntimeType type);

		// Token: 0x06001296 RID: 4758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMetadataToken(RuntimeType type);

		// Token: 0x06001297 RID: 4759 RVA: 0x0004B26D File Offset: 0x0004946D
		internal static int GetToken(RuntimeType type)
		{
			return RuntimeTypeHandle.GetMetadataToken(type);
		}

		// Token: 0x06001298 RID: 4760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetGenericTypeDefinition_impl(RuntimeType type);

		// Token: 0x06001299 RID: 4761 RVA: 0x0004B275 File Offset: 0x00049475
		internal static Type GetGenericTypeDefinition(RuntimeType type)
		{
			return RuntimeTypeHandle.GetGenericTypeDefinition_impl(type);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004B280 File Offset: 0x00049480
		internal static bool IsPrimitive(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return (corElementType >= CorElementType.Boolean && corElementType <= CorElementType.R8) || corElementType == CorElementType.I || corElementType == CorElementType.U;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0004B2A9 File Offset: 0x000494A9
		internal static bool IsByRef(RuntimeType type)
		{
			return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.ByRef;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004B2B5 File Offset: 0x000494B5
		internal static bool IsPointer(RuntimeType type)
		{
			return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.Ptr;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004B2C4 File Offset: 0x000494C4
		internal static bool IsArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004B2E4 File Offset: 0x000494E4
		internal static bool IsSzArray(RuntimeType type)
		{
			return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.SzArray;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004B2F0 File Offset: 0x000494F0
		internal static bool HasElementType(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray || corElementType == CorElementType.Ptr || corElementType == CorElementType.ByRef;
		}

		// Token: 0x060012A0 RID: 4768
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern CorElementType GetCorElementType(RuntimeType type);

		// Token: 0x060012A1 RID: 4769
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasInstantiation(RuntimeType type);

		// Token: 0x060012A2 RID: 4770
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsComObject(RuntimeType type);

		// Token: 0x060012A3 RID: 4771
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceOfType(RuntimeType type, object o);

		// Token: 0x060012A4 RID: 4772
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasReferences(RuntimeType type);

		// Token: 0x060012A5 RID: 4773 RVA: 0x0004B31A File Offset: 0x0004951A
		internal static bool IsComObject(RuntimeType type, bool isGenericCOM)
		{
			return !isGenericCOM && RuntimeTypeHandle.IsComObject(type);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00033AE1 File Offset: 0x00031CE1
		internal static bool IsContextful(RuntimeType type)
		{
			return typeof(ContextBoundObject).IsAssignableFrom(type);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00033991 File Offset: 0x00031B91
		internal static bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2)
		{
			return false;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004B327 File Offset: 0x00049527
		internal static bool IsInterface(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
		}

		// Token: 0x060012A9 RID: 4777
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetArrayRank(RuntimeType type);

		// Token: 0x060012AA RID: 4778
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

		// Token: 0x060012AB RID: 4779
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetElementType(RuntimeType type);

		// Token: 0x060012AC RID: 4780
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetModule(RuntimeType type);

		// Token: 0x060012AD RID: 4781
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericVariable(RuntimeType type);

		// Token: 0x060012AE RID: 4782
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetBaseType(RuntimeType type);

		// Token: 0x060012AF RID: 4783 RVA: 0x0004B336 File Offset: 0x00049536
		internal static bool CanCastTo(RuntimeType type, RuntimeType target)
		{
			return RuntimeTypeHandle.type_is_assignable_from(target, type);
		}

		// Token: 0x060012B0 RID: 4784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool type_is_assignable_from(Type a, Type b);

		// Token: 0x060012B1 RID: 4785
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericTypeDefinition(RuntimeType type);

		// Token: 0x060012B2 RID: 4786
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetGenericParameterInfo(RuntimeType type);

		// Token: 0x060012B3 RID: 4787 RVA: 0x0004B33F File Offset: 0x0004953F
		internal static bool IsSubclassOf(RuntimeType childType, RuntimeType baseType)
		{
			return RuntimeTypeHandle.is_subclass_of(childType._impl.Value, baseType._impl.Value);
		}

		// Token: 0x060012B4 RID: 4788
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool is_subclass_of(IntPtr childType, IntPtr baseType);

		// Token: 0x060012B5 RID: 4789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeType internal_from_name(string name, ref StackCrawlMark stackMark, Assembly callerAssembly, bool throwOnError, bool ignoreCase, bool reflectionOnly);

		// Token: 0x060012B6 RID: 4790 RVA: 0x0004B35C File Offset: 0x0004955C
		internal static RuntimeType GetTypeByName(string typeName, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, bool loadTypeFromPartialName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (typeName == string.Empty)
			{
				if (throwOnError)
				{
					throw new TypeLoadException("A null or zero length string does not represent a valid Type.");
				}
				return null;
			}
			else if (reflectionOnly)
			{
				int num = typeName.IndexOf(',');
				if (num < 0 || num == 0 || num == typeName.Length - 1)
				{
					throw new ArgumentException("Assembly qualifed type name is required", "typeName");
				}
				string text = typeName.Substring(num + 1);
				Assembly assembly;
				try
				{
					assembly = Assembly.ReflectionOnlyLoad(text);
				}
				catch
				{
					if (throwOnError)
					{
						throw;
					}
					return null;
				}
				return (RuntimeType)assembly.GetType(typeName.Substring(0, num), throwOnError, ignoreCase);
			}
			else
			{
				RuntimeType runtimeType = RuntimeTypeHandle.internal_from_name(typeName, ref stackMark, null, throwOnError, ignoreCase, false);
				if (throwOnError && runtimeType == null)
				{
					throw new TypeLoadException("Error loading '" + typeName + "'");
				}
				return runtimeType;
			}
		}

		// Token: 0x0400077E RID: 1918
		private IntPtr value;
	}
}
