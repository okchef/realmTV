using System.Net.WebSockets;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;

public class NetworkConnection
{
    private static ClientWebSocket webSocket;

    static HttpClient client = new HttpClient();

    public async static Task Connect() {
        webSocket = new ClientWebSocket();
        using (var cts = new CancellationTokenSource()) {
            await webSocket.ConnectAsync(new System.Uri("ws://localhost:8080/game"), cts.Token);
        }
    }

    public async static Task<string> Receive() {
        byte[] buffer = new byte[65536];
        var segment = new System.ArraySegment<byte>(buffer, 0, buffer.Length);
        WebSocketReceiveResult receiveResult;
        using (var cts = new CancellationTokenSource()) {
            receiveResult = await webSocket.ReceiveAsync(segment, cts.Token);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
    }

    public async static void Close() {
        if (webSocket != null && WebSocketState.Open.Equals(webSocket.State)) {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        }
    }

    public static bool IsOpen() {
        return webSocket != null && WebSocketState.Open.Equals(webSocket.State);
    }

    public async static Task<RealmState> GetStateAsync() {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/state");
        if (response.IsSuccessStatusCode) {
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RealmState>(responseContent);
        } else {
            return null;
        }
    }
}
