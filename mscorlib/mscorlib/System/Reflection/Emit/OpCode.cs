using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes a Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x02000678 RID: 1656
	[ComVisible(true)]
	public readonly struct OpCode : IEquatable<OpCode>
	{
		// Token: 0x0600335C RID: 13148 RVA: 0x000C012C File Offset: 0x000BE32C
		internal OpCode(int p, int q)
		{
			this.op1 = (byte)(p & 255);
			this.op2 = (byte)((p >> 8) & 255);
			this.push = (byte)((p >> 16) & 255);
			this.pop = (byte)((p >> 24) & 255);
			this.size = (byte)(q & 255);
			this.type = (byte)((q >> 8) & 255);
			this.args = (byte)((q >> 16) & 255);
			this.flow = (byte)((q >> 24) & 255);
		}

		/// <summary>Returns the generated hash code for this Opcode.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x0600335D RID: 13149 RVA: 0x000C01B9 File Offset: 0x000BE3B9
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Tests whether the given object is equal to this Opcode.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of Opcode and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x0600335E RID: 13150 RVA: 0x000C01C8 File Offset: 0x000BE3C8
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return opCode.op1 == this.op1 && opCode.op2 == this.op2;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.OpCode" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to the current instance.</param>
		// Token: 0x0600335F RID: 13151 RVA: 0x000C0207 File Offset: 0x000BE407
		public bool Equals(OpCode obj)
		{
			return obj.op1 == this.op1 && obj.op2 == this.op2;
		}

		/// <summary>Returns this Opcode as a <see cref="T:System.String" />.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> containing the name of this Opcode.</returns>
		// Token: 0x06003360 RID: 13152 RVA: 0x000C0227 File Offset: 0x000BE427
		public override string ToString()
		{
			return this.Name;
		}

		/// <summary>The name of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The name of the MSIL instruction.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000C022F File Offset: 0x000BE42F
		public string Name
		{
			get
			{
				if (this.op1 == 255)
				{
					return OpCodeNames.names[(int)this.op2];
				}
				return OpCodeNames.names[256 + (int)this.op2];
			}
		}

		/// <summary>The size of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The size of the MSIL instruction.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000C025D File Offset: 0x000BE45D
		public int Size
		{
			get
			{
				return (int)this.size;
			}
		}

		/// <summary>The operand type of an Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The operand type of an MSIL instruction.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000C0265 File Offset: 0x000BE465
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.args;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pops the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pops the stack.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x000C026D File Offset: 0x000BE46D
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.pop;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pushes operand onto the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pushes operand onto the stack.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000C0275 File Offset: 0x000BE475
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.push;
			}
		}

		/// <summary>The value of the immediate operand of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The value of the immediate operand of the MSIL instruction.</returns>
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000C027D File Offset: 0x000BE47D
		public short Value
		{
			get
			{
				if (this.size == 1)
				{
					return (short)this.op2;
				}
				return (short)(((int)this.op1 << 8) | (int)this.op2);
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06003367 RID: 13159 RVA: 0x000C029F File Offset: 0x000BE49F
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.op1 == b.op1 && a.op2 == b.op2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06003368 RID: 13160 RVA: 0x000C02BF File Offset: 0x000BE4BF
		public static bool operator !=(OpCode a, OpCode b)
		{
			return a.op1 != b.op1 || a.op2 != b.op2;
		}

		// Token: 0x040019ED RID: 6637
		internal readonly byte op1;

		// Token: 0x040019EE RID: 6638
		internal readonly byte op2;

		// Token: 0x040019EF RID: 6639
		private readonly byte push;

		// Token: 0x040019F0 RID: 6640
		private readonly byte pop;

		// Token: 0x040019F1 RID: 6641
		private readonly byte size;

		// Token: 0x040019F2 RID: 6642
		private readonly byte type;

		// Token: 0x040019F3 RID: 6643
		private readonly byte args;

		// Token: 0x040019F4 RID: 6644
		private readonly byte flow;
	}
}
