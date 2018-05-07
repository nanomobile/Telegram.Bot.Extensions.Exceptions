# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased]

### Added

- Type `InvalidGameShortNameException`
- Type `InvalidQueryIdException`
- Type `ChatDescriptionIsNotModifiedException`
- Type `ChatNotModifiedException`
- Type `SendMessageToBotException`
- Type `TooManyRequestsException`
- Type `BotIsNotMemberException`
- Type `MessageIsNotModifiedException`
- Type `InvalidParameterException`
- Type `ChatNotFoundException`
- Type `ContactRequestException`
- Type `InvalidUserIdException`
- Type `UserNotFoundException`
- Type `InvalidStickerSetNameException`
- Type `InvalidStickerEmojisException`
- Type `InvalidStickerDimensionsException`
- Type `StickerSetNameExistsException`
- Type `StickerSetNotModifiedException`

### Changed

- `BadRequestException` is thrown on `Error 400: Bad request` if more specific exception is not defined
- `ForbiddenException` is thrown on `Error 403: Forbidden` if more specific exception is not defined

### Fixed

- Access modifier of abstract class `BadRequestException` and `ForbiddenException` ctors to `protected`

### Removed

