using System;

namespace System.Drawing.Imaging
{
	/// <summary>Provides properties that get the frame dimensions of an image. Not inheritable.</summary>
	// Token: 0x02000086 RID: 134
	public sealed class FrameDimension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.FrameDimension" /> class using the specified Guid structure.</summary>
		/// <param name="guid">A Guid structure that contains a GUID for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object. </param>
		// Token: 0x06000459 RID: 1113 RVA: 0x0000DBFB File Offset: 0x0000BDFB
		public FrameDimension(Guid guid)
		{
			this._guid = guid;
		}

		/// <summary>Gets a globally unique identifier (GUID) that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>A Guid structure that contains a GUID that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000DC0A File Offset: 0x0000BE0A
		public Guid Guid
		{
			get
			{
				return this._guid;
			}
		}

		/// <summary>Gets the time dimension.</summary>
		/// <returns>The time dimension.</returns>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000DC12 File Offset: 0x0000BE12
		public static FrameDimension Time
		{
			get
			{
				return FrameDimension.s_time;
			}
		}

		/// <summary>Returns a value that indicates whether the specified object is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>Returns true if <paramref name="o" /> is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object; otherwise, false.</returns>
		/// <param name="o">The object to test. </param>
		// Token: 0x0600045C RID: 1116 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		public override bool Equals(object o)
		{
			FrameDimension frameDimension = o as FrameDimension;
			return frameDimension != null && this._guid == frameDimension._guid;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>Returns an int value that is the hash code of this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x0600045D RID: 1117 RVA: 0x0000DC46 File Offset: 0x0000BE46
		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x0600045E RID: 1118 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		public override string ToString()
		{
			if (this == FrameDimension.s_time)
			{
				return "Time";
			}
			if (this == FrameDimension.s_resolution)
			{
				return "Resolution";
			}
			if (this == FrameDimension.s_page)
			{
				return "Page";
			}
			string text = "[FrameDimension: ";
			Guid guid = this._guid;
			return text + guid.ToString() + "]";
		}

		// Token: 0x04000263 RID: 611
		private static FrameDimension s_time = new FrameDimension(new Guid("{6aedbd6d-3fb5-418a-83a6-7f45229dc872}"));

		// Token: 0x04000264 RID: 612
		private static FrameDimension s_resolution = new FrameDimension(new Guid("{84236f7b-3bd3-428f-8dab-4ea1439ca315}"));

		// Token: 0x04000265 RID: 613
		private static FrameDimension s_page = new FrameDimension(new Guid("{7462dc86-6180-4c7e-8e3f-ee7333a7a483}"));

		// Token: 0x04000266 RID: 614
		private Guid _guid;
	}
}
