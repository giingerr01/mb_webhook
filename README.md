# MusicBee Webhook Plugin

`mb_webhook` is a Windows plugin for [MusicBee](https://getmusicbee.com/) that sends a now-playing notification when the track changes. It can post formatted messages to Discord and Slack, and send a JSON payload to a custom webhook endpoint.

The project targets .NET Framework 4.8. Version `2.1.0` is available as a prebuilt release. Most MusicBee users can install it by downloading the latest release and copying the plugin DLL into MusicBee's `Plugins` folder; building from source is optional.

## Features

- Sends track title, artist, album, bitrate, and available album artwork to Discord and Slack.
- Sends track metadata as JSON to a configurable custom webhook.
- Looks up artwork through the iTunes Search API.
- Provides a MusicBee settings dialog for the three webhook URLs.
- Saves settings and plugin logs under MusicBee's persistent storage directory.

Notifications are sent on track changes. This plugin sends outbound HTTP requests; it does not host a webhook server.

## Screenshots

Screenshots can be added to the `assets` folder and linked here. These image links are placeholders until the PNG files are added:

<!--
![Plugin settings](assets/plugin-settings.png)
![Discord notification example](assets/discord-notification.png)
![Slack notification example](assets/slack-notification.png)
-->

## Download

Download the latest prebuilt package from the [GitHub Releases page](https://github.com/giingerr01/mb_webhook/releases/latest) (version `2.1.0` or newer), then extract the downloaded archive.

## Install in MusicBee

1. Close MusicBee.
2. In the extracted release files, locate `mb_webhook.dll`.
3. Copy `mb_webhook.dll` into the `Plugins` folder inside your MusicBee installation. For example, a standard installation may use `C:\Program Files (x86)\MusicBee\Plugins`. If you use a portable installation, use the `Plugins` folder next to its `MusicBee.exe`. Create the `Plugins` folder if it does not already exist.
4. Reopen MusicBee. Go to **Edit → Preferences → Plugins**, select **Webhook Plugin**, and open its configuration dialog.
5. Enter the webhook URL for each service you want to use, then select **Save**.
6. Change or play a track to test the notification.

## Build from source (optional)

To compile the plugin yourself, you will need Windows, Visual Studio with the **.NET desktop development** workload, the **.NET Framework 4.8 Developer Pack**, and NuGet package restore.

1. Clone or download this repository.
2. Open `whook_plugin.sln` in Visual Studio and restore the NuGet packages if needed.
3. Select the **Release** configuration and build the solution.
4. Find `mb_webhook.dll` in `bin\Release\` and install it using the steps above.

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
