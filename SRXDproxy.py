import asyncio
import websockets

# === CONFIG ===
PROXY_PORT = 9000
SPINSTATUS_PORT = 38304
SPINSTATUS_HOST = "localhost"
# =================

async def proxy_handler(client_ws):
    print("New client connected")

    try:
        target_uri = f"ws://{SPINSTATUS_HOST}:{SPINSTATUS_PORT}"
        async with websockets.connect(target_uri) as target_ws:
            print("Connected to SpinStatus")

            async def client_to_target():
                async for msg in client_ws:
                    await target_ws.send(msg)
                    print("Other PC:", msg)

            async def target_to_client():
                async for msg in target_ws:
                    await client_ws.send(msg)
                    print("This PC Forwarded:", msg)

            await asyncio.gather(
                client_to_target(),
                target_to_client()
            )

    except Exception as err:
        print("SpinStatus error:", err)

async def main():
    server = await websockets.serve(
        proxy_handler,
        "0.0.0.0",
        PROXY_PORT
    )

    print(f"WebSocket proxy running on port {PROXY_PORT}")
    await server.wait_closed()

asyncio.run(main())
