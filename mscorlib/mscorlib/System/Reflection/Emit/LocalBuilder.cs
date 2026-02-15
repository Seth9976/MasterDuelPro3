using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Represents a local variable within a method or constructor.</summary>
	// Token: 0x02000672 RID: 1650
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06003289 RID: 12937 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600328A RID: 12938 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600328B RID: 12939 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600328C RID: 12940 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000BDAD6 File Offset: 0x000BBCD6
		internal LocalBuilder(Type t, ILGenerator ilgen)
		{
			this.type = t;
			this.ilgen = ilgen;
		}

		/// <summary>Sets the name and lexical scope of this local variable.</summary>
		/// <param name="name">The name of the local variable. </param>
		/// <param name="startOffset">The beginning offset of the lexical scope of the local variable. </param>
		/// <param name="endOffset">The ending offset of the lexical scope of the local variable. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- There is no symbolic writer defined for the containing module. </exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x0600328E RID: 12942 RVA: 0x000BDAEC File Offset: 0x000BBCEC
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			this.name = name;
			this.startOffset = startOffset;
			this.endOffset = endOffset;
		}

		/// <summary>Sets the name of this local variable.</summary>
		/// <param name="name">The name of the local variable. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- There is no symbolic writer defined for the containing module. </exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x0600328F RID: 12943 RVA: 0x000BDB03 File Offset: 0x000BBD03
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the local variable.</returns>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000B5589 File Offset: 0x000B3789
		public override Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value indicating whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>true if the object referred to by the local variable is pinned in memory; otherwise, false.</returns>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x000B5579 File Offset: 0x000B3779
		public override bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the zero-based index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000B5581 File Offset: 0x000B3781
		public override int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x000BDB0E File Offset: 0x000BBD0E
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000BDB16 File Offset: 0x000BBD16
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000BDB1E File Offset: 0x000BBD1E
		internal int EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000176B9 File Offset: 0x000158B9
		internal LocalBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040019A4 RID: 6564
		private string name;

		// Token: 0x040019A5 RID: 6565
		internal ILGenerator ilgen;

		// Token: 0x040019A6 RID: 6566
		private int startOffset;

		// Token: 0x040019A7 RID: 6567
		private int endOffset;
	}
}
