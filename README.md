# hax-boilerplate

[![dependabot.yml](https://github.com/winstxnhdw/hax-boilerplate/actions/workflows/dependabot.yml/badge.svg)](https://github.com/winstxnhdw/hax-boilerplate/actions/workflows/dependabot.yml)

A boilerplate for developing mods for Unity applications.

## Rationale

Most open-source Unity mod distributions use a plugin framework like BepInEx. These frameworks rely on both developers and end-users to modify the game's files. Not only is this not a good user experience, but it also pollutes the game's files with unnecessary modifications. This boilerplate relies on [SharpMonoInjectorCore](https://github.com/winstxnhdw/SharpMonoInjectorCore) to perform runtime injection of the mod's code into the game's process. This allows for a more pleasant user experience with less clutter.
