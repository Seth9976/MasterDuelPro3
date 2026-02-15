using System;
using System.Runtime.CompilerServices;
using Mono;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of an event and provides access to event metadata.</summary>
	// Token: 0x020005F8 RID: 1528
	[Serializable]
	public abstract class EventInfo : MemberInfo
	{
		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</returns>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x0000F253 File Offset: 0x0000D453
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		/// <summary>Returns the method used to add an event handler delegate to the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		// Token: 0x06002CAF RID: 11439 RVA: 0x000B18EA File Offset: 0x000AFAEA
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		/// <summary>Returns the method used to remove an event handler delegate from the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		// Token: 0x06002CB0 RID: 11440 RVA: 0x000B18F3 File Offset: 0x000AFAF3
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		/// <summary>When overridden in a derived class, retrieves the MethodInfo object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method of the event, specifying whether to return non-public methods.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		/// <param name="nonPublic">true if non-public methods can be returned; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06002CB1 RID: 11441
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, retrieves the MethodInfo object for removing a method of the event, specifying whether to return non-public methods.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		/// <param name="nonPublic">true if non-public methods can be returned; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06002CB2 RID: 11442
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, returns the method that is called when the event is raised, specifying whether to return non-public methods.</summary>
		/// <returns>A MethodInfo object that was called when the event was raised.</returns>
		/// <param name="nonPublic">true if non-public methods can be returned; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06002CB3 RID: 11443
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		/// <summary>Gets the Type object of the underlying event-handler delegate associated with this event.</summary>
		/// <returns>A read-only Type object representing the delegate event handler.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000B18FC File Offset: 0x000AFAFC
		public virtual Type EventHandlerType
		{
			get
			{
				ParameterInfo[] parametersInternal = this.GetAddMethod(true).GetParametersInternal();
				Type typeFromHandle = typeof(Delegate);
				for (int i = 0; i < parametersInternal.Length; i++)
				{
					Type parameterType = parametersInternal[i].ParameterType;
					if (parameterType.IsSubclassOf(typeFromHandle))
					{
						return parameterType;
					}
				}
				return null;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002CB5 RID: 11445 RVA: 0x000B1944 File Offset: 0x000AFB44
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002CB6 RID: 11446 RVA: 0x000B194D File Offset: 0x000AFB4D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002CB7 RID: 11447 RVA: 0x0004AE04 File Offset: 0x00049004
		public static bool operator ==(EventInfo left, EventInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002CB8 RID: 11448 RVA: 0x000B1955 File Offset: 0x000AFB55
		public static bool operator !=(EventInfo left, EventInfo right)
		{
			return !(left == right);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000B1964 File Offset: 0x000AFB64
		private static void AddEventFrame<T, D>(EventInfo.AddEvent<T, D> addEvent, object obj, object dele)
		{
			if (obj == null)
			{
				throw new TargetException("Cannot add a handler to a non static event with a null target");
			}
			if (!(obj is T))
			{
				throw new TargetException("Object doesn't match target");
			}
			if (!(dele is D))
			{
				throw new ArgumentException(string.Format("Object of type {0} cannot be converted to type {1}.", dele.GetType(), typeof(D)));
			}
			addEvent((T)((object)obj), (D)((object)dele));
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000B19CC File Offset: 0x000AFBCC
		private static void StaticAddEventAdapterFrame<D>(EventInfo.StaticAddEvent<D> addEvent, object obj, object dele)
		{
			addEvent((D)((object)dele));
		}

		// Token: 0x06002CBB RID: 11451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x06002CBC RID: 11452 RVA: 0x000B19DC File Offset: 0x000AFBDC
		internal static EventInfo GetEventFromHandle(RuntimeEventHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			EventInfo eventInfo = EventInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (eventInfo == null)
			{
				throw new ArgumentException("The event handle and the type handle are incompatible.");
			}
			return eventInfo;
		}

		// Token: 0x040016EF RID: 5871
		private EventInfo.AddEventAdapter cached_add_event;

		// Token: 0x020005F9 RID: 1529
		// (Invoke) Token: 0x06002CBE RID: 11454
		private delegate void AddEventAdapter(object _this, Delegate dele);

		// Token: 0x020005FA RID: 1530
		// (Invoke) Token: 0x06002CC0 RID: 11456
		private delegate void AddEvent<T, D>(T _this, D dele);

		// Token: 0x020005FB RID: 1531
		// (Invoke) Token: 0x06002CC2 RID: 11458
		private delegate void StaticAddEvent<D>(D dele);
	}
}
