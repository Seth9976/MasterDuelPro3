using System;

namespace System.Reflection.Emit
{
	/// <summary>Describes how values are pushed onto a stack or popped off a stack.</summary>
	// Token: 0x0200064F RID: 1615
	public enum StackBehaviour
	{
		/// <summary>No values are popped off the stack.</summary>
		// Token: 0x040018B6 RID: 6326
		Pop0,
		/// <summary>Pops one value off the stack.</summary>
		// Token: 0x040018B7 RID: 6327
		Pop1,
		/// <summary>Pops 1 value off the stack for the first operand, and 1 value of the stack for the second operand.</summary>
		// Token: 0x040018B8 RID: 6328
		Pop1_pop1,
		/// <summary>Pops a 32-bit integer off the stack.</summary>
		// Token: 0x040018B9 RID: 6329
		Popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x040018BA RID: 6330
		Popi_pop1,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x040018BB RID: 6331
		Popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit integer off the stack for the second operand.</summary>
		// Token: 0x040018BC RID: 6332
		Popi_popi8,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, a 32-bit integer off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040018BD RID: 6333
		Popi_popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x040018BE RID: 6334
		Popi_popr4,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x040018BF RID: 6335
		Popi_popr8,
		/// <summary>Pops a reference off the stack.</summary>
		// Token: 0x040018C0 RID: 6336
		Popref,
		/// <summary>Pops a reference off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x040018C1 RID: 6337
		Popref_pop1,
		/// <summary>Pops a reference off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x040018C2 RID: 6338
		Popref_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a value off the stack for the third operand.</summary>
		// Token: 0x040018C3 RID: 6339
		Popref_popi_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit integer off the stack for the third operand.</summary>
		// Token: 0x040018C4 RID: 6340
		Popref_popi_popi8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040018C5 RID: 6341
		Popref_popi_popr4,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit floating point number off the stack for the third operand.</summary>
		// Token: 0x040018C6 RID: 6342
		Popref_popi_popr8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a reference off the stack for the third operand.</summary>
		// Token: 0x040018C7 RID: 6343
		Popref_popi_popref,
		/// <summary>No values are pushed onto the stack.</summary>
		// Token: 0x040018C8 RID: 6344
		Push0,
		/// <summary>Pushes one value onto the stack.</summary>
		// Token: 0x040018C9 RID: 6345
		Push1,
		/// <summary>Pushes 1 value onto the stack for the first operand, and 1 value onto the stack for the second operand.</summary>
		// Token: 0x040018CA RID: 6346
		Push1_push1,
		/// <summary>Pushes a 32-bit integer onto the stack.</summary>
		// Token: 0x040018CB RID: 6347
		Pushi,
		/// <summary>Pushes a 64-bit integer onto the stack.</summary>
		// Token: 0x040018CC RID: 6348
		Pushi8,
		/// <summary>Pushes a 32-bit floating point number onto the stack.</summary>
		// Token: 0x040018CD RID: 6349
		Pushr4,
		/// <summary>Pushes a 64-bit floating point number onto the stack.</summary>
		// Token: 0x040018CE RID: 6350
		Pushr8,
		/// <summary>Pushes a reference onto the stack.</summary>
		// Token: 0x040018CF RID: 6351
		Pushref,
		/// <summary>Pops a variable off the stack.</summary>
		// Token: 0x040018D0 RID: 6352
		Varpop,
		/// <summary>Pushes a variable onto the stack.</summary>
		// Token: 0x040018D1 RID: 6353
		Varpush,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040018D2 RID: 6354
		Popref_popi_pop1
	}
}
