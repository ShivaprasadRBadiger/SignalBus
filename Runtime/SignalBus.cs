using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.com.srb.signalbus.Runtime
{
	public class SignalBus : ISignalBus
	{
		private readonly Dictionary<Type, HashSet<Delegate>> signalHandlers = new Dictionary<Type, HashSet<Delegate>>();

		public void Register<T>(Action<T> signalHandler) where T : struct
		{
			Type signalType = typeof(T);
			if (!signalHandlers.ContainsKey(signalType))
			{
				signalHandlers[signalType] = new HashSet<Delegate>();
			}
			var success = signalHandlers[signalType].Add(signalHandler);
			if (!success)
				Debug.LogError($"Duplicate subscription for signal {signalType.Name}!");
		}

		public void Unregister<T>(Action<T> signalHandler) where T : struct
		{
			Type signalType = typeof(T);
			var success = false;
			if (signalHandlers.TryGetValue(signalType, out var handlers))
			{
				success = handlers.Remove(signalHandler);
				return;
			}
			if (!success)
				Debug.LogError($"Failed to remove non existant handler for signal {signalType.Name}!");
		}

		public void Fire<T>(T signal) where T : struct
		{
			Type signalType = typeof(T);
			if (signalHandlers.TryGetValue(signalType, out var handlers))
			{
				foreach (Delegate handler in handlers)
					((Action<T>)handler)(signal);
			}
		}
	}
}