# MusicBee Webhook Plugin

`mb_webhook` is a Windows plugin for [MusicBee](https://getmusicbee.com/) that sends a now-playing notification when the track changes. It can post formatted messages to Discord and Slack, and send a JSON payload to a custom webhook endpoint.

The project targets .NET Framework 4.8. Version metadata is currently `1.0.0`; a prebuilt download has not yet been published, so build the plugin from source using the instructions below.

## Features

- Sends track title, artist, album, bitrate, and available album artwork to Discord and Slack.
- Sends track metadata as JSON to a configurable custom webhook.
- Looks up artwork through the iTunes Search API.
- Provides a MusicBee settings dialog for the three webhook URLs.
- Saves settings and plugin logs under MusicBee's persistent storage directory.

Notifications are sent on track changes. This plugin sends outbound HTTP requests; it does not host a webhook server.

## Requirements

- Windows and MusicBee
- Visual Studio with the **.NET desktop development** workload
- The **.NET Framework 4.8 Developer Pack**
- NuGet package restore

## Build

1. Clone or download this repository.
2. Open `whook_plugin.sln` in Visual Studio.
3. Restore the NuGet packages if Visual Studio does not do so automatically.
4. Select the **Release** configuration and build the solution.
5. Find `mb_webhook.dll` in `bin\Release\`.

## Install

1. Copy `mb_webhook.dll` to the `Plugins` folder inside your MusicBee installation. For a portable installation, use its own `Plugins` folder.
2. Restart MusicBee.
3. Open **Edit → Preferences → Plugins**, select **Webhook Plugin**, and open its configuration dialog.
4. Enter the incoming webhook URL for each service you want to use, then select **Save**.
5. Change or play a track to send a notification.

The project does not currently provide an installer or a prebuilt release download. If the build is configured to copy the DLL to a MusicBee Plugins folder on your machine, the manual copy step may already be complete.

## Webhook settings and data

The settings dialog has fields for **Discord URL**, **Slack URL**, and **Custom URL**. The plugin saves these values to `webhook\webhooks.json` under MusicBee's persistent storage directory. Enter valid URLs for the services you want to use. The plugin attempts all three destinations on each track change, so blank or invalid URLs for unused services may produce request errors in the log.

The plugin writes diagnostics to `webhook\webhook_plugin_log.log` in the same directory. If a notification is not received, check the URL and the log for request errors.

Webhook URLs are credentials: anyone who obtains one may be able to post to that destination. Keep them private and do not commit them to source control.

## Custom webhook payload

The custom endpoint receives an HTTP `POST` with the `application/json` content type when a track changes. The payload has this shape:

```json
{
  "schemaVersion": 1,
  "event": "track_changed",
  "timestamp": "2026-01-01T12:00:00.0000000+00:00",
  "player": {
    "name": "MusicBee",
    "status": null
  },
  "track": {
    "title": "Track title",
    "artist": "Artist name",
    "album": "Album name",
    "year": 2026,
    "genre": "Genre",
    "albumArtist": "Album artist",
    "durationMs": 210000
  },
  "audio": {
    "bitrateKbps": "320",
    "sampleRateHz": "44100",
    "bitdepth": 0,
    "channels": 0,
    "codec": "MP3"
  },
  "art": {
    "url": "https://example.com/artwork.jpg"
  }
}
```

Values depend on the metadata available for the current track. Artwork may be unavailable. The custom endpoint must be reachable from the computer running MusicBee and accept JSON `POST` requests.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
