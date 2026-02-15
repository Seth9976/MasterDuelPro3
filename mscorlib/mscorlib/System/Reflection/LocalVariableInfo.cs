using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a local variable and provides access to local variable metadata.</summary>
	// Token: 0x02000639 RID: 1593
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class LocalVariableInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.LocalVariableInfo" /> class.</summary>
		// Token: 0x06002F4F RID: 12111 RVA: 0x00003CE1 File Offset: 0x00001EE1
		protected LocalVariableInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>true if the object referred to by the variable is pinned in memory; otherwise, false.</returns>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x000B5579 File Offset: 0x000B3779
		public virtual bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000B5581 File Offset: 0x000B3781
		public virtual int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The type of the local variable.</returns>
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000B5589 File Offset: 0x000B3789
		public virtual Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns a user-readable string that describes the local variable.</summary>
		/// <returns>A string that displays information about the local variable, including the type name, index, and pinned status.</returns>
		// Token: 0x06002F53 RID: 12115 RVA: 0x000B5594 File Offset: 0x000B3794
		public override string ToString()
		{
			if (this.is_pinned)
			{
				return string.Format("{0} ({1}) (pinned)", this.type, this.position);
			}
			return string.Format("{0} ({1})", this.type, this.position);
		}

		// Token: 0x0400184D RID: 6221
		internal Type type;

		// Token: 0x0400184E RID: 6222
		internal bool is_pinned;

		// Token: 0x0400184F RID: 6223
		internal ushort position;
	}
}
