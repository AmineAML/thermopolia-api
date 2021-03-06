# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html),
and inspired from the [Angular Changelog](https://github.com/angular/angular/blob/master/CHANGELOG.md).


## [Unreleased]

## [v0.8.0] - 2022-01-22
### Fixed
- Readme file referenced wrong urls [cd89b9b](https://github.com/AmineAML/thermopolia-api/commit/cb315faab589232e653a4218cce29340542ae2d9)
- Migration Docker container wasn't connecting the the PostgreSQL Docker container because we were using the machine's binded port [ab80b6e](https://github.com/AmineAML/thermopolia-api/commit/c9d8cb93197821dc064f4d9ad58649e986fde90e)
- Views folder not published [c9d8cb9](https://github.com/AmineAML/thermopolia-api/commit/7d1f41af5fed37f144265fd2e0a62ea3a91d68ee)
- Wrong email address on the newsletter template [ 7d1f41a](https://github.com/AmineAML/thermopolia-api/commit/76c8465441cd38fa743a1a0ab430f9a017cbbbac)

### Added
- GitHub Actions pipeline for production [cb315fa](https://github.com/AmineAML/thermopolia-api/commit/3f744ebb33caee67f31ba78c96fae63f1bd4d25b)
- Filter to block access from clients that are not allowed [7d1f41a](https://github.com/AmineAML/thermopolia-api/commit/76c8465441cd38fa743a1a0ab430f9a017cbbbac)

### Changed
- Redis port on docker compose production file was preventing the API from connecting because we were using the port binded to the machine while connecting from the a Docker container to another [49adbd7](https://github.com/AmineAML/thermopolia-api/commit/ab80b6efa3df5f5a364cd616fe1494a6a7c0254c)

## [v0.7.0] - 2021-12-29
### Changed
- Updated to .Net 6 [8d54935](https://github.com/AmineAML/thermopolia-api/commit/47889dfae458565822195d981810aa132cf71e9c) [0a98bb1](https://github.com/AmineAML/thermopolia-api/commit/134e5ff694411a3d749911eb581dda303bd35edb)
- Enable CORS [134e5ff](https://github.com/AmineAML/thermopolia-api/commit/4195a8bc12ea751aa861d46fd563e86c8898bdb9)
- Change the newsletter templates' logo [bcfa3e2](https://github.com/AmineAML/thermopolia-api/commit/bcc846e17443bfd4aa88882c1bc49bda14188ceb)

### Fixed
- A diet in the diets json file used a wrong link for its image url [134e5ff](https://github.com/AmineAML/thermopolia-api/commit/4195a8bc12ea751aa861d46fd563e86c8898bdb9)
- Drinks route was returning foods recipes because 'drinks' is not used as a value for `dishType` parameter [4195a8b](https://github.com/AmineAML/thermopolia-api/commit/1cd3575fa18b7d5e1665275157f7630822d26aee)

### Added
- Add `Dockerfile`, `Dockerfile.migration` and `docker-compose.prod.yml` for production with an update for configuration and environment variables with updates to the newsletter service and templates [1cd3575](https://github.com/AmineAML/thermopolia-api/commit/bcfa3e2c561862b7ecbc98daece604b09006f24d)

## [v0.6.0] - 2021-10-02
### Added
- Added documentation for the recipes routes [b48a518](https://github.com/AmineAML/thermopolia-api/commit/b48a518fcca6aac9041aeffa64f09a916a251560)
- Added PostgreSQL as a database [885c2da](https://github.com/AmineAML/thermopolia-api/commit/885c2da7b85f1046912ec4cebfb71ef2584010a2) ([#4](https://app.ora.pm/p/363614?c=6211357))
- Added newsletter subscription route [2839099](https://github.com/AmineAML/thermopolia-api/commit/2839099789b2bb4bea6627014d43feac533c969f) ([#5](https://app.ora.pm/p/363614?c=6211361))
- Added newsletter subscribors list route [2839099](https://github.com/AmineAML/thermopolia-api/commit/2839099789b2bb4bea6627014d43feac533c969f)
- Added Redis for caching [1b0277c](https://github.com/AmineAML/thermopolia-api/commit/1b0277c11ad3974dd3dfd63357d37db953ef7cbe) ([#2](https://app.ora.pm/p/363614?c=6211332))
- Setup newsletter mailing with FluentEmail [6a50d04](https://github.com/AmineAML/thermopolia-api/commit/6a50d044e3c87d721141b992e68b3abfaa935282)
- Added Hangfire for daily emails to the newsletter subscribors [e22c8b0](https://github.com/AmineAML/thermopolia-api/commit/e22c8b0e5007f54ff652a798fea813ba231484fa)
- Added a static page with important links for PGAdmin, Swagger documentation and Hangfire dashboard for admins [f0f5b85](https://github.com/AmineAML/thermopolia-api/commit/f0f5b8589b42351cb80fffaa47119ae68f04eb44)

## [v0.5.0] - 2021-09-09
### Added
- Init project [3c6cbc0](https://github.com/AmineAML/thermopolia-api/commit/3c6cbc00a7ca93ababe6b54a85b6d6fd18e1457f) [72b1d22](https://github.com/AmineAML/thermopolia-api/commit/72b1d22c9e29b3c0f0c6a93631e7827283153a41) [000c688](https://github.com/AmineAML/thermopolia-api/commit/000c688cadf6639bf19a45d6d9d1e769c055c846) [aac8c98](https://github.com/AmineAML/thermopolia-api/commit/aac8c986394114117ed99958304edcfb78567122) ([#1](https://app.ora.pm/p/363614?c=6211305))
- Added GET random drinks [7ad65d5](https://github.com/AmineAML/thermopolia-api/commit/7ad65d500d518e32321b2b5e5e58892a025d28e2) ([#1](https://app.ora.pm/p/363614?c=6211305))
- Added GET drink by id [7ad65d5](https://github.com/AmineAML/thermopolia-api/commit/7ad65d500d518e32321b2b5e5e58892a025d28e2) ([#1](https://app.ora.pm/p/363614?c=6211305))
- Added GET random recipes [3c6cbc0](https://github.com/AmineAML/thermopolia-api/commit/3c6cbc00a7ca93ababe6b54a85b6d6fd18e1457f) ([#1](https://app.ora.pm/p/363614?c=6211305))
- Added GET recipe by id [3c6cbc0](https://github.com/AmineAML/thermopolia-api/commit/3c6cbc00a7ca93ababe6b54a85b6d6fd18e1457f) ([#1](https://app.ora.pm/p/363614?c=6211305))
- Added GET random diet [bf8abff](https://github.com/AmineAML/thermopolia-api/commit/bf8abff62bcf58bda42e1c39fcfed0ea310e8f77) ([#1](https://app.ora.pm/p/363614?c=6211305))
- Added Postman collection [a86d788](https://github.com/AmineAML/thermopolia-api/commit/a86d788f6dfcf3127355f8acf3e5a687eaf4f2ea) ([#1](https://app.ora.pm/p/363614?c=6211305))
- Store diets in a JSON file (readonly requests) [bf8abff](https://github.com/AmineAML/thermopolia-api/commit/bf8abff62bcf58bda42e1c39fcfed0ea310e8f77) ([#1](https://app.ora.pm/p/363614?c=6211305))





[v0.8.0]: https://github.com/AmineAML/thermopolia-api/compare/v0.7.0...v0.8.0
[v0.7.0]: https://github.com/AmineAML/thermopolia-api/compare/v0.6.0...v0.7.0
[v0.6.0]: https://github.com/AmineAML/thermopolia-api/compare/v0.5.0...v0.6.0
[v0.5.0]: https://github.com/AmineAML/thermopolia-api/releases/tag/v0.5.0