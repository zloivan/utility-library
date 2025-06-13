# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.4.1] - 2025-06-14
### Added
- LookAtCamera script added 

## [1.4.0] - 2025-06-13
### Changed
- Update namespaces for the SingletonBehaviour script

## [1.3.0] - 2025-06-10
### Changed
- Refactored PersistentSingleton into SingletonBehaviour with thread-safe implementation
- Improved singleton functionality with better lifecycle management
- Added configurable persistence across scenes option
- Added proper application quit handling to prevent null reference exceptions
- Enhanced singleton destruction safety with proper cleanup

### Added
- Thread-safe singleton instance access with lock mechanism
- Configurable auto-detachment from parent on Awake
- Better debug logging for singleton conflicts and lifecycle events

## [1.2.0] - 2024-08-03
### Added
- Addedd Validator
- Added Logs that can be switched off

## [1.0.3] - 2024-07-30
### Added
- Added persistent singleton

## [1.0.2] - 2024-07-27
### Added
- Reorganize classes and small changes to assemblies

## [1.0.1] - 2024-07-08
### Added
- Added utility script to get specified classes from assemblies

## [1.0.0] - 2024-06-28
### Added
- First release of most used extensions methods