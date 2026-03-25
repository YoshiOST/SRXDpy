import asyncio
import json
import sys
import websockets

args = sys.argv[1:]

ip = "127.0.0.1"
port = 38304
sbip = "127.0.0.1"
sbport = 9000

print(args)

i = 0
while i < len(args):
    arg = args[i]

    if arg == "--ip" and i + 1 < len(args) and not args[i + 1].startswith("--"):
        ip = args[i + 1]
        print("found ip")
        i += 1

    elif arg == "--port" and i + 1 < len(args) and not args[i + 1].startswith("--"):
        port = int(args[i + 1])
        print("found port")
        i += 1

    elif arg == "--sbip" and i + 1 < len(args) and not args[i + 1].startswith("--"):
        sbip = args[i + 1]
        i += 1

    elif arg == "--sbport" and i + 1 < len(args) and not args[i + 1].startswith("--"):
        sbport = int(args[i + 1])
        i += 1

    i += 1


print(f"Connecting to {ip}:{port} for SpinRhythm")
print(f"Connecting to {sbip}:{sbport} for StreamerBot")


async def ask_retry():
    answer = input("Retry connection? (y/n): ").lower()
    if answer == "y":
        return True
    else:
        print("Exiting...")
        return False


async def connect():
    spin_uri = f"ws://{ip}:{port}"
    sb_uri = f"ws://{sbip}:{sbport}"

    try:
        async with websockets.connect(spin_uri) as spin_ws:
            print("Connected to SpinStatus")

            async with websockets.connect(sb_uri) as streamer_ws:
                print("Connected to Streamer.bot")

                async for message in spin_ws:
                    msg = json.loads(message)
                    print("Received:", msg)

                    if msg.get("type") == "trackStart":
                        track = msg.get("status", {})

                        payload = {
                            "id": "spinrhythm-track",
                            "args": {
                                "artist": track.get("artist"),
                                "track_name": track.get("title"),
                                "difficulty": track.get("difficulty"),
                            },
                        }

                        await streamer_ws.send(json.dumps(payload))
                        print("Sent to Streamer.bot:", payload)

    except Exception as err:
        print("❌ Connection error:", err)
        retry = await ask_retry()
        if retry:
            await connect()


asyncio.run(connect())
