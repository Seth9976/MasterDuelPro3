using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Holds a reference to a value.</summary>
	/// <typeparam name="T">The type of the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011C RID: 284
	public class StrongBox<T> : IStrongBox
	{
		/// <summary>Initializes a new StrongBox which can receive a value when used in a reference call.</summary>
		// Token: 0x060009A6 RID: 2470 RVA: 0x00009F1D File Offset: 0x0000811D
		public StrongBox()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> class by using the supplied value. </summary>
		/// <param name="value">A value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> will reference.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002602A File Offset: 0x0002422A
		public StrongBox(T value)
		{
			this.Value = value;
		}

		/// <summary>Represents the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</summary>
		// Token: 0x040002FA RID: 762
		public T Value;
	}
}
