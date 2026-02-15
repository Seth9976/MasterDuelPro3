using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a 3-by-3 affine matrix that represents a geometric transform. This class cannot be inherited.</summary>
	// Token: 0x0200009F RID: 159
	public sealed class Matrix : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> class as the identity matrix.</summary>
		// Token: 0x060004AE RID: 1198 RVA: 0x0000EA3B File Offset: 0x0000CC3B
		public Matrix()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMatrix(out this.nativeMatrix));
		}

		/// <summary>Gets an array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>An array of floating-point values that represents the elements of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000EA54 File Offset: 0x0000CC54
		public float[] Elements
		{
			get
			{
				float[] array = new float[6];
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(float)) * 6);
				try
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetMatrixElements(this.nativeMatrix, intPtr));
					Marshal.Copy(intPtr, array, 0, 6);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
		}

		/// <summary>Gets the x translation value (the dx value, or the element in the third row and first column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The x translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public float OffsetX
		{
			get
			{
				return this.Elements[4];
			}
		}

		/// <summary>Gets the y translation value (the dy value, or the element in the third row and second column) of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>The y translation value of this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000EABE File Offset: 0x0000CCBE
		public float OffsetY
		{
			get
			{
				return this.Elements[5];
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060004B2 RID: 1202 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
		public void Dispose()
		{
			if (this.nativeMatrix != IntPtr.Zero)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDeleteMatrix(this.nativeMatrix));
				this.nativeMatrix = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Drawing2D.Matrix" /> and is identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <returns>This method returns true if <paramref name="obj" /> is the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> identical to this <see cref="T:System.Drawing.Drawing2D.Matrix" />; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000EB00 File Offset: 0x0000CD00
		public override bool Equals(object obj)
		{
			Matrix matrix = obj as Matrix;
			if (matrix != null)
			{
				bool flag;
				GDIPlus.CheckStatus(GDIPlus.GdipIsMatrixEqual(this.nativeMatrix, matrix.nativeMatrix, out flag));
				return flag;
			}
			return false;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000EB34 File Offset: 0x0000CD34
		~Matrix()
		{
			this.Dispose();
		}

		/// <summary>Returns a hash code.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.Drawing2D.Matrix" />.</returns>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000329 RID: 809
		internal IntPtr nativeMatrix;
	}
}
