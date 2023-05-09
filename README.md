
# SignalBus

SignalBus is a lightweight C# class that provides a simple mechanism for decoupled communication between components through the use of signals. It allows components to register themselves as signal handlers and receive signals of specific types.

## Features

-   Efficient and decoupled communication between components using signals.
-   Supports using structs as signal identifiers and argument carriers.
-   Register and unregister signal handlers with ease.
-   Send signals to all registered handlers of a specific signal type.

## Usage

### Signal Definition

To use the `SignalBus`, you need to define a signal struct that serves as the identifier and carrier of the signal. For example:

```cs
public struct MySignal
{
    public int Id;
    public string Message;
}
```
### Registering Signal Handlers

To receive and handle signals, components need to register themselves as signal handlers using the `Register` method. For example:

```cs

public class MySignalHandler
{
    public MySignalHandler(SignalBus signalBus)
    {
       signalBus.Register<MySignal>(HandleSignal);
    }
    public void HandleSignal(MySignal signal)
    {
        // Handle the signal here
        Console.WriteLine($"Received MySignal: Id={signal.Id}, Message={signal.Message}");
    }
}


```
### Sending Signals

To send a signal and notify all registered signal handlers, you can use the `Send` method. For example:

```cs
var signal = new MySignal { Id = 123, Message = "Hello, World!" };

// Send the signal to all registered signal handlers
SignalBus.Fire(signal);
```
### Unregistering Signal Handlers

If a component no longer wants to receive signals, it can unregister itself as a signal handler using the `Unregister` method. For example:

```cs

// Unregister the signal handler
SignalBus.Unregister<MySignal>(HandleSignal); 
```
## Performance Considerations

The `SignalBus` class provides a basic implementation for handling signals and communicating between components. However, it's important to note that the performance of the `SignalBus` can be impacted by the number of registered signal handlers and the frequency of signal dispatches.

For scenarios with a large number of subscribers and frequent signal dispatches, the performance can be improved by considering alternative data structures or optimizations. Profiling and benchmarking different implementations and approaches are recommended to ensure the best performance for your specific use case.

## License

This project is licensed under the [MIT License](https://chat.openai.com/c/LICENSE).

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## Acknowledgments

The `SignalBus` class was inspired by the observer pattern and the need for decoupled communication between components.
