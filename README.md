# MusicBee Webhook Plugin

`mb_webhook` is a Windows plugin for MusicBee that sends now-playing track information to Discord and Slack webhooks. It is a MusicBee plugin, not a standalone HTTP service.

## Features

- Sends track title, artist, album, and bitrate to Discord and Slack.
- Looks up album artwork and includes it in supported messages.
- Sends notifications to configured Discord and Slack webhook URLs.
- Saves webhook settings in MusicBee's persistent storage.
- Sends an additional JSON payload to a local development endpoint at `http://localhost:3000/webhook`.

The local endpoint is optional and is not included in this repository. If nothing is listening there, the plugin logs the failed request; Discord and Slack delivery can still be configured independently.

## Installation

There is currently no installer or prebuilt plugin binary in this repository. Build the plugin from source, then install its DLL:

1. Install Visual Studio with the **.NET desktop development** workload and the **.NET Framework 4.8 Developer Pack**.
2. Clone or download this repository and open `whook_plugin.sln` in Visual Studio.
3. Restore the NuGet packages if Visual Studio does not do so automatically.
4. Select the **Release** configuration and build the solution.
5. Locate `mb_webhook.dll` in `bin\Release\`.
6. Copy `mb_webhook.dll` to the `Plugins` folder inside your MusicBee installation. For a portable MusicBee install, use that installation's `Plugins` folder.
7. Restart MusicBee.

If the build automatically copies the DLL to a MusicBee Plugins folder configured on the build machine, step 6 may already be done. Otherwise, manually copy the Release DLL as described above.

## Configure webhook URLs

1. In MusicBee, open **Edit → Preferences → Plugins**.
2. Select **Webhook Plugin** and open its configuration dialog.
3. Enter your Discord and/or Slack incoming webhook URL, then save.
4. Play or change a track to send a now-playing notification.

The plugin saves these URLs in `webhooks.json` under MusicBee's persistent storage directory. Keep webhook URLs private; anyone who obtains one may be able to post to that destination.

## Showcase

Add screenshots at the paths below when available:

![Discord notification showcase](docs/images/discord-showcase.png)

![Slack notification showcase](docs/images/slack-showcase.png)

## Build requirements

- Windows
- Visual Studio with .NET desktop development tools
- .NET Framework 4.8 Developer Pack
- NuGet package restore

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
