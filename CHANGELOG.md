# Change Log
All notable changes to this project will be documented in this file.
</br>
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [2.0.0-beta.1.0] - 2022-06-15
 ### Added
 * `Argument(uint Index, string Name, string Description)` - Added a new constructor for `Argument<T>`.
 * `HelpName` - Added a new property for `Argument<T>`.
 * Support for verbs/subcommands.

 ### Changed
 * `Option<T>` - Property `Alias` has been changed to accepting only one string, use the property `Aliases` for multiple aliases.
 * The error message `Type mismatch`  has been changed to  a more intelligible message.

 ### Removed
 * Index-less `Option<T>` and `Argument<T>` attributes  - From now on, you must pass the correct order/index of the parameters to its respective attribute. This feature was removed because most of the time, it wasn't working as intended.

## [2.0.0-alpha.1.0] - 2022-06-09
 Rewrote everything and introduced major changes.