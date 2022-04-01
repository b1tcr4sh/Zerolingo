<img src="https://github.com/vividuwu/Zerolingo/blob/master/src/Assets/ZerolingoLogoText.png">
<hr>

# Zerolingo 

### Scripts for farming xp on Duolingo spanish by spamming stories.
> *What do you mean "This isn't doing your homework"?*

#### This is a mini-project with a cringetastic name that I'm doing to help me practice my C# skills, learn more about web protocol, and learn how to use puppeteer *and definitely not to do my homwework for me or anything.*

Essentially, this app is suuper simple.  It logs into to Duolingo with your provided login, collects a list of all of the available story lessons, then just loops through the list and autocompletes them.  Although the efficiency varies, it can farm about **1200 XP per hour**, depending on how long it takes to complete each lesson.

This app uses [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) and is written in C#.  The provided [releases](https://vividuwu/zerolingo/releases) are built to be *self contained* meaning that they bundle the .NET runtime and libraries needed to run the app, in the event that the user doesn't have .NET installed.  This does cause a slightly larger download, but the overall size is only ~30Mb, which is pretty manageable. 
> This app uses the [Puppeteersharp](https://github.com/hardkoded/puppeteer-sharp) API to communicate with the Chromium instance via [Chrome Dev Tools](https://developer.chrome.com/docs/devtools/).

## Usage

In the [realeases](https://github.com/vividuwu/releases) on the Github repo, you can download the most recent release, depending on whether or not you are running Windows 10 or some form of Linux OS.
> The releases only have prebuilt versions for AMD64 systems, so if running on a x32 or ARM system, you can use Dotnet's CLI or Visual Studio to compile an executable for your architecture.

#### Disclaimer
Even though it may be tempting, having a lot of interactions with other windows on the desktop while the bot is running can sometimes cause unexpected crashes, so preferably, just leave it to run and don't touch your pc while it's running.

### Windows
Download the latest [release](https://github.com/vividuwu/zerolingo/releases) for Windows (10) and unzip the file.  Within the zip are all of the libraries required for the app to run (.dll files) and the executable itself (Zerolingo.exe).  Because the file name starts with a *Z*, it is at the bottom of the folder in the file explorer view.  Just run the file, and if a Windows Smart Screen popup appears, click "More Info" then "Run Anyway" to run the app. (This happens because I am not a verified publisher, so Windows automatically flags the software as potentially unsafe.)  After that, the program should run and begin downloading Chromium, just follow the on screen instructions in the console and spend your newly acquired freetime elsewhere!
#### DISCLAIMER FOR WINDOWS USERS!!!
If on Windows, when the program runs, [occasionally it may pause execution due to the Windows Console's Quick Mode feature.](https://dev.to/mhmd_azeez/why-my-console-app-freezes-randomly-and-i-need-to-press-a-key-for-it-to-continue-44h9)  If this happens (The program feels like it should be doing something, and it's not), then just select (focus) the command prompt window, and click any key.

### Linux
Download the latest [release](https://github.com/vividuwu/zerolingo/releases) for Linux and unzip the file.  This can be done with the "unzip" utility with the following command:
```bash
$ unzip Zerolingo-X.X.X-LinX64.zip
```
Once this is done, open up the directory and find the 'Zerolingo' executable.  Make sure that it is flagged as executable with the following command:
```
$ chmod +x ./Zerolingo
```
After this, just run it, either by double clicking in a UI (Ie. Dolphin) or running
```
$ ./Zerolingo
```
It should either start in the terminal you opened it or create a new window and begin downloading Chromium, just follow the on screen instructions in the console and spend your newly acquired freetime elsewhere!

## Exiting the Program
There is no built-in way to exit the program while it is running, so to do so, you will need to first issue an interupt signal to the terminal/command line where it is running by entering CTRL+C (Maybe a few times) this should terminate the process.  If the Chromium window is still open after this, just manually close it like you would any other window.


## Features

- Logs into Duolingo from terminal.
- Handles login both for accounts that were authenticated with Google and those that weren't.
- Collects a list of the user's available story lessons.
- Loops through the list of lessons.
- Spams through the lesson.
- Brute forces the matching tokens by clicking them in a random order extremely quickly. (Stupid but it works lol)

### Planned

- ~~Loop throught a list of stories, as provided in a JSON file.~~
- ~~Toggleable debug view (Runs Chromium with UI)~~
- Handle incorrect logins
