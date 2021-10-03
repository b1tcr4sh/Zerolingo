# Zerolingo

### Scripts for farming xp on Duolingo spanish by spamming stories.
> *What do you mean "This isn't doing your homework"?*

#### This is a mini-project with a cringetastic name that I'm doing to help me practice my C# skills, learn more about web protocol, and learn how to use puppeteer *and definitely not to do my homwework for me or anything.*

This is a monorepo setup, with both a C# .NET version, and a Javascript Node.js version of the same project.  The Node.js version doesn't really do anything at the moment, but the C# version is, as of writing this, on release 1.11.7, so completely useable.
> These scripts use [Puppeteer](https://github.com/puppeteer/puppeteer) and [Puppeteersharp](https://github.com/hardkoded/puppeteer-sharp) respectively.

## Usage

To download and use either of these programs, you can either download the source code for the project, and compile it with dotnet/run it with Node.js, or go to the [releases](https://github.com/vividuwu/Zerolingo/releases) and download the most recent version, then run the executable file.

If on Windows, when the program runs, [occasionally it may pause execution due to the Windows Console's Quick Mode feature.](https://dev.to/mhmd_azeez/why-my-console-app-freezes-randomly-and-i-need-to-press-a-key-for-it-to-continue-44h9)  If this happens (The program feels like it should be doing something, and it's not), then just select(focus) the command prompt window, and click any key.


## Features

Currently, the C# version will log into Duolingo with your provided credentials (also handling accounts that were created with Google) then begin grinding on the "Una Cita"/"A Date" story.  Once it completes, it will continue to loop indefinitely until the user issues an interrupt code (ctrl+c) in the command line.

### Planned

- Loop throught a list of stories, as provided in a JSON file.
- Handle incorrect logins
- Toggleable debug view (Runs Chromium with UI)