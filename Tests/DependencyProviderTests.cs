using System;

using Assets.com.srb.signalbus.Runtime;

using NUnit.Framework;

[TestFixture]
public class SignalBusTests
{
  private SignalBus signalBus;
  public struct Signal
  {
    public int Id;
    public string Message;
  }
  [SetUp]
  public void SetUp()
  {
    signalBus = new SignalBus();
  }

  [Test]
  public void TestSignalHandling()
  {
    bool signalHandled = false;
    Signal signal = new Signal { Id = 123, Message = "Hello, World!" };

    // Register a signal handler
    signalBus.Register<Signal>(receivedSignal =>
    {
      signalHandled = true;
      Assert.AreEqual(signal.Id, receivedSignal.Id);
      Assert.AreEqual(signal.Message, receivedSignal.Message);
    });

    // Send the signal
    signalBus.Fire(signal);

    Assert.IsTrue(signalHandled);
  }

  [Test]
  public void TestSignalUnregister()
  {
    bool signalHandled = false;
    Signal signal = new Signal { Id = 123, Message = "Hello, World!" };

    // Register a signal handler
    Action<Signal> signalHandler = receivedSignal =>
    {
      signalHandled = true;
    };
    signalBus.Register<Signal>(signalHandler);

    // Send the signal
    signalBus.Fire(signal);

    Assert.IsTrue(signalHandled);

    signalHandled = false;

    // Unregister the signal handler
    signalBus.Unregister<Signal>(signalHandler);

    // Send the signal again
    signalBus.Fire(signal);

    Assert.IsFalse(signalHandled);
  }
}