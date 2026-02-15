using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents a typed weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	/// <typeparam name="T">The type of the object referenced.</typeparam>
	// Token: 0x020001FD RID: 509
	[Serializable]
	public sealed class WeakReference<T> : ISerializable where T : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object.</summary>
		/// <param name="target">The object to reference, or null.</param>
		// Token: 0x06001372 RID: 4978 RVA: 0x0004F66C File Offset: 0x0004D86C
		public WeakReference(T target)
			: this(target, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object and uses the specified resurrection tracking.</summary>
		/// <param name="target">The object to reference, or null.</param>
		/// <param name="trackResurrection">true to track the object after finalization; false to track the object only until finalization.</param>
		// Token: 0x06001373 RID: 4979 RVA: 0x0004F678 File Offset: 0x0004D878
		public WeakReference(T target, bool trackResurrection)
		{
			this.trackResurrection = trackResurrection;
			GCHandleType gchandleType = (trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(target, gchandleType);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004F6AC File Offset: 0x0004D8AC
		private WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.trackResurrection = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(T));
			GCHandleType gchandleType = (this.trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(value, gchandleType);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data necessary to serialize the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="info">An object that holds all the data necessary to serialize or deserialize the current <see cref="T:System.WeakReference`1" /> object.</param>
		/// <param name="context">The location where serialized data is stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06001375 RID: 4981 RVA: 0x0004F710 File Offset: 0x0004D910
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.trackResurrection);
			if (this.handle.IsAllocated)
			{
				info.AddValue("TrackedObject", this.handle.Target);
				return;
			}
			info.AddValue("TrackedObject", null);
		}

		/// <summary>Sets the target object that is referenced by this <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="target">The new target object.</param>
		// Token: 0x06001376 RID: 4982 RVA: 0x0004F76C File Offset: 0x0004D96C
		public void SetTarget(T target)
		{
			this.handle.Target = target;
		}

		/// <summary>Tries to retrieve the target object that is referenced by the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <returns>true if the target was retrieved; otherwise, false.</returns>
		/// <param name="target">When this method returns, contains the target object, if it is available. This parameter is treated as uninitialized.</param>
		// Token: 0x06001377 RID: 4983 RVA: 0x0004F77F File Offset: 0x0004D97F
		public bool TryGetTarget(out T target)
		{
			if (!this.handle.IsAllocated)
			{
				target = default(T);
				return false;
			}
			target = (T)((object)this.handle.Target);
			return target != null;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004F7BC File Offset: 0x0004D9BC
		~WeakReference()
		{
			this.handle.Free();
		}

		// Token: 0x04000998 RID: 2456
		private GCHandle handle;

		// Token: 0x04000999 RID: 2457
		private bool trackResurrection;
	}
}
