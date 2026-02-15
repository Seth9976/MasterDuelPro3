using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000009 RID: 9
	[CLSCompliant(false)]
	public sealed class Syscall : Stdlib
	{
		// Token: 0x06000028 RID: 40
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_get_at_fdcwd", SetLastError = true)]
		private static extern int get_at_fdcwd();

		// Token: 0x06000029 RID: 41
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_L_ctermid")]
		private static extern int _L_ctermid();

		// Token: 0x0600002A RID: 42
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_L_cuserid")]
		private static extern int _L_cuserid();

		// Token: 0x0600002B RID: 43
		[DllImport("libc", EntryPoint = "poll", SetLastError = true)]
		private static extern int sys_poll(Syscall._pollfd[] ufds, uint nfds, int timeout);

		// Token: 0x0600002C RID: 44 RVA: 0x00002468 File Offset: 0x00000668
		public static int poll(Pollfd[] fds, uint nfds, int timeout)
		{
			if ((long)fds.Length < (long)((ulong)nfds))
			{
				throw new ArgumentOutOfRangeException("fds", "Must refer to at least `nfds' elements");
			}
			Syscall._pollfd[] array = new Syscall._pollfd[nfds];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].fd = fds[i].fd;
				array[i].events = NativeConvert.FromPollEvents(fds[i].events);
			}
			int num = Syscall.sys_poll(array, nfds, timeout);
			for (int j = 0; j < array.Length; j++)
			{
				fds[j].revents = NativeConvert.ToPollEvents(array[j].revents);
			}
			return num;
		}

		// Token: 0x0600002D RID: 45
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_get_utime_now", SetLastError = true)]
		private static extern long get_utime_now();

		// Token: 0x0600002E RID: 46
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_get_utime_omit", SetLastError = true)]
		private static extern long get_utime_omit();

		// Token: 0x0400002D RID: 45
		internal static object readdir_lock = new object();

		// Token: 0x0400002E RID: 46
		public static readonly int AT_FDCWD = Syscall.get_at_fdcwd();

		// Token: 0x0400002F RID: 47
		internal static object fstab_lock = new object();

		// Token: 0x04000030 RID: 48
		internal static object grp_lock = new object();

		// Token: 0x04000031 RID: 49
		internal static object pwd_lock = new object();

		// Token: 0x04000032 RID: 50
		private static object signal_lock = new object();

		// Token: 0x04000033 RID: 51
		public static readonly int L_ctermid = Syscall._L_ctermid();

		// Token: 0x04000034 RID: 52
		public static readonly int L_cuserid = Syscall._L_cuserid();

		// Token: 0x04000035 RID: 53
		internal static object getlogin_lock = new object();

		// Token: 0x04000036 RID: 54
		public static readonly IntPtr MAP_FAILED = (IntPtr)(-1);

		// Token: 0x04000037 RID: 55
		public static readonly long UTIME_NOW = Syscall.get_utime_now();

		// Token: 0x04000038 RID: 56
		public static readonly long UTIME_OMIT = Syscall.get_utime_omit();

		// Token: 0x04000039 RID: 57
		private static object tty_lock = new object();

		// Token: 0x0400003A RID: 58
		internal static object usershell_lock = new object();

		// Token: 0x0200000A RID: 10
		private struct _pollfd
		{
			// Token: 0x0400003B RID: 59
			public int fd;

			// Token: 0x0400003C RID: 60
			public short events;

			// Token: 0x0400003D RID: 61
			public short revents;
		}
	}
}
