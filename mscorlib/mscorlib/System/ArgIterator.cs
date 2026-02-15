using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a variable-length argument list; that is, the parameters of a function that takes a variable number of arguments.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C3 RID: 451
	[StructLayout(LayoutKind.Auto)]
	public struct ArgIterator
	{
		// Token: 0x060011AE RID: 4526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Setup(IntPtr argsp, IntPtr start);

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgIterator" /> structure using the specified argument list.</summary>
		/// <param name="arglist">An argument list consisting of mandatory and optional arguments. </param>
		// Token: 0x060011AF RID: 4527 RVA: 0x0004855C File Offset: 0x0004675C
		public ArgIterator(RuntimeArgumentHandle arglist)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			if (arglist.args == IntPtr.Zero)
			{
				throw new PlatformNotSupportedException();
			}
			this.Setup(arglist.args, IntPtr.Zero);
		}

		/// <summary>This method is not supported, and always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This comparison is not supported. No value is returned.</returns>
		/// <param name="o">An object to be compared to this instance. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011B0 RID: 4528 RVA: 0x000485BA File Offset: 0x000467BA
		public override bool Equals(object o)
		{
			throw new NotSupportedException("ArgIterator does not support Equals.");
		}

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011B1 RID: 4529 RVA: 0x000485C6 File Offset: 0x000467C6
		public override int GetHashCode()
		{
			return this.sig.GetHashCode();
		}

		/// <summary>Returns the next argument in a variable-length argument list.</summary>
		/// <returns>The next argument as a <see cref="T:System.TypedReference" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read beyond the end of the list. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011B2 RID: 4530 RVA: 0x000485D4 File Offset: 0x000467D4
		[CLSCompliant(false)]
		public unsafe TypedReference GetNextArg()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException("Invalid iterator position.");
			}
			TypedReference typedReference = default(TypedReference);
			this.IntGetNextArg((void*)(&typedReference));
			return typedReference;
		}

		// Token: 0x060011B3 RID: 4531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void IntGetNextArg(void* res);

		/// <summary>Returns the number of arguments remaining in the argument list.</summary>
		/// <returns>The number of remaining arguments.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011B4 RID: 4532 RVA: 0x0004860C File Offset: 0x0004680C
		public int GetRemainingCount()
		{
			return this.num_args - this.next_arg;
		}

		// Token: 0x04000730 RID: 1840
		private IntPtr sig;

		// Token: 0x04000731 RID: 1841
		private IntPtr args;

		// Token: 0x04000732 RID: 1842
		private int next_arg;

		// Token: 0x04000733 RID: 1843
		private int num_args;
	}
}
