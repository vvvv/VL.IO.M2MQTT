using MQTTnet;
using System.Reactive.Linq;

namespace IO.MQTT;
public static class MqttReceiveHelper
{
    public static IObservable<MqttApplicationMessageReceivedEventArgs> MessageArrived(IMqttClient client)
    {
        return Observable.FromEvent<Func<MqttApplicationMessageReceivedEventArgs, Task>, MqttApplicationMessageReceivedEventArgs> (handler =>
        {
            Func<MqttApplicationMessageReceivedEventArgs, Task> maHandler = (args) =>
            {
                handler(args);
                return Task.CompletedTask;
            };

            return maHandler;
        },
        paHandler => client.ApplicationMessageReceivedAsync += paHandler,
        paHandler => client.ApplicationMessageReceivedAsync -= paHandler);
    }
}