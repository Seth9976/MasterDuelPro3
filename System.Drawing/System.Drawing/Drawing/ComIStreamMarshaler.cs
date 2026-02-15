using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x02000028 RID: 40
	internal sealed class ComIStreamMarshaler : ICustomMarshaler
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00003F24 File Offset: 0x00002124
		private ComIStreamMarshaler()
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006383 File Offset: 0x00004583
		private static ICustomMarshaler GetInstance(string cookie)
		{
			return ComIStreamMarshaler.defaultInstance;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000638A File Offset: 0x0000458A
		public IntPtr MarshalManagedToNative(object managedObj)
		{
			return ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface((IStream)managedObj);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006397 File Offset: 0x00004597
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseInterface(pNativeData);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000639F File Offset: 0x0000459F
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(pNativeData, false);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000063A8 File Offset: 0x000045A8
		public void CleanUpManagedData(object managedObj)
		{
			ComIStreamMarshaler.NativeToManagedWrapper.ReleaseInterface((IStream)managedObj);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000063B5 File Offset: 0x000045B5
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x0400010A RID: 266
		private static readonly ComIStreamMarshaler defaultInstance = new ComIStreamMarshaler();

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x060001C5 RID: 453
		private delegate int QueryInterfaceDelegate(IntPtr @this, [In] ref Guid riid, IntPtr ppvObject);

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x060001C7 RID: 455
		private delegate int AddRefDelegate(IntPtr @this);

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x060001C9 RID: 457
		private delegate int ReleaseDelegate(IntPtr @this);

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x060001CB RID: 459
		private delegate int ReadDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x060001CD RID: 461
		private delegate int WriteDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x060001CF RID: 463
		private delegate int SeekDelegate(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x060001D1 RID: 465
		private delegate int SetSizeDelegate(IntPtr @this, long libNewSize);

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x060001D3 RID: 467
		private delegate int CopyToDelegate(IntPtr @this, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x060001D5 RID: 469
		private delegate int CommitDelegate(IntPtr @this, int grfCommitFlags);

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x060001D7 RID: 471
		private delegate int RevertDelegate(IntPtr @this);

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x060001D9 RID: 473
		private delegate int LockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x060001DB RID: 475
		private delegate int UnlockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x060001DD RID: 477
		private delegate int StatDelegate(IntPtr @this, out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x02000036 RID: 54
		// (Invoke) Token: 0x060001DF RID: 479
		private delegate int CloneDelegate(IntPtr @this, out IntPtr ppstm);

		// Token: 0x02000037 RID: 55
		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamInterface
		{
			// Token: 0x0400010B RID: 267
			internal IntPtr lpVtbl;

			// Token: 0x0400010C RID: 268
			internal IntPtr gcHandle;
		}

		// Token: 0x02000038 RID: 56
		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamVtbl
		{
			// Token: 0x0400010D RID: 269
			internal ComIStreamMarshaler.QueryInterfaceDelegate QueryInterface;

			// Token: 0x0400010E RID: 270
			internal ComIStreamMarshaler.AddRefDelegate AddRef;

			// Token: 0x0400010F RID: 271
			internal ComIStreamMarshaler.ReleaseDelegate Release;

			// Token: 0x04000110 RID: 272
			internal ComIStreamMarshaler.ReadDelegate Read;

			// Token: 0x04000111 RID: 273
			internal ComIStreamMarshaler.WriteDelegate Write;

			// Token: 0x04000112 RID: 274
			internal ComIStreamMarshaler.SeekDelegate Seek;

			// Token: 0x04000113 RID: 275
			internal ComIStreamMarshaler.SetSizeDelegate SetSize;

			// Token: 0x04000114 RID: 276
			internal ComIStreamMarshaler.CopyToDelegate CopyTo;

			// Token: 0x04000115 RID: 277
			internal ComIStreamMarshaler.CommitDelegate Commit;

			// Token: 0x04000116 RID: 278
			internal ComIStreamMarshaler.RevertDelegate Revert;

			// Token: 0x04000117 RID: 279
			internal ComIStreamMarshaler.LockRegionDelegate LockRegion;

			// Token: 0x04000118 RID: 280
			internal ComIStreamMarshaler.UnlockRegionDelegate UnlockRegion;

			// Token: 0x04000119 RID: 281
			internal ComIStreamMarshaler.StatDelegate Stat;

			// Token: 0x0400011A RID: 282
			internal ComIStreamMarshaler.CloneDelegate Clone;
		}

		// Token: 0x02000039 RID: 57
		private sealed class ManagedToNativeWrapper
		{
			// Token: 0x060001E2 RID: 482 RVA: 0x000063C4 File Offset: 0x000045C4
			static ManagedToNativeWrapper()
			{
				EventHandler eventHandler = new EventHandler(ComIStreamMarshaler.ManagedToNativeWrapper.OnShutdown);
				AppDomain currentDomain = AppDomain.CurrentDomain;
				currentDomain.DomainUnload += eventHandler;
				currentDomain.ProcessExit += eventHandler;
				ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable = new ComIStreamMarshaler.IStreamVtbl
				{
					QueryInterface = new ComIStreamMarshaler.QueryInterfaceDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.QueryInterface),
					AddRef = new ComIStreamMarshaler.AddRefDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.AddRef),
					Release = new ComIStreamMarshaler.ReleaseDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Release),
					Read = new ComIStreamMarshaler.ReadDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Read),
					Write = new ComIStreamMarshaler.WriteDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Write),
					Seek = new ComIStreamMarshaler.SeekDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Seek),
					SetSize = new ComIStreamMarshaler.SetSizeDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.SetSize),
					CopyTo = new ComIStreamMarshaler.CopyToDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.CopyTo),
					Commit = new ComIStreamMarshaler.CommitDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Commit),
					Revert = new ComIStreamMarshaler.RevertDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Revert),
					LockRegion = new ComIStreamMarshaler.LockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.LockRegion),
					UnlockRegion = new ComIStreamMarshaler.UnlockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.UnlockRegion),
					Stat = new ComIStreamMarshaler.StatDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Stat),
					Clone = new ComIStreamMarshaler.CloneDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Clone)
				};
				ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x00006554 File Offset: 0x00004754
			private ManagedToNativeWrapper(IStream managedInterface)
			{
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable == IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
					}
					ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount++;
				}
				try
				{
					this.managedInterface = managedInterface;
					this.gcHandle = GCHandle.Alloc(this);
					ComIStreamMarshaler.IStreamInterface streamInterface = new ComIStreamMarshaler.IStreamInterface();
					streamInterface.lpVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.comVtable;
					streamInterface.gcHandle = (IntPtr)this.gcHandle;
					this.comInterface = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamInterface)));
					Marshal.StructureToPtr<ComIStreamMarshaler.IStreamInterface>(streamInterface, this.comInterface, false);
				}
				catch
				{
					this.Dispose();
					throw;
				}
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x00006638 File Offset: 0x00004838
			private void Dispose()
			{
				if (this.gcHandle.IsAllocated)
				{
					this.gcHandle.Free();
				}
				if (this.comInterface != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.comInterface);
					this.comInterface = IntPtr.Zero;
				}
				this.managedInterface = null;
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (--ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && Environment.HasShutdownStarted)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x000066D4 File Offset: 0x000048D4
			private static void OnShutdown(object sender, EventArgs e)
			{
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable != IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x0000672C File Offset: 0x0000492C
			private static void CreateVtable()
			{
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamVtbl)));
				Marshal.StructureToPtr<ComIStreamMarshaler.IStreamVtbl>(ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable, ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, false);
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x00006757 File Offset: 0x00004957
			private static void DisposeVtable()
			{
				Marshal.DestroyStructure(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, typeof(ComIStreamMarshaler.IStreamVtbl));
				Marshal.FreeHGlobal(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable);
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = IntPtr.Zero;
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x00006781 File Offset: 0x00004981
			internal static IStream GetUnderlyingInterface(IntPtr comInterface, bool outParam)
			{
				if (Marshal.ReadIntPtr(comInterface) == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
				{
					IStream stream = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(comInterface).managedInterface;
					if (outParam)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
					}
					return stream;
				}
				return null;
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x000067AC File Offset: 0x000049AC
			internal static IntPtr GetInterface(IStream managedInterface)
			{
				if (managedInterface == null)
				{
					return IntPtr.Zero;
				}
				IntPtr underlyingInterface;
				if ((underlyingInterface = ComIStreamMarshaler.NativeToManagedWrapper.GetUnderlyingInterface(managedInterface)) == IntPtr.Zero)
				{
					underlyingInterface = new ComIStreamMarshaler.ManagedToNativeWrapper(managedInterface).comInterface;
				}
				return underlyingInterface;
			}

			// Token: 0x060001EA RID: 490 RVA: 0x000067E4 File Offset: 0x000049E4
			internal static void ReleaseInterface(IntPtr comInterface)
			{
				if (comInterface != IntPtr.Zero)
				{
					IntPtr intPtr = Marshal.ReadIntPtr(comInterface);
					if (intPtr == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
						return;
					}
					((ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot)Marshal.PtrToStructure((IntPtr)((long)intPtr + (long)(IntPtr.Size * 2)), typeof(ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot))).Release(comInterface);
				}
			}

			// Token: 0x060001EB RID: 491 RVA: 0x0000684E File Offset: 0x00004A4E
			private static int GetHRForException(Exception e)
			{
				return (int)ComIStreamMarshaler.ManagedToNativeWrapper.exceptionGetHResult.Invoke(e, null);
			}

			// Token: 0x060001EC RID: 492 RVA: 0x00006864 File Offset: 0x00004A64
			private static ComIStreamMarshaler.ManagedToNativeWrapper GetObject(IntPtr @this)
			{
				return (ComIStreamMarshaler.ManagedToNativeWrapper)((GCHandle)Marshal.ReadIntPtr(@this, IntPtr.Size)).Target;
			}

			// Token: 0x060001ED RID: 493 RVA: 0x00006890 File Offset: 0x00004A90
			private static int QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				int num;
				try
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.IID_IUnknown.Equals(riid) || ComIStreamMarshaler.ManagedToNativeWrapper.IID_IStream.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						ComIStreamMarshaler.ManagedToNativeWrapper.AddRef(@this);
						num = 0;
					}
					else
					{
						Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
						num = -2147467262;
					}
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00006908 File Offset: 0x00004B08
			private static int AddRef(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
					lock (managedToNativeWrapper)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper2 = @object;
						num = managedToNativeWrapper2.refCount + 1;
						managedToNativeWrapper2.refCount = num;
						num = num;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x060001EF RID: 495 RVA: 0x00006968 File Offset: 0x00004B68
			private static int Release(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
					lock (managedToNativeWrapper)
					{
						if (@object.refCount != 0)
						{
							ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper2 = @object;
							num = managedToNativeWrapper2.refCount - 1;
							managedToNativeWrapper2.refCount = num;
							if (num == 0)
							{
								@object.Dispose();
							}
						}
						num = @object.refCount;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x000069E0 File Offset: 0x00004BE0
			private static int Read(IntPtr @this, byte[] pv, int cb, IntPtr pcbRead)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Read(pv, cb, pcbRead);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x00006A20 File Offset: 0x00004C20
			private static int Write(IntPtr @this, byte[] pv, int cb, IntPtr pcbWritten)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Write(pv, cb, pcbWritten);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x00006A60 File Offset: 0x00004C60
			private static int Seek(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Seek(dlibMove, dwOrigin, plibNewPosition);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x00006AA0 File Offset: 0x00004CA0
			private static int SetSize(IntPtr @this, long libNewSize)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.SetSize(libNewSize);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x00006ADC File Offset: 0x00004CDC
			private static int CopyTo(IntPtr @this, IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.CopyTo(pstm, cb, pcbRead, pcbWritten);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x00006B1C File Offset: 0x00004D1C
			private static int Commit(IntPtr @this, int grfCommitFlags)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Commit(grfCommitFlags);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x00006B58 File Offset: 0x00004D58
			private static int Revert(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Revert();
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x00006B94 File Offset: 0x00004D94
			private static int LockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.LockRegion(libOffset, cb, dwLockType);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x00006BD4 File Offset: 0x00004DD4
			private static int UnlockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.UnlockRegion(libOffset, cb, dwLockType);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00006C14 File Offset: 0x00004E14
			private static int Stat(IntPtr @this, out STATSTG pstatstg, int grfStatFlag)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Stat(out pstatstg, grfStatFlag);
					num = 0;
				}
				catch (Exception ex)
				{
					pstatstg = default(STATSTG);
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x060001FA RID: 506 RVA: 0x00006C58 File Offset: 0x00004E58
			private static int Clone(IntPtr @this, out IntPtr ppstm)
			{
				ppstm = IntPtr.Zero;
				int num;
				try
				{
					IStream stream;
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Clone(out stream);
					ppstm = ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface(stream);
					num = 0;
				}
				catch (Exception ex)
				{
					num = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(ex);
				}
				return num;
			}

			// Token: 0x0400011B RID: 283
			private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

			// Token: 0x0400011C RID: 284
			private static readonly Guid IID_IStream = new Guid("0000000C-0000-0000-C000-000000000046");

			// Token: 0x0400011D RID: 285
			private static readonly MethodInfo exceptionGetHResult = typeof(Exception).GetTypeInfo().GetProperty("HResult", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.ExactBinding, null, typeof(int), new Type[0], null).GetGetMethod(true);

			// Token: 0x0400011E RID: 286
			private static readonly ComIStreamMarshaler.IStreamVtbl managedVtable;

			// Token: 0x0400011F RID: 287
			private static IntPtr comVtable;

			// Token: 0x04000120 RID: 288
			private static int vtableRefCount;

			// Token: 0x04000121 RID: 289
			private IStream managedInterface;

			// Token: 0x04000122 RID: 290
			private IntPtr comInterface;

			// Token: 0x04000123 RID: 291
			private GCHandle gcHandle;

			// Token: 0x04000124 RID: 292
			private int refCount = 1;

			// Token: 0x0200003A RID: 58
			[StructLayout(LayoutKind.Sequential)]
			private sealed class ReleaseSlot
			{
				// Token: 0x04000125 RID: 293
				internal ComIStreamMarshaler.ReleaseDelegate Release;
			}
		}

		// Token: 0x0200003B RID: 59
		private sealed class NativeToManagedWrapper : IStream
		{
			// Token: 0x060001FB RID: 507 RVA: 0x00006CA4 File Offset: 0x00004EA4
			private NativeToManagedWrapper(IntPtr comInterface, bool outParam)
			{
				this.comInterface = comInterface;
				this.managedVtable = (ComIStreamMarshaler.IStreamVtbl)Marshal.PtrToStructure(Marshal.ReadIntPtr(comInterface), typeof(ComIStreamMarshaler.IStreamVtbl));
				if (!outParam)
				{
					this.managedVtable.AddRef(comInterface);
				}
			}

			// Token: 0x060001FC RID: 508 RVA: 0x00006CF4 File Offset: 0x00004EF4
			~NativeToManagedWrapper()
			{
				this.Dispose(false);
			}

			// Token: 0x060001FD RID: 509 RVA: 0x00006D24 File Offset: 0x00004F24
			private void Dispose(bool disposing)
			{
				this.managedVtable.Release(this.comInterface);
				if (disposing)
				{
					this.comInterface = IntPtr.Zero;
					this.managedVtable = null;
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x060001FE RID: 510 RVA: 0x00006D58 File Offset: 0x00004F58
			internal static IntPtr GetUnderlyingInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					ComIStreamMarshaler.NativeToManagedWrapper nativeToManagedWrapper = (ComIStreamMarshaler.NativeToManagedWrapper)managedInterface;
					nativeToManagedWrapper.managedVtable.AddRef(nativeToManagedWrapper.comInterface);
					return nativeToManagedWrapper.comInterface;
				}
				return IntPtr.Zero;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x00006D98 File Offset: 0x00004F98
			internal static IStream GetInterface(IntPtr comInterface, bool outParam)
			{
				if (comInterface == IntPtr.Zero)
				{
					return null;
				}
				return ComIStreamMarshaler.ManagedToNativeWrapper.GetUnderlyingInterface(comInterface, outParam) ?? new ComIStreamMarshaler.NativeToManagedWrapper(comInterface, outParam);
			}

			// Token: 0x06000200 RID: 512 RVA: 0x00006DC8 File Offset: 0x00004FC8
			internal static void ReleaseInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					((ComIStreamMarshaler.NativeToManagedWrapper)managedInterface).Dispose(true);
				}
			}

			// Token: 0x06000201 RID: 513 RVA: 0x00006DDE File Offset: 0x00004FDE
			private static void ThrowExceptionForHR(int result)
			{
				if (result < 0)
				{
					throw new COMException(null, result);
				}
			}

			// Token: 0x06000202 RID: 514 RVA: 0x00006DEC File Offset: 0x00004FEC
			public void Read(byte[] pv, int cb, IntPtr pcbRead)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Read(this.comInterface, pv, cb, pcbRead));
			}

			// Token: 0x06000203 RID: 515 RVA: 0x00006E0C File Offset: 0x0000500C
			public void Write(byte[] pv, int cb, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Write(this.comInterface, pv, cb, pcbWritten));
			}

			// Token: 0x06000204 RID: 516 RVA: 0x00006E2C File Offset: 0x0000502C
			public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Seek(this.comInterface, dlibMove, dwOrigin, plibNewPosition));
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00006E4C File Offset: 0x0000504C
			public void SetSize(long libNewSize)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.SetSize(this.comInterface, libNewSize));
			}

			// Token: 0x06000206 RID: 518 RVA: 0x00006E6A File Offset: 0x0000506A
			public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.CopyTo(this.comInterface, pstm, cb, pcbRead, pcbWritten));
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00006E8C File Offset: 0x0000508C
			public void Commit(int grfCommitFlags)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Commit(this.comInterface, grfCommitFlags));
			}

			// Token: 0x06000208 RID: 520 RVA: 0x00006EAA File Offset: 0x000050AA
			public void Revert()
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Revert(this.comInterface));
			}

			// Token: 0x06000209 RID: 521 RVA: 0x00006EC7 File Offset: 0x000050C7
			public void LockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.LockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			// Token: 0x0600020A RID: 522 RVA: 0x00006EE7 File Offset: 0x000050E7
			public void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.UnlockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			// Token: 0x0600020B RID: 523 RVA: 0x00006F07 File Offset: 0x00005107
			public void Stat(out STATSTG pstatstg, int grfStatFlag)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Stat(this.comInterface, out pstatstg, grfStatFlag));
			}

			// Token: 0x0600020C RID: 524 RVA: 0x00006F28 File Offset: 0x00005128
			public void Clone(out IStream ppstm)
			{
				IntPtr intPtr;
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Clone(this.comInterface, out intPtr));
				ppstm = ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(intPtr, true);
			}

			// Token: 0x04000126 RID: 294
			private IntPtr comInterface;

			// Token: 0x04000127 RID: 295
			private ComIStreamMarshaler.IStreamVtbl managedVtable;
		}
	}
}
