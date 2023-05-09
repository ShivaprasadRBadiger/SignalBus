
using System;

namespace Assets.com.srb.signalbus.Runtime
{
	public interface ISignalBus
	{
		public void Register<T>(Action<T> signalHandler) where T : struct;
		public void Unregister<T>(Action<T> signalHandler) where T : struct;
		public void Fire<T>(T signalData) where T : struct;
	}
}