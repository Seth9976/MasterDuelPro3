using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Supports all classes in the .NET Framework class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all classes in the .NET Framework; it is the root of the type hierarchy.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DB RID: 475
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Serializable]
	public class Object
	{
		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001270 RID: 4720 RVA: 0x00033FD7 File Offset: 0x000321D7
		public virtual bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Determines whether the specified object instances are considered equal.</summary>
		/// <returns>true if the objects are considered equal; otherwise, false. If both <paramref name="objA" /> and <paramref name="objB" /> are null, the method returns true.</returns>
		/// <param name="objA">The first object to compare. </param>
		/// <param name="objB">The second object to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001271 RID: 4721 RVA: 0x0004AE04 File Offset: 0x00049004
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		// Token: 0x06001272 RID: 4722 RVA: 0x00002C89 File Offset: 0x00000E89
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Object()
		{
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06001273 RID: 4723 RVA: 0x00002C89 File Offset: 0x00000E89
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Finalize()
		{
		}

		/// <summary>Serves as a hash function for a particular type. </summary>
		/// <returns>A hash code for the current object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001274 RID: 4724 RVA: 0x0004AE1B File Offset: 0x0004901B
		public virtual int GetHashCode()
		{
			return object.InternalGetHashCode(this);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the current instance.</summary>
		/// <returns>The exact runtime type of the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001275 RID: 4725
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetType();

		/// <summary>Creates a shallow copy of the current <see cref="T:System.Object" />.</summary>
		/// <returns>A shallow copy of the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001276 RID: 4726
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected extern object MemberwiseClone();

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001277 RID: 4727 RVA: 0x0004AE23 File Offset: 0x00049023
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> instances are the same instance.</summary>
		/// <returns>true if <paramref name="objA" /> is the same instance as <paramref name="objB" /> or if both are null; otherwise, false.</returns>
		/// <param name="objA">The first object to compare. </param>
		/// <param name="objB">The second object  to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001278 RID: 4728 RVA: 0x00033FD7 File Offset: 0x000321D7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		// Token: 0x06001279 RID: 4729
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o);

		// Token: 0x0600127A RID: 4730 RVA: 0x00002C89 File Offset: 0x00000E89
		private void FieldGetter(string typeName, string fieldName, ref object val)
		{
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00002C89 File Offset: 0x00000E89
		private void FieldSetter(string typeName, string fieldName, object val)
		{
		}
	}
}
