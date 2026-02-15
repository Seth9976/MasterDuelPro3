using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents the state of a <see cref="T:System.Drawing.Graphics" /> object. This object is returned by a call to the <see cref="M:System.Drawing.Graphics.Save" /> methods. This class cannot be inherited.</summary>
	// Token: 0x02000098 RID: 152
	public sealed class GraphicsState : MarshalByRefObject
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000E853 File Offset: 0x0000CA53
		internal GraphicsState(int nativeState)
		{
			this.nativeState = nativeState;
		}

		// Token: 0x040002E0 RID: 736
		internal int nativeState;
	}
}
